namespace IvyTalk.Printer.Label.Controls
{
    interface IChangeField
    {
        bool Change(string[] Fields, string CurField, out string Field);
    }
}
