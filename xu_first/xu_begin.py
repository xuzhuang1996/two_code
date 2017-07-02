import tensorflow as tf
import shutil
import os.path

#if os.path.exists("./tmp/beginner-export"):
 #   shutil.rmtree("./tmp/beginner-export")

# Import data
from tensorflow.examples.tutorials.mnist import input_data


mnist = input_data.read_data_sets("MNIST_data", one_hot=True)

g = tf.Graph()

with g.as_default():
    # Create the model
    x = tf.placeholder("float", [None, 784])
    W = tf.Variable(tf.zeros([784, 10]), name="vaiable_W")
    b = tf.Variable(tf.zeros([1,10]), name="variable_b")
    y = tf.nn.softmax(tf.matmul(x, W,transpose_a=False, transpose_b=False) + b)

    # Define loss and optimizer
    y_ = tf.placeholder("float", [None, 10])
    cross_entropy = -tf.reduce_sum(y_ * tf.log(y))
    train_step = tf.train.GradientDescentOptimizer(0.01).minimize(cross_entropy)

    sess = tf.Session()

    # Train
    #init = tf.initialize_all_variables()
    init = tf.global_variables_initializer()
    sess.run(init)

    for i in range(1000):
        batch_xs, batch_ys = mnist.train.next_batch(100)
        train_step.run({x: batch_xs, y_: batch_ys}, sess)

    # Test trained model
    correct_prediction = tf.equal(tf.argmax(y, 1), tf.argmax(y_, 1))
    accuracy = tf.reduce_mean(tf.cast(correct_prediction, "float"))

    print(accuracy.eval({x: mnist.test.images, y_: mnist.test.labels}, sess))

# Store variable
_W = W.eval(sess)
_b = b.eval(sess)

sess.close()

# Create new graph for exporting
g_2 = tf.Graph()
with g_2.as_default():
    # Reconstruct graph
    x_2 = tf.placeholder("float", [None, 784], name="input")
    #Tensor("input:0", shape=(?, 784), dtype=float32)
    x_2 = tf.reshape(x_2, [1, 784])



    W_2 = tf.constant(_W, name="constant_W")
    #Tensor("constant_W:0", shape=(784, 10), dtype=float32)

    b_2 = tf.constant(_b, name="constant_b")
    #Tensor("constant_b:0", shape=(10,), dtype=float32)

    y_2 = tf.nn.softmax(tf.matmul(x_2, W_2,transpose_a=False, transpose_b=False) + b_2, name="output")
    #Tensor("output:0", shape=(?, 10), dtype=float32)
    sess_2 = tf.Session()

    #init_2 = tf.initialize_all_variables();
    init_2 =tf.global_variables_initializer()
    sess_2.run(init_2)

    graph_def = g_2.as_graph_def()

    tf.train.write_graph(graph_def, 'model',
                         'beginner-graph.pb', as_text=False)

    # Test trained model
    '''
    y__2 = tf.placeholder("float", [None, 10])
    correct_prediction_2 = tf.equal(tf.argmax(y_2, 1), tf.argmax(y__2, 1))
    accuracy_2 = tf.reduce_mean(tf.cast(correct_prediction_2, "float"))
    print(accuracy_2.eval({x_2: mnist.test.images, y__2: mnist.test.labels}, sess_2))'''