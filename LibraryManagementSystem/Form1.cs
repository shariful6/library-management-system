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
    public partial class Form1 : Form
    {
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\SHARIFUL\Documents\Libraray.mdf;Integrated Security=True;Connect Timeout=30");
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from admin where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'", sql);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {

                this.Hide();
                Main m = new Main();
                m.Show();

                
            }
            else
            {
                MessageBox.Show("Please Check Username and Password");
            }

            sql.Close();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
         
        }
    }
}
