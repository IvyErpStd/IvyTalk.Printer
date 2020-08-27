using System;
using System.Data;

namespace IvyTalk.Printer.Label
{
    public class PrintP : IBLL.IPrint
    {
        string path;
        string name;
        public PrintP() { }
        public PrintP(string path, string name)
        {
            this.path = path;
            this.name = name;
        }
        void IBLL.IPrint.Print(DataTable dtMain, DataTable dtDetail, string sheetType)
        {
            try
            {

                string file = path + "\\print_style\\" + sheetType + ".xml";
                string xml = System.IO.File.ReadAllText(file, System.Text.Encoding.GetEncoding("gb2312"));

                PrintV pv = new PrintV();
                pv.Print(xml, dtMain, dtDetail, name);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
