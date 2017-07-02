using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xu
{
    public delegate void MyDelegate(string Item1);

    public partial class student : Form
    {
        public MyDelegate myDelegate;//声明一个委托的对象
        public student()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入地点");
                return;
            }
            else {
                myDelegate(textBox1.Text);
                this.Dispose();
            }
        }
    }
}
