using System;
using System.Windows.Forms;

namespace ISСonnectionService
{
    public partial class TariffEditorForm : Form
    {
        public TariffEditorForm()
        {
            InitializeComponent();
            refreshData();
        }

        public void refreshData()
        {
            dataGridView1.Rows.Clear();
            using (var reader = Program.database.GetReader("select * from Tariffs"))
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.GetString(2));
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Program.database.ExecuteNonQuery(
                $"insert into Tariffs (name, price, term) VALUES ('{textBox1.Text}', '{numericUpDown1.Value}', '{dateTimePicker1.Value.Day}.{dateTimePicker1.Value.Month}.{dateTimePicker1.Value.Year}')");
            refreshData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.database.ExecuteNonQuery($"delete from Tariffs where name = '{dataGridView1.CurrentRow.Cells[0].Value}'");
            refreshData();
        }
    }
}