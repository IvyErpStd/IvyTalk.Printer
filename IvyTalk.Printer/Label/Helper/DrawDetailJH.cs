using System;
using System.Data;
using System.Drawing;

namespace IvyTalk.Printer.Label.Helper
{
    class DrawDetailJH : IDrawDetail
    {
        void IDrawDetail.Draw(System.Drawing.Graphics g, string styleXml, System.Data.DataTable tbdetail, int pageIndex)
        {

            //
            ReadXml r = new ReadXml(styleXml);
            var tbstyle = new DataTable();
            tbstyle.Columns.Add("colname");
            tbstyle.Columns.Add("colbyname");
            tbstyle.Columns.Add("width", typeof(int));
            tbstyle.Columns.Add("align");
            tbstyle.Columns.Add("format");
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject3/Column"))
            {
                if (r2.Read("Display") == "1")
                {
                    var row = tbstyle.NewRow();
                    tbstyle.Rows.Add(row);
                    row["colname"] = r2.Read("ColName");
                    row["colbyname"] = r2.Read("ColByname");
                    row["width"] = Convert.ToInt16(r2.Read("Width"));
                    row["align"] = r2.Read("Align");
                    row["format"] = r2.Read("Format");
                }
            }
            //
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

            int width2 = 0;
            int width3 = 0;
            foreach (DataRow row in tbstyle.Rows)
            {
                if (row["colname"].ToString() == "#")
                {
                    width3 = Conv.ToInt16(row["width"]);
                }
                else
                {
                    width2 += Conv.ToInt16(row["width"]);
                }

            }

            int num = 0;
            foreach (DataColumn col in tbdetail.Columns)
            {
                if (col.ColumnName.StartsWith("#") == true)
                {
                    num++;
                }
            }
            int addwidth = num * width3;
            if (Width - width2 > 0)
            {
                addwidth = addwidth - (Width - width2);
            }
            decimal rate = 1;
            if (addwidth > 0)
            {
                rate = (decimal)(width2 - addwidth) / (decimal)width2;
            }

            DataRow specRow = null;
            foreach (DataRow row in tbstyle.Rows)
            {
                if (row["colname"].ToString() == "#")
                {
                    specRow = row;
                }
                else
                {
                    row["width"] = (int)(Conv.ToDecimal(row["width"]) * rate);
                }
            }
            //

            foreach (DataColumn col in tbdetail.Columns)
            {
                if (col.ColumnName.StartsWith("#") == true)
                {
                    DataRow row = tbstyle.NewRow();
                    row.ItemArray = specRow.ItemArray;
                    row["colname"] = col.ColumnName;
                    row["colbyname"] = col.ColumnName.Substring(1);
                    var specRowIndex = tbstyle.Rows.IndexOf(specRow);
                    tbstyle.Rows.InsertAt(row, specRowIndex);

                }
            }
            if (specRow != null)
                tbstyle.Rows.Remove(specRow);
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
                    if (field == "#")
                    {
                        foreach (DataColumn col in tbdetail.Columns)
                        {
                            if (col.ColumnName.StartsWith("#") == true)
                            {
                                row[col.ColumnName] = tb.Compute("sum([" + col.ColumnName + "])", "");
                            }
                        }
                    }
                    else
                    {
                        if (tbdetail.Columns.Contains(field) == true)
                        {
                            row[field] = tb.Compute("sum([" + field + "])", "");
                        }

                    }

                }
                row["i"] = "小计";
                tb.Rows.Add(row);
            }

            //
            int l = left;
            int t = top;
            for (int i = 0; i < tbstyle.Rows.Count; i++)
            {
                DataRow row = tbstyle.Rows[i];
                string colname = row["colname"].ToString();
                string colbyname = row["colbyname"].ToString();
                int colwidth = Convert.ToInt16(row["width"].ToString());
                int align = Convert.ToInt16(row["align"].ToString());
                var sf = Helper.Conv.AlignToStringFormat(22);
                string format = row["format"].ToString();

                Rectangle rec = new Rectangle(l, t, colwidth, columnHeight);
                g.DrawString(colbyname, font, new SolidBrush(color), rec, sf);
                g.DrawRectangle(Pens.Black, rec);
                if (l >= Width)
                {
                    break;
                }
                l += colwidth;

            }
            //
            l = left;
            t = top + columnHeight;

            for (int i = 0; i < tbstyle.Rows.Count; i++)
            {
                DataRow dr = tbstyle.Rows[i];

                string Field = dr["colname"].ToString();
                string colbyname = dr["colbyname"].ToString();
                int colwidth = Convert.ToInt16(dr["width"].ToString());
                int align = Convert.ToInt16(dr["align"].ToString());
                var sf = Helper.Conv.AlignToStringFormat(align);
                string Format = dr["format"].ToString();
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
            //

        }
    }

}
