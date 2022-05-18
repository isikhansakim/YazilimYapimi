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
            for (int i = 0; i < 14; i++)
                tablo.Columns.RemoveAt(8);

            tablo.Columns.RemoveAt(0);

            SqlDataAdapter da1 = new SqlDataAdapter("select top 1 Soru_Metni from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            da1.SelectCommand.ExecuteNonQuery();
            DataTable tablo1 = new DataTable();
            da1.Fill(tablo1);
            dataGridView2.DataSource = tablo1;



            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Store Auto Sized Widths:
            int colw = dataGridView2.Columns[0].Width;

            // Remove AutoSizing:
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // Set Width to calculated AutoSize value:
            dataGridView2.Columns[0].Width = colw;



            SqlDataAdapter da2 = new SqlDataAdapter("select top 1 Soru_SIKA,Soru_SIKB,Soru_SIKC,Soru_SIKD from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            da2.SelectCommand.ExecuteNonQuery();
            DataTable tablo2 = new DataTable();
            da2.Fill(tablo2);
            dataGridView3.DataSource = tablo2;

            dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView3.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Now that DataGridView has calculated it's Widths; we can now store each column Width values.
            for (int i = 0; i <= dataGridView3.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw1 = dataGridView3.Columns[i].Width;

                // Remove AutoSizing:
                dataGridView3.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // Set Width to calculated AutoSize value:
                dataGridView3.Columns[i].Width = colw1;
            }

            SqlCommand resimGetir = new SqlCommand("select top 1 Soru_Resmi, Soru_AResim, Soru_BResim, Soru_CResim, Soru_DResim from tbl_sorular join tbl_sinavsorular on tbl_sorular.Soru_id = tbl_sinavsorular.sorular_soru_id", baglanti);
            SqlDataReader resimOku = resimGetir.ExecuteReader();
            if (resimOku.Read())
            {
                pictureBox1.ImageLocation = resimOku[0].ToString();
                pictureBox2.ImageLocation = resimOku[1].ToString();
                pictureBox3.ImageLocation = resimOku[2].ToString();
                pictureBox4.ImageLocation = resimOku[3].ToString();
                pictureBox5.ImageLocation = resimOku[4].ToString();
            }

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            dataGridView3.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            button2.Visible = true;


            SoruGetir();
        }
        List<DateTime> sorulacakGun = new List<DateTime>();

        private void eklemesayısı(int x, DateTime bilindigiGun)
        {
            int[] dizi = { 1, 6, 23, 60, 90, 180};
                              
            int donendeger = dizi[x - 1];
            bilindigiGun = bilindigiGun.AddDays(donendeger);

            sorulacakGun.Add(bilindigiGun);

        }

        private void frmOgrenciEkrani_Load(object sender, EventArgs e)
        {
            button1.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            label1.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            button2.Visible = false;

            List<int> soru_id = new List<int>();
            List<int> bilinmeSayi = new List<int>();
            List<DateTime> bilindigiGun = new List<DateTime>();
            baglanti.Open();

            SqlCommand dogrugetir = new SqlCommand("select * from Tbl_Ogrenci join Tbl_DogruSorular on Tbl_Ogrenci.Ogr_id = Tbl_DogruSorular.Ogr_id join Tbl_Kullanici on Tbl_Ogrenci.Kullanici_id = Tbl_Kullanici.Kullanici_id where Tbl_Kullanici.KullaniciAdi = @t1", baglanti);
            dogrugetir.Parameters.AddWithValue("@t1", Sistem.KulAd);
            dogrugetir.ExecuteNonQuery();

            //bilindiği gün
            SqlDataReader sbgr = dogrugetir.ExecuteReader();
            while (sbgr.Read())
            {
                bilindigiGun.Add(Convert.ToDateTime(sbgr[7].ToString()));
            }
            sbgr.Close();
            //bilinme sayisi
            SqlDataReader bsg = dogrugetir.ExecuteReader();
            while (bsg.Read())
            {
                bilinmeSayi.Add(Convert.ToInt32(bsg[6].ToString()));
            }

            bsg.Close();


            int listLength = bilinmeSayi.Count;

            for (int i = 0; i < listLength; i++)
            {
                eklemesayısı(Convert.ToInt32(bilinmeSayi[i].ToString()), bilindigiGun[i].Date);
            }

            //soru_id
            SqlDataReader dgr = dogrugetir.ExecuteReader();
            while (dgr.Read())
            {
                soru_id.Add(Convert.ToInt32(dgr[4].ToString()));
            }
            dgr.Close();

            for (int i = 0; i < listLength; i++)
            {
                if (DateTime.Now.Date == sorulacakGun[i].Date)
                {
                    SqlCommand sinavekle1 = new SqlCommand("insert into tbl_sinavsorular (sorular_soru_id) values (@a1)", baglanti);
                    sinavekle1.Parameters.AddWithValue("@a1", soru_id[i]);
                    sinavekle1.ExecuteNonQuery();
                }
            }

            //önceden bilinmemiş rastgele 10 soru getir
            SqlCommand sorual = new SqlCommand("select top 10 * from Tbl_Sorular left join Tbl_DogruSorular on Tbl_Sorular.Soru_id = Tbl_DogruSorular.Soru_id where Tbl_DogruSorular.BilinmeSayisi is null order by newid()", baglanti);
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

            
            SqlCommand sorunluisaretle = new SqlCommand("UPDATE Tbl_DogruSorular SET Silinecek = '1' FROM Tbl_DogruSorular INNER JOIN Tbl_SinavSorular ON sorular_soru_id = Soru_id WHERE Tbl_DogruSorular.Ogr_id = '1' and Soru_id = sorular_soru_id", baglanti);
            sorunluisaretle.ExecuteNonQuery();
            

            baglanti.Close();
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

            SqlCommand bilsay = new SqlCommand("select * from Tbl_DogruSorular join Tbl_Ogrenci on Tbl_DogruSorular.Ogr_id = Tbl_Ogrenci.Ogr_id where Tbl_Ogrenci.Ogr_id = @e1 and Tbl_DogruSorular.soru_id = @e2", baglanti);
            bilsay.Parameters.AddWithValue("@e1", ogrid);
            bilsay.Parameters.AddWithValue("@e2", soru_id);
            bilsay.ExecuteNonQuery();
            SqlDataReader bsr = bilsay.ExecuteReader();
            int bilinmeSayisi = 1;
            if (bsr.Read())
            {
                bilinmeSayisi = Convert.ToInt32(bsr[2].ToString());
                bilinmeSayisi++;
            }

            bsr.Close();



            if (yanit == label1.Text)
            {
                SqlCommand dogru = new SqlCommand("insert into tbl_DogruSorular (Soru_id, Ogr_id, BilinmeSayisi, BilindigiGun) values (@d1, @d2, @d3, @d4)", baglanti);
                dogru.Parameters.AddWithValue("@d1", soru_id);
                dogru.Parameters.AddWithValue("@d2", ogrid);
                dogru.Parameters.AddWithValue("@d3", bilinmeSayisi);
                dogru.Parameters.AddWithValue("@d4", DateTime.Now.Date);
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
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
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
            SqlCommand sinavsil = new SqlCommand("delete Tbl_SinavSorular where 1 = 1", baglanti);
            sinavsil.ExecuteNonQuery();

            //Silinecek dogruSoruları silme işlemi
            SqlCommand sorunlusil = new SqlCommand("delete Tbl_DogruSorular where Tbl_DogruSorular.Silinecek = 1", baglanti);
            sorunlusil.ExecuteNonQuery();
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

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sistem.frmOgrenciAyarlar = new frmOgrenciAyarlar();
            Sistem.frmOgrenciAyarlar.Show();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sistem.frmGirisEkrani.Show();
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}