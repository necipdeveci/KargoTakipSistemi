using System;
using System.Windows.Forms;

namespace kargotakipsistemi.Dogrulamalar
{
    public static class AdresDogrulayici
    {
        public static bool Dogrula(
            TextBox tbAdresBaslik,
            ComboBox cbAdresTip,
            ComboBox cbIl,
            ComboBox cbIlce,
            ComboBox cbMahalle,
            TextBox tbKapiNo,
            TextBox tbBinaAdi,
            TextBox tbKat,
            TextBox tbDaire,
            TextBox tbPostaKodu,
            TextBox tbAcikAdres,
            TextBox tbEkAciklama,
            string referansTipi)
        {
            if (string.IsNullOrWhiteSpace(tbAdresBaslik.Text) || tbAdresBaslik.Text.Length > 50)
            {
                MessageBox.Show("Adres baþlýðý zorunlu ve en fazla 50 karakter olmalýdýr.");
                return false;
            }
            if (cbAdresTip.SelectedIndex < 0)
            {
                MessageBox.Show("Adres tipi seçilmelidir.");
                return false;
            }
            if (cbIl.SelectedIndex < 0)
            {
                MessageBox.Show("Ýl seçilmelidir.");
                return false;
            }
            if (cbIlce.SelectedIndex < 0)
            {
                MessageBox.Show("Ýlçe seçilmelidir.");
                return false;
            }
            if (cbMahalle.SelectedIndex < 0)
            {
                MessageBox.Show("Mahalle seçilmelidir.");
                return false;
            }
            if (tbKapiNo.Text.Length > 10)
            {
                MessageBox.Show("Kapý no en fazla 10 karakter olmalýdýr.");
                return false;
            }
            if (tbBinaAdi.Text.Length > 50)
            {
                MessageBox.Show("Bina adý en fazla 50 karakter olmalýdýr.");
                return false;
            }
            if (tbKat.Text.Length > 10)
            {
                MessageBox.Show("Kat en fazla 10 karakter olmalýdýr.");
                return false;
            }
            if (tbDaire.Text.Length > 10)
            {
                MessageBox.Show("Daire en fazla 10 karakter olmalýdýr.");
                return false;
            }
            if (tbPostaKodu.Text.Length > 10)
            {
                MessageBox.Show("Posta kodu en fazla 10 karakter olmalýdýr.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbAcikAdres.Text) || tbAcikAdres.Text.Length > 255)
            {
                MessageBox.Show("Açýk adres zorunlu ve en fazla 255 karakter olmalýdýr.");
                return false;
            }
            if (tbEkAciklama.Text.Length > 255)
            {
                MessageBox.Show("Ek açýklama en fazla 255 karakter olmalýdýr.");
                return false;
            }

            if (referansTipi == "Musteri")
            {
                var tip = cbAdresTip.SelectedItem?.ToString();
                if (tip != "Ev" && tip != "Ýþ")
                {
                    MessageBox.Show("Müþteri için adres tipi yalnýzca 'Ev' veya 'Ýþ' olabilir.");
                    return false;
                }
            }

            return true;
        }
    }
}
