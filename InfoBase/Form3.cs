using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InfoBase.xUI;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing.Imaging;
using System.IO;
//using System.Configuration;

namespace InfoBase
{
    

    public partial class menuPage : Form
    {
        static public string empID;
        public Size OriginalImageSize { get; set; }        //Store original image size.
        public Size NewImageSize { get; set; }

        static int count = 0, counter=0;   //variable to count if user ID already exists

        // for changing the text color using placeholder
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32
        SendMessage(
        IntPtr hwnd,
        int msg,
        int wParam,
        [MarshalAs(UnmanagedType.LPWStr)] string lparam);
        private const int EM_SETCUBEBANNER = 0x1501;
        /*******************************************************/

        //for viewing profile
        /*DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        SqlConnection cs = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

        SqlDataAdapter da = new SqlDataAdapter();

        BindingSource bsource = new BindingSource();*/
        /**************************************************/

        public menuPage()
        {
            InitializeComponent();
            //removes the header of the tab control
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            /**************************************************/
            tabControl1.SelectedTab = tabProfile;
            //set format on textboxes
            SendMessage(txtJoiningDate.Handle, EM_SETCUBEBANNER, 0, "dd/mm/yyyy");
            SendMessage(txtLeavingDate.Handle, EM_SETCUBEBANNER, 0, "dd/mm/yyyy");
            SendMessage(birth_date.Handle, EM_SETCUBEBANNER, 0, "dd/mm/yyyy");
            SendMessage(txtProcessDate.Handle, EM_SETCUBEBANNER, 0, "dd/mm/yyyy");

            btnViewProfile.Visible = false;
        }

       

        //function for scaling image
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
        
       /*************************************************************************************************************************************************************/
       

        //Function for setting same property to all button
        private void btnProperty(Button btn)
        {
            btn.Font = new Font("Microsoft Sans Serif", 10);
            btn.BackColor = Color.FromArgb(139, 134, 130);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            btn.TextAlign = ContentAlignment.MiddleLeft;
        }
        /****************************************************************************/

        //Function for setting same property to all menu items
        private void mnuProperty(InfoBase.xUI.Menu mnu)
        {
            mnu.BackgroundColor = Color.FromArgb(98, 98, 99);
            mnu.ForeColor = Color.White;
            mnu.TextFont = new Font("Microsoft Sans Serif", 12);
            mnu.TextAlign = ContentAlignment.MiddleLeft;
        }
        /*************************************************************************/

        //Load event of form menuPage
        private void Form3_Load(object sender, EventArgs e)
        {
            //accordion menu items (submenu)
            //submenu- "add employee information" under "add new record"
            Button btnEmpInfoMnu = new Button();
            btnEmpInfoMnu.Text = "Employee Information";
            btnProperty(btnEmpInfoMnu);
            btnEmpInfoMnu.Click += new System.EventHandler(this.btnEmpInfoMnu_Click);


            //submenu- add salary information under menu- add new record
            Button btnSalaryMnu = new Button();
            btnSalaryMnu.Text = "Salary Information";
            btnProperty(btnSalaryMnu);
            btnSalaryMnu.Click += new System.EventHandler(this.btnSalaryMnu_Click);

            //submenu- view profile under menu- home
            Button btnViewProfileMnu = new Button();
            btnViewProfileMnu.Text = "View Profile";
            btnProperty(btnViewProfileMnu);
            btnViewProfileMnu.Click += new System.EventHandler(this.btnViewProfileMnu_Click);

            //submenu- view salary report under menu- home
            Button btnViewSalaryMnu = new Button();
            btnViewSalaryMnu.Text = "View Salary Report";
            btnProperty(btnViewSalaryMnu);
            btnViewSalaryMnu.Click += new System.EventHandler(this.btnViewSalaryMnu_Click);

            //submenu- about infobase under menu- about
            Button btnAbtInfoMnu = new Button();
            btnAbtInfoMnu.Text = "About InfoBase";
            btnProperty(btnAbtInfoMnu);
            btnAbtInfoMnu.Click += new System.EventHandler(this.btnAbtInfoMnu_Click);
           
            //Submenu - help under menu-about
            Button btnHelpMnu = new Button();
            btnHelpMnu.Text = "Help Section";
            btnProperty(btnHelpMnu);
            btnHelpMnu.Click += new System.EventHandler(this.btnHelpMnu_Click);
            /**************************************************************************************/
         
            //Menu Items
            //Menu- home
            xUI.Menu mnuHome = new xUI.Menu();
            mnuProperty(mnuHome);
            mnuHome.Title = "Home";
            mnuHome.Controls.Add(btnViewProfileMnu);
            mnuHome.Controls.Add(btnViewSalaryMnu);
            //mnuHome.Click += new EventHandler(mnuHome_Click);
           
            //menu- Add new
            xUI.Menu mnuAdd = new xUI.Menu();
            mnuProperty(mnuAdd);
            mnuAdd.Title = "Add New";
            mnuAdd.Controls.Add(btnEmpInfoMnu);
            mnuAdd.Controls.Add(btnSalaryMnu);
            

            //Menu- Payroll
            xUI.Menu mnuAbt = new xUI.Menu();
            mnuProperty(mnuAbt);
            mnuAbt.Title = "About";
            mnuAbt.Controls.Add(btnHelpMnu);
            mnuAbt.Controls.Add(btnAbtInfoMnu);
           

            /*********************************************************************************************************/

            //Accordion Menu
            Accordion accordion1 = new Accordion();
            accordion1.Menu.Add(mnuHome);
            accordion1.Menu.Add(mnuAdd);
            accordion1.Menu.Add(mnuAbt);
            accordion1.Bind();

            this.Controls.Add(accordion1);  
            /********************************************************************************************************/
        }

        //when the submenu is clicked, open the corresponding tabPages
        //opens page for viewing employee information
        private void btnViewProfileMnu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabProfile;
        }

        //opens page for viewing salary report
        private void btnViewSalaryMnu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabSalRep;
            //add years to the combobox list
            int currentYear, year;

            currentYear = DateTime.Now.Year;
            cmbYear.Text = "Select Year";
            for (year = 2010; year <= currentYear; year++)
            {
                cmbYear.Items.Add(year);
            }
            //add months to combobox
            for (int i = 0; i < 12; i++)
            {
                cmbMonth.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i]);
            }
            
            
        }

       private void tabBank_Click(object sender, EventArgs e)
       {

       }

        //opens page for inserting employee information
       private void btnEmpInfoMnu_Click(object sender, EventArgs e)
       {
           tabControl1.SelectedTab = tabOfficial;
       }

        //opens page for inserting salary information 
       private void btnSalaryMnu_Click(object sender, EventArgs e)
       {
           tabControl1.SelectedTab = tabSalIn;
       }
        //opens page- "about infobase"
       private void btnAbtInfoMnu_Click(object sender, EventArgs e)
       {
           tabControl1.SelectedTab = tabAbt;
       }
        //opens help section
       private void btnHelpMnu_Click(object sender, EventArgs e)
       {
           tabControl1.SelectedTab = tabHelp;
       }

      

       
        /******************************************************************************************************************/

        //function for clearing all textboxes after save button is clicked
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
        /******************************************************************************************************************************/
        //saves official information
       private void btnSaveOfficial_Click(object sender, EventArgs e)
       {
           if (count == 0)
           {
               empID = txtEmpID.Text;
               string empName = txtEmpName.Text;
               string designation = txtDesignation.Text;
               string joinDate = txtJoiningDate.Text;
               string salary = txtSalary.Text;

               //Without inserting employee ID, name, designation, joindate and salary in the official information page, user can not insert any information in database.
               if (empID == "" || empName == "" || designation == "" || joinDate == "" || salary == "")
               {
                   lblAlertOfficial.Text = "Please fill up the fields with * mark.";
                   lblAlertOfficial.Visible = true;
               }

               else
               {
                   try
                   {
                       SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
                       SqlDataAdapter adp = new SqlDataAdapter();

                       adp.InsertCommand = new SqlCommand("INSERT INTO official VALUES (@emp_ID, @emp_name, @dept, @designation, @join_date, @leaving_date, @emp_state, @rep_auth, @salary, @off_email, @off_mob, @off_tnt, @job_loc)", con);
                       adp.InsertCommand.Parameters.Add("@emp_ID", SqlDbType.Int).Value = Convert.ToInt32(empID);
                       adp.InsertCommand.Parameters.Add("@emp_name", SqlDbType.VarChar).Value = empName;
                       adp.InsertCommand.Parameters.Add("@dept", SqlDbType.VarChar).Value = txtDept.Text;
                       adp.InsertCommand.Parameters.Add("@designation", SqlDbType.VarChar).Value = designation;
                       DateTime joiningDate = DateTime.ParseExact(joinDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                       adp.InsertCommand.Parameters.Add("@join_date", SqlDbType.Date).Value = joiningDate;
                       DateTime leavingDate;
                       if (DateTime.TryParseExact(txtLeavingDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out leavingDate))
                           adp.InsertCommand.Parameters.Add("@leaving_date", SqlDbType.Date).Value = leavingDate;
                       else
                           adp.InsertCommand.Parameters.Add("@leaving_date", SqlDbType.Date).Value = DBNull.Value;

                       adp.InsertCommand.Parameters.Add("@emp_state", SqlDbType.VarChar).Value = txtEmpStatus.Text;
                       adp.InsertCommand.Parameters.Add("@rep_auth", SqlDbType.VarChar).Value = txtRepAuth.Text;
                       adp.InsertCommand.Parameters.Add("@salary", SqlDbType.Decimal).Value = Convert.ToDecimal(salary);
                       adp.InsertCommand.Parameters.Add("@off_email", SqlDbType.VarChar).Value = txtOffEmail.Text;
                       adp.InsertCommand.Parameters.Add("@off_mob", SqlDbType.VarChar).Value = txtOffMob.Text;
                       adp.InsertCommand.Parameters.Add("@off_tnt", SqlDbType.VarChar).Value = txtOffTnt.Text;
                       adp.InsertCommand.Parameters.Add("@job_loc", SqlDbType.VarChar).Value = txtJobLoc.Text;
                       con.Open();
                       int c = adp.InsertCommand.ExecuteNonQuery();
                       con.Close();
                       ClearTextBox(this);
                       if (c > 0)
                       {
                           MessageBox.Show("Insertion Successful");
                           lblOfficialNext.Visible = true;
                       }

                   }
                   catch (Exception ex)
                   {
                       throw new Exception("Problems adding object" + MessageBox.Show(ex.ToString()), ex);
                   }
               }
           }

       }
        /***************************************************************************************************************/

        //when next on official information tabpage is clicked, the personal information tabpage is shown.
       private void lblOfficialNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           tabControl1.SelectedTab = tabPersonal;
       }
       /****************************************************************************************************************/

        //saves employee's personal information
       private void btnSavePresent_Click(object sender, EventArgs e)
       {
           //lblPersonalSkip.Visible = true;
           try
           {
               SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

               //query for present information
               SqlDataAdapter adp = new SqlDataAdapter();

               adp.InsertCommand = new SqlCommand("INSERT INTO present_info VALUES (@mail_add, @phone, @mobile, @email, @fax, @emp_ID)", con);
               adp.InsertCommand.Parameters.Add("@mail_add", SqlDbType.VarChar).Value = mail_add1.Text;
               adp.InsertCommand.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone1.Text;
               adp.InsertCommand.Parameters.Add("@mobile", SqlDbType.VarChar).Value = mobile1.Text;
               adp.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email1.Text;
               adp.InsertCommand.Parameters.Add("@fax", SqlDbType.VarChar).Value = fax1.Text;
               adp.InsertCommand.Parameters.Add("@emp_ID", SqlDbType.Int).Value = Convert.ToInt32(empID);
               con.Open();
               int c = adp.InsertCommand.ExecuteNonQuery();
               con.Close();

               //query for permanent information
               SqlConnection con1 = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
               SqlDataAdapter adp1 = new SqlDataAdapter();
               adp1.InsertCommand = new SqlCommand("INSERT INTO permanent_info VALUES (@mail_add, @phone, @mobile, @email, @fax, @emp_ID)", con1);
               adp1.InsertCommand.Parameters.Add("@mail_add", SqlDbType.VarChar).Value = mail_add2.Text;
               adp1.InsertCommand.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone2.Text;
               adp1.InsertCommand.Parameters.Add("@mobile", SqlDbType.VarChar).Value = mobile2.Text;
               adp1.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email2.Text;
               adp1.InsertCommand.Parameters.Add("@fax", SqlDbType.VarChar).Value = fax2.Text;
               adp1.InsertCommand.Parameters.Add("@emp_ID", SqlDbType.Int).Value = Convert.ToInt32(empID);
               con1.Open();
               int c1 = adp1.InsertCommand.ExecuteNonQuery();
               con1.Close();

               ClearTextBox(this);
               if (c > 0 || c1 > 0)
               {
                   MessageBox.Show("Insertion Successful");
                   lblPersonalNext.Visible = true;
                   lblPersonalSkip.Visible = false;
               }
           }
           catch (Exception ex)
           {
               throw new Exception("Problems adding object" + MessageBox.Show(ex.ToString()), ex);
           }
                  

       }
       /****************************************************************************************************************/


       //when next on personal information tabpage is clicked, the emergency contact information tabpage is shown.
       private void lblPersonalNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           tabControl1.SelectedTab = tabEmergency;
       }
       /****************************************************************************************************************/

        //saves emergency contact information
       private void btnSaveEmergency_Click(object sender, EventArgs e)
       {
           //lblEmergencySkip.Visible = true;
           try
           {
               SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
               SqlDataAdapter adp = new SqlDataAdapter();

               adp.InsertCommand = new SqlCommand("INSERT INTO emergency_contact VALUES (@name, @relationship, @mail_add, @phn, @email, @emp_ID)", con);
               adp.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name.Text;
               adp.InsertCommand.Parameters.Add("@relationship", SqlDbType.VarChar).Value = rltnship.Text;
               adp.InsertCommand.Parameters.Add("@mail_add", SqlDbType.VarChar).Value = mail_add.Text;
               adp.InsertCommand.Parameters.Add("@phn", SqlDbType.VarChar).Value = phn.Text;
               adp.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email.Text;
               adp.InsertCommand.Parameters.Add("@emp_ID", SqlDbType.Int).Value = Convert.ToInt32(empID);
               con.Open();
               int c = adp.InsertCommand.ExecuteNonQuery();
               con.Close();
               ClearTextBox(this);
               if (c > 0)
               {
                   MessageBox.Show("Insertion Successful");
                   lblEmergencySkip.Visible = false;
                   lblEmergencyNext.Visible = true;
               }
           }
           catch (Exception ex)
           {
               throw new Exception("Problems adding object" + MessageBox.Show(ex.ToString()), ex);
           }
       }
       /****************************************************************************************************************/

        //skip label for going from personal information to emergency contact information
       private void lblPersonalSkip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           tabControl1.SelectedTab = tabEmergency;
       }
       /****************************************************************************************************************/

        //when next on emergency contact information tabpage is clicked, the passport & bank information tabpage is shown.
       private void lblEmergencyNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           tabControl1.SelectedTab = tabBank;
       }
       /****************************************************************************************************************/

       private void lblEmergencySkip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           tabControl1.SelectedTab = tabBank;
       }
       /****************************************************************************************************************/

        //browse password image button
       private void btnBrowsePassImg_Click(object sender, EventArgs e)
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
       /****************************************************************************************************************/

        //resize passport image
       private void btnResizePassImg_Click(object sender, EventArgs e)
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
       /****************************************************************************************************************/

        //browse NID photo
       private void btnBrowseIDImg_Click(object sender, EventArgs e)
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
       /****************************************************************************************************************/

        //resize NID image
       private void btnResizeIDImg_Click(object sender, EventArgs e)
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
       /****************************************************************************************************************/

        //save button- passport & bank information
       private void btnSaveBank_Click(object sender, EventArgs e)
       {
           string location1, location2;
           //lblBankSkip.Visible = true;
           //save  passport image in Pass_image folder
           if (pictureBox2.Image != null)
           {
               string imagepath = pictureBox2.ImageLocation.ToString();
               string picname = "pass" + empID; //imagepath.Substring(imagepath.LastIndexOf('\\'));
               string path = @"C:\Users\Administrator\Documents\Visual Studio 2010\Projects\InfoBase\InfoBase";  //Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));
               Bitmap imgImage = new Bitmap(pictureBox2.Image);    //Create an object of Bitmap class/
               imgImage.Save(path + "\\pass_image\\" + picname + ".jpg");
               location1 = path + "\\pass_image\\" + picname +".jpg";
           }
           else
               location1 = "";
              
               //save NID image in NID_image folder
           if (pictureBox3.Image != null)
           {
               string imagepath1 = pictureBox3.ImageLocation.ToString();
               string picname1 = "NID" + empID; //imagepath1.Substring(imagepath1.LastIndexOf('\\'));
               string path1 = @"C:\Users\Administrator\Documents\Visual Studio 2010\Projects\InfoBase\InfoBase";  //Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));
               Bitmap imgImage1 = new Bitmap(pictureBox3.Image);    //Create an object of Bitmap class/
               imgImage1.Save(path1 + "\\NID_image\\" + picname1 + ".jpg");
               location2 = path1 + "\\NID_image\\" + picname1 + ".jpg";
           }
           else
               location2 = " ";
          


           try
           {
               SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
               SqlDataAdapter adp = new SqlDataAdapter();
               adp.InsertCommand = new SqlCommand("INSERT INTO  passBank VALUES (@empID, @passNo, @passImage, @NIDNo, @NIDImage, @TINInfo, @TINNo, @bankName, @branch, @accNo, @accType)", con);
               adp.InsertCommand.Parameters.Add("@empID", SqlDbType.Int).Value = Convert.ToInt32(empID);
               adp.InsertCommand.Parameters.Add("@passNo", SqlDbType.VarChar).Value = pass_no.Text;
               adp.InsertCommand.Parameters.Add("@passImage", SqlDbType.VarChar).Value = location1;
               adp.InsertCommand.Parameters.Add("@NIDNo", SqlDbType.VarChar).Value = NID_no.Text;
               adp.InsertCommand.Parameters.Add("@NIDImage", SqlDbType.VarChar).Value = location2;
               adp.InsertCommand.Parameters.Add("@TINInfo", SqlDbType.VarChar).Value = TIN_info.Text;
               adp.InsertCommand.Parameters.Add("@TINNo", SqlDbType.VarChar).Value = TIN.Text;
               adp.InsertCommand.Parameters.Add("@bankName", SqlDbType.VarChar).Value = bank_name.Text;
               adp.InsertCommand.Parameters.Add("@branch", SqlDbType.VarChar).Value = branch.Text;
               adp.InsertCommand.Parameters.Add("@accNo", SqlDbType.VarChar).Value = acc_no.Text;
               adp.InsertCommand.Parameters.Add("@accType", SqlDbType.VarChar).Value = acc_type.Text;

               pictureBox2.Image = null;
               pictureBox3.Image = null;

               con.Open();
               int c = adp.InsertCommand.ExecuteNonQuery();
               con.Close();
               ClearTextBox(this);
               if (c > 0)
               {
                   MessageBox.Show("Insertion Successful");
                   lblBankSkip.Visible = false;
                   lblBankNext.Visible = true;
               }
           }
           catch (Exception ex)
           {
               throw new Exception("Problems adding object" + MessageBox.Show(ex.ToString()), ex);
           }
       } 
        /****************************************************************************************************************/

       //skip label for going from passport and bank information to other information tab
       private void lblBankSkip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           tabControl1.SelectedTab = tabOther;
       }

       //when next on passport and bank information tabpage is clicked, the other information tabpage is shown.
       private void lblBankNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           tabControl1.SelectedTab = tabOther;
       }
       /****************************************************************************************************************/

        //save button- other information
       private void btnSaveOther_Click(object sender, EventArgs e)
       {
           string location;
           //save image
           if (pictureBox1.Image != null)
           {
               string imagepath = pictureBox1.ImageLocation.ToString();
               string picname = empID;                                                                                 //imagepath.Substring(imagepath.LastIndexOf('\\'));
               string path = @"C:\Users\Administrator\Documents\Visual Studio 2010\Projects\InfoBase\InfoBase";        //Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("bin"));
               Bitmap imgImage = new Bitmap(pictureBox1.Image);    //Create an object of Bitmap class/
               imgImage.Save(path + "\\Image\\" + picname + ".jpg");
               location = path + "\\Image\\" + picname + ".jpg";
           }
           else
               location = "";


           try
           {
               SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

               SqlDataAdapter adp = new SqlDataAdapter();
               //StringBuilder input = new StringBuilder();
               adp.InsertCommand = new SqlCommand("INSERT INTO othersInfo VALUES (@empID, @gender, @maritalStatus, @spouseName, @mailAdd, @contactNo, @birthDate, @childrenNo, @bloodGroup, @birthPlace, @birthNationality, @religion, @expYear, @height, @weight, @presentNationality, @serviceLength, @photo, @age)", con);
               adp.InsertCommand.Parameters.Add("@empID", SqlDbType.Int).Value = Convert.ToInt32(empID);
               if (checkBox1.Checked)
                   adp.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = checkBox1.Text.ToString();
               else if (checkBox2.Checked)
                   adp.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = checkBox2.Text.ToString();
               else
                   adp.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = DBNull.Value;
               if (checkBox3.Checked)
                   adp.InsertCommand.Parameters.Add("@maritalStatus", SqlDbType.VarChar).Value = checkBox3.Text.ToString();
               else if (checkBox4.Checked)
                   adp.InsertCommand.Parameters.Add("@maritalStatus", SqlDbType.VarChar).Value = checkBox4.Text.ToString();
               else
                   adp.InsertCommand.Parameters.Add("@maritalStatus", SqlDbType.VarChar).Value = DBNull.Value;
               //input.AppendFormat("{0},", checkBox1.Text);

               adp.InsertCommand.Parameters.Add("@spouseName", SqlDbType.VarChar).Value = spouse.Text;
               adp.InsertCommand.Parameters.Add("@mailAdd", SqlDbType.VarChar).Value = mail.Text;
               adp.InsertCommand.Parameters.Add("@contactNo", SqlDbType.VarChar).Value = contact.Text;
               DateTime birthDate;
               if (DateTime.TryParseExact(birth_date.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out birthDate))
                   adp.InsertCommand.Parameters.Add("@birthDate", SqlDbType.Date).Value = birthDate;
               else
                   adp.InsertCommand.Parameters.Add("@birthDate", SqlDbType.Date).Value = DBNull.Value;
               //adp.InsertCommand.Parameters.Add("@birthDate", SqlDbType.Date).Value = birth_date.Text;
               adp.InsertCommand.Parameters.Add("@childrenNo", SqlDbType.Int).Value = children_no.Text.ToTolerantInteger();
               adp.InsertCommand.Parameters.Add("@bloodGroup", SqlDbType.VarChar).Value = blood_group.Text;
               adp.InsertCommand.Parameters.Add("@birthPlace", SqlDbType.VarChar).Value = birth_place.Text;
               adp.InsertCommand.Parameters.Add("@birthNationality", SqlDbType.VarChar).Value = birth_nationality.Text;
               adp.InsertCommand.Parameters.Add("@religion", SqlDbType.VarChar).Value = religion.Text;
               adp.InsertCommand.Parameters.Add("@expYear", SqlDbType.VarChar).Value = exp.Text;
               string height1 = height.Text;
               string weight1 = weight.Text;
               adp.InsertCommand.Parameters.Add("@height", SqlDbType.Decimal).Value = height1.ToTolerantDecimal();
               adp.InsertCommand.Parameters.Add("@weight", SqlDbType.Decimal).Value = weight1.ToTolerantDecimal();
               adp.InsertCommand.Parameters.Add("@presentNationality", SqlDbType.VarChar).Value = present_nationality.Text;
               //adp.InsertCommand.Parameters.Add("@[age(year)]", SqlDbType.Int).Value = Convert.ToInt32(age.Text);
               adp.InsertCommand.Parameters.Add("@serviceLength", SqlDbType.VarChar).Value = service.Text;
               adp.InsertCommand.Parameters.Add("@photo", SqlDbType.VarChar).Value = location;
               adp.InsertCommand.Parameters.Add("@age", SqlDbType.Int).Value = age.Text.ToTolerantInteger();

               pictureBox1.Image = null;
               con.Open();
               int c = adp.InsertCommand.ExecuteNonQuery();
               con.Close();
               ClearTextBox(this);
               if (c > 0)
                   MessageBox.Show("Insertion Successful");
           }
           catch (Exception ex)
           {
               throw new Exception("Problems adding object" + MessageBox.Show(ex.ToString()), ex);
           }

       }

        //browse button- other information image
       private void btnBrowseOtherImg_Click(object sender, EventArgs e)
       {
           OpenFileDialog openImage = new OpenFileDialog();
           DialogResult result = openImage.ShowDialog();
           if (result == DialogResult.OK)
           {
               //Load image in picture box and set size mode property of image as normal.
               pictureBox1.Load(openImage.FileName);
               pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
               //Store source location of image in textbox.
               textBox3.Text = openImage.FileName;
               //Retrive height and width of image and store in OriginalImageSize variable.
               int imgWidth = pictureBox1.Image.Width;
               int imgHeight = pictureBox1.Image.Height;
               OriginalImageSize = new Size(imgWidth, imgHeight);
               string imgSize = "Width " + imgWidth + " px  Height " + imgHeight + " px";
               before_resize.Text = imgSize;
           }
       }

        //resize button- other information image
       private void btnResizeOtherImg_Click(object sender, EventArgs e)
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
       /****************************************************************************************************************/

       //save button- for insert salary information
       private void btnSaveSalInsert_Click(object sender, EventArgs e)
       {
           string empID1 = txtSalEmpId.Text;
           string empName = txtSalEmpName.Text;
           string gross = txtGrossSalary.Text;
           string date = txtProcessDate.Text;
           //without inserting empID1, empName, gross and date the user can not proceed further

           if (empID1 == "" || empName == "" || gross == "" || date == "")
           {
               lblAlert.Text = "Please fill up the * marked fields.";
               lblAlert.Visible = true;
           }

           else
           {
               try
               {
                   SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
                   SqlDataAdapter adp = new SqlDataAdapter();
                   adp.InsertCommand = new SqlCommand("INSERT INTO salary_2 VALUES (@empID, @empName, @grossSalary, @basicSalary, @houseRent, @conveyance, @medical, @lateDeduct, @otherDeduct, @taxDeduct, @bonus, @bonusAmount,@loanAmount, @loanDeduct, @advanceSalary, @salaryPayable,  @processingDate, @remarks )", con);
                   adp.InsertCommand.Parameters.Add("@empID", SqlDbType.Int).Value = Convert.ToInt32(empID1);
                   adp.InsertCommand.Parameters.Add("@empName", SqlDbType.VarChar).Value = empName;
                   adp.InsertCommand.Parameters.Add("@grossSalary", SqlDbType.Decimal).Value = Convert.ToDecimal(gross);
                   
                   decimal grossSalary = Convert.ToDecimal(gross);
                   decimal basicSalary = grossSalary * 0.60M; //basic salary is 60% of gross
                   decimal houseRent = grossSalary * .20M;   //house rent is 20% of gross
                   decimal conveyance = grossSalary * .10M;  //conveyance is 10% of gross
                   decimal medical = grossSalary * .10M;     //medical is 10% of gross*/
                   adp.InsertCommand.Parameters.Add("@basicSalary", SqlDbType.VarChar).Value = basicSalary;
                   adp.InsertCommand.Parameters.Add("@houseRent", SqlDbType.VarChar).Value = houseRent;
                   adp.InsertCommand.Parameters.Add("@conveyance", SqlDbType.VarChar).Value = conveyance;
                   adp.InsertCommand.Parameters.Add("@medical", SqlDbType.VarChar).Value = medical;
                   
                   string late = txtLateDeduct.Text;
                   string other = txtOtherDeduct.Text;
                   string tax = txtTaxDeduct.Text;
                   decimal lateDeduct, otherDeduct, taxDeduct;
                   adp.InsertCommand.Parameters.Add("@lateDeduct", SqlDbType.VarChar).Value = late.ToTolerantDecimal(); 
                   lateDeduct = late.ToTolerantDecimal(); 
                   adp.InsertCommand.Parameters.Add("@otherDeduct", SqlDbType.VarChar).Value = other.ToTolerantDecimal();
                   otherDeduct = other.ToTolerantDecimal(); 
                   adp.InsertCommand.Parameters.Add("@taxDeduct", SqlDbType.VarChar).Value = tax.ToTolerantDecimal(); 
                   taxDeduct = tax.ToTolerantDecimal(); 
                   decimal bonus = 0;
                   if (chkBasicBonus.Checked)
                   {
                       adp.InsertCommand.Parameters.Add("@bonus", SqlDbType.VarChar).Value = chkBasicBonus.Text.ToString();
                       adp.InsertCommand.Parameters.Add("@bonusAmount", SqlDbType.VarChar).Value = basicSalary;
                       bonus = basicSalary;
                   }
                   else if (chkGrossBonus.Checked)
                   {

                       adp.InsertCommand.Parameters.Add("@bonus", SqlDbType.VarChar).Value = chkGrossBonus.Text.ToString();
                       adp.InsertCommand.Parameters.Add("@bonusAmount", SqlDbType.VarChar).Value = grossSalary;
                       bonus = grossSalary;
                   }
                   else
                   {
                       string b = "no bonus";
                       adp.InsertCommand.Parameters.Add("@bonus", SqlDbType.VarChar).Value = b;
                       adp.InsertCommand.Parameters.Add("@bonusAmount", SqlDbType.VarChar).Value = 0;
                       bonus = 0.0M;
                   }


                   decimal loanAmount, loanDeduct, advanceSalary, newLoanAmount;
                   string loan = txtLoanAmount.Text;
                   loanAmount = loan.ToTolerantDecimal(); 
                   //adp.InsertCommand.Parameters.Add("@loanAmount", SqlDbType.VarChar).Value = loan.ToTolerantDecimal(); 

                   string lDeduct = txtLoanDeduct.Text;
                   loanDeduct = lDeduct.ToTolerantDecimal();
                   //updates loan amount when money is deducted from salary
                   newLoanAmount = loanAmount - loanDeduct;
                   if (lDeduct=="")
                       adp.InsertCommand.Parameters.Add("@loanAmount", SqlDbType.VarChar).Value = loan.ToTolerantDecimal();
                   else
                       adp.InsertCommand.Parameters.Add("@loanAmount", SqlDbType.VarChar).Value = newLoanAmount; 
                   adp.InsertCommand.Parameters.Add("@loanDeduct", SqlDbType.VarChar).Value = lDeduct.ToTolerantDecimal();  
                  // newLoanAmount = loanAmount - loanDeduct;
                  
                   string advance = txtAdvance.Text;
                   adp.InsertCommand.Parameters.Add("@advanceSalary", SqlDbType.VarChar).Value = advance.ToTolerantDecimal(); 
                   advanceSalary = advance.ToTolerantDecimal(); 
                   // }

                   decimal salaryPayable = grossSalary - lateDeduct - otherDeduct - taxDeduct - loanDeduct - advanceSalary;
                   adp.InsertCommand.Parameters.Add("@salaryPayable", SqlDbType.VarChar).Value = salaryPayable;
                   DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                   adp.InsertCommand.Parameters.Add("@processingDate", SqlDbType.VarChar).Value = processDate;
                   adp.InsertCommand.Parameters.Add("@remarks", SqlDbType.VarChar).Value = txtRemark.Text; 
                   con.Open();
                   int i = adp.InsertCommand.ExecuteNonQuery();
                   con.Close();
                   

                   //ClearTextBox(this);
                   if (i > 0)
                       MessageBox.Show("Insertion Successful");
                   //ClearTextBox(this);
               }
               catch (Exception ex)
               {
                   throw new Exception("Problems adding object" + MessageBox.Show(ex.ToString()), ex);
               }
           }
       }
       /****************************************************************************************************************/

        

      
        //button for viewing employee profile separately
       private void btnViewProfile_Click(object sender, EventArgs e)
       {
           viewProfile vp = new viewProfile();
           vp.txtViewEmpId.Text = this.dataGridProfile.CurrentRow.Cells[0].Value.ToString();
           int s = int.Parse(this.dataGridProfile.CurrentRow.Cells[0].Value.ToString());
           
           SqlConnection Conn = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
           // view official Information
           SqlCommand Comm1 = new SqlCommand("SELECT emp_name, dept, designation, join_date, leaving_date, emp_state, rep_auth, salary, off_email, off_mob, off_tnt,job_loc FROM official WHERE emp_ID='" + s + "'", Conn);
           Conn.Open();
           SqlDataReader DR1 = Comm1.ExecuteReader();
           if (DR1.Read())
           {

               
               vp.txtViewEmpName.Text = DR1.GetValue(0).ToString();
               vp.txtViewDept.Text = DR1.GetValue(1).ToString();
               vp.txtViewDesignation.Text = DR1.GetValue(2).ToString();
               vp.txtViewJoin.Text = DR1.GetValue(3).ToString();
               vp.txtViewLeave.Text = DR1.GetValue(4).ToString();
               vp.txtViewEmpStat.Text = DR1.GetValue(5).ToString();
               vp.txtViewRepAuth.Text = DR1.GetValue(6).ToString();
               vp.txtviewSalary.Text = DR1.GetValue(7).ToString();
               vp.txtViewOfcEmail.Text = DR1.GetValue(8).ToString();
               vp.txtViewOfcMob.Text = DR1.GetValue(9).ToString();
               vp.txtViewTnt.Text = DR1.GetValue(10).ToString();
               vp.txtViewJobLoc.Text = DR1.GetValue(11).ToString();
           }
           Conn.Close();

           //view personal(present information)
           Conn.Open();
           SqlCommand comm2 = new SqlCommand("SELECT mail_add, phone,mobile,email,fax FROM present_info WHERE emp_ID='" + s + "'", Conn);
           SqlDataReader DR2 = comm2.ExecuteReader();
           if (DR2.Read())
           {
               vp.txtViewMailAdd.Text = DR2.GetValue(0).ToString();
               vp.txtViewPhn.Text = DR2.GetValue(1).ToString();
               vp.txtViewMob.Text = DR2.GetValue(2).ToString();
               vp.txtViewEmail.Text = DR2.GetValue(3).ToString();
               vp.txtViewFax.Text = DR2.GetValue(4).ToString();
           }
           Conn.Close();

           //view personal(permanent information)
           Conn.Open();
           SqlCommand comm3 = new SqlCommand("SELECT mail_add, phone,mobile,email,fax FROM permanent_info WHERE emp_ID='" + s + "'", Conn);
           SqlDataReader DR3 = comm3.ExecuteReader();
           if (DR3.Read())
           {
               vp.txtViewMailAddP.Text = DR3.GetValue(0).ToString();
               vp.txtViewPhnP.Text = DR3.GetValue(1).ToString();
               vp.txtViewMobP.Text = DR3.GetValue(2).ToString();
               vp.txtViewEmailP.Text = DR3.GetValue(3).ToString();
               vp.txtViewFaxP.Text = DR3.GetValue(4).ToString();
           }
           Conn.Close();

           //view emergency contact information
           Conn.Open();
           SqlCommand comm4 = new SqlCommand("SELECT  name, relationship, mail_add, phn, email  FROM emergency_contact WHERE emp_ID='" + s + "'", Conn);

           SqlDataReader DR4 = comm4.ExecuteReader();
           if (DR4.Read())
           {

               vp.txtViewEmgName.Text = DR4.GetValue(0).ToString();
               vp.txtViewEmgRltn.Text = DR4.GetValue(1).ToString();
               vp.txtViewEmgMailAdd.Text = DR4.GetValue(2).ToString();
               vp.txtViewEmgPhn.Text = DR4.GetValue(3).ToString();
               vp.txtViewEmgEmail.Text = DR4.GetValue(4).ToString();
           }

           Conn.Close();


           //view bank and passport information
           Conn.Open();
           SqlCommand comm5 = new SqlCommand("SELECT passNo, passImage, NIDNo, NIDImage, TINInfo, TINNo, bankName, branch, accNo, accType FROM passBank WHERE empID='" + s + "'", Conn);
           SqlDataReader DR5 = comm5.ExecuteReader();
           if (DR5.Read())
           {
               vp.txtViewPassNo.Text = DR5.GetValue(0).ToString();
               string passPic = DR5.GetValue(1).ToString();
               vp.txtViewNIDNo.Text = DR5.GetValue(2).ToString();
               string NIDPic = DR5.GetValue(3).ToString();
               vp.txtViewTINNo.Text = DR5.GetValue(4).ToString();
               vp.txtViewTINInfo.Text = DR5.GetValue(5).ToString();
               vp.txtViewBankName.Text = DR5.GetValue(6).ToString();
               vp.txtViewBranchName.Text = DR5.GetValue(7).ToString();
               vp.txtViewAccNo.Text = DR5.GetValue(8).ToString();
               vp.txtViewAccType.Text = DR5.GetValue(9).ToString();

               if (passPic != "")
               {
                   vp.pictureBox1.ImageLocation = passPic;
                   vp.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
               }
               else
               {

                   vp.pictureBox1.Image = null;
               }

               if (NIDPic != "")
               {
                   vp.pictureBox3.ImageLocation = NIDPic;
                   vp.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
               }
               else
               {

                   vp.pictureBox3.Image = null;
               }

             
           }
           Conn.Close();

           //view other information
           Conn.Open();
           SqlCommand comm6 = new SqlCommand("SELECT gender, maritalStatus, spouseName, mailAdd, contactNo, birthDate, childrenNo, bloodGroup, birthPlace, birthNationality, religion, expYear, height, weight, presentNationality, serviceLength,Photo, age FROM othersInfo WHERE empID='" + s + "'", Conn);
           SqlDataReader DR6 = comm6.ExecuteReader();
           if (DR6.Read())
           {
               vp.txtViewOtherGender.Text = DR6.GetValue(0).ToString();
               vp.txtViewOtherMar.Text = DR6.GetValue(1).ToString();
               vp.txtViewOtherSpouse.Text = DR6.GetValue(2).ToString();
               vp.txtViewOtherMail.Text = DR6.GetValue(3).ToString();
               vp.txtViewOtherContact.Text = DR6.GetValue(4).ToString();
               vp.txtViewOtherBirthDate.Text = DR6.GetValue(5).ToString();
               vp.txtViewOtherChildNo.Text = DR6.GetValue(6).ToString();
               vp.txtViewOtherBlood.Text = DR6.GetValue(7).ToString();
               vp.txtViewOtherBirthPlace.Text = DR6.GetValue(8).ToString();
               vp.txtViewOtherNationality.Text = DR6.GetValue(9).ToString();
               vp.txtViewOtherReligion.Text = DR6.GetValue(10).ToString();
               vp.txtViewOtherExp.Text = DR6.GetValue(11).ToString();
               vp.txtViewOtherHeight.Text = DR6.GetValue(12).ToString();
               vp.txtViewOtherWeight.Text = DR6.GetValue(13).ToString();
               vp.txtViewOtherPreNationality.Text = DR6.GetValue(14).ToString();
               vp.txtViewOtherService.Text = DR6.GetValue(15).ToString();
               string pic = DR6.GetValue(16).ToString();
               //MessageBox.Show (pic);

               if (pic != "")
               {
                   vp.picViewProfile.ImageLocation = pic;
                   vp.picViewProfile.SizeMode = PictureBoxSizeMode.StretchImage;
               }
               else
               {
                   
                   vp.picViewProfile.Image = null;
               }
               vp.txtViewOtherAge.Text = DR6.GetValue(17).ToString();
           }
           
           vp.Show();
       }
        /****************************************************************************************************************/

       //check if user ID is unique
       public bool ExecuteReader(string user_txt)
       {
           SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
           SqlDataAdapter adp = new SqlDataAdapter();
           con.Open();
           SqlCommand cmd = new SqlCommand("select * from official where emp_ID = @emp_ID", con);
           SqlParameter param = new SqlParameter();
           param.ParameterName = "@emp_ID";
           param.Value = user_txt;
           cmd.Parameters.Add(param);
           SqlDataReader reader = cmd.ExecuteReader();
           if (reader.HasRows)
               return true;
           else
               return false;
       }

       public bool CheckUsername(string user_txt)
       {
           bool Result;
           Result = ExecuteReader(user_txt);
           return Result;
       }

      
       private void txtEmpID_TextChanged(object sender, EventArgs e)
       {
           if (!string.IsNullOrEmpty(txtEmpID.Text))
           {
               if (CheckUsername(txtEmpID.Text.Trim()))
               {
                   //MessageBox.Show( "UserName Already Taken");
                   lblChkID.Visible = true;
                   lblChkID.Text = "Data already Exists";
                  
                   count++;
               }
               else
               {

                   lblChkID.Visible = false;
                   
                   count = 0;

               }
           }
       }
       /******************************************************************************/

       //loads table in datagrid view
       private void btnLoadTable_Click(object sender, EventArgs e)
       {

           DataTable dt = new DataTable();
           DataSet ds = new DataSet();
           SqlConnection cs = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

           SqlDataAdapter da = new SqlDataAdapter();

           BindingSource bsource = new BindingSource();
           cs.Open();
           da.SelectCommand = new SqlCommand("SELECT emp_ID,emp_name, designation from official; ", cs);
           ds.Clear();
           da.Fill(ds);
           dataGridProfile.DataSource = ds.Tables[0];
           //dataGrid.DataBind();
           btnViewProfile.Visible = true;
           cs.Close();
       }
       /******************************************************************************/


        //displays salary report according to selected month and year
       private void btnSalRep_Click(object sender, EventArgs e)
       {
           DataTable dt1 = new DataTable();
           DataSet ds1 = new DataSet();
           SqlConnection cs1 = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

           SqlDataAdapter da1 = new SqlDataAdapter();

           BindingSource bsource1 = new BindingSource();
           cs1.Open();
           int m = cmbMonth.SelectedIndex+1;
           int y = Convert.ToInt32(cmbYear.Text);
           da1.SelectCommand = new SqlCommand("SELECT * FROM salary_2 WHERE datepart(mm,processingDate)=" + m + "AND datepart(yyyy,processingDate)=" + y+ ";", cs1);
           ds1.Clear();
           da1.Fill(ds1);
           dataGridViewSalary.DataSource = ds1.Tables[0];
           //SqlDataAdapter da1= new SqlDataAdapter;
           int sum = 0;
           for (int i = 0; i < dataGridViewSalary.Rows.Count; ++i)
           {
               sum += Convert.ToInt32(dataGridViewSalary.Rows[i].Cells[15].Value);
           }
           lblNetTotal.Text = sum.ToString();
           //cs.Close();

       }
       /******************************************************************************/

       private void tabSalRep_Click(object sender, EventArgs e)
       {
           
           
       }

       private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
       {
       }


        //check if data already exists in salary table
       public bool ExecuteReader1(string txtUser)
       {
           SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
           SqlDataAdapter adp = new SqlDataAdapter();
           con.Open();
           SqlCommand cmd = new SqlCommand("select * from salary_2 where empID = @empID", con);
           SqlParameter param = new SqlParameter();
           param.ParameterName = "@empID";
           param.Value = txtUser;
           cmd.Parameters.Add(param);
           SqlDataReader reader = cmd.ExecuteReader();
           if (reader.HasRows)
               return true;
           else
               return false;
       }

       public bool CheckUsername1(string txtUser)
       {
           bool Result;
           Result = ExecuteReader1(txtUser);
           return Result;
       }

      
       

       private void txtSalEmpId_TextChanged(object sender, EventArgs e)
       {
           if (!string.IsNullOrEmpty(txtSalEmpId.Text))
           {
               if (CheckUsername1(txtSalEmpId.Text.Trim()))
               {
                   //MessageBox.Show( "UserName Already Taken");
                    counter++;
               }
               else
               {
                   counter = 0;

               }
           }

           //when data exists in salary table, open the edit mode
           if (counter > 0)
           {
              
              
               try
               {
                   int id = Convert.ToInt32(txtSalEmpId.Text);
                   SqlConnection conn = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
                   DataTable td = new DataTable();
                   SqlDataAdapter da = new SqlDataAdapter("select  empName, grossSalary, loanAmount from salary_2 where empID ='" + id + "'", conn);
                   conn.Open();
                   da.Fill(td);
                   conn.Close();
                   txtSalEmpName.Text = td.Rows[0].ItemArray[0].ToString();
                   txtGrossSalary.Text = td.Rows[0].ItemArray[1].ToString();
                   txtLoanAmount.Text = td.Rows[0].ItemArray[2].ToString();
                   conn.Close();
               }
               catch (Exception ex)
               {
                   throw new Exception("Problem adding object."+ MessageBox.Show(ex.ToString()), ex);
               }

           }
       }

       
        /******************************************************************************/

       
    }

   

    //class for puting 0 into textboxes (decimal) , which are left empty
    public static class StringExtensions
    {
        public static decimal ToTolerantDecimal(this string @this)
        {
            return string.IsNullOrEmpty(@this) ? 0.0m : decimal.Parse(@this);
        }

        public static int ToTolerantInteger(this string @this)
        {
            return string.IsNullOrEmpty(@this) ? 0 : int.Parse(@this);
        }
    }

   
}


