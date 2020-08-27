using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace IvyTalk.Printer.Label.Helper
{
    class DrawDetailDefault : IDrawDetail
    {

        void IDrawDetail.Draw(Graphics g, string styleXml, System.Data.DataTable tbdetail, int pageIndex)
        {


            ReadXml r = new ReadXml(styleXml);
            int left = Convert.ToInt16(r.Read("PrintObject3/Left"));
            int top = Convert.ToInt16(r.Read("PrintObject3/Top"));
            int Width = Convert.ToInt16(r.Read("PrintObject3/Width"));
            int Height = Convert.ToInt16(r.Read("PrintObject3/Height"));
            int columnHeight = Convert.ToInt16(r.Read("PrintObject3/ColumnHeight"));
            int rowHeight = Convert.ToInt16(r.Read("PrintObject3/RowHeight"));
            FontConverter fc = new FontConverter();
            var font = (Font)fc.ConvertFromString(r.Read("PrintObject3/Font"));
            var color = Color.FromArgb(Convert.ToInt32(r.Read("PrintObject3/Color")));
            int AutoRow = Convert.ToInt16(r.Read("PrintObject3/AutoRow"));
            int SmallTotal = Helper.Conv.ToInt16(r.Read("PrintObject3/SmallTotal"));
            string SmallTotalFields = r.Read("PrintObject3/SmallTotalFields");
            //
            int pageRowCount = 0;
            if (SmallTotal == 1)
            {
                pageRowCount = (Height - columnHeight - rowHeight) / rowHeight;
            }
            else
            {
                pageRowCount = (Height - columnHeight) / rowHeight;
            }
            var tb = Helper.Conv.Paging(tbdetail, pageRowCount, pageIndex);
            if (AutoRow == 1)
            {
                for (int j = tb.Rows.Count - 1; j < pageRowCount; j++)
                {
                    tb.Rows.Add(tb.NewRow());
                }
            }
            if (SmallTotal == 1)
            {

                DataRow row = tb.NewRow();

                foreach (string field in SmallTotalFields.Split(','))
                {
                    if (tbdetail.Columns.Contains(field) == true)
                    {
                        row[field] = tb.Compute("sum([" + field + "])", "");
                    }

                }
                row["i"] = "小计";
                tb.Rows.Add(row);

            }
            //
            int l = left;
            int t = top;
            List<Helper.ReadXml> lst = r.ReadList("PrintObject3/Column");
            for (int i = 0; i < lst.Count; i++)
            {
                var r2 = lst[i];
                if (r2.Read("Display") == "1")
                {
                    string colname = r2.Read("ColName");
                    string colbyname = r2.Read("ColByname");
                    int colwidth = Convert.ToInt16(r2.Read("Width"));
                    int align = Convert.ToInt16(r2.Read("Align"));
                    var sf = Helper.Conv.AlignToStringFormat(22);
                    string format = r2.Read("Format");
                    Rectangle rec = new Rectangle(l, t, colwidth, columnHeight);
                    g.DrawString(colbyname, font, new SolidBrush(color), rec, sf);
                    g.DrawRectangle(Pens.Black, rec);
                    if (l >= Width)
                    {
                        break;
                    }
                    l += colwidth;

                }
            }
            //
            l = left;
            t = top + columnHeight;

            for (int i = 0; i < lst.Count; i++)
            {
                var r2 = lst[i];
                if (r2.Read("Display") == "1")
                {

                    string Field = r2.Read("ColName");
                    string colbyname = r2.Read("ColByname");
                    int colwidth = Convert.ToInt16(r2.Read("Width"));
                    int align = Convert.ToInt16(r2.Read("Align"));
                    var sf = Helper.Conv.AlignToStringFormat(align);
                    string Format = r2.Read("Format");
                    DataColumn col = tbdetail.Columns[Field];

                    foreach (DataRow row in tb.Rows)
                    {
                        Rectangle rec = new Rectangle(l, t, colwidth, columnHeight);
                        string context = "";
                        if (Format == "")
                        {
                            context = row[Field].ToString();
                        }
                        else if (Format == "大写金额")
                        {
                            context = Helper.Conv.DaXie2(row[Field].ToString());
                        }
                        else
                        {
                            if (col.DataType == typeof(decimal))
                            {
                                context = Helper.Conv.ToDecimal(row[Field]).ToString(Format);
                            }
                            else if (col.DataType == typeof(Int16))
                            {
                                context = Helper.Conv.ToInt16(row[Field]).ToString(Format);
                            }
                            else if (col.DataType == typeof(Int32))
                            {
                                context = Helper.Conv.ToInt32(row[Field]).ToString(Format);
                            }
                            else if (col.DataType == typeof(DateTime))
                            {
                                context = Helper.Conv.ToDateTime(row[Field]).ToString(Format);
                            }
                            else
                            {
                                context = row[Field].ToString();
                            }
                        }
                        int sum = 0;
                        foreach (DataColumn c in tb.Columns)
                        {
                            if (row[c.ColumnName] == null || string.IsNullOrEmpty(row[c.ColumnName].ToString()))
                                sum++;
                        }

                        if (sum > tbdetail.Columns.Count / 2)
                            context = "";

                        g.DrawString(context, font, new SolidBrush(color), rec, sf);
                        g.DrawRectangle(Pens.Black, rec);
                        t += rowHeight;
                    }
                    if (l >= Width)
                    {
                        break;
                    }
                    l += colwidth;
                    t = top + columnHeight;

                }
            }
        }


    }
}
