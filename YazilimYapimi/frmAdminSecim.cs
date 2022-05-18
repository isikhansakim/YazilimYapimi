using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazilimYapimi
{
    public partial class frmAdminSecim : Form
    {
        public frmAdminSecim()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sistem.frmGirisEkrani.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sistem.frmOgrenciEkrani = new frmOgrenciEkrani();
            Sistem.frmOgrenciEkrani.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sistem.frmSoruEklemeEkrani = new frmSoruEklemeEkrani();
            Sistem.frmSoruEklemeEkrani.Show();
            this.Hide();
        }
    }
}
