"""
Based on the tflearn CIFAR-10 example at:
https://github.com/tflearn/tflearn/blob/master/examples/images/convnet_cifar10.py
"""

from __future__ import division, print_function, absolute_import

from skimage import color, io
from scipy.misc import imresize
import numpy as np
from sklearn.cross_validation import train_test_split
import os
from glob import glob

import tflearn
from tflearn.data_utils import shuffle, to_categorical
from tflearn.layers.core import input_data, dropout, fully_connected
from tflearn.layers.conv import conv_2d, max_pool_2d
from tflearn.layers.estimator import regression
from tflearn.data_preprocessing import ImagePreprocessing
from tflearn.data_augmentation import ImageAugmentation
from tflearn.metrics import Accuracy
import tensorflow as tf

###################################
### Import picture files
###################################

files_path = 'images'

cat_files_path = os.path.join(files_path, 'cat*.jpg')
dog_files_path = os.path.join(files_path, 'dog*.jpg')

cat_files = sorted(glob(cat_files_path))
dog_files = sorted(glob(dog_files_path))

n_files = len(cat_files) + len(dog_files)
print(n_files)

size_image = 64

allX = np.zeros((n_files, size_image, size_image, 3), dtype='float32')
ally = np.zeros(n_files)
count = 0
for f in cat_files:
    try:
        img = io.imread(f)
        new_img = imresize(img, (size_image, size_image, 3))
        allX[count] = np.array(new_img)
        ally[count] = 0
        count += 1
    except:
        continue

for f in dog_files:
    try:
        img = io.imread(f)
        new_img = imresize(img, (size_image, size_image, 3))
        allX[count] = np.array(new_img)
        ally[count] = 1
        count += 1
    except:
        continue

###################################
# Prepare train & test samples
###################################

# test-train split
X, X_test, Y, Y_test = train_test_split(allX, ally, test_size=0.1, random_state=42)

# encode the Ys
Y = to_categorical(Y, 2)
Y_test = to_categorical(Y_test, 2)

###################################
# Image transformations
###################################

# normalisation of images
img_prep = ImagePreprocessing()
img_prep.add_featurewise_zero_center()
img_prep.add_featurewise_stdnorm()

# Create extra synthetic training data by flipping & rotating images
img_aug = ImageAugmentation()
img_aug.add_random_flip_leftright()
img_aug.add_random_rotation(max_angle=25.)

###################################
# Define network architecture
###################################

# Input is a 32x32 image with 3 color channels (red, green and blue)
network = input_data(shape=[None, 64, 64, 3],
                     data_preprocessing=img_prep,
                     data_augmentation=img_aug)

# Store layers weight & bias



# 1: Convolution layer with 32 filters, each 3x3x3
conv_1 = conv_2d(network, 32, 3, activation='relu', name='conv_1')

# 2: Max pooling layer
network = max_pool_2d(conv_1, 2)

# 3: Convolution layer with 64 filters
conv_2 = conv_2d(network , 64, 3, activation='relu', name='conv_2')

# 4: Convolution layer with 64 filters
conv_3 = conv_2d(conv_2, 64, 3, activation='relu', name='conv_3')

# 5: Max pooling layer
network = max_pool_2d(conv_3, 2)

# 6: Fully-connected 512 node layer
network1 = fully_connected(network, 512, activation='relu',name='one')

# 7: Dropout layer to combat overfitting
network = dropout(network1, 0.5)
# 8: Fully-connected layer with two outputs
network2 = fully_connected(network, 2, activation='softmax',name='two')
# Configure how the network will be trained
acc = Accuracy(name="Accuracy")
network = regression(network2, optimizer='adam',
                     loss='categorical_crossentropy',
                     learning_rate=0.0005, metric=acc)

# Wrap the network in a model object

model = tflearn.DNN(network, checkpoint_path='model_cat_dog_6.tflearn', max_checkpoints=3,
                    tensorboard_verbose=3, tensorboard_dir='tmp/tflearn_logs/')

###################################
# Train model for 100 epochs
###################################
model.fit(X, Y, validation_set=(X_test, Y_test), batch_size=500,
          n_epoch=100, run_id='model_cat_dog_6', show_metric=True)

#model.save('model_cat_dog_6_final.pb')
w1=model.get_weights(conv_1.W)
b1=model.get_weights(conv_1.b)
w2=model.get_weights(conv_2.W)
b2=model.get_weights(conv_2.b)
w3=model.get_weights(conv_3.W)
b3=model.get_weights(conv_3.b)
'''
fc1=tflearn.get_layer_variables_by_name('one')
fc2=tflearn.get_layer_variables_by_name('two')
w4=fc1[0]
b4=fc1[1]
w5=fc2[0]
b5=fc2[1]

w4=network1.W
b4=network1.b
w5=network2.W
b5=network2.b
'''
#不这么取值，会报错float32_ref
w4=model.get_weights(network1.W)
b4=model.get_weights(network1.b)
w5=model.get_weights(network2.W)
b5=model.get_weights(network2.b)
# Create some wrappers for simplicity
def conv2d(x, W, b, strides=1):
    # Conv2D wrapper, with bias and relu activation
    x = tf.nn.conv2d(x, W, strides=[1, strides, strides, 1], padding='SAME')
    x = tf.nn.bias_add(x, b)
    return tf.nn.relu(x)


def maxpool2d(x, k=2):
    # MaxPool2D wrapper
    return tf.nn.max_pool(x, ksize=[1, k, k, 1], strides=[1, k, k, 1],
                          padding='SAME')


# Create new graph for exporting
g = tf.Graph()
with g.as_default():
    # Input is a 32x32 image with 3 color channels (red, green and blue)
    #n = input_data(shape=[1, 64, 64, 3],data_preprocessing=img_prep,data_augmentation=img_aug,name="input")
    n=tf.placeholder(dtype="float32", shape=[None, 64, 64, 3], name="input")

    n= tf.reshape(n, [1, 64,64,3])

    # 1: Convolution layer with 32 filters, each 3x3x3
    c1 = conv2d(n,w1,b1)
    n = maxpool2d(c1, 2)
    # 3: Convolution layer with 64 filters
    c2 = conv2d(n,w2,b2)
    # 4: Convolution layer with 64 filters
    c3 = conv2d(c2,w3,b3)
    # 5: Max pooling layer
    n = max_pool_2d(c3, 2)

    w4 = tf.constant(w4, name="WD1")
    #AttributeError: 'numpy.ndarray' object has no attribute 'get_shape'
    w5 = tf.constant(w5, name="WD1")
    # 6: Fully-connected 512 node layer
    FC1 = tf.reshape(n, [-1, w4.get_shape().as_list()[0]])
    FC1 = tf.add(tf.matmul(FC1, w4), b4)
    FC1 = tf.nn.relu(FC1)
    # 8: Fully-connected layer with two outputs
    FC2 = tf.reshape(n, [-1, w5.get_shape().as_list()[0]])
    OUTPUT = tf.nn.softmax(tf.matmul(FC1, w5) + b5, name="output")

    sess = tf.Session()
    init=tf.global_variables_initializer()
    sess.run(init)
    #result = print(sess.run(OUTPUT))

    #out = tf.Variable(n, name="output")
    #print(out)



    output_graph_def = tf.graph_util.convert_variables_to_constants(sess, sess.graph_def, output_node_names=['output'])
    with tf.gfile.FastGFile('cat_dog.pb', mode='wb') as f:
        f.write(output_graph_def.SerializeToString())



