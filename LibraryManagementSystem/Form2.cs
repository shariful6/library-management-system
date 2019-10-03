using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace LibraryManagementSystem
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\SHARIFUL\Documents\Libraray.mdf;Integrated Security=True;Connect Timeout=30");
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into admin(username,password) values('"+richTextBox1.Text+"','"+richTextBox2.Text+"')",con);
            int a=cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Account Created Successfully");
            }
            con.Close();     
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();

        }
    }
}
