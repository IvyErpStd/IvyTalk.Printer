using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace IvyTalk.Printer.Label.Helper
{
    class Barcode
    {
        public static Dictionary<string, DataRow> Code128A = null;
        public static Dictionary<string, DataRow> Code128B = null;
        public static Dictionary<string, DataRow> Code128C = null;


        public static void IniCode128()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("ID");
            tb.Columns.Add("Code128A");
            tb.Columns.Add("Code128B");
            tb.Columns.Add("Code128C");
            tb.Columns.Add("BandCode");
            tb.CaseSensitive = true;
            //
            tb.Rows.Add("0", " ", " ", "00", "212222");
            tb.Rows.Add("1", "!", "!", "01", "222122");
            tb.Rows.Add("2", "\"", "\"", "02", "222221");
            tb.Rows.Add("3", "#", "#", "03", "121223");
            tb.Rows.Add("4", "$", "$", "04", "121322");
            tb.Rows.Add("5", "%", "%", "05", "131222");
            tb.Rows.Add("6", "&", "&", "06", "122213");
            tb.Rows.Add("7", "'", "'", "07", "122312");
            tb.Rows.Add("8", "(", "(", "08", "132212");
            tb.Rows.Add("9", ")", ")", "09", "221213");
            tb.Rows.Add("10", "*", "*", "10", "221312");
            tb.Rows.Add("11", "+", "+", "11", "231212");
            tb.Rows.Add("12", ",", ",", "12", "112232");
            tb.Rows.Add("13", "-", "-", "13", "122132");
            tb.Rows.Add("14", ".", ".", "14", "122231");
            tb.Rows.Add("15", "/", "/", "15", "113222");
            tb.Rows.Add("16", "0", "0", "16", "123122");
            tb.Rows.Add("17", "1", "1", "17", "123221");
            tb.Rows.Add("18", "2", "2", "18", "223211");
            tb.Rows.Add("19", "3", "3", "19", "221132");
            tb.Rows.Add("20", "4", "4", "20", "221231");
            tb.Rows.Add("21", "5", "5", "21", "213212");
            tb.Rows.Add("22", "6", "6", "22", "223112");
            tb.Rows.Add("23", "7", "7", "23", "312131");
            tb.Rows.Add("24", "8", "8", "24", "311222");
            tb.Rows.Add("25", "9", "9", "25", "321122");
            tb.Rows.Add("26", ":", ":", "26", "321221");
            tb.Rows.Add("27", ";", ";", "27", "312212");
            tb.Rows.Add("28", "<", "<", "28", "322112");
            tb.Rows.Add("29", "=", "=", "29", "322211");
            tb.Rows.Add("30", ">", ">", "30", "212123");
            tb.Rows.Add("31", "?", "?", "31", "212321");
            tb.Rows.Add("32", "@", "@", "32", "232121");
            tb.Rows.Add("33", "A", "A", "33", "111323");
            tb.Rows.Add("34", "B", "B", "34", "131123");
            tb.Rows.Add("35", "C", "C", "35", "131321");
            tb.Rows.Add("36", "D", "D", "36", "112313");
            tb.Rows.Add("37", "E", "E", "37", "132113");
            tb.Rows.Add("38", "F", "F", "38", "132311");
            tb.Rows.Add("39", "G", "G", "39", "211313");
            tb.Rows.Add("40", "H", "H", "40", "231113");
            tb.Rows.Add("41", "I", "I", "41", "231311");
            tb.Rows.Add("42", "J", "J", "42", "112133");
            tb.Rows.Add("43", "K", "K", "43", "112331");
            tb.Rows.Add("44", "L", "L", "44", "132131");
            tb.Rows.Add("45", "M", "M", "45", "113123");
            tb.Rows.Add("46", "N", "N", "46", "113321");
            tb.Rows.Add("47", "O", "O", "47", "133121");
            tb.Rows.Add("48", "P", "P", "48", "313121");
            tb.Rows.Add("49", "Q", "Q", "49", "211331");
            tb.Rows.Add("50", "R", "R", "50", "231131");
            tb.Rows.Add("51", "S", "S", "51", "213113");
            tb.Rows.Add("52", "T", "T", "52", "213311");
            tb.Rows.Add("53", "U", "U", "53", "213131");
            tb.Rows.Add("54", "V", "V", "54", "311123");
            tb.Rows.Add("55", "W", "W", "55", "311321");
            tb.Rows.Add("56", "X", "X", "56", "331121");
            tb.Rows.Add("57", "Y", "Y", "57", "312113");
            tb.Rows.Add("58", "Z", "Z", "58", "312311");
            tb.Rows.Add("59", "[", "[", "59", "332111");
            tb.Rows.Add("60", "\\", "\\", "60", "314111");
            tb.Rows.Add("61", "]", "]", "61", "221411");
            tb.Rows.Add("62", "^", "^", "62", "431111");
            tb.Rows.Add("63", "_", "_", "63", "111224");
            tb.Rows.Add("64", "NUL", "`", "64", "111422");
            tb.Rows.Add("65", "SOH", "a", "65", "121124");
            tb.Rows.Add("66", "STX", "b", "66", "121421");
            tb.Rows.Add("67", "ETX", "c", "67", "141122");
            tb.Rows.Add("68", "EOT", "d", "68", "141221");
            tb.Rows.Add("69", "ENQ", "e", "69", "112214");
            tb.Rows.Add("70", "ACK", "f", "70", "112412");
            tb.Rows.Add("71", "BEL", "g", "71", "122114");
            tb.Rows.Add("72", "BS", "h", "72", "122411");
            tb.Rows.Add("73", "HT", "i", "73", "142112");
            tb.Rows.Add("74", "LF", "j", "74", "142211");
            tb.Rows.Add("75", "VT", "k", "75", "241211");
            tb.Rows.Add("76", "FF", "I", "76", "221114");
            tb.Rows.Add("77", "CR", "m", "77", "413111");
            tb.Rows.Add("78", "SO", "n", "78", "241112");
            tb.Rows.Add("79", "SI", "o", "79", "134111");
            tb.Rows.Add("80", "DLE", "p", "80", "111242");
            tb.Rows.Add("81", "DC1", "q", "81", "121142");
            tb.Rows.Add("82", "DC2", "r", "82", "121241");
            tb.Rows.Add("83", "DC3", "s", "83", "114212");
            tb.Rows.Add("84", "DC4", "t", "84", "124112");
            tb.Rows.Add("85", "NAK", "u", "85", "124211");
            tb.Rows.Add("86", "SYN", "v", "86", "411212");
            tb.Rows.Add("87", "ETB", "w", "87", "421112");
            tb.Rows.Add("88", "CAN", "x", "88", "421211");
            tb.Rows.Add("89", "EM", "y", "89", "212141");
            tb.Rows.Add("90", "SUB", "z", "90", "214121");
            tb.Rows.Add("91", "ESC", "{", "91", "412121");
            tb.Rows.Add("92", "FS", "|", "92", "111143");
            tb.Rows.Add("93", "GS", "}", "93", "111341");
            tb.Rows.Add("94", "RS", "~", "94", "131141");
            tb.Rows.Add("95", "US", "DEL", "95", "114113");
            tb.Rows.Add("96", "FNC3", "FNC3", "96", "114311");
            tb.Rows.Add("97", "FNC2", "FNC2", "97", "411113");
            tb.Rows.Add("98", "SHIFT", "SHIFT", "98", "411311");
            tb.Rows.Add("99", "CODEC", "CODEC", "99", "113141");
            tb.Rows.Add("100", "CODEB", "FNC4", "CODEB", "114131");
            tb.Rows.Add("101", "FNC4", "CODEA", "CODEA", "311141");
            tb.Rows.Add("102", "FNC1", "FNC1", "FNC1", "411131");
            tb.Rows.Add("103", "StartA", "StartA", "StartA", "211412");
            tb.Rows.Add("104", "StartB", "StartB", "StartB", "211214");
            tb.Rows.Add("105", "StartC", "StartC", "StartC", "211232");
            tb.Rows.Add("106", "Stop", "Stop", "Stop", "2331112");
            //
            Code128A = new Dictionary<string, DataRow>();
            Code128B = new Dictionary<string, DataRow>();
            Code128C = new Dictionary<string, DataRow>();
            foreach (DataRow row in tb.Rows)
            {
                DataRow r;

                Code128A.Add(row["Code128A"].ToString(), row);
                Code128C.Add(row["Code128C"].ToString(), row);

            }
        }


        public static string Code128A_BS(string context)
        {
            if (Code128A == null)
            {
                Barcode.IniCode128();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("211412");
            int val = 103;
            for (int i = 0; i < context.Length; i++)
            {
                var c = context[i];
                DataRow row;
                if (Code128A.TryGetValue(c.ToString(), out row) == true)
                {
                    sb.Append(row["BandCode"].ToString());
                    val += (i + 1) * Convert.ToInt16(row["ID"]);
                }

            }
            val = val % 103;
            foreach (KeyValuePair<string, DataRow> kv in Code128A)
            {
                if (val.ToString() == kv.Value["ID"].ToString())
                {
                    sb.Append(kv.Value["BandCode"].ToString());
                }
            }
            //
            sb.Append("2331112");
            //
            string str = sb.ToString();
            sb.Clear();
            int flag = 0;
            foreach (char c in str)
            {
                if (flag == 0)
                {
                    flag = 1;
                    for (int i = 0; i < Convert.ToInt16(c.ToString()); i++)
                    {
                        sb.Append("b");
                    }
                }
                else
                {
                    flag = 0;
                    for (int i = 0; i < Convert.ToInt16(c.ToString()); i++)
                    {
                        sb.Append("s");
                    }
                }
            }
            return sb.ToString();
        }

        public static string Code128B_BS(string context)
        {
            return "";
        }

        public static string Code128C_BS(string context)
        {
            if (Code128C == null)
            {
                Barcode.IniCode128();
            }
            if (context.Length % 2 != 0)
            {
                return Code128A_BS(context);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("211232");
            int val = 105;
            int index = 0;
            for (int i = 0; i < context.Length; i += 2)
            {
                index++;
                var c = context[i];
                var c2 = context[i + 1];
                DataRow row;
                if (Code128C.TryGetValue(c.ToString() + c2.ToString(), out row) == true)
                {
                    sb.Append(row["BandCode"].ToString());
                    val += index * Convert.ToInt16(row["ID"]);
                }

            }
            val = val % 103;
            foreach (KeyValuePair<string, DataRow> kv in Code128C)
            {
                if (val.ToString() == kv.Value["ID"].ToString())
                {
                    sb.Append(kv.Value["BandCode"].ToString());
                }
            }
            //
            sb.Append("2331112");
            //
            string str = sb.ToString();
            sb.Clear();
            int flag = 0;
            foreach (char c in str)
            {
                if (flag == 0)
                {
                    flag = 1;
                    for (int i = 0; i < Convert.ToInt16(c.ToString()); i++)
                    {
                        sb.Append("b");
                    }
                }
                else
                {
                    flag = 0;
                    for (int i = 0; i < Convert.ToInt16(c.ToString()); i++)
                    {
                        sb.Append("s");
                    }
                }
            }
            return sb.ToString();
        }


        public static void  CreateBarcodeImage(Graphics g,int x,int y, string CodeType, string str, Single  line_w, int h)
        {

            string bs = "";
            if (CodeType == "A")
            {
                bs = Barcode.Code128A_BS(str);

            }
            else if (CodeType == "C")
            {
                bs = Barcode.Code128C_BS(str);
            }
            else
            {
                bs = Barcode.Code128A_BS(str);
            }

            //
          
          
            Single i = -1; 
            foreach (char c in bs)
            {
                if (c == 'b')
                {
                    i += line_w;
                    g.DrawLine(new Pen(Color.Black, line_w),x+ i, y+0, x+i, y+h);
                }
                else
                {
                    i += line_w;
                }
            }
            
        }




    }
}
