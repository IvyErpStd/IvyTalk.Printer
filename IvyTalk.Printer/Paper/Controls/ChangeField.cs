using System;
using System.Windows.Forms;

namespace IvyTalk.Printer.Paper.Controls
{
    public partial class ChangeField : Form,IChangeField 
    {
        public ChangeField()
        {
            InitializeComponent();
        }

        
        bool IChangeField.Change(string[] Fields, string CurField, out string Field)
        {
            foreach (string f in Fields)
            {
                this.comboBox1.Items.Add(f);
            }
            this.comboBox1.Text = CurField;
            //
            if (this.ShowDialog() == DialogResult.OK)
            {
                Field = this.comboBox1.Text;
                return true;
            }
            else
            {
                Field = "";
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
