namespace xu
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axLicenseControl1 = new AxESRI.ArcGIS.Controls.AxLicenseControl();
            this.axSceneControl1 = new AxESRI.ArcGIS.Controls.AxSceneControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.场景漫游ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新生帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.常去地点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.实况ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查找地点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(-10, 361);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // axSceneControl1
            // 
            this.axSceneControl1.Location = new System.Drawing.Point(214, 42);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSceneControl1.OcxState")));
            this.axSceneControl1.Size = new System.Drawing.Size(797, 543);
            this.axSceneControl1.TabIndex = 1;
            this.axSceneControl1.OnMouseDown += new AxESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseDownEventHandler(this.axSceneControl1_OnMouseDown);
            this.axSceneControl1.OnMouseUp += new AxESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseUpEventHandler(this.axSceneControl1_OnMouseUp);
            this.axSceneControl1.OnMouseMove += new AxESRI.ArcGIS.Controls.ISceneControlEvents_OnMouseMoveEventHandler(this.axSceneControl1_OnMouseMove);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(214, 8);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(285, 28);
            this.axToolbarControl1.TabIndex = 5;
            this.axToolbarControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnMouseDownEventHandler(this.axToolbarControl1_OnMouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.新生帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1035, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.场景漫游ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "场景";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存ToolStripMenuItem.Text = "保存场景";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 场景漫游ToolStripMenuItem
            // 
            this.场景漫游ToolStripMenuItem.Name = "场景漫游ToolStripMenuItem";
            this.场景漫游ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.场景漫游ToolStripMenuItem.Text = "场景漫游";
            this.场景漫游ToolStripMenuItem.Click += new System.EventHandler(this.场景漫游ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.属性ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "要素";
            // 
            // 属性ToolStripMenuItem
            // 
            this.属性ToolStripMenuItem.Name = "属性ToolStripMenuItem";
            this.属性ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.属性ToolStripMenuItem.Text = "属性查询";
            this.属性ToolStripMenuItem.Click += new System.EventHandler(this.属性ToolStripMenuItem_Click);
            // 
            // 新生帮助ToolStripMenuItem
            // 
            this.新生帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常去地点ToolStripMenuItem,
            this.实况ToolStripMenuItem,
            this.查找地点ToolStripMenuItem});
            this.新生帮助ToolStripMenuItem.Name = "新生帮助ToolStripMenuItem";
            this.新生帮助ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.新生帮助ToolStripMenuItem.Text = "新生帮助";
            // 
            // 常去地点ToolStripMenuItem
            // 
            this.常去地点ToolStripMenuItem.Name = "常去地点ToolStripMenuItem";
            this.常去地点ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.常去地点ToolStripMenuItem.Text = "常去地点";
            this.常去地点ToolStripMenuItem.Click += new System.EventHandler(this.常去地点ToolStripMenuItem_Click);
            // 
            // 实况ToolStripMenuItem
            // 
            this.实况ToolStripMenuItem.Name = "实况ToolStripMenuItem";
            this.实况ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.实况ToolStripMenuItem.Text = "实况";
            this.实况ToolStripMenuItem.Click += new System.EventHandler(this.实况ToolStripMenuItem_Click);
            // 
            // 查找地点ToolStripMenuItem
            // 
            this.查找地点ToolStripMenuItem.Name = "查找地点ToolStripMenuItem";
            this.查找地点ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.查找地点ToolStripMenuItem.Text = "查找地点";
            this.查找地点ToolStripMenuItem.Click += new System.EventHandler(this.查找地点ToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("华文新魏", 10.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(0, 246);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(207, 339);
            this.textBox2.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 592);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axSceneControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSceneControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private AxESRI.ArcGIS.Controls.AxSceneControl axSceneControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 场景漫游ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripMenuItem 新生帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 常去地点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 实况ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查找地点ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

