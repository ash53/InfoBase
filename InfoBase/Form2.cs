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
using System.Drawing.Drawing2D;

namespace InfoBase
{
    public partial class Form2 : Form
    {

        static int counter=0;   //variable to count if user name is taken or not
        
        //rounded corner of form
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
        /******************************************************************************/

        // for changing the text color using placeholder
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32
        SendMessage(
        IntPtr hwnd,
        int msg,
        int wParam,
        [MarshalAs(UnmanagedType.LPWStr)] string lparam);
          private const int EM_SETCUBEBANNER = 0x1501;
          /******************************************************************************/

        //this form is for registration purpose
        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); // for rounded corner
              
           SendMessage(user_name.Handle, EM_SETCUBEBANNER, 0, "User Name");
            SendMessage(password.Handle, EM_SETCUBEBANNER, 0, "Password");
            SendMessage(email.Handle, EM_SETCUBEBANNER, 0, "Email");
            
        }

        /******************************************************************************/  

        

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

        /******************************************************************************/
      

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)  //close button
        {
            this.Close();
        }
        /******************************************************************************/

        //check if user name is unique
        public bool ExecuteReader(string user_txt)
        {
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
            SqlDataAdapter adp = new SqlDataAdapter();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from registration where user_name = @user_name", con);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@user_name";
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
            Result =ExecuteReader(user_txt);
            return Result;
        }

        protected void user_name_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(user_name.Text))
            {
                if (CheckUsername(user_name.Text.Trim()))
                {
                    //MessageBox.Show( "UserName Already Taken");
                    //lblChkUserName.Text = "User name already taken.";
                    lblInvalid.Visible = true;
                    lblChkUserName.Visible = false;
                    counter++;
                }
                else
                {
                    // MessageBox.Show("UserName Available");
                    //lblChkUserName.Text = "User name available.";

                    lblChkUserName.Visible = true;
                    lblInvalid.Visible = false;
                    counter = 0;

                }
            }
        }
        /******************************************************************************/

        //function for clearing the textboxes after the submit button is clicked
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

        /******************************************************************************/
        
        //function for submit button's click event
        private void submit_Click_1(object sender, EventArgs e)
        {
            if (counter > 0)
            {
                MessageBox.Show("Invalid User Name! Registration incomplete.");
                ClearTextBox(this);
                lblChkUserName.Visible = false;
                lblInvalid.Visible = false;
            }
            else
            {
                SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");

                SqlDataAdapter adp = new SqlDataAdapter();
                adp.InsertCommand = new SqlCommand("INSERT INTO registration VALUES (@user_name, @password, @email)", con);
                adp.InsertCommand.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user_name.Text;
                adp.InsertCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = password.Text;
                adp.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email.Text;

                con.Open();
                int i;

                i = adp.InsertCommand.ExecuteNonQuery();
                con.Close();
                ClearTextBox(this);
                lblChkUserName.Visible = false;
                lblInvalid.Visible = false;

                if (i > 0)
                    MessageBox.Show("Registration Complete");
                counter = 0;
            }
          
        }
        /******************************************************************************************************************************/
     
    }

    
}
