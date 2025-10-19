using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kargotakipsistemi
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (var musteriC = new KtsContext())
            {
                var musteriler = musteriC.Musteriler
                .Select(m => new
                {
                    m.MusteriId,
                    TamAd = m.Ad + " " + m.Soyad
                })
                .ToList();

                comboBox1.DataSource = musteriler;
                comboBox1.DisplayMember = "TamAd";
                comboBox1.ValueMember = "MusteriId";

                var musteriler2 = musteriC.Musteriler
                .Select(m => new
                {
                    m.MusteriId,
                    TamAd = m.Ad + " " + m.Soyad
                })
                .ToList();
                comboBox4.DataSource = musteriler2;
                comboBox4.DisplayMember = "TamAd";
                comboBox4.ValueMember = "MusteriId";
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string[] alfabe = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string takipNumarasi = (alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10) + alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10) + alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10) + alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10)).ToString();
            takipNotxtBox.Text = takipNumarasi;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMusterAdresYonet_Click(object sender, EventArgs e)
        {
            AdresEkleForm adresYonetim = new AdresEkleForm();
            adresYonetim.Show();
        }

        private void btnPersonelAdresYonet_Click(object sender, EventArgs e)
        {
            AdresEkleForm adresYonetim = new AdresEkleForm();
            adresYonetim.Show();
        }
    }
}
