namespace IvyTalk.Printer.Paper.IBLL
{
    public interface IPrint
    {
        void Print(string style_id, string xml, System.Data.DataTable tbmain, System.Data.DataTable tbdetail);
    }
}
