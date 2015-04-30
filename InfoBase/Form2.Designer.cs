namespace InfoBase
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button3 = new System.Windows.Forms.Button();
            this.user_name = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.email = new System.Windows.Forms.TextBox();
            this.submit = new InfoBase.RoundButton();
            this.lblChkUserName = new System.Windows.Forms.Label();
            this.lblInvalid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(361, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 26);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // user_name
            // 
            this.user_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_name.Location = new System.Drawing.Point(42, 71);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(260, 24);
            this.user_name.TabIndex = 10;
            this.user_name.TextChanged += new System.EventHandler(this.user_name_TextChanged);
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Location = new System.Drawing.Point(42, 120);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(260, 24);
            this.password.TabIndex = 11;
            // 
            // email
            // 
            this.email.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.Location = new System.Drawing.Point(42, 168);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(260, 24);
            this.email.TabIndex = 12;
            // 
            // submit
            // 
            this.submit.BackColor = System.Drawing.Color.Transparent;
            this.submit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("submit.BackgroundImage")));
            this.submit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit.ForeColor = System.Drawing.SystemColors.Window;
            this.submit.Location = new System.Drawing.Point(124, 230);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(96, 35);
            this.submit.TabIndex = 13;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = false;
            this.submit.Click += new System.EventHandler(this.submit_Click_1);
            // 
            // lblChkUserName
            // 
            this.lblChkUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblChkUserName.Image = ((System.Drawing.Image)(resources.GetObject("lblChkUserName.Image")));
            this.lblChkUserName.Location = new System.Drawing.Point(332, 52);
            this.lblChkUserName.Name = "lblChkUserName";
            this.lblChkUserName.Size = new System.Drawing.Size(42, 43);
            this.lblChkUserName.TabIndex = 14;
            this.lblChkUserName.Visible = false;
            // 
            // lblInvalid
            // 
            this.lblInvalid.BackColor = System.Drawing.Color.Transparent;
            this.lblInvalid.Image = ((System.Drawing.Image)(resources.GetObject("lblInvalid.Image")));
            this.lblInvalid.Location = new System.Drawing.Point(307, 69);
            this.lblInvalid.Name = "lblInvalid";
            this.lblInvalid.Size = new System.Drawing.Size(32, 26);
            this.lblInvalid.TabIndex = 15;
            this.lblInvalid.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(401, 306);
            this.Controls.Add(this.lblInvalid);
            this.Controls.Add(this.lblChkUserName);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.email);
            this.Controls.Add(this.password);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.button3);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Opacity = 0.9D;
            this.Text = "InfoBase";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox user_name;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox email;
        private RoundButton submit;
        private System.Windows.Forms.Label lblChkUserName;
        private System.Windows.Forms.Label lblInvalid;
    }
}