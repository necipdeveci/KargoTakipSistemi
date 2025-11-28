using System;
using System.Windows.Forms;

namespace kargotakipsistemi.Dogrulamalar
{
    public static class PersonelDogrulayici
    {
        public static bool Dogrula(
            TextBox tbAd,
            TextBox tbSoyad,
            TextBox tbMail,
            TextBox tbSifre,
            TextBox tbTel,
            ComboBox cbCinsiyet,
            ComboBox cbRol,
            ComboBox cbSube,
            TextBox tbEhliyet,
            DateTimePicker dtpDogumTarih,
            ComboBox cbArac,
            NumericUpDown nudMaas)
        {
            if (string.IsNullOrWhiteSpace(tbAd.Text) || tbAd.Text.Length > 50)
            {
                MessageBox.Show("Ad alaný zorunlu ve en fazla 50 karakter olmalýdýr.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbSoyad.Text) || tbSoyad.Text.Length > 50)
            {
                MessageBox.Show("Soyad alaný zorunlu ve en fazla 50 karakter olmalýdýr.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbMail.Text) || tbMail.Text.Length > 100)
            {
                MessageBox.Show("Mail alaný zorunlu ve en fazla 100 karakter olmalýdýr.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbSifre.Text) || tbSifre.Text.Length > 128)
            {
                MessageBox.Show("Þifre alaný zorunlu ve en fazla 128 karakter olmalýdýr.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbTel.Text) || tbTel.Text.Length > 15)
            {
                MessageBox.Show("Telefon alaný zorunlu ve en fazla 15 karakter olmalýdýr.");
                return false;
            }
            if (cbCinsiyet.SelectedIndex < 0)
            {
                MessageBox.Show("Cinsiyet seçilmelidir.");
                return false;
            }
            if (cbRol.SelectedIndex < 0)
            {
                MessageBox.Show("Rol seçilmelidir.");
                return false;
            }
            if (cbSube.SelectedIndex < 0)
            {
                MessageBox.Show("Þube seçilmelidir.");
                return false;
            }
            // Ehliyet zorunlu (DB kolonunda NOT NULL). Boþ býrakýlamaz.
            if (string.IsNullOrWhiteSpace(tbEhliyet.Text))
            {
                MessageBox.Show("Ehliyet sýnýfý zorunludur.");
                return false;
            }
            if (tbEhliyet.Text.Length > 5)
            {
                MessageBox.Show("Ehliyet sýnýfý en fazla 5 karakter olmalýdýr.");
                return false;
            }
            if (dtpDogumTarih.Value > DateTime.Now)
            {
                MessageBox.Show("Doðum tarihi bugünden ileri olamaz.");
                return false;
            }
            if (cbArac.SelectedIndex >= 0 && cbArac.SelectedItem == null)
            {
                MessageBox.Show("Araç seçimi geçersiz.");
                return false;
            }
            if (nudMaas.Value < 0)
            {
                MessageBox.Show("Maaþ negatif olamaz.");
                return false;
            }

            return true;
        }
    }
}
