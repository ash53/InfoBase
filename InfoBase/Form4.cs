using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace InfoBase
{
    public partial class Form4 : Form
    {

        //rounded corner
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );

        public Size OriginalImageSize { get; set; }        //Store original image size.
        public Size NewImageSize { get; set; }


        public Form4()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); // for rounded corner
        }

        static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);
            int sourceWidth = imgPhoto.Width;     //store original width of source image.
            int sourceHeight = imgPhoto.Height;   //store original height of source image.
            int sourceX = 0;        //x-axis of source image.
            int sourceY = 0;        //y-axis of source image.
            int destX = 0;          //x-axis of destination image.
            int destY = 0;          //y-axis of destination image.
            //Create a new bitmap object.
            //user deffiend width 120
            //user deffiend height 80
            Bitmap bmPhoto = new Bitmap(120, 100, PixelFormat.Format24bppRgb);
            //Set resolution of bitmap.
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Create a graphics object and set quality of graphics.
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //Draw image by using DrawImage() method of graphics class.
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, 120, 100), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
            grPhoto.Dispose();  //Dispose graphics object.
            return bmPhoto;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }


        //to move the form when the FormBorderStyle is set to none.
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void fax2_TextChanged(object sender, EventArgs e)
        {

        }

        public void ClearTextBox(Control control)
        {
            foreach (Control c in control.Controls)
            {

                if (c is TextBox)
                {

                    ((TextBox)c).Clear();

                }

                if (c.HasChildren)
                {

                    ClearTextBox(c);

                }

            }
        }

        //official information
        private void roundButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.InsertCommand = new SqlCommand("INSERT INTO official VALUES (@emp_ID, @emp_name, @dept, @designation, @join_date, @leaving_date, @emp_state, @rep_auth, @salary, @off_email, @off_mob, @off_tnt, @job_loc)", con);
            adp.InsertCommand.Parameters.Add("@emp_ID", SqlDbType.VarChar).Value = empID.Text;
            adp.InsertCommand.Parameters.Add("@emp_name", SqlDbType.VarChar).Value = emp_name.Text;
            adp.InsertCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = dept.Text;
            adp.InsertCommand.Parameters.Add("@designation", SqlDbType.VarChar).Value = designation.Text;
            adp.InsertCommand.Parameters.Add("@join_date", SqlDbType.VarChar).Value = joining.Text;
            adp.InsertCommand.Parameters.Add("@leaving_date", SqlDbType.VarChar).Value = leaving.Text;
            adp.InsertCommand.Parameters.Add("@emp_state", SqlDbType.VarChar).Value = emp_status.Text;
            adp.InsertCommand.Parameters.Add("@rep_auth", SqlDbType.VarChar).Value = rep_auth.Text;
            adp.InsertCommand.Parameters.Add("@salary", SqlDbType.VarChar).Value = salary.Text;
            adp.InsertCommand.Parameters.Add("@off_email", SqlDbType.VarChar).Value = off_email.Text;
            adp.InsertCommand.Parameters.Add("@off_mob", SqlDbType.VarChar).Value = off_mob.Text;
            adp.InsertCommand.Parameters.Add("@off_tnt", SqlDbType.VarChar).Value = off_tnt.Text;
            adp.InsertCommand.Parameters.Add("@job_loc", SqlDbType.VarChar).Value = job_loc.Text;
            con.Open();
            int i=adp.InsertCommand.ExecuteNonQuery();
            con.Close();
            ClearTextBox(this);
            if(i>0)
            MessageBox.Show("Insertion Successful");
        }


        //present information
        private void roundButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.InsertCommand = new SqlCommand("INSERT INTO present_info VALUES (@mail_add, @phone, @mobile, @email, @fax)", con);
            adp.InsertCommand.Parameters.Add("@mail_add", SqlDbType.VarChar).Value = mail_add1.Text;
            adp.InsertCommand.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone1.Text;
            adp.InsertCommand.Parameters.Add("@mobile", SqlDbType.VarChar).Value = mobile1.Text;
            adp.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email1.Text;
            adp.InsertCommand.Parameters.Add("@fax", SqlDbType.VarChar).Value = fax1.Text;
            con.Open();
            int i=adp.InsertCommand.ExecuteNonQuery();
            con.Close();

            ClearTextBox(this);
            if(i>0)
            MessageBox.Show("Insertion Successful");
        }

        //permanent_info
        private void roundButton3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.InsertCommand = new SqlCommand("INSERT INTO permanent_info VALUES (@mail_add1, @phone1, @mobile1, @email1, @fax1)", con);
            adp.InsertCommand.Parameters.Add("@mail_add1", SqlDbType.VarChar).Value = mail_add2.Text;
            adp.InsertCommand.Parameters.Add("@phone1", SqlDbType.VarChar).Value = phone2.Text;
            adp.InsertCommand.Parameters.Add("@mobile1", SqlDbType.VarChar).Value = mobile2.Text;
            adp.InsertCommand.Parameters.Add("@email1", SqlDbType.VarChar).Value = email2.Text;
            adp.InsertCommand.Parameters.Add("@fax1", SqlDbType.VarChar).Value = fax2.Text;
            con.Open();
            int i=adp.InsertCommand.ExecuteNonQuery();
            con.Close();
            ClearTextBox(this);
            if(i>0)
            MessageBox.Show("Insertion Successful");
        }


        //emergency contact
        private void roundButton4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
            SqlDataAdapter adp = new SqlDataAdapter();

            adp.InsertCommand = new SqlCommand("INSERT INTO emergency_contact VALUES (@name, @relationship, @mail_add, @phn, @email)", con);
            adp.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name.Text;
            adp.InsertCommand.Parameters.Add("@relationship", SqlDbType.VarChar).Value = rltnship.Text;
            adp.InsertCommand.Parameters.Add("@mail_add", SqlDbType.VarChar).Value = mail_add.Text;
            adp.InsertCommand.Parameters.Add("@phn", SqlDbType.VarChar).Value = phn.Text;
            adp.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email.Text;
            con.Open();
            int i=adp.InsertCommand.ExecuteNonQuery();
            con.Close();
            ClearTextBox(this);
            if(i>0)
            MessageBox.Show("Insertion Successful");
        }


        //other info
        private void roundButton6_Click(object sender, EventArgs e)
        {
            //save image
            string imagepath = pictureBox1.ImageLocation.ToString();
            string picname = imagepath.Substring(imagepath.LastIndexOf('\\'));
            string path = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));
            Bitmap imgImage = new Bitmap(pictureBox1.Image);    //Create an object of Bitmap class/
            imgImage.Save(path + "\\Image\\" + picname + ".jpg");
            string location = path + "'\'Image'\'" + picname;
            //MessageBox.Show("image svaed in :" + path + "'\'Image'\'" + picname);
            
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

            SqlDataAdapter adp = new SqlDataAdapter();
            //StringBuilder input = new StringBuilder();
            adp.InsertCommand = new SqlCommand("INSERT INTO other_info VALUES (@gender, @mar_stat, @spouse_name, @mail_add, @contact_no, @birth_date, @children_no, @blood_group, @birth_place, @birth_nationality, @religion, @experience_year, @height, @weight, @present_nationality, @age, @service_length, @photo)", con);
            if (checkBox1.Checked)
                adp.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = checkBox1.Text.ToString();
            if (checkBox2.Checked)
                adp.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = checkBox2.Text.ToString();
            if (checkBox3.Checked)
                adp.InsertCommand.Parameters.Add("@mar_stat", SqlDbType.VarChar).Value = checkBox3.Text.ToString();
            if (checkBox4.Checked)
                adp.InsertCommand.Parameters.Add("@mar_stat", SqlDbType.VarChar).Value = checkBox4.Text.ToString();
            //input.AppendFormat("{0},", checkBox1.Text);

            adp.InsertCommand.Parameters.Add("@spouse_name", SqlDbType.VarChar).Value = spouse.Text;
            adp.InsertCommand.Parameters.Add("@mail_add", SqlDbType.VarChar).Value = mail.Text;
            adp.InsertCommand.Parameters.Add("@contact_no", SqlDbType.VarChar).Value = contact.Text;
            adp.InsertCommand.Parameters.Add("@birth_date", SqlDbType.VarChar).Value = birth_date.Text;
            adp.InsertCommand.Parameters.Add("@children_no", SqlDbType.VarChar).Value = children_no.Text;
            adp.InsertCommand.Parameters.Add("@blood_group", SqlDbType.VarChar).Value = blood_group.Text;
            adp.InsertCommand.Parameters.Add("@birth_place", SqlDbType.VarChar).Value = birth_place.Text;
            adp.InsertCommand.Parameters.Add("@birth_nationality", SqlDbType.VarChar).Value = birth_nationality.Text;
            adp.InsertCommand.Parameters.Add("@religion", SqlDbType.VarChar).Value = religion.Text;
            adp.InsertCommand.Parameters.Add("@experience_year", SqlDbType.VarChar).Value = exp.Text;
            adp.InsertCommand.Parameters.Add("@height", SqlDbType.VarChar).Value = height.Text;
            adp.InsertCommand.Parameters.Add("@weight", SqlDbType.VarChar).Value = weight.Text;
            adp.InsertCommand.Parameters.Add("@present_nationality", SqlDbType.VarChar).Value = present_nationality.Text;
            adp.InsertCommand.Parameters.Add("@age", SqlDbType.VarChar).Value = age.Text;
            adp.InsertCommand.Parameters.Add("@service_length", SqlDbType.VarChar).Value = service.Text;
            adp.InsertCommand.Parameters.Add("@photo", SqlDbType.Image).Value = location;
            con.Open();
            int i=adp.InsertCommand.ExecuteNonQuery();
            con.Close();
            ClearTextBox(this);
            if(i>0)
            MessageBox.Show("Insertion Successful");
        }

        //browse image button-other_info image
        private void roundButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openImage = new OpenFileDialog();
            DialogResult result = openImage.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Load image in picture box and set size mode property of image as normal.
                pictureBox1.Load(openImage.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                //Store source location of image in textbox.
                textBox1.Text = openImage.FileName;
                //Retrive height and width of image and store in OriginalImageSize variable.
                int imgWidth = pictureBox1.Image.Width;
                int imgHeight = pictureBox1.Image.Height;
                OriginalImageSize = new Size(imgWidth, imgHeight);
                string imgSize = "Width " + imgWidth + " px  Height " + imgHeight + " px";
                before_resize.Text = imgSize;
            }
            
        }

        //after resize-other_info image
        private void resize_Click(object sender, EventArgs e)
        {
            string imgSize;
            //image width greater than 102 or height greater than 76then only Resize
            if (pictureBox1.Image.Width > 102 || pictureBox1.Image.Height > 76)
            {
                Image scaledImage = ScaleByPercent(pictureBox1.Image, 10);
                pictureBox1.Image = scaledImage;        //Display that image in picture box.
                //Retrive width and height of image.
                imgSize = "Width " + scaledImage.Width + " px  Height " + scaledImage.Height + " px";
            }
            else
            {
                imgSize = "Width " + pictureBox1.Image.Width + " px  Height " + pictureBox1.Image.Height + " px";
            }
            after_resize.Text = imgSize;
        }

        //browse photo-passport
        private void roundButton8_Click(object sender, EventArgs e)
        {
             OpenFileDialog openImage = new OpenFileDialog();
            DialogResult result = openImage.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Load image in picture box and set size mode property of image as normal.
                pictureBox2.Load(openImage.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
                //Store source location of image in textbox.
                pic1.Text = openImage.FileName;
                //Retrive height and width of image and store in OriginalImageSize variable.
                int imgWidth = pictureBox2.Image.Width;
                int imgHeight = pictureBox2.Image.Height;
                OriginalImageSize = new Size(imgWidth, imgHeight);
                string imgSize = "Width " + imgWidth + " px  Height " + imgHeight + " px";
                pass_before.Text = imgSize;
            }
        }


        //resize photo- passport
        private void roundButton9_Click(object sender, EventArgs e)
        {
            string imgSize;
            //image width greater than 102 or height greater than 76then only Resize
            if (pictureBox2.Image.Width > 102 || pictureBox2.Image.Height > 76)
            {
                Image scaledImage = ScaleByPercent(pictureBox2.Image, 10);
                pictureBox2.Image = scaledImage;        //Display that image in picture box.
                //Retrive width and height of image.
                imgSize = "Width " + scaledImage.Width + " px  Height " + scaledImage.Height + " px";
            }
            else
            {
                imgSize = "Width " + pictureBox2.Image.Width + " px  Height " + pictureBox2.Image.Height + " px";
            }
            pass_after.Text = imgSize;
        }

        //save button- passport and bank info
        private void roundButton7_Click(object sender, EventArgs e)
        {
            //save image- passport
            string imagepath = pictureBox2.ImageLocation.ToString();
            string picname = imagepath.Substring(imagepath.LastIndexOf('\\'));
            string path = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));
            Bitmap imgImage = new Bitmap(pictureBox2.Image);    //Create an object of Bitmap class/
            imgImage.Save(path + "\\pass_image\\" + picname + ".jpg");
            string location1 = path + "'\'pass_image'\'" + picname;
            //MessageBox.Show("image svaed in :" + path + "'\'pass_image'\'" + picname);
            //save image- NID
            string imagepath1 = pictureBox3.ImageLocation.ToString();
            string picname1 = imagepath1.Substring(imagepath1.LastIndexOf('\\'));
            string path1 = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));
            Bitmap imgImage1 = new Bitmap(pictureBox3.Image);    //Create an object of Bitmap class/
            imgImage1.Save(path1 + "\\NID_image\\" + picname1 + ".jpg");
            string location2 = path1 + "'\'NID_image'\'" + picname1;
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.InsertCommand = new SqlCommand("INSERT INTO  passport_info VALUES (@pass_no, @pass_image, @NID_no, @NID_image, @TIN_info, @TIN_no, @bank_name, @branch, @account_no, @account_type)", con);
            adp.InsertCommand.Parameters.Add("@pass_no", SqlDbType.VarChar).Value = pass_no.Text;
            adp.InsertCommand.Parameters.Add("@pass_image", SqlDbType.VarChar).Value = location1;
            adp.InsertCommand.Parameters.Add("@NID_no", SqlDbType.VarChar).Value = NID_no.Text;
            adp.InsertCommand.Parameters.Add("@NID_image", SqlDbType.VarChar).Value = location2;
            adp.InsertCommand.Parameters.Add("@TIN_info", SqlDbType.VarChar).Value = TIN_info.Text;
            adp.InsertCommand.Parameters.Add("@TIN_no", SqlDbType.VarChar).Value =TIN.Text;
            adp.InsertCommand.Parameters.Add("@bank_name", SqlDbType.VarChar).Value = bank_name.Text;
            adp.InsertCommand.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch.Text;
            adp.InsertCommand.Parameters.Add("@account_no", SqlDbType.VarChar).Value = acc_no.Text;
            adp.InsertCommand.Parameters.Add("@account_type", SqlDbType.VarChar).Value = acc_type.Text;

            pictureBox2.Image = null;
            pictureBox3.Image = null;

            con.Open();
            adp.InsertCommand.ExecuteNonQuery();
            con.Close();
            ClearTextBox(this);
        }

        //NID-browse photo
        private void roundButton11_Click(object sender, EventArgs e)
        {
            OpenFileDialog openImage = new OpenFileDialog();
            DialogResult result = openImage.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Load image in picture box and set size mode property of image as normal.
                pictureBox3.Load(openImage.FileName);
                pictureBox3.SizeMode = PictureBoxSizeMode.Normal;
                //Store source location of image in textbox.
                pic2.Text = openImage.FileName;
                //Retrive height and width of image and store in OriginalImageSize variable.
                int imgWidth = pictureBox3.Image.Width;
                int imgHeight = pictureBox3.Image.Height;
                OriginalImageSize = new Size(imgWidth, imgHeight);
                string imgSize = "Width " + imgWidth + " px  Height " + imgHeight + " px";
                NID_before.Text = imgSize;
            }
        }

        //NID- resize photo
        private void roundButton10_Click(object sender, EventArgs e)
        {
            string imgSize;
            //image width greater than 102 or height greater than 76then only Resize
            if (pictureBox3.Image.Width > 102 || pictureBox3.Image.Height > 76)
            {
                Image scaledImage = ScaleByPercent(pictureBox3.Image, 10);
                pictureBox3.Image = scaledImage;        //Display that image in picture box.
                //Retrive width and height of image.
                imgSize = "Width " + scaledImage.Width + " px  Height " + scaledImage.Height + " px";
            }
            else
            {
                imgSize = "Width " + pictureBox3.Image.Width + " px  Height " + pictureBox3.Image.Height + " px";
            }
            NID_after.Text = imgSize;
        }

       

    }
}
