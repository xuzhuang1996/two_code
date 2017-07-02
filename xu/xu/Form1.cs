using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using System.Threading;

namespace xu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ini();
        }


        public bool move = false;
        public int clickSceneTime = 0;
        public IPoint scenePanPoints1 = new PointClass();
        public IPoint scenePanPoints2 = new PointClass();
        public bool shuxing = false;


        public void ini()
        {
            //加载地图
            string file = @"C:\Users\qiutao\Desktop\school_xu.sxd";
            if (axSceneControl1.CheckSxFile(file))
            {
                axSceneControl1.LoadSxFile(file);
            }
            else MessageBox.Show("no");
            //注册滑轮事件
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.axSceneControl1_Wheel);


        }

        private void HandleIdentify_MouseDown(object sender, ISceneControlEvents_OnMouseDownEvent e)
        {
            MessageBox.Show("2");
            IHit3DSet pHit3DSet = null;

            this.axSceneControl1.SceneGraph.LocateMultiple(this.axSceneControl1.SceneViewer, e.x, e.y, esriScenePickMode.esriScenePickAll, false, out pHit3DSet);
            pHit3DSet.OnePerLayer();
            if (pHit3DSet.Hits.Count == 0)
            {
                MessageBox.Show("没有选中任何要素!");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void axSceneControl1_Wheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (axSceneControl1.Visible == true)
            {
                try
                {
                    System.Drawing.Point pSceLoc = axSceneControl1.PointToScreen(this.axSceneControl1.Location);
                    //System.Drawing.Point Pt = this.PointToScreen(e.Location);
                    //if (Pt.X < pSceLoc.X || Pt.X > pSceLoc.X + axSceneControl1.Width || Pt.Y < pSceLoc.Y || Pt.Y > pSceLoc.Y + axSceneControl1.Height) return;
                    System.Drawing.Point Pt = axSceneControl1.PointToClient(Control.MousePosition);
                    if (Pt.X < 0 || Pt.Y < 0) return;
                    double scale = 0.2;
                    if (e.Delta > 0) scale = -0.2;
                    else scale = 0.2;

                    ICamera pCamera = axSceneControl1.Camera;
                    IPoint pPtObs = pCamera.Observer;
                    IPoint pPtTar = pCamera.Target;
                    pPtObs.X += (pPtObs.X - pPtTar.X) * scale;
                    pPtObs.Y += (pPtObs.Y - pPtTar.Y) * scale;
                    pPtObs.Z += (pPtObs.Z - pPtTar.Z) * scale;
                    pCamera.Observer = pPtObs;
                    axSceneControl1.SceneGraph.RefreshViewers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void axSceneControl1_OnMouseDown(object sender, AxESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseDownEvent e)
        {
            if (!shuxing) return;
            textBox2.Text = "";
            this.axSceneControl1.Scene.ClearSelection();//清除之前的选择集，去除高亮显示  
            IHit3DSet pHit3DSet = null;
            this.axSceneControl1.SceneGraph.LocateMultiple(this.axSceneControl1.SceneViewer, e.x, e.y, esriScenePickMode.esriScenePickAll, false, out pHit3DSet);
            pHit3DSet.OnePerLayer();
            if (pHit3DSet.Hits.Count <= 0)
            {
                MessageBox.Show("没有选中任何要素!");
                return;
            }

            IHit3D pHit3D = pHit3DSet.Hits.get_Element(0) as IHit3D;
            IFeature pFeature = pHit3D.Object as IFeature;
            IFields pFields = pFeature.Fields;
            //确认选择显示的属性
            for (int i = 2; i < pFields.FieldCount-1; i++)
            {
                IField pField = pFields.get_Field(i);
                if (pField.Type != esriFieldType.esriFieldTypeGeometry)
                {
                    textBox2.Text += pField.Name + ":" + pFeature.get_Value(pFields.FindField(pField.Name));
                    textBox2.Text += "\r\n";
                }
            }
            //图片问题
            string temp =(string) pFeature.get_Value(pFields.FindField("picture_"));
            this.pictureBox1.Image = Image.FromFile(@"C:\Users\qiutao\Desktop\picture\"+temp+".jpg", false);  

            IDisplay3D pDisplay3D = this.axSceneControl1.SceneGraph as IDisplay3D;
            pDisplay3D.FlashGeometry(pHit3D.Owner, pHit3D.Object);//闪烁一次，pHit3D.Owner是一个ILayer类型，pHit3D.Object是一个IFeature类型
            //pDisplay3D.AddFlashFeature(pFeature.Shape);//保持高亮
            this.axSceneControl1.Scene.SelectFeature(pHit3D.Owner as ILayer, pFeature);//加入选择集，并自动高亮
        }

        private void axSceneControl1_OnMouseMove(object sender, AxESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseMoveEvent e)
        {

        }

        private void axSceneControl1_OnMouseUp(object sender, AxESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseUpEvent e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        public void choose()
        {
            //axToolbarControl1

        }


        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string sFileName;
                SaveFileDialog pSaveFile = new SaveFileDialog();
                pSaveFile.Title = "保存图片";

                pSaveFile.Filter = "JPEG图片(*.jpg)|*.jpg";

                pSaveFile.ShowDialog();

                sFileName = pSaveFile.FileName;
                if (pSaveFile.FilterIndex == 1 && sFileName != null)
                {
                    axSceneControl1.SceneViewer.GetScreenShot(esri3DOutputImageType.JPEG, sFileName);
                    MessageBox.Show("成功保存图片至:" + sFileName);
                }

            }
            catch
            {
                MessageBox.Show("出现错误返回");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void axToolbarControl1_OnMouseDown(object sender, IToolbarControlEvents_OnMouseDownEvent e)
        {
            shuxing = false;
        }

        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shuxing = true;
        }

        private void 场景漫游ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                timer1.Enabled = true;
            else timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            axSceneControl1.Camera.Rotate(0.5);
            axSceneControl1.Refresh();
        }


        private void button2_Click_2(object sender, EventArgs e)
        {
            

        }

        private void car(IFeature feature)
        {
            if (feature == null) return;
            IPolyline polyline = (IPolyline)feature.Shape;
            double d = polyline.Length;
            IPoint point1 = new PointClass();
            IPoint point2 = new PointClass();
            //导入模型设置参数
            IMarker3DSymbol pmark3dsymbol = new Marker3DSymbolClass();
            pmark3dsymbol.CreateFromFile(@"C:\Users\qiutao\Desktop\file.3ds");
            IMarkerSymbol marksy = (IMarkerSymbol)pmark3dsymbol;
            marksy.Size = 5;
            //marksy.Angle = 90;
            IElement pelement = new MarkerElementClass();
            IMarkerElement pmarkelement = (IMarkerElement)pelement;
            pmarkelement.Symbol = (IMarkerSymbol)marksy;
            ICamera camera = axSceneControl1.SceneViewer.Camera;
            //
            IGraphicsLayer player = axSceneControl1.SceneGraph.Scene.BasicGraphicsLayer;
            IGraphicsContainer3D pgraphiccontainer3d = (IGraphicsContainer3D)player;
            IPoint point3 = new PointClass();
            IScene pscene = axSceneControl1.SceneGraph.Scene;

            for (int i = 2; i <= (int)d; i += 20)
            {

                //i该点在曲线上最近的点距曲线起点的距离
                polyline.QueryPoint(esriSegmentExtension.esriNoExtension, i, false, point1);
                polyline.QueryPoint(esriSegmentExtension.esriExtendAtFrom, i - 100, false, point2);//50
                point2.Z = 13;
                point2.X = point2.X - 100;//50

                point3.X = point1.X;
                point3.Y = point1.Y;
                point3.Z = 13;
                camera.Target = point3;
                camera.Observer = point2;

                pelement.Geometry = point1;

                pgraphiccontainer3d.DeleteAllElements();
                pgraphiccontainer3d.AddElement((IElement)pmarkelement);
                axSceneControl1.SceneGraph.RefreshViewers();
            }
            }

        

        private void 常去地点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            student s = new student();
            s.myDelegate += new MyDelegate(show_line);
            s.Show();
        }

        private void show_line(string name)
        {
            if (name == "") return;
            this.axSceneControl1.Scene.ClearSelection();//清除之前的选择集，去除高亮显示  
            IFeatureLayer all = this.axSceneControl1.Scene.get_Layer(2) as IFeatureLayer;
            IFeatureClass all_f = all.FeatureClass;

            for (int i = 0; i < all_f.FeatureCount(null); i++)
            {
                IFeature pFeature = all_f.GetFeature(i);
                IFields pFields = all_f.Fields;
                IField pField = pFields.get_Field(3);
                
                if ((string)pFeature.get_Value(pFields.FindField(pField.Name)) == name)
                {
                    this.axSceneControl1.Scene.SelectFeature(all, pFeature);//加入选择集，并自动高亮
                    //放大至要素
                    //IEnvelope pEenvelop = pFeature.Shape.Envelope;
                    //ICamera pCamera = axSceneControl1.Scene.SceneGraph.ActiveViewer.Camera;//将当前视点跳转到
                    //pCamera.SetDefaultsMBB(pEenvelop);//设置相机
                    axSceneControl1.Refresh();
                    
                }
            } //MessageBox.Show("error");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //取消跨线程访问的检查
            Control.CheckForIllegalCrossThreadCalls = false;
            AxSceneControl.CheckForIllegalCrossThreadCalls = false;
        }

        private void 实况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISelection selection = axSceneControl1.Scene.FeatureSelection;
            IEnumFeatureSetup iEnumFeatureSetup = (IEnumFeatureSetup)selection;
            IEnumFeature enumFeature = (IEnumFeature)iEnumFeatureSetup;
            enumFeature.Reset();
            IFeature feature = enumFeature.Next();
            if (feature != null) car(feature);
        }

        private void 查找地点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            find f = new find();
            f.myDelegate = new MyLine(find_my);
            f.Show();
        }

        private void find_my(string name) {
            if (name == "") return;
            this.axSceneControl1.Scene.ClearSelection();//清除之前的选择集，去除高亮显示  
            IFeatureLayer all = this.axSceneControl1.Scene.get_Layer(0) as IFeatureLayer;
            IFeatureClass all_f = all.FeatureClass;
            for (int i = 0; i < all_f.FeatureCount(null); i++)
            {
                IFeature pFeature = all_f.GetFeature(i);
                IFields pFields = all_f.Fields;
                IField pField = pFields.get_Field(4);
                if ((string)pFeature.get_Value(pFields.FindField(pField.Name)) == name)
                {
                    this.axSceneControl1.Scene.SelectFeature(all, pFeature);//加入选择集，并自动高亮
                    //放大至要素
                    IEnvelope pEenvelop = pFeature.Shape.Envelope;
                    ICamera pCamera = axSceneControl1.Scene.SceneGraph.ActiveViewer.Camera;//将当前视点跳转到
                    //pEenvelop.ZMin = axSceneControl1.Scene.Extent.ZMin;//添加z轴上的范维
                    //pEenvelop.ZMax = axSceneControl1.Scene.Extent.ZMax;
                    pCamera.SetDefaultsMBB(pEenvelop);//设置相机
                    axSceneControl1.Refresh();
                    return;
                }
            }
            MessageBox.Show("未找到");
        }
    }
}
