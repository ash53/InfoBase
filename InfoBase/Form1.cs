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
//using System.Windows.Forms.Form.FormClosed;

namespace InfoBase
{
    public partial class Form1 : Form
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


        // for changing the text color using placeholder
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32
        SendMessage(
        IntPtr hwnd,
        int msg,
        int wParam,
        [MarshalAs(UnmanagedType.LPWStr)] string lparam);
        private const int EM_SETCUBEBANNER = 0x1501;
      

      
        //rounded corner for header label
        private void makeCircleLabel(Label lbl)
        {
            Rectangle r = new Rectangle(0, 0, lbl.Width, lbl.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 30;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            lbl.Region = new Region(gp);
        }
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); // for rounded corner
            makeCircleLabel(label1);
            makeCircleLabel(label2);

            SendMessage(textBox1.Handle, EM_SETCUBEBANNER, 0, "User Name");
            SendMessage(textBox2.Handle, EM_SETCUBEBANNER, 0, "Password");
           
            
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //minimize button
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
            this.WindowState = FormWindowState.Normal; 
            } 
            else 
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

       
        //close button
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //login area label
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 newForm = new Form2(); //registration form
            newForm.Show();
        }

        //login button
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            int count = 0;
            if (textBox1.Text != "" & textBox2.Text != "")
            {
                string queryText = "SELECT Count(*) FROM registration " +
                                   "WHERE user_name = @Username AND password = @Password";
                using (SqlConnection cn = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI"))
                using (SqlCommand cmd = new SqlCommand(queryText, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Username", textBox1.Text);  // cmd is SqlCommand 
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        //MessageBox.Show("Logged In. Welcome to InfoBase!");
                        count++;

                    }

                    else
                        MessageBox.Show("User name or password was wrong! Please try again.");
                }
            }
            ClearTextBox(this);
            if (count >= 1)
            {
                menuPage newForm = new menuPage();
                newForm.Show();
                //newForm.FormClosed += new FormClosedEventHandler(Form4_FormClosed);
               //this.Hide();

            }

 
        }

      /* private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        */
        
    }

   
    //class for round button
    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
