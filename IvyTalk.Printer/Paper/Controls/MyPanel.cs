using System.Windows.Forms;

namespace IvyTalk.Printer.Paper.Controls
{
    class MyPanel : System.Windows.Forms.Panel
    {
        public MyPanel()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.AllPaintingInWmPaint, true);
        }

    }
}
