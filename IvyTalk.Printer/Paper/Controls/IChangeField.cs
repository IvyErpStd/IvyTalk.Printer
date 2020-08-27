namespace IvyTalk.Printer.Paper.Controls
{
    interface IChangeField
    {
        bool Change(string[] Fields, string CurField, out string Field);
    }
}
