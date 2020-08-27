using System;
using System.Windows.Forms;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class ChangePrintArea : Form,IChangePrintArea 
    {
        public ChangePrintArea()
        {
            InitializeComponent();
        }

        bool IChangePrintArea.Change(int Area, out int Area2)
        {
            this.comboBox1.Text = Area.ToString();
            if (this.ShowDialog() == DialogResult.OK)
            {
                Area2 = Convert.ToInt16(this.comboBox1.Text);
                return true;
            }
            else
            {
                Area2 = 0;
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("未指定");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
