using System;
using System.Drawing;
using System.Windows.Forms;
using IvyTalk.Printer.Label.Helper;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class PrintObject8 : UserControl,IPrintObject ,ISizeable,IDeleteable ,IFontable ,
        IColorable ,IContextAlignAble ,IChangeAreaAble ,IFormatable ,IBorderable 
    {
        public PrintObject8()
        {
            InitializeComponent();
        }

        int IPrintObject.objectType
        {
            get { return 8; }
        }


        void IPrintObject.Show(Control par)
        {
            par.Controls.Add(this);
        }

        private bool _Selected = false;
        bool IPrintObject.Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                if (_Selected == false)
                {
                    select.Remove(this);
                    _FirstSelected = false;
                }
                else
                {
                    IPrintObject ins = this;
                    ins.FirstSelected = true;
                    select.Add(this);
                    select.FirstSelectObject = this;
                }
                this.Refresh();
            }
        }

        private IDesign select;
        void IPrintObject.SetSelectControl(IDesign select)
        {
          
            this.select = select;
            PrintObjectMouse.Bind(this, select);
        }

        private bool _FirstSelected = false;
        bool IPrintObject.FirstSelected
        {
            get
            {
                return _FirstSelected;
            }
            set
            {
                _FirstSelected = value ;
                this.Refresh();
            }
        }

       
        string IPrintObject.xml
        {
            get
            {
                StringBuilderForXML sb = new StringBuilderForXML();
                sb.Append("Left", this.Left.ToString());
                sb.Append("Top", this.Top.ToString());
                sb.Append("Width", this.Width.ToString());
                sb.Append("Height", this.Height.ToString());
                FontConverter fc = new FontConverter();
                sb.Append("Font", fc.ConvertToInvariantString(this.Font));
                sb.Append("Color", this._Color.ToArgb().ToString());
                sb.Append("Align", this._Align.ToString());
                sb.Append("Area", this._Area.ToString());
                sb.Append("Format", this._Format);
                sb.Append("BorderLeft", this._BorderLeft.ToString());
                sb.Append("BorderRight", this._BorderRight.ToString());
                sb.Append("BorderTop", this._BorderTop.ToString());
                sb.Append("BorderBottom", this._BorderBottom.ToString());
                string str = sb.ToString();
                sb.Clear();
                sb.Append("PrintObject8", str);
                return sb.ToString();
            }
            set
            {

                ReadXml r = new ReadXml(value);
                IFontable fontable = this;
                FontConverter fc = new FontConverter();
                fontable.Font = (Font)fc.ConvertFromString(r.Read("Font"));
                int left, top, width, height;
                int.TryParse(r.Read("Left"), out left);
                int.TryParse(r.Read("Top"), out top);
                int.TryParse(r.Read("Width"), out width);
                int.TryParse(r.Read("Height"), out height);
                this.Left = left;
                this.Top = top;
                this.Width = width;
                this.Height = height;
                IColorable colorable = this;
                colorable.Color = Color.FromArgb(Convert.ToInt32(r.Read("Color")));
                IContextAlignAble contextalignable = this;
                contextalignable.Align = Convert.ToInt16(r.Read("Align"));
                IChangeAreaAble areaable = this;
                areaable.Area = Convert.ToInt16(r.Read("Area"));
                IFormatable formatable = this;
                formatable.Format = r.Read("Format");
                IBorderable borderable = this;
                borderable.BorderLeft = Convert.ToInt16(r.Read("BorderLeft"));
                borderable.BorderRight = Convert.ToInt16(r.Read("BorderRight"));
                borderable.BorderTop = Convert.ToInt16(r.Read("BorderTop"));
                borderable.BorderBottom = Convert.ToInt16(r.Read("BorderBottom"));
                //
                this.Refresh();
            }
        }


        void IDeleteable.Delete(Control par)
        {
            IPrintObject ins = this;
            ins.Selected = false;
            par.Controls.Remove(this);
        }

       

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            var sf = Conv.AlignToStringFormat(this._Align);
            e.Graphics.DrawString("时间", this.Font, new SolidBrush(this._Color), rec,sf);
            if (this._BorderLeft == 1)
            {
                e.Graphics.DrawLine(Pens.Black, 0, 0, 0, this.Height);
            }
            if (this._BorderRight == 1)
            {
                e.Graphics.DrawLine(Pens.Black, this.Width - 1, 0, this.Width - 1, this.Height);
            }
            if (this._BorderTop == 1)
            {
                e.Graphics.DrawLine(Pens.Black, 0, 0, this.Width, 0);
            }
            if (this._BorderBottom == 1)
            {
                e.Graphics.DrawLine(Pens.Black, 0, this.Height - 1, this.Width - 1, this.Height - 1);
            }
            if (_Selected == false)
            {
                Pen p = new Pen(Color.Gray);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            }
            else
            {
                if (_FirstSelected == true)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), 1, 1, this.Width - 2, this.Height - 2);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Blue, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }



        string IPrintObject.propertyInfo
        {
            get { return "类型：时间"; }
        }

        private Color _Color = Color.Black;
        Color IColorable.Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                this.Refresh();
            }
        }

        private int _Align = 21;
        int IContextAlignAble.Align
        {
            get
            {
                return _Align;
            }
            set
            {
                _Align = value;
                this.Refresh();
            }
        }

        private int _Area = 1;
        int IChangeAreaAble.Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
            }
        }

        private string _Format = "";
        string IFormatable.Format
        {
            get
            {
                return _Format;
            }
            set
            {
                _Format = value;
            }
        }

        private void PrintObject8_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private int _BorderLeft = 0;
        int IBorderable.BorderLeft
        {
            get
            {
                return _BorderLeft;
            }
            set
            {
                _BorderLeft = value;
            }
        }

        private int _BorderRight = 0;
        int IBorderable.BorderRight
        {
            get
            {
                return _BorderRight;
            }
            set
            {
                _BorderRight = value;
            }
        }

        private int _BorderTop = 0;
        int IBorderable.BorderTop
        {
            get
            {
                return _BorderTop;
            }
            set
            {
                _BorderTop = value;
            }
        }

        private int _BorderBottom = 0;
        int IBorderable.BorderBottom
        {
            get
            {
                return _BorderBottom;
            }
            set
            {
                _BorderBottom = value;
            }
        }


    }
}
