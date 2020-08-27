using System.Data;

namespace IvyTalk.Printer.Label.IBLL
{
    public interface IPrint
    {
        //void Print(ISheet sheet);
        void Print(DataTable dtMain, DataTable dtDetail, string sheetType);
    }
}
