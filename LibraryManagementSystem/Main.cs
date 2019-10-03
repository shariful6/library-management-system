using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public partial class Main : Form
    {
        DataTable dt;
        string std_id;
    
        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\SHARIFUL\Documents\Libraray.mdf;Integrated Security=True;Connect Timeout=30");
        public Main()
        {
         
            InitializeComponent();
            show_book();
            student();
            show_lend();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();
            SqlCommand cmd = new SqlCommand("insert into book_list(book_name,writer_name,quantity,dept) values('"+richTextBox1.Text+"','"+richTextBox2.Text+"','"+richTextBox3.Text+"','"+comboBox1.Text+"')",sql);
            int a = cmd.ExecuteNonQuery();

            if (a > 0)
            {
                MessageBox.Show("Book Successfully Added !");
            }
            else
            {
                MessageBox.Show("Failed To Save Book!!!");
            }
            sql.Close();
            show_book();
        }

        public void show_book()
        {
            sql.Close();
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from book_list",sql);
           dataGridView1.Rows.Clear();
             dt = new DataTable();
            sda.Fill(dt);
            foreach(DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value=item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value=item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value=item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value=item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value=item[4].ToString();
            }



           sql.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();
            SqlCommand cmd = new SqlCommand("insert into registration(id,name,date,dept,image) values ('"+richTextBox6.Text+"','"+richTextBox5.Text+"','"+dateTimePicker1.Text+"','"+comboBox2.Text+"',@image)",sql);
            cmd.Parameters.AddWithValue("@image",savephoto());
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Registration Successfull");
            }

            else
            {
                MessageBox.Show("Failed To Registration !!");
            }


            sql.Close();
            student();


        }

        public byte[] savephoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox3.Image.Save(ms,pictureBox3.Image.RawFormat);
            return ms.GetBuffer();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Filter = "Image File(*.png;*.jpg)|*.png;*.jpg";
            if (op.ShowDialog() == DialogResult.OK)
            {

                pictureBox3.Image=new Bitmap(op.FileName);
            }
        }

        void student()
        {
            sql.Close();
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from registration", sql);
            dataGridView2.Rows.Clear();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView2.Rows[n].Cells[4].Value=item[4];
            }


            sql.Close();
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            try
            {
                std_id = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                string name = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                string dept = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
               
               
                richTextBox4.Text = std_id;
                richTextBox7.Text = name;
                comboBox3.Text = dept;
                MemoryStream ms = new MemoryStream((byte[])dataGridView2.SelectedRows[0].Cells[4].Value);
                Image im = Image.FromStream(ms);
                pictureBox4.Image = im;
            }
            catch
            {

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {

            sql.Close();
            sql.Open();
            SqlCommand cmd = new SqlCommand("Update registration set id='" + richTextBox4.Text + "',name='" + richTextBox7.Text + "',dept='" + comboBox3.Text + "',image=@image where id='"+std_id+"'", sql);
            cmd.Parameters.AddWithValue("@image",savephoto2());
            cmd.ExecuteNonQuery();
            student();


            sql.Close();
        }

        public byte[] savephoto2()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox4.Image.Save(ms, pictureBox4.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Filter = "Image File(*.png;*.jpg)|*.png;*.jpg";
            if (op.ShowDialog() == DialogResult.OK)
            {

                pictureBox4.Image = new Bitmap(op.FileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();

            SqlCommand cmd = new SqlCommand("Update book_list set quantity=(quantity.toInt32()-1) where Book_id='"+richTextBox9.Text+"'",sql);
             cmd = new SqlCommand("insert into lend(std_id,book_id,book_name,writer_name,date) values('" + richTextBox11.Text + "','" + richTextBox9.Text + "','" + richTextBox10.Text + "','" + richTextBox8.Text + "','" + dateTimePicker2 .Text+ "')", sql);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Successfully lended!!");
            }

            else
            {
                MessageBox.Show("Failed To Lend");
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
           string book_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string writer = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            richTextBox9.Text = book_id;
            richTextBox10.Text = name;
            richTextBox8.Text = writer;
            
               
               
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
           //dummy data
        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            
            string st_id =  dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            string book_id = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            label21.Text=book_id;
            label22.Text=st_id;


        }

        void show_lend()
        {
            sql.Close();
            sql.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from lend", sql);
            dataGridView3.Rows.Clear();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView3.Rows.Add();
                dataGridView3.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView3.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView3.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView3.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView3.Rows[n].Cells[4].Value = item[4].ToString();
            }


            sql.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();
            SqlCommand cmd = new SqlCommand("delete from lend where book_id='"+label21.Text+"' and std_id='"+label22.Text+"'",sql);
            cmd.ExecuteNonQuery();
            sql.Close();
            label21.Text = "";
            label22.Text = "";
            show_lend();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();
            SqlCommand cmd = new SqlCommand("delete from registration where id='"+richTextBox4.Text+"'",sql);
            int a = cmd.ExecuteNonQuery();

            if (a > 0)
            {
                MessageBox.Show("Successfully Deleted ");
            }
            else
            {
                MessageBox.Show("Failed To Delete!!");
            }
            sql.Close();
            student();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("book_name like '%{0}%'",textBox1.Text);
            dataGridView1.DataSource = dv;
        }

    }
}
