using System.Data;

namespace IvyTalk.Printer.Label.Helper
{
    interface IDrawDetail
    {
        void Draw(System.Drawing.Graphics g, string styleXml, DataTable tbdetail,int pageIndex);
    }
}
