using System;
using System.Drawing;
using System.Windows.Forms;
using IvyTalk.Printer.Label.Helper;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class InputSizeForPage : Form,IInputSize 
    {
        public InputSizeForPage()
        {
            InitializeComponent();
        }


        private void InputSizeForPage_Load(object sender, EventArgs e)
        {
            
        }



        bool IInputSize.Input(Size OriSize, out Size Size)
        {
            this.textBox1.Text = (OriSize.Width/Conv.getAnCMInterval()).ToString("0.##");
            this.textBox2.Text = (OriSize.Height/Conv.getAnCMInterval()).ToString("0.##");
            if (this.ShowDialog() == DialogResult.OK)
            {
                Size = new Size((int)width, (int)height);
                return true;
            }
            else
            {
                Size = new Size(0,0);
                return false;
            }

        }

        decimal width, height;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                decimal.TryParse(this.textBox1.Text, out width);
                decimal.TryParse(this.textBox2.Text, out height);
                width = width * Conv.ToDecimal(Conv.getAnCMInterval());
                height = height* Conv.ToDecimal(Conv.getAnCMInterval());

                if (width < 50)
                {
                    throw new Exception("宽度不正确");
                }
                if (height < 50)
                {
                    throw new Exception("高度不正确");
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            this.contextMenuStrip1.Show(button3, e.X, e.Y);
        }

        private void a4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "21";
            this.textBox2.Text = "29.7";
        }
    }
}
