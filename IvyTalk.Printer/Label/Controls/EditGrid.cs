using System;
using System.Data;
using System.Windows.Forms;

namespace IvyTalk.Printer.Label.Controls
{
    public partial class EditGrid : Form,IEditGrid 
    {
        public EditGrid()
        {
            InitializeComponent();
            //
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToAddRows = false;
            foreach (DataGridViewColumn col in this.dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
           
        }

        private void EditGrid_Load(object sender, EventArgs e)
        {

        }

       
        bool IEditGrid.Edit(DataTable tb, out DataTable tb2)
        {
            //

            this.dataGridView1.DataSource = tb.Copy();
            //
            if (this.ShowDialog() == DialogResult.OK)
            {
                tb2 = (DataTable)this.dataGridView1.DataSource;
                return true;
            }
            else
            {

                tb2 = null;
                return false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            //
            this.DialogResult = DialogResult.OK;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.Exception.Message);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                DataGridViewColumn col = this.dataGridView1.Columns[e.ColumnIndex];
                if (col.Name  == colFormat.Name )
                {
                 
                    DataRowView drv =(DataRowView) this.dataGridView1.Rows[e.RowIndex].DataBoundItem;
                    DataRow dr = drv.Row;
                    string format = dr["format"].ToString();
                    IInput input = new InputFormatString();
                    if (input.Input(format, out format) == true)
                    {
                        dr["format"] = format;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            foreach (DataRow row in tb.Rows)
            {
                row["display"] = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            foreach (DataRow row in tb.Rows)
            {
                row["display"] = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv =(DataRowView) this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;
                object[] itemarr = dr.ItemArray;
                tb.Rows.Remove(dr);
                dr.ItemArray = itemarr;
                tb.Rows.InsertAt(dr, 0);
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[0];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;

                int index = tb.Rows.IndexOf(dr);
               
                if (index != 0)
                {
                    index--;
                    object[] itemarr = dr.ItemArray;
                    tb.Rows.Remove(dr);
                    dr.ItemArray = itemarr;
                    tb.Rows.InsertAt(dr, index);
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;

                int index = tb.Rows.IndexOf(dr);

                if (index !=tb.Rows.Count-1)
                {
                    index++;
                    object[] itemarr = dr.ItemArray;
                    tb.Rows.Remove(dr);
                    dr.ItemArray = itemarr;
                    tb.Rows.InsertAt(dr, index);
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var tb = (DataTable)this.dataGridView1.DataSource;
            if (this.dataGridView1.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)this.dataGridView1.CurrentRow.DataBoundItem;
                DataRow dr = drv.Row;
                object[] itemarr = dr.ItemArray;
                tb.Rows.Remove(dr);
                dr.ItemArray = itemarr;
                tb.Rows.Add(dr);
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[tb.Rows.Count-1].Cells[0];
            }
        }

    }
}
