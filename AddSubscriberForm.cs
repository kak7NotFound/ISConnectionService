using System;
using System.Windows.Forms;

namespace ISСonnectionService
{
    public partial class AddSubscriberForm : Form
    {
        public AddSubscriberForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.database.ExecuteNonQuery($"insert into Subscribers (name, passport, location) VALUES ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}')");
            Close();
        }
    }
}