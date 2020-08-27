using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IvyTalk.Printer.Label.Helper;

namespace IvyTalk.Printer.Label
{
    public class PrintV : Form, IBLL.IPrint
    {
        private ToolStripButton toolStripButton3;
        private PrintPreviewControl printPreviewControl1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripButton toolStripButton1;
        private ToolStrip toolStrip1;


        public PrintV()
        {
            this.InitializeComponent();
        }

        string xml = "";
        string sheetType;
        DataTable tbmain;
        DataTable tbdetail;

        void IBLL.IPrint.Print(DataTable dtMain, DataTable dtDetail, string sheetType)
        {
            try
            {
                tbmain = dtMain;
                tbdetail = dtDetail;
                this.sheetType = sheetType;

                this.ShowDialog();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        public void showPrint(DataTable dtMain, DataTable dtDetail, string sheetType, string path)
        {
            try
            {
                tbmain = dtMain;
                tbdetail = dtDetail;
                this.sheetType = sheetType;

                string file = path + "\\print_style\\" + sheetType + ".xml";
                xml = System.IO.File.ReadAllText(file, System.Text.Encoding.GetEncoding("gb2312"));

                this.ShowDialog();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintV));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripLabel1,
            this.toolStripComboBox1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(662, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton3.Text = "打印";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "页码";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl1.Location = new System.Drawing.Point(0, 25);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(662, 456);
            this.printPreviewControl1.TabIndex = 1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton1.Text = "退出";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // PrintV
            // 
            this.ClientSize = new System.Drawing.Size(662, 481);
            this.Controls.Add(this.printPreviewControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PrintV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrintV_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PrintDialog f = new PrintDialog();

            f.Document = pd;
            if (f.ShowDialog() == DialogResult.OK)
            {
                PageIndex = 0;
                pd.Print();
                this.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        System.Drawing.Printing.PrintDocument pd;
        private void PrintV_Load(object sender, EventArgs e)
        {
            PageCount = this.GetPageCount(xml);
            PageSize = this.GetPageSize(xml);
            this.toolStripComboBox1.Items.Clear();
            for (int i = 1; i <= PageCount; i++)
            {
                this.toolStripComboBox1.Items.Add(i.ToString());
            }
            this.toolStripComboBox1.SelectedIndex = 0;
            //
            if (tbdetail != null)
            {
                if (tbdetail.Columns.Contains("i") == false)
                {
                    tbdetail.Columns.Add("i");
                    int index = 0;
                    foreach (DataRow row in tbdetail.Rows)
                    {
                        index++;
                        row["i"] = index;
                    }
                }
            }
            //
            pd = new System.Drawing.Printing.PrintDocument();
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("printPage", PageSize.Width, PageSize.Height);
            pd.PrintPage += this.pd_pagePrint;
            this.printPreviewControl1.Document = pd;
            this.printPreviewControl1.Zoom = 1;
        }

        public void Print(string xml, DataTable tbmain, DataTable tbdetail)
        {
            this.xml = xml;
            this.tbmain = tbmain;
            this.tbdetail = tbdetail;
            PageCount = this.GetPageCount(xml);
            PageSize = this.GetPageSize(xml);
            //
            if (tbdetail != null)
            {
                if (tbdetail.Columns.Contains("i") == false)
                {
                    tbdetail.Columns.Add("i");
                    int index = 0;
                    foreach (DataRow row in tbdetail.Rows)
                    {
                        index++;
                        row["i"] = index;
                    }
                }
            }
            //
            pd = new System.Drawing.Printing.PrintDocument();
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("printPage", PageSize.Width, PageSize.Height);
            pd.PrintPage += this.pd_pagePrint;
            PageIndex = 0;
            pd.Print();
        }

        public void Print(string xml, DataTable tbmain, DataTable tbdetail, string name)
        {
            this.xml = xml;
            this.tbmain = tbmain;
            this.tbdetail = tbdetail;
            PageCount = this.GetPageCount(xml);
            PageSize = this.GetPageSize(xml);
            //
            if (tbdetail != null)
            {
                if (tbdetail.Columns.Contains("i") == false)
                {
                    tbdetail.Columns.Add("i");
                    int index = 0;
                    foreach (DataRow row in tbdetail.Rows)
                    {
                        index++;
                        row["i"] = index;
                    }
                }
            }
            //
            pd = new System.Drawing.Printing.PrintDocument();
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("printPage", PageSize.Width, PageSize.Height);
            pd.PrinterSettings.PrinterName = name;
            pd.PrintPage += this.pd_pagePrint;
            PageIndex = 0;
            pd.Print();
        }

        private System.Drawing.Size GetPageSize(string xml)
        {
            ReadXml r = new ReadXml(xml);
            int w = Convert.ToInt16(r.Read("Page/Width"));
            int h = Convert.ToInt16(r.Read("Page/Height"));
            return new System.Drawing.Size(w, h);
        }

        private int GetPageCount(string xml)
        {

            ReadXml r = new ReadXml(xml);
            //
            if (r.Read("PrintObject3") == "")
            {
                return 1;
            }
            else
            {

                int columnHeight = Convert.ToInt16(r.Read("PrintObject3/ColumnHeight"));
                int rowHeight = Convert.ToInt16(r.Read("PrintObject3/RowHeight"));
                int gridWidth = Convert.ToInt16(r.Read("PrintObject3/Width"));
                int gridHeight = Convert.ToInt16(r.Read("PrintObject3/Height"));
                int smallTotal = Conv.ToInt16(r.Read("PrintObject3/SmallTotal"));
                int anPageRowCount = 0;
                if (smallTotal == 1)
                {
                    anPageRowCount = (gridHeight - columnHeight - rowHeight) / rowHeight;
                }
                else
                {
                    anPageRowCount = (gridHeight - columnHeight) / rowHeight;
                }

                if (tbdetail.Rows.Count == 0)
                {
                    return 1;
                }
                else
                {
                    int cnt = tbdetail.Rows.Count / anPageRowCount;
                    if (tbdetail.Rows.Count % anPageRowCount != 0)
                    {
                        cnt += 1;
                    }
                    return cnt;
                }
            }

        }

        private void Draw(System.Drawing.Graphics g, int pageIndex, bool isFirst, bool isLast)
        {
            ReadXml r = new ReadXml(xml);
            foreach (ReadXml r2 in r.ReadList("PrintObject1"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                string context = r2.Read("Context");
                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == 2 && isFirst == false || Area == 4 && isLast == false)
                {

                }
                else
                {

                    g.DrawString(context, font, new SolidBrush(color), rec, sf);
                    if (_BorderLeft == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left, top + Height);
                    }
                    if (_BorderRight == 1)
                    {
                        g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                    }
                    if (_BorderTop == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left + Width, top);
                    }
                    if (_BorderBottom == 1)
                    {
                        g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                    }
                }

            }
            foreach (ReadXml r2 in r.ReadList("PrintObject2"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                string Field = r2.Read("Field");
                string Format = r2.Read("Format");
                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == 2 && isFirst == false || Area == 4 && isLast == false)
                {

                }
                else
                {
                    if (tbmain.Rows.Count != 0)
                    {
                        DataRow row = tbmain.Rows[0];
                        DataColumn col = tbmain.Columns[Field];
                        string context = "";
                        if (Format == "")
                        {
                            context = row[Field].ToString();
                        }
                        else if (Format == "大写金额")
                        {
                            context = Conv.DaXie2(row[Field].ToString());
                        }
                        else if (Format == "条形码")
                        {
                             Barcode.CreateBarcodeImage(g,rec.X,rec.Y,"C", row[Field].ToString(), 1F, rec.Height); 
                        }
                        else if (Format == "二维码")
                        {
                            QRCode code = new QRCode();
                            Bitmap bit = code.grant_qrcode(row[Field].ToString(),rec.Width,rec.Width);
                            g.DrawImage(bit, rec);
                        }
                        else
                        {
                            if (col.DataType == typeof(decimal))
                            {
                                context = Conv.ToDecimal(row[Field]).ToString(Format);
                            }
                            else if (col.DataType == typeof(Int16))
                            {
                                context = Conv.ToInt16(row[Field]).ToString(Format);
                            }
                            else if (col.DataType == typeof(Int32))
                            {
                                context = Conv.ToInt32(row[Field]).ToString(Format);
                            }
                            else if (col.DataType == typeof(DateTime))
                            {
                                context = Conv.ToDateTime(row[Field]).ToString(Format);
                            }
                            else
                            {
                                context = row[Field].ToString();
                            }
                        }
                        if (Format == "条形码")
                        {

                        }
                        else if (Format == "二维码")
                        {

                        }
                        else
                        {
                            g.DrawString(context, font, new SolidBrush(color), rec, sf);
                        } 
                        if (_BorderLeft == 1)
                        {
                            g.DrawLine(Pens.Black, left, top, left, top + Height);
                        }
                        if (_BorderRight == 1)
                        {
                            g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                        }
                        if (_BorderTop == 1)
                        {
                            g.DrawLine(Pens.Black, left, top, left + Width, top);
                        }
                        if (_BorderBottom == 1)
                        {
                            g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                        }
                    }

                }
            }
            if (r.Read("PrintObject3") != "")
            {

                int flag = 0;
                foreach (ReadXml r2 in r.ReadList("PrintObject3/Column"))
                {
                    if (r2.Read("ColName") == "#")
                    {
                        flag = 1;
                        break;
                    }
                }
                //


                if (flag == 0)
                {

                    IDrawDetail draw = new DrawDetailDefault();
                    draw.Draw(g, xml, tbdetail, pageIndex);
                }
                else
                {

                    IDrawDetail draw = new DrawDetailJH();
                    draw.Draw(g, xml, tbdetail, pageIndex);
                }

            }
            foreach (ReadXml r2 in r.ReadList("PrintObject4"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                if (Area == 2 && isFirst == false || Area == 4 && isLast == false)
                {

                }
                else
                {
                    g.DrawLine(new Pen(color), left + Width / 2, top, left + Width / 2, top + Height);
                }

            }
            foreach (ReadXml r2 in r.ReadList("PrintObject5"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                if (Area == 2 && isFirst == false || Area == 4 && isLast == false)
                {

                }
                else
                {
                    g.DrawLine(new Pen(color), left, top + Height / 2, left + Width, top + Height / 2);
                }
            }
            foreach (ReadXml r2 in r.ReadList("PrintObject6"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                var img = Conv.StringToImage(r2.Read("Image"));
                int Area = Convert.ToInt16(r2.Read("Area"));
                Rectangle rec = new Rectangle(left, top, Width, Height);
                if (Area == 2 && isFirst == false || Area == 4 && isLast == false)
                {

                }
                else
                {
                    g.DrawImage(img, rec);
                }
            }
            foreach (ReadXml r2 in r.ReadList("PrintObject7"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));

                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == 2 && isFirst == false || Area == 4 && isLast == false)
                {

                }
                else
                {
                    string context = "第N页,共M页".Replace("N", pageIndex.ToString())
                        .Replace("M", PageCount.ToString());
                    g.DrawString(context, font, new SolidBrush(color), rec, sf);
                    if (_BorderLeft == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left, top + Height);
                    }
                    if (_BorderRight == 1)
                    {
                        g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                    }
                    if (_BorderTop == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left + Width, top);
                    }
                    if (_BorderBottom == 1)
                    {
                        g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                    }
                }
            }
            foreach (ReadXml r2 in r.ReadList("PrintObject8"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                string format = r2.Read("Format");
                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == 2 && isFirst == false || Area == 4 && isLast == false)
                {

                }
                else
                {
                    string context = "";
                    if (format == "")
                    {
                        context = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        context = System.DateTime.Now.ToString(format);
                    }
                    g.DrawString(context, font, new SolidBrush(color), rec, sf);
                    if (_BorderLeft == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left, top + Height);
                    }
                    if (_BorderRight == 1)
                    {
                        g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                    }
                    if (_BorderTop == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left + Width, top);
                    }
                    if (_BorderBottom == 1)
                    {
                        g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                    }
                }
            }
        }

        private System.Drawing.Size PageSize = new System.Drawing.Size(100, 100);
        private int PageCount = 0;
        private int PageIndex = 0;
        private void pd_pagePrint(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            PageIndex++;
            bool isFirst = false;
            bool isLast = false;
            if (PageIndex == 1)
            {
                isFirst = true;
                if (PageIndex == PageCount)
                {
                    isLast = true;
                    e.HasMorePages = false;
                }
                else
                {
                    isLast = false;
                    e.HasMorePages = true;
                }
            }
            else
            {
                isFirst = false;
                if (PageIndex == PageCount)
                {
                    isLast = true;
                    e.HasMorePages = false;
                }
                else
                {
                    isLast = false;
                    e.HasMorePages = true;
                }
            }
            //
            Draw(e.Graphics, PageIndex, isFirst, isLast);

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.printPreviewControl1.StartPage = this.toolStripComboBox1.SelectedIndex;
        }





    }
}
