using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RoseLove.Extensions;
using RoseLove.Model;

namespace RoseLove
{
    public partial class FrmMain : Form
    {
        private Image img = Image.FromFile(Path.Combine(Application.StartupPath, "../../Images/qq.png"));
        private Random random = new Random();
        private readonly List<Balloon> ballonList = new List<Balloon>();
        private int width, height;

        public FrmMain()
        {
            width = SystemInformation.VirtualScreen.Width;
            height = SystemInformation.VirtualScreen.Height;
            InitializeComponent();
            //启用双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
            CheckForIllegalCrossThreadCalls = false;

            ballonList.Add(new Balloon
            {
                Angle = 10.5f,
                X = 200,
                Y = 0,
                Speed = 5,
                Img = AA(img).GetRotateImage(10.5f)
            });
            ballonList.Add(new Balloon
            {
                Angle = -10.5f,
                X = 400,
                Y = 0,
                Speed = 7,
                Img = AA(img)
            });
        }

        public Image AA(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            Graphics g = Graphics.FromImage(bmp);
            String str = "邓";
            Font font = new Font("微软雅黑", 20);
            SolidBrush sbrush = new SolidBrush(Color.Blue);
            g.DrawString(str, font, sbrush, new PointF(12, 5));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return bmp;
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var invalidIndexList = new List<int>();
            for (int i = 0, len = ballonList.Count; i < len; i++)
            {
                var balloon = ballonList[i];
                g.DrawImage(balloon.Img, new Rectangle(balloon.X, balloon.Y, 60, 92));
                balloon.CalcLocation();
                if (balloon.Y > height)
                {
                    invalidIndexList.Add(i);
                }
            }
            //移除无效
            for (int i = invalidIndexList.Count - 1; i >= 0; i--)
            {
                ballonList.RemoveAt(invalidIndexList[i]);
            }

        }

        private float GetRandom()
        {
            return (float)random.NextDouble() * 20;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
