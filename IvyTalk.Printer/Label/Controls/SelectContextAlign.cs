using System;
using System.Windows.Forms;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class SelectContextAlign : Form,ISelectContextAlign 
    {
        public SelectContextAlign()
        {
            InitializeComponent();
        }

        bool ISelectContextAlign.Select(int def, out int res)
        {
            this.comboBox1.Text = def.ToString();
            if (this.ShowDialog() == DialogResult.OK)
            {
                res = Convert.ToInt16(this.comboBox1.Text);
                return true;
            }
            else
            {
                res = 0;
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("未选择对齐方式");
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
