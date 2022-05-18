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
{    public partial class frmKayitEkrani : Form

    {
        SqlConnection baglanti = new SqlConnection(Sistem.sqlbag);
        public frmKayitEkrani()
        {
            InitializeComponent();
        }

        private void btnUyeOl_Click(object sender, EventArgs e)
        {
                   
            baglanti.Open();
            
            //
            SqlCommand komut = new SqlCommand("insert into tbl_Kullanici (KullaniciAdi,Sifre,Adi,Soyadi) values (@a1,@a2,@a3,@a4)", baglanti);
            komut.Parameters.AddWithValue("@a1", txtKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@a2", txtSifre.Text);
            komut.Parameters.AddWithValue("@a3", txtAd.Text);
            komut.Parameters.AddWithValue("@a4", txtSoyad.Text);
            komut.ExecuteNonQuery();
            SqlCommand komut1 = new SqlCommand("select Kullanici_id from Tbl_Kullanici where KullaniciAdi = @b1", baglanti);
            komut1.Parameters.AddWithValue("@b1", txtKullaniciAdi.Text);

            SqlDataReader reader = komut1.ExecuteReader();
            reader.Read();


            string Kulid = reader[0].ToString();


            reader.Close();
            komut1.ExecuteNonQuery();
            
            
            baglanti.Close();
            if(comboBox2.Text == "Öğrenci") { 
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("insert into Tbl_Ogrenci (Kullanici_id,Ogr_Sinif ,KayitTarihi) values (@c1,@c2,@c3)", baglanti);
                komut2.Parameters.AddWithValue("@c1", Convert.ToInt32(Kulid));
                komut2.Parameters.AddWithValue("@c2", Convert.ToInt32(comboBox1.Text));
                komut2.Parameters.AddWithValue("@c3", DateTime.Now.Date);
                komut2.ExecuteNonQuery();
                baglanti.Close();
            }

            if (comboBox2.Text == "Sınav Sorumlusu")
            {
                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("insert into Tbl_SinavSorumlusu (Kullanici_id,KayitTarihi) values (@d1,@d2)", baglanti);
                komut3.Parameters.AddWithValue("@d1", Convert.ToInt32(Kulid));
                komut3.Parameters.AddWithValue("@d2", DateTime.Now.Date);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }

            MessageBox.Show("Kaydınız başarıyla gerçekleşmiştir.");
            this.Hide();
        }
    }
}
