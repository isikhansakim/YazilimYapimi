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



namespace YazilimYapimi
{
   
    public partial class frmSoruEklemeEkrani : Form
    {
        SqlConnection baglanti = new SqlConnection(Sistem.sqlbag);
        public frmSoruEklemeEkrani()
        {
            InitializeComponent();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            lblSoruResim.Text = openFileDialog1.FileName;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            AsikkiPic.ImageLocation = openFileDialog1.FileName;  
            lblAResim.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            BsikkiPic.ImageLocation = openFileDialog1.FileName;
            lblBResim.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            CsikkiPic.ImageLocation = openFileDialog1.FileName;
            lblCResim.Text = openFileDialog1.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            DsikkiPic.ImageLocation = openFileDialog1.FileName;
            lblDResim.Text = openFileDialog1.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label12.Text = "A";
            }
            else if (radioButton3.Checked)
            {
                label12.Text = "B";
            }
            else if (radioButton2.Checked)
            {
                label12.Text = "C";
            }
            else if (radioButton4.Checked)
            {
                label12.Text = "D";
            }
            else
            {
                MessageBox.Show("Doğru cevabı belirtiniz.");
                //yap burayı başa döndür kapattırma falan anladın sen ;)
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Sorular (Soru_UniteNo, Soru_KonuNo, Soru_No, Soru_Sinifi, Soru_Dersi, Soru_Unitesi, Soru_Konusu, Soru_Metni, Soru_Resmi, Soru_SIKA, Soru_AResim,  Soru_SIKB, Soru_BResim, Soru_SIKC, Soru_CResim, Soru_SIKD, Soru_DResim, Soru_Cevap) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@b1,@b2,@b3,@b4,@b5,@b6,@b7,@b8,@c1)", baglanti);
            komut.Parameters.AddWithValue("@a1", Unitetxt.Text);
            komut.Parameters.AddWithValue("@a2", KonuTxt.Text);
            komut.Parameters.AddWithValue("@a3", SoruNoTxt.Text);
            komut.Parameters.AddWithValue("@a4", SoruSinifiTxt.Text);
            komut.Parameters.AddWithValue("@a5", DersTxt.Text);
            komut.Parameters.AddWithValue("@a6", UniteAdTxt.Text);
            komut.Parameters.AddWithValue("@a7", KonuAdiTxt.Text);
            komut.Parameters.AddWithValue("@a8", richTextBox1.Text);
            komut.Parameters.AddWithValue("@a9", lblSoruResim.Text);
            komut.Parameters.AddWithValue("@b1", AsikkiTxt.Text);
            komut.Parameters.AddWithValue("@b2", lblAResim.Text);
            komut.Parameters.AddWithValue("@b3", BsikkiTxt.Text);
            komut.Parameters.AddWithValue("@b4", lblBResim.Text);
            komut.Parameters.AddWithValue("@b5", CsikkiTxt.Text);
            komut.Parameters.AddWithValue("@b6", lblCResim.Text);
            komut.Parameters.AddWithValue("@b7", DsikkiTxt.Text);
            komut.Parameters.AddWithValue("@b8", lblDResim.Text);
            komut.Parameters.AddWithValue("@c1", label12.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Soru eklendi."); 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sistem.frmGirisEkrani.Show();
            this.Hide();
        }

        
    }
}
