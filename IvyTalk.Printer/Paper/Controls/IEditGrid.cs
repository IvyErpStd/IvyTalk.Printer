namespace IvyTalk.Printer.Paper.Controls
{
    interface IEditGrid
    {
        bool Edit(System.Data.DataTable tb, GridStyleInfo info, out System.Data.DataTable tb2, out GridStyleInfo info2);
    }
}
