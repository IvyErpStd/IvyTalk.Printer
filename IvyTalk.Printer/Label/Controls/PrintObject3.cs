using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IvyTalk.Printer.Label.Helper;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class PrintObject3 : UserControl,IPrintObject ,ISizeable ,IGridable ,IDeleteable ,IFontable ,IColorable 
    {

        GridStyleInfo info = new GridStyleInfo();
        public PrintObject3()
        {
            InitializeComponent();
            //
            tbstyle.Columns.Add("display",typeof(bool));
            tbstyle.Columns.Add("colname");
            tbstyle.Columns.Add("colbyname");
            tbstyle.Columns.Add("width", typeof(int));
            tbstyle.Columns.Add("align");
            tbstyle.Columns.Add("format");
            //
            info.columnHeight = 30;
            info.rowHeight = 30;
            info.autoRow = 1;
            info.smallTotal = 0;
        }

        int IPrintObject.objectType
        {
            get {return 3;}
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
                _FirstSelected = value;
                this.Refresh();
            }
        }

        private void PrintObject3_Load(object sender, EventArgs e)
        {

        }

        private void PrintObject3_Paint(object sender, PaintEventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //
            int left = 0;
            foreach (DataRow row in tbstyle.Rows)
            {
                if ((bool)row["display"] == true)
                {
                    Rectangle rec = new Rectangle(left, 0, Convert.ToInt16(row["width"]), info.columnHeight);
                    var sf = Conv.AlignToStringFormat(Convert.ToInt16(row["align"]));
                    e.Graphics.DrawString(row["colbyname"].ToString(), this.Font,new SolidBrush(this._Color), rec,sf);
                    e.Graphics.DrawRectangle(Pens.Black, rec);
                    left += Convert.ToInt16(row["width"]);
                }
           
            }
            //
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

        

        private System.Data.DataTable tbstyle = new DataTable();
        

        bool IGridable.EditGrid(string[] fields)
        {
            var tb = new DataTable();
            IEditGrid edit = new EditGrid();
            foreach (string field in fields)
            {
                int flag = 0;
                foreach (DataRow row in tbstyle.Rows)
                {
                    if (row["colname"].ToString() == field)
                    {
                        flag = 1;
                    }
                }
                //
                if (flag == 0)
                {
                    DataRow r = tbstyle.NewRow();
                    tbstyle.Rows.Add(r);
                    r["display"] = false;
                    r["colname"] = field;
                    r["colbyname"] = field;
                    r["width"] =100;
                    r["align"] = "21";
                    r["format"] = "";
                }
              
            }
            //
            List<DataRow> lstrow = new List<DataRow>();
            foreach (DataRow row in tbstyle.Rows)
            {
                string colname = row["colname"].ToString();
                int flag = 0;
                foreach (string field in fields)
                {
                    if (colname == field)
                    {
                        flag = 1;
                    }
                }
                //
                if (flag == 0)
                {
                    lstrow.Add(row);
                }
            }
            foreach (DataRow row in lstrow)
            {
                tbstyle.Rows.Remove(row);
            }
            //
            if (edit.Edit(tbstyle, out tb) == true)
            {
                tbstyle = tb;
                this.Refresh();
                return true;
            }
            else
            {
                return false;
            }
        }

      
        bool IGridable.SetStyle()
        {
            ISetGridStyle style = new SetGridStyle();
            GridStyleInfo info = this.info;
            if (style.SetStyle(info, out info) == true)
            {
                this.info = info;
                this.Refresh();
                return true;
            }
            else
            {
                return false;
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
                sb.Append("ColumnHeight", info.columnHeight.ToString());
                sb.Append("RowHeight",  info.rowHeight.ToString());
                sb.Append("AutoRow",  info.autoRow.ToString());
                sb.Append("SmallTotal", info.smallTotal.ToString());
                sb.Append("SmallTotalFields", info.smallTotalFields);
                FontConverter fc = new FontConverter();
                sb.Append("Font", fc.ConvertToInvariantString(this.Font));
                sb.Append("Color", this._Color.ToArgb().ToString());
                //
                foreach (DataRow row in tbstyle.Rows)
                {
                    StringBuilderForXML sb3 = new StringBuilderForXML();
                    if ((bool)row["display"] == false)
                    {
                        sb3.Append("Display", "0");
                    }
                    else
                    {
                        sb3.Append("Display", "1");
                    }
                    sb3.Append("ColName", row["colname"].ToString());
                    sb3.Append("ColByname", row["colbyname"].ToString());
                    sb3.Append("Width", row["width"].ToString());
                    sb3.Append("Align", row["align"].ToString());
                    sb3.Append("Format", row["format"].ToString());
                    string str3 = sb3.ToString();
                    sb3.Clear();
                    sb3.Append("Column", str3);
                    //
                    sb.Append(sb3.ToString());
                }
                //
                string str = sb.ToString();
                sb.Clear();
                sb.Append("PrintObject3", str);
                return sb.ToString();
            }
            set
            {
                ReadXml r = new ReadXml(value);
                IFontable fontable = this;
                FontConverter fc = new FontConverter();
                fontable.Font = (Font)fc.ConvertFromString(r.Read("Font"));
                this.Left = Convert.ToInt16(r.Read("Left"));
                this.Top = Convert.ToInt16(r.Read("Top"));
                this.Width = Convert.ToInt16(r.Read("Width"));
                this.Height = Convert.ToInt16(r.Read("Height"));
                info.columnHeight = Convert.ToInt16(r.Read("ColumnHeight"));
                info.rowHeight = Convert.ToInt16(r.Read("RowHeight"));
                info.autoRow = Convert.ToInt16(r.Read("AutoRow"));
                info.smallTotal = Conv.ToInt16(r.Read("SmallTotal"));
                info.smallTotalFields = r.Read("SmallTotalFields");
                IColorable colorable = this;
                colorable.Color = Color.FromArgb(Convert.ToInt32(r.Read("Color")));
             
                //
                tbstyle.Rows.Clear();
                foreach (ReadXml r2 in r.ReadList("Column"))
                {
                    var row = tbstyle.NewRow();
                    tbstyle.Rows.Add(row);
                    if (r2.Read("Display") == "0")
                    {
                        row["display"] = false;
                    }
                    else
                    {
                        row["display"] = true;
                    }
                    row["colname"] = r2.Read("ColName");
                    row["colbyname"] = r2.Read("ColByname");
                    row["width"] = Convert.ToInt16(r2.Read("Width"));
                    row["align"] = r2.Read("Align");
                    row["format"] = r2.Read("Format");
                }
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

        private void PrintObject3_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }


        string IPrintObject.propertyInfo
        {
            get { return "类型：表格"; }
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

       
    }
}
