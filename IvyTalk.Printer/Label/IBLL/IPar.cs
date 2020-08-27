namespace IvyTalk.Printer.Label.IBLL
{
    public interface IPar
    {
        string Read(string class_name, string par_name );
        void Write(string class_name, string par_name, string par_value);
    }

}
