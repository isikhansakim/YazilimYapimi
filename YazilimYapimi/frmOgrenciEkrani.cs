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
    
    public partial class frmOgrenciEkrani : Form
    {
        SqlConnection baglanti = new SqlConnection(Sistem.sqlbag);
        public frmOgrenciEkrani()
        {
            InitializeComponent();
        }
        
        private void SoruGetir()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select top 1 * from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            da.SelectCommand.ExecuteNonQuery();
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            for (int i = 0; i < 15; i++)
                tablo.Columns.RemoveAt(8);

            tablo.Columns.RemoveAt(0);

            SqlDataAdapter da1 = new SqlDataAdapter("select top 1 Soru_Metni, Soru_Resmi from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            da1.SelectCommand.ExecuteNonQuery();
            DataTable tablo1 = new DataTable();
            da1.Fill(tablo1);
            dataGridView2.DataSource = tablo1;

            SqlCommand k = new SqlCommand("select Soru_AResim from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            k.ExecuteNonQuery();
            SqlDataReader resimr = k.ExecuteReader();

       
            if (resimr.HasRows)
            {
                resimr.Close();
                SqlDataAdapter da2 = new SqlDataAdapter("select top 1 Soru_SIKA, Soru_AResim, Soru_SIKB,Soru_BResim, Soru_SIKC,Soru_CResim, Soru_SIKD,Soru_DResim from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
                da2.SelectCommand.ExecuteNonQuery();
                DataTable tablo2 = new DataTable();
                da2.Fill(tablo2);
                dataGridView3.DataSource = tablo2;
            }
            else
            {
                resimr.Close();
                SqlDataAdapter da2 = new SqlDataAdapter("select top 1 Soru_SIKA, Soru_AResim, Soru_SIKB,Soru_BResim, Soru_SIKC,Soru_CResim, Soru_SIKD,Soru_DResim  from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
                da2.SelectCommand.ExecuteNonQuery();
                DataTable tablo2 = new DataTable();
                da2.Fill(tablo2);
                dataGridView3.DataSource = tablo2;
                
            }
            
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            dataGridView3.Visible = true;
            
            SoruGetir();
        }

        private void frmOgrenciEkrani_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            //label1.Visible = false;
            baglanti.Open();

            Sistem sistem = new Sistem();
            SqlCommand sorual = new SqlCommand("SELECT TOP 10 soru_id FROM tbl_sorular where Soru_SorulduMu = '0' ORDER BY NEWID()", baglanti);
            SqlDataReader idreader = sorual.ExecuteReader();
            List<int> Soru_id = new List<int>();
            while (idreader.Read())
            {
                Soru_id.Add(Convert.ToInt32(idreader[0].ToString()));
            }
            idreader.Close();

            for (int i = 0; i < 10; i++)
            {
                SqlCommand sinavekle = new SqlCommand("insert into tbl_sinavsorular (sorular_soru_id) values (@a1)", baglanti);
                sinavekle.Parameters.AddWithValue("@a1", Soru_id[i]);
                sinavekle.ExecuteNonQuery();
            }
            
            baglanti.Close();



        }   
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sistem.frmGirisEkrani.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cevap = new SqlCommand("select top 1 * from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            cevap.ExecuteNonQuery();
            SqlDataReader cr = cevap.ExecuteReader();
            cr.Read();
            string yanit = cr[8].ToString();
            int soru_id = Convert.ToInt32(cr[0].ToString());
            cr.Close();

            SqlCommand ogr = new SqlCommand("select * from Tbl_Ogrenci join Tbl_Kullanici on Tbl_Ogrenci.Kullanici_id = Tbl_Kullanici.Kullanici_id where Tbl_Kullanici.KullaniciAdi = @w1", baglanti);
            ogr.Parameters.AddWithValue("@w1", Sistem.KulAd);
            ogr.ExecuteNonQuery();
            SqlDataReader ogridr = ogr.ExecuteReader();
            ogridr.Read();
            int ogrid = Convert.ToInt32(ogridr[1].ToString());
            ogridr.Close();

            SqlCommand bilsay = new SqlCommand("select * from Tbl_DogruSorular join Tbl_Ogrenci on Tbl_DogruSorular.Ogr_id = Tbl_Ogrenci.Ogr_id where Tbl_Ogrenci.Ogr_id = @e1", baglanti);
            bilsay.Parameters.AddWithValue("@e1", ogrid);
            bilsay.ExecuteNonQuery();
            SqlDataReader bsr = bilsay.ExecuteReader();
            int bilinmeSayisi = 0;
            if (bsr.Read())
            {
                bilinmeSayisi = Convert.ToInt32(bsr[2].ToString());
            }
            bsr.Close();



            if (yanit == label1.Text)
            {
                SqlCommand dogru = new SqlCommand("insert into tbl_DogruSorular (Soru_id, Ogr_id, BilinmeSayisi, BilindigiGun) values (@d1, @d2, @d3, @d4)", baglanti);
                dogru.Parameters.AddWithValue("@d1", soru_id);
                dogru.Parameters.AddWithValue("@d2", ogrid);
                dogru.Parameters.AddWithValue("@d3", bilinmeSayisi);
                dogru.Parameters.AddWithValue("@d4", DateTime.Now);
                dogru.ExecuteNonQuery();
            }
            baglanti.Close();


            baglanti.Open();
            SqlCommand sorusil = new SqlCommand("delete top (1) from Tbl_SinavSorular ", baglanti);
            sorusil.ExecuteNonQuery();
            baglanti.Close();
            baglanti.Open();
            SqlCommand q = new SqlCommand("select * from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            q.ExecuteNonQuery();
            SqlDataReader r = q.ExecuteReader();

            List<int> l = new List<int>();
            while (r.Read())
            {
                l.Add(Convert.ToInt32(r[0].ToString()));
            }
            if (l.Count == 0)
            {
                MessageBox.Show("Sınavınız bitmiştir");
                baglanti.Close();
            }
            else if (l.Count == 1)
            {
                button2.Text = "Bitir";
                baglanti.Close();
                SoruGetir();
            }
            else
            {
                baglanti.Close();
                SoruGetir();
            }
            
        }

        private void frmOgrenciEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            baglanti.Open();
            SqlCommand sinavsil = new SqlCommand("delete Tbl_SinavSorular where 1 = 1",baglanti);
            sinavsil.ExecuteNonQuery();
            baglanti.Close();
        }
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "A";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "B";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "C";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "D";
        }
    }
}
