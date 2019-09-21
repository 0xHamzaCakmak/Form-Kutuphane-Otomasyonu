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

namespace Datagrid_ekleme_silme_kutuphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglantı = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Kütüphane;Integrated Security=True");

        private void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler,baglantı);
            DataSet ds = new DataSet();da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("select * from Kitap");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into Kitap (Ad, yazar,sayfano,basınevi,tür) values (@adi,@yazari,@sayfanosu,@basımevi,@türü)", baglantı);
            komut.Parameters.AddWithValue("@adi", textBox1.Text);
            komut.Parameters.AddWithValue("@yazari", textBox2.Text);
            komut.Parameters.AddWithValue("@sayfanosu", textBox3.Text);
            komut.Parameters.AddWithValue("@basımevi", textBox4.Text);
            komut.Parameters.AddWithValue("@türü", textBox7.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select * from Kitap");
            baglantı.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox3.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("delete from Kitap where ad=@adi", baglantı);
            komut.Parameters.AddWithValue("@adi", textBox5.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select * from Kitap");
            baglantı.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("select * from Kitap where Ad like '%" + textBox6.Text + "%'", baglantı);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds =new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglantı.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string ad = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string yazar = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string sayfano = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string basımevi = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            string tur = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();

            textBox1.Text = ad;
            textBox2.Text = yazar;
            textBox3.Text = sayfano;
            textBox4.Text = basımevi;
            textBox5.Text = ad;
            textBox7.Text = tur;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("update Kitap set yazar='"+textBox2.Text+ "',sayfano='" + textBox3.Text + "',basınevi='" + textBox4.Text + "',tür='" + textBox7.Text + "' where ad='"+textBox1.Text+"'",baglantı);
            komut.ExecuteNonQuery();
            verilerigoster("select * from Kitap");
            baglantı.Close();

        }
    }
}
