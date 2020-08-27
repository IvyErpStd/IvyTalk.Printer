using System;
using System.Drawing;
using System.Windows.Forms;
using IvyTalk.Printer.Label.Helper;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class PrintObject4 : UserControl,IPrintObject,ISizeable ,IDeleteable ,IColorable ,IChangeAreaAble 
    {
        public PrintObject4()
        {
            InitializeComponent();
           

        }

        int IPrintObject.objectType
        {
            get { return 4; }
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
                sb.Append("Color", this._Color.ToArgb().ToString());
                sb.Append("Area", this._Area.ToString());
                string str = sb.ToString();
                sb.Clear();
                sb.Append("PrintObject4", str);
                return sb.ToString();
            }
            set
            {

                ReadXml r = new ReadXml(value);
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
                IChangeAreaAble areaable = this;
                areaable.Area = Convert.ToInt16(r.Read("Area"));
                //
                this.Refresh();
            }
        }

        private void PrintObject4_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
           
            e.Graphics.DrawLine(new Pen(this._Color), this.Width / 2, 0, this.Width / 2, this.Height);
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

      

        private void PrintObject4_SizeChanged(object sender, EventArgs e)
        {
          
            this.Refresh();
        }


        void IDeleteable.Delete(Control par)
        {
            IPrintObject ins = this;
            ins.Selected = false;
            par.Controls.Remove(this);
        }


        string IPrintObject.propertyInfo
        {
            get { return "类型：竖线"; }
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

    }
}
