using System;
using System.Windows.Forms;

namespace ISСonnectionService
{
    public partial class AddAgreementForm : Form
    {
        public AddAgreementForm()
        {
            InitializeComponent();
            refreshData();
        }

        public void refreshData()
        {
            comboBox1.Items.Clear();
            using (var reader = Program.database.GetReader("select name from Subscribers"))
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
            }

            comboBox2.Items.Clear();
            using (var reader = Program.database.GetReader("select name from Tariffs"))
            {
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.database.ExecuteNonQuery(
                $"insert into Contracts (subscriberName, tariff, price) VALUES ('{comboBox1.Text}', '{comboBox2.Text}', '{Int32.Parse(textBox1.Text)}')");
            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            using (var reader =
                   Program.database.GetReader($"select price from Tariffs where name = '{comboBox2.Text}'"))
            {
                while (reader.Read())
                {
                    textBox1.Text = reader.GetString(0);
                }
            }
        }
    }
}