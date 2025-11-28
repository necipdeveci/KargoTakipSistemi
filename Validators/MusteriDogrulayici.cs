using System;
using System.Windows.Forms;

namespace kargotakipsistemi.Dogrulamalar
{
    public static class MusteriDogrulayici
    {
        public static bool Dogrula(TextBox tbAd, TextBox tbSoyad, TextBox tbMail, TextBox tbTel, DateTimePicker dtpDogum, TextBox tbNot)
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
            if (string.IsNullOrWhiteSpace(tbTel.Text) || tbTel.Text.Length > 15)
            {
                MessageBox.Show("Telefon alaný zorunlu ve en fazla 15 karakter olmalýdýr.");
                return false;
            }
            if (dtpDogum.Value > DateTime.Now)
            {
                MessageBox.Show("Doðum tarihi bugünden ileri olamaz.");
                return false;
            }
            if (tbNot.Text.Length > 255)
            {
                MessageBox.Show("Not alaný en fazla 255 karakter olmalýdýr.");
                return false;
            }
            return true;
        }
    }
}
