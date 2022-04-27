using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ISСonnectionService
{
    public partial class DataReaderForm : Form
    {
        public DataReaderForm()
        {
            InitializeComponent();
            refreshData();
        }

        public void refreshData()
        {
            dataGridView1.Rows.Clear();
            List<string> data = new List<string>();
            
            using (var reader = Program.database.GetReader("select * from Contracts"))
            {
                while (reader.Read())
                {
                    data.Add(reader.GetString(0));
                    data.Add(reader.GetString(1));
                    data.Add(reader.GetInt32(2).ToString());
                    using (var r1 = Program.database.GetReader($"select term from Tariffs"))
                    {
                        while (r1.Read())
                        {
                            data.Add(r1.GetString(0));
                        }
                    }
                    using (var r2 = Program.database.GetReader($"select passport, location from Subscribers"))
                    {
                        while (r2.Read())
                        {
                            data.Add(r2.GetString(0));
                            data.Add(r2.GetString(1));
                        }
                    }
                    dataGridView1.Rows.Add(data[0], data[1], data[2],data[3],data[4],data[5]);
                    data.Clear();
                }
                
            }


            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.database.ExecuteNonQuery($"delete from Contracts where subscriberName = '{dataGridView1.CurrentRow.Cells[0].Value}' and tariff = '{dataGridView1.CurrentRow.Cells[1].Value}'");
            refreshData();
        }
    }
}