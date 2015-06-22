using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageDownloadInterface;
using MeizituImpl;

namespace AmazingDestop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IImageDownload meizitu = new MeizituDownload();
            meizitu.DownloadImages("D://1", () =>
            {
                Console.WriteLine("OK");
            });
        }
    }
}
