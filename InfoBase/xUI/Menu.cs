using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
//using System.Web.UI;

namespace InfoBase.xUI
{
    class Menu   // : Control
    {
        private string _title;
        private List<Control> _controlCollection = new List<Control>();
        private Color _backgroundColor;
        private Color _foreColor;
        private Font _font;
        private ContentAlignment _textAlignment;
       // private event  EventHandler  _Click;

        /*public event Click
        {
       get {return this._Click;}
       set {this._Click = value;}
        }*/


        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

       public List<Control> Controls
        {
            get { return this._controlCollection; }
        }

        public Color BackgroundColor
        {
            get { return this._backgroundColor; }
            set { this._backgroundColor = value; }
        }

        public Color ForeColor
        {
            get { return this._foreColor; }
            set { this._foreColor = value; }
        }

        public Font TextFont
        {
            get { return this._font; }
            set { this._font = value; }
        }

        public ContentAlignment TextAlign
        {
            get { return this._textAlignment; }
            set { this._textAlignment = value; }
        }
      /* protected virtual void  OnClick(EventArgs e)
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
        }
        */
        
    }
}
