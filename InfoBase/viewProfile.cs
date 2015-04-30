using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoBase
{
    public partial class viewProfile : Form
    {

        public String navId;
        public viewProfile()
        {
            InitializeComponent();
            //removes the header of the tab control
            tabControlView.Appearance = TabAppearance.FlatButtons;
            tabControlView.ItemSize = new Size(0, 1);
            tabControlView.SizeMode = TabSizeMode.Fixed;
            /**************************************************/
        }

        private void btnViewOfcInfo_Click(object sender, EventArgs e)
        {
            tabControlView.SelectedTab = tabViewOfcInfo;
        }

        private void btnViewEmgInfo_Click(object sender, EventArgs e)
        {
            tabControlView.SelectedTab = tabViewEmgInfo;
        }

      private void btnViewOtherInfo_Click(object sender, EventArgs e)
        {
            tabControlView.SelectedTab = tabViewOtherInfo;
        }

        private void btnViewBankInfo_Click_1(object sender, EventArgs e)
        {
            tabControlView.SelectedTab = tabViewBankInfo;
        }

        private void viewProfile_Load(object sender, EventArgs e)
        {
        }

       

        //save button in view profile form
        private void btnViewProfileSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Server = (local); DataBase=employee_info; Integrated Security=SSPI");
            con.Open();
            int i = int.Parse(txtViewEmpId.Text);

            //update official information
            string query = "UPDATE [official] SET [dept]=@dept,[designation]=@designation,[join_date]=@join_date,[leaving_date]=@leaving_date,[emp_state]=@emp_state,[rep_auth]=@rep_auth, [salary]=@salary, [off_email]=@off_email, [off_mob]= @off_mob, [off_tnt]=@off_tnt,[job_loc]=@job_loc WHERE emp_ID='" + i + "'";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@dept",  txtViewDept.Text);
            cmd.Parameters.AddWithValue("@designation",  txtViewDesignation.Text);
            //DateTime joinDate= DateTime.ParseExact(txtViewJoin.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture);
           // if (DateTime.TryParseExact(txtViewJoin.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out joiningDate))
            cmd.Parameters.AddWithValue("@join_date", txtViewJoin.Text);  //joinDate);
            //else
               // cmd.Parameters.AddWithValue("@join_date", SqlDbType.Date).Value = DBNull.Value;
            DateTime leaveDate;
            if (DateTime.TryParseExact(txtViewLeave.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out leaveDate))
            cmd.Parameters.AddWithValue("@leaving_date",leaveDate);
            else
                cmd.Parameters.AddWithValue("@leaving_date",  DBNull.Value);

            cmd.Parameters.AddWithValue("@emp_state", txtViewEmpStat.Text);
            cmd.Parameters.AddWithValue("@rep_auth",  txtViewRepAuth.Text);
            cmd.Parameters.AddWithValue("@salary", txtviewSalary.Text.ToTolerantDecimal());
            cmd.Parameters.AddWithValue("@off_email", txtViewOfcEmail.Text);
            cmd.Parameters.AddWithValue("@off_mob", txtViewOfcMob.Text);
            cmd.Parameters.AddWithValue("@off_tnt", txtViewTnt.Text);
            cmd.Parameters.AddWithValue("@job_loc",txtViewJobLoc.Text);


            //update present information
            string q = "UPDATE [present_info] SET [mail_add]=@mail_add, [phone]=@phone,[mobile]=@mobile,[email]= @email,[fax]=@fax WHERE emp_ID='" + i + "'";

            SqlCommand cmdd = new SqlCommand(q, con);

            cmdd.Parameters.AddWithValue("@mail_add", txtViewMailAdd.Text);
            cmdd.Parameters.AddWithValue("@phone", txtViewPhn.Text);
            cmdd.Parameters.AddWithValue("@mobile", txtViewMob.Text);
            cmdd.Parameters.AddWithValue("@email", txtViewEmail.Text);
            cmdd.Parameters.AddWithValue("@fax ", txtViewFax.Text);



            //update permanent information
            string qu = "UPDATE [permanent_info] SET [mail_add]=@mail_add, [phone]=@phone,[mobile]=@mobile,[email]= @email,[fax]=@fax WHERE emp_ID='" + i + "'";

            SqlCommand cmde = new SqlCommand(qu, con);

            cmde.Parameters.AddWithValue("@mail_add", txtViewMailAddP.Text);
            cmde.Parameters.AddWithValue("@phone", txtViewPhnP.Text);
            cmde.Parameters.AddWithValue("@mobile", txtViewMobP.Text);
            cmde.Parameters.AddWithValue("@email", txtViewEmailP.Text);
            cmde.Parameters.AddWithValue("@fax ", txtViewFaxP.Text);



            //update emergency contact information
            string que = "UPDATE [emergency_contact] SET [name]=@name, [relationship]=@relationship, [mail_add]=@mail_add,[phn]=@phn,[email]=@email WHERE emp_ID='" + i + "'";
            SqlCommand cmdf = new SqlCommand(que, con);

            cmdf.Parameters.AddWithValue("@name", txtViewEmgName.Text);
            cmdf.Parameters.AddWithValue("@relationship", txtViewEmgRltn.Text);
            cmdf.Parameters.AddWithValue("@mail_add", txtViewEmgMailAdd.Text);
            cmdf.Parameters.AddWithValue("@phn", txtViewEmgPhn.Text);
            cmdf.Parameters.AddWithValue("@email", txtViewEmgEmail.Text);


            //update other information
            string quf = "UPDATE [othersInfo] SET [gender]=@gender, [maritalStatus]=@maritalStatus, [spouseName]=@spouseName, [mailAdd]=@mailAdd, [contactno]=@contactNo, [birthdate]=@birthDate, [childrenNo]=@childrenNo, [bloodGroup]=@bloodGroup, [birthPlace]=@birthPlace, [birthNationality]=@birthNationality, [religion]=@religion, [expYear]=@expYear, [height]=@height, [weight]=@weight, [presentNationality]=@presentNationality, [serviceLength]=@serviceLength, [age]=@age WHERE empID='" + i + "'";
            
            SqlCommand cmdg = new SqlCommand(quf, con);

            cmdg.Parameters.AddWithValue("@gender", txtViewOtherGender.Text);
            cmdg.Parameters.AddWithValue("@maritalstatus", txtViewOtherMar.Text);
            cmdg.Parameters.AddWithValue("@spouseName", txtViewOtherSpouse.Text);
            cmdg.Parameters.AddWithValue("@mailAdd", txtViewOtherMail.Text);
            cmdg.Parameters.AddWithValue("@contactNo", txtViewOtherContact.Text);
            DateTime birthDate;
            if (DateTime.TryParseExact(txtViewOtherBirthDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out birthDate))
                cmdg.Parameters.AddWithValue("@birthDate", birthDate);
            else
                cmdg.Parameters.AddWithValue("@birthDate", DBNull.Value);
            cmdg.Parameters.AddWithValue("@childrenNo", txtViewOtherChildNo.Text.ToTolerantInteger());
            cmdg.Parameters.AddWithValue("@bloodGroup", txtViewOtherBlood.Text);
            cmdg.Parameters.AddWithValue("@birthPlace", txtViewOtherBirthPlace.Text);
            cmdg.Parameters.AddWithValue("@birthNationality", txtViewOtherNationality.Text);
            cmdg.Parameters.AddWithValue("@religion", txtViewOtherReligion.Text);
            cmdg.Parameters.AddWithValue("@expYear", txtViewOtherExp.Text);
            cmdg.Parameters.AddWithValue("@height", txtViewOtherHeight.Text.ToTolerantDecimal());
            cmdg.Parameters.AddWithValue("@weight", txtViewOtherWeight.Text.ToTolerantDecimal());
            cmdg.Parameters.AddWithValue("@presentNationality", txtViewOtherPreNationality.Text);
            cmdg.Parameters.AddWithValue("@serviceLength", txtViewOtherService.Text);
             //cmdg.Parameters.AddWithValue
            cmdg.Parameters.AddWithValue("@age", txtViewOtherAge.Text);


            //update bank and passport information
            string queryBank = "UPDATE [passBank] SET [passNo]=@passNo,  [NIDNo]=@NIDNo,  [TINInfo]=@TINInfo, [TINNo]=@TINNo, [bankName]=@bankName, [branch]=@branch, [accNo]=@accNo, [acctype]=@accType  WHERE empID='" + i + "'";
            SqlCommand cmdBank = new SqlCommand(queryBank, con);
            cmdBank.Parameters.AddWithValue("@passNo", txtViewPassNo.Text);
            cmdBank.Parameters.AddWithValue("@NIDNo", txtViewNIDNo.Text);
            cmdBank.Parameters.AddWithValue("@TINInfo", txtViewTINInfo.Text);
            cmdBank.Parameters.AddWithValue("@TINNO", txtViewTINNo.Text);
            cmdBank.Parameters.AddWithValue("@bankName", txtViewBankName.Text);
            cmdBank.Parameters.AddWithValue("@branch", txtViewBranchName.Text);
            cmdBank.Parameters.AddWithValue("@accNo", txtViewAccNo.Text);
            cmdBank.Parameters.AddWithValue("@accType", txtViewAccType.Text);
           

            try
            {
                int ofc,pre,per,emg, other, bank;
                ofc= cmd.ExecuteNonQuery();
               pre= cmdd.ExecuteNonQuery();

                per= cmde.ExecuteNonQuery();
                emg= cmdf.ExecuteNonQuery();
                other= cmdg.ExecuteNonQuery();
                bank = cmdBank.ExecuteNonQuery();
                if (ofc>0 && pre>0 && per>0 && emg>0 && other>0 && bank>0)
                    MessageBox.Show("Records are updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            con.Close();
        }

        //edit button in view profile form
        private void btnViewProfileEdit_Click(object sender, EventArgs e)
        {
            btnViewProfileSave.Visible = true;
            foreach (Control x in this.Controls)
            {
                if (x is TextBox && x != txtViewEmpId)
                {
                    ((TextBox)x).Enabled = true;
                }
            }

        }

    }
    
   

}
