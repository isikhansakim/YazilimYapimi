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
    public partial class frmOgrenciAyarlar : Form
    {
        public frmOgrenciAyarlar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sistem.birdogrudansonraki = Convert.ToInt32(textBox1.Text);
            Sistem.ikidogrudansonraki = Convert.ToInt32(textBox2.Text);
            Sistem.ucdogrudansonraki = Convert.ToInt32(textBox3.Text);
            Sistem.dortdogrudansonraki = Convert.ToInt32(textBox4.Text);
            Sistem.besdogrudansonraki = Convert.ToInt32(textBox5.Text);
            Sistem.altidogrudansonraki = Convert.ToInt32(textBox6.Text);

            MessageBox.Show("Veriler Güncellendi.");
            this.Hide();
        }
    }
}
