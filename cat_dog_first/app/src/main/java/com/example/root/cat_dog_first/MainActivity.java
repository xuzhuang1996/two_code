package com.example.root.cat_dog_first;

import android.Manifest;
import android.app.Activity;
import android.content.ContentResolver;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.net.Uri;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.content.Intent;
import android.widget.ImageView;
import android.widget.LinearLayout.LayoutParams;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.List;
import java.util.concurrent.Executors;
import java.util.concurrent.Executor;

public class MainActivity extends AppCompatActivity {
    private static int REQ_1 =1;
    private static int REQ_2 =2;
    private ImageView mImageview;
    private int my_w=64;
    private int my_h=64;
    public Bitmap pixel;
    private Bitmap bitmap;
    public float [] picture;

    //file path for picture
    private String nFilePath;
    // Storage Permissions
    private static final int REQUEST_EXTERNAL_STORAGE = 1;
    private static String[] PERMISSIONS_STORAGE = {
            Manifest.permission.READ_EXTERNAL_STORAGE,
            Manifest.permission.WRITE_EXTERNAL_STORAGE
    };

    /**
     * Checks if the app has permission to write to device storage
     *
     * If the app does not has permission then the user will be prompted to grant permissions
     *
     * @param activity
     */
    public static void verifyStoragePermissions(Activity activity) {
        // Check if we have write permission
        int permission = ActivityCompat.checkSelfPermission(activity, Manifest.permission.WRITE_EXTERNAL_STORAGE);

        if (permission != PackageManager.PERMISSION_GRANTED) {
            // We don't have permission so prompt the user
            ActivityCompat.requestPermissions(
                    activity,
                    PERMISSIONS_STORAGE,
                    REQUEST_EXTERNAL_STORAGE
            );
        }
    }





    //for tensorflow
    private static final int INPUT_SIZE = 64;
    private static final String INPUT_NAME = "input";
    private static final String OUTPUT_NAME = "output";
    private static final String MODEL_FILE = "file:///android_asset/cat_dog.pb";
    private static final String LABEL_FILE = "file:///android_asset/cat_dog.txt";
    private Classifier classifier;
    private Executor executor = Executors.newSingleThreadExecutor();


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mImageview=(ImageView) findViewById(R.id.IV);
        nFilePath= Environment.getExternalStorageDirectory().getPath();
        //path of my picture
        nFilePath= nFilePath + "/" + "temp.png";
        verifyStoragePermissions(this);

        initTensorFlowAndLoadModel();
    }

    public void startCamera(View view){
        Intent  intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        //use high :yuan tu
        Uri photoUri=Uri.fromFile(new File(nFilePath));
        intent.putExtra(MediaStore.EXTRA_OUTPUT,photoUri);//MediaStore.EXTRA_OUTPUT can transform the picture'path into photoUri
        startActivityForResult(intent,REQ_1);
    }

    public void choose(View view){
        Intent  intent = new Intent(Intent.ACTION_PICK, MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
        startActivityForResult(intent,REQ_2);
    }


    public float [] bit_to_float(Bitmap bit){
        int [] p=new int [bit.getWidth()*bit.getHeight()];
        bit.getPixels(p,0,bit.getWidth(),0,0,bit.getWidth(),bit.getHeight());
        Log.i("xuxuxuxu","444");
        if(p.length<0) {
            Log.i("xuxuxuxu","4");
            new AlertDialog.Builder(this).setTitle("error:")
                    .setMessage("bit_to_float")
                    .setPositiveButton("ok", null)
                    .show();

        }else{
            Log.i("xuxuxuxu","5");
        }

        Log.i("p.value",String.valueOf(p[1]));

        float [] pp=new float [bit.getWidth()*bit.getHeight()*3];
        for(int i=0,k=0;i<p.length;i++)
        {
            int clr = p[i];
            int  red   = (clr & 0x00ff0000) >> 16;  //取高两位
            int  green = (clr & 0x0000ff00) >> 8; //取中两位
            int  blue  =  clr & 0x000000ff; //取低两位

            pp[k]=(float)red;pp[k+1]=(float)green;pp[k+2]=(float)blue;
            k+=3;
        }

        return pp;
    }
    //receive the data
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        //startActivityForResult() return resultCode==RESULT_OK,then deal with data
        if(resultCode==RESULT_OK){
            //can prove that request is the Intent'request
            if(requestCode==REQ_1){
                //get the return'data(parameter is Intent'data)
                //Bundle bundle=data.getExtras();
                //bitmap=(Bitmap) bundle.get("data");

                // new high:yuan tu

                FileInputStream fis=null;
                try {
                    fis = new FileInputStream(nFilePath);
                    bitmap = BitmapFactory.decodeStream(fis);//analytical our picture to bitmap
                    mImageview.setImageBitmap(bitmap);
                }catch (FileNotFoundException e){
                    e.printStackTrace();
                }finally {
                    try {
                        fis.close();
                    }catch (IOException e){
                        e.printStackTrace();
                    }
                }

                //resiaze
                pixel=Bitmap.createScaledBitmap(bitmap,my_w,my_h,true);
                //pixel=resize(bitmap);
                mImageview.setImageBitmap(bitmap);
            }else if(requestCode==REQ_2){
                Uri selectImage=data.getData();
                ContentResolver cr =this.getContentResolver();
                try{
                    if(bitmap!=null)bitmap.recycle();
                    bitmap=BitmapFactory.decodeStream(cr.openInputStream(selectImage));
                }catch (FileNotFoundException e){
                    e.printStackTrace();
                }
                //resiaze
                pixel=Bitmap.createScaledBitmap(bitmap,my_w,my_h,true);
                //pixel=resize(bitmap);
                mImageview.setImageBitmap(bitmap);

            }


        }
    }

    public Bitmap resize(Bitmap bitmap){
        float sw=(float)my_w/bitmap.getWidth();
        float sh=(float)my_h/bitmap.getHeight();
        Matrix m=new Matrix();
        m.postScale(sw,sh);
        return Bitmap.createBitmap(bitmap,0,0,bitmap.getWidth(),bitmap.getHeight(),m,false);

    }

    public void compute(View view){
        if(pixel.getHeight()<=0)return;
        picture=bit_to_float(pixel);

        if(picture.length<=0)return;
        Log.i("picture.value",String.valueOf(picture[1]));
        Log.i("picture.length",String.valueOf(picture.length));
        final List<Classifier.Recognition> results = classifier.recognizeImage(picture);
        if (results.size() > 0) {
            String value = results.get(0).getTitle();
            new AlertDialog.Builder(this).setTitle(" the animal is : ")
                    .setMessage(value)
                    .setPositiveButton("ok",null)
                    .show();
        }else{
            new AlertDialog.Builder(this).setTitle("no found: ")
                    .setMessage("compute")
                    .setPositiveButton("ok",null)
                    .show();
            pixel.recycle();
            bitmap.recycle();
            return;
        }

    }

    //my function
    private void initTensorFlowAndLoadModel() {
        executor.execute(new Runnable() {
            @Override
            public void run() {
                try {
                    //TensorFlowImageClassifier used from TensorFlowImageClassifier.java
                    classifier = TensorFlowImageClassifier.create(
                            getAssets(),
                            MODEL_FILE,
                            LABEL_FILE,
                            INPUT_SIZE,
                            INPUT_NAME,
                            OUTPUT_NAME);
                    //makeButtonVisible();
                    Log.d("xuu", "Load Success");
                } catch (final Exception e) {
                    throw new RuntimeException("Error initializing TensorFlow!", e);
                }
            }
        });
    }
}
