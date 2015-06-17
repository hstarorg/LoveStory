using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoseLove
{
    public partial class FrmMain : Form
    {
        private string path = Path.Combine(Application.StartupPath, "../../Images/qq.png");
        public FrmMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
           
            Graphics g = e.Graphics;
            TextureBrush b2 = new TextureBrush(BuildBitmap(path, 20));
          //  b2.RotateTransform(20);
            Rectangle rect = new Rectangle(500, 300, 60, 92);//定义矩形,参数为起点横纵坐标以及其长和宽
            g.FillRectangle(b2, rect);

        }

        public Image BuildBitmap(string path,float py)
        {
            Bitmap bitmap =new Bitmap(100,100);

             var g = Graphics.FromImage(bitmap);
             g.Clear(Color.White);
             //设置画板的坐标原点为中点
             g.TranslateTransform(bitmap.Width/2, bitmap.Height/2);
             //以指定角度对画板进行旋转
             g.RotateTransform(py);
             //把数字画到画板的中点位置
             g.DrawImage(Image.FromFile(path), new Rectangle(10, 10,90,90));
             return bitmap;
         }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            pictureBox1.Image = BuildBitmap(path, 30);
            //new Thread(() =>
            //{ while (pictureBox1.Top < 800)
            //    {
            //        Thread.Sleep(50);
            //        pictureBox1.Top += 2;
            //    }

            //}).Start();
        }
    }
}
