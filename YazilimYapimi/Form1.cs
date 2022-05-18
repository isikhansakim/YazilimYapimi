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
            baglanti.Open();


            //veritabanındaki bilgileri alma
            SqlCommand komut = new SqlCommand("SELECT * From Tbl_Kullanici where KullaniciAdi = @a1;", baglanti);
            komut.Parameters.AddWithValue("@a1", txtKullaniciAdi.Text);
            komut.ExecuteNonQuery();

            //veritabanındaki verileri okuma
            SqlDataReader reader = komut.ExecuteReader();
            if (reader.Read())
            {
                //okudğumuz verileri programa kaydetme
                Sistem.KulAd = reader[1].ToString();
                Sistem.Sifre = reader[2].ToString();
            }
            reader.Close();
            komut.ExecuteNonQuery();

            //Öğrenci idsini alma
            SqlCommand command = new SqlCommand("select * from Tbl_Kullanici join Tbl_Ogrenci on Tbl_Kullanici.Kullanici_id = Tbl_Ogrenci.Kullanici_id where Tbl_Kullanici.KullaniciAdi = @k1", baglanti);
            command.Parameters.AddWithValue("@k1", txtKullaniciAdi.Text);
            SqlDataReader reader1 = command.ExecuteReader();

            //Girdiğimiz kullanıcıya ait bir veri var mı
            if (reader1.Read()) {
                //Girdiğimiz kullanıcıya ait bir öğrenci id var mı
                if (reader1[6].ToString() != null)
                {
                    reader1.Close();

                    //girilen şifre ile veritabanındaki şifrenin kontrolü
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
            }
            reader1.Close();

            //Sınav sorumlusu idsini alma
            SqlCommand command1 = new SqlCommand("select * from Tbl_Kullanici join Tbl_SinavSorumlusu on Tbl_Kullanici.Kullanici_id = Tbl_SinavSorumlusu.Kullanici_id where Tbl_Kullanici.KullaniciAdi = @l1", baglanti);
            command1.Parameters.AddWithValue("@l1", txtKullaniciAdi.Text);
            SqlDataReader reader2 = command1.ExecuteReader();
            
            //girdiğimiz kullanıcıya ait bir veri var mı
            if (reader2.Read()) { 
                //girdiğimiz kullanıcıya ait bir sorumlu_id var mı
                if (reader2[6].ToString() != null)
                {
                    reader2.Close();

                    //girilen şifre ile veritabanındaki şifrenin kontrolü
                    if ((txtKullaniciAdi.Text == Sistem.KulAd) && (txtSifre.Text == Sistem.Sifre))
                    {
                        Sistem.frmSoruEklemeEkrani = new frmSoruEklemeEkrani();
                        Sistem.frmSoruEklemeEkrani.Show();
                        this.Hide();
                    }

                    else
                    {
                        MessageBox.Show("Girdiğiniz Kullanıcı Adı veya Parola yanlış tülfen tekrar deneyiniz");
                    }
                }
            }
            baglanti.Close();

            if ((txtKullaniciAdi.Text == "admin") && (txtSifre.Text == "admin"))
            {
                frmAdminSecim f = new frmAdminSecim();
                f.Show();
                this.Hide();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSifremiUnuttum frmSifremiUnuttum = new frmSifremiUnuttum();
            frmSifremiUnuttum.Show();
        }
    }
}
