using System;
using System.Windows.Forms;
using IvyTalk.Printer.Label.Helper;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class SetGridStyle : Form,ISetGridStyle 
    {
        public SetGridStyle()
        {
            InitializeComponent();
        }

        private GridStyleInfo info
        {
            get
            {
                GridStyleInfo res = new GridStyleInfo();
                res.columnHeight = Conv.ToInt16(textBox1.Text.Trim());
                res.rowHeight = Conv.ToInt16(textBox2.Text.Trim());
                if (checkBox1.Checked == true)
                {
                    res.autoRow = 1;
                }
                else
                {
                    res.autoRow = 0;
                }
                if (checkBox2.Checked == true)
                {

                    res.smallTotal = 1;
                }
                else
                {
                    res.smallTotal = 0;
                }
                res.smallTotalFields = textBox3.Text.Trim();
                return res;
            }
            set
            {
                textBox1.Text = value.columnHeight.ToString();
                textBox2.Text = value.rowHeight.ToString();
                if (value.autoRow == 1)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if (value.smallTotal == 1)
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                textBox3.Text = value.smallTotalFields;
            }
        }

        bool ISetGridStyle.SetStyle(GridStyleInfo info, out GridStyleInfo info2)
        {
            this.info = info;
            if (this.ShowDialog() == DialogResult.OK)
            {
                info2 = this.info;
                return true;
            }
            else
            {
                info2 = null;
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int columnHeight=0;
                int rowHeight=0;
                int.TryParse(textBox1.Text, out columnHeight);
                int.TryParse(textBox2.Text, out rowHeight);
                if (columnHeight < 10)
                {
                    throw new Exception("列高不正确");
                }
                if (rowHeight < 10)
                {
                    throw new Exception("行高不正确");
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

    }
}
