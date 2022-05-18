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
    public partial class frmSifremiUnuttum : Form
    {
        SqlConnection baglanti = new SqlConnection(Sistem.sqlbag);
        public frmSifremiUnuttum()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            baglanti.Open();
            SqlCommand sifrebul = new SqlCommand("select Sifre from Tbl_Kullanici where KullaniciAdi = @k1", baglanti);
            sifrebul.Parameters.AddWithValue("@k1", textBox1.Text);
            SqlDataReader sifreoku = sifrebul.ExecuteReader();
            if (sifreoku.Read())
            {
                string sifre = sifreoku[0].ToString();
                label3.Text = sifre;

            }

            baglanti.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            this.Hide();
        }
    }
}
