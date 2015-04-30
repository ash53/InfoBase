using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoBase
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            //Image img = Image.FromFile("");
           // img.
            //pictureBox1.Image = Image.FromFile(@"D:\image1.png"); 
            //pictureBox1.ImageLocation = @"D:\image1.png";
           /// pictureBox1.BackgroundImage = Image.FromFile(@"image1.png");
            pictureBox1.ImageLocation = @"C:\Users\Administrator\Documents\Visual Studio 2010\Projects\bin\Image\image1.png";
           pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;


            //pictureBox1.Load(@"C:\Users\Administrator\Documents\visual studio 2010\Projects\InfoBase\InfoBase\Image\image1.png");
        }
    }
}
