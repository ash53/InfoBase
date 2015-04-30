using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoBase.xUI
{
    class Accordion: Panel
    {
        private List<Menu> _menuCollection = new List<Menu>();
        //public event EventHandler Click;

        public List<Menu> Menu
        {
            get { return this._menuCollection; }
        }
       /* protected virtual void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        // Method of IPostBackEventHandler that raises change events.
        public void RaisePostBackEvent(string eventArgument)
        {
            OnClick(EventArgs.Empty);
        }*/

        public void Bind()
        {
            int panelHeight = 0;
            this._menuCollection.Reverse();
            foreach (Menu menu in this._menuCollection)
            {
                Label lblTitle = new Label();
                Panel menuPanel = new Panel();

                lblTitle.Text = menu.Title;
                lblTitle.BackColor = menu.BackgroundColor;
                lblTitle.ForeColor = menu.ForeColor;
                lblTitle.Font = menu.TextFont;
                lblTitle.TextAlign = menu.TextAlign;
                lblTitle.Dock = DockStyle.Top;
                lblTitle.Click += new EventHandler(lblTitle_Click);

                menu.Controls.Reverse();
                foreach (Control control in menu.Controls)
                {
                    control.Dock = DockStyle.Top;
                    menuPanel.Controls.Add(control);
                }

                menuPanel.Dock = DockStyle.Top;
                menuPanel.AutoSize = true;
                menuPanel.Visible = false;

                this.Controls.Add(menuPanel);
                this.Controls.Add(lblTitle);

                panelHeight += lblTitle.Height;

            }
            this.Height = panelHeight;
        }

        void lblTitle_Click(object sender, EventArgs e)
        {
            this.AutoSize = true;
            Label menu = (Label)sender;
            int index = this.Controls.GetChildIndex(menu) - 1;

            if (this.Controls[index].Visible)
            {
                this.Controls[index].Visible = false;
                
            }
            else
            {
                this.Controls[index].Visible = true;
            }
        }
    }
}
