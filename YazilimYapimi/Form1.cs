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
    public partial class frmGirisEkrani : Form
    {
        SqlConnection baglanti = new SqlConnection(Sistem.sqlbag);
        public frmGirisEkrani()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Sistem.frmGirisEkrani = new frmGirisEkrani();
        }

        private void lblUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sistem.frmKayitEkrani = new frmKayitEkrani();
            Sistem.frmKayitEkrani.Show();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {



            if (comboBox1.Text == "Öğrenci")
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand("SELECT * From Tbl_Kullanici where KullaniciAdi = @a1;", baglanti);
                komut.Parameters.AddWithValue("@a1", txtKullaniciAdi.Text);



                SqlDataReader reader = komut.ExecuteReader();
                reader.Read();


                Sistem.KulAd = reader[1].ToString();
                Sistem.Sifre = reader[2].ToString();


                reader.Close();
                komut.ExecuteNonQuery();

                baglanti.Close();
                if ((txtKullaniciAdi.Text == Sistem.KulAd) && (txtSifre.Text == Sistem.Sifre))
                {
                    Sistem.frmOgrenciEkrani = new frmOgrenciEkrani();
                    Sistem.frmOgrenciEkrani.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Girdiğiniz Kullanıcı Adı veya Parola yanlış tülfen tekrar deneyiniz");
                }

            }
            else if(comboBox1.Text == "Sınav Sorumlusu")
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sistem.frmSoruEklemeEkrani = new frmSoruEklemeEkrani();
            Sistem.frmSoruEklemeEkrani.Show();
        }
    }
}
