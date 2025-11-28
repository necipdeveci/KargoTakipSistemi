using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace kargotakipsistemi.Dogrulamalar
{
    public static class GonderiDogrulayici
    {
        public static bool Dogrula(
            ComboBox cbGonderen,
            ComboBox cbGonderenAdres,
            ComboBox cbAlici,
            ComboBox cbAliciAdres,
            TextBox tbBoyut,
            NumericUpDown nudAgirlik,
            ComboBox cbTeslimatTip,
            DateTimePicker dtpGonderiTarih,
            DateTimePicker dtpTahminiTeslim,
            NumericUpDown nudUcret,
            NumericUpDown nudIndirim,
            NumericUpDown nudEkMasraf)
        {
            // Müþteri seçimleri
            if (cbGonderen.SelectedIndex < 0)
            {
                MessageBox.Show("Gönderici müþteri seçilmelidir.");
                return false;
            }
            if (cbAlici.SelectedIndex < 0)
            {
                MessageBox.Show("Alýcý müþteri seçilmelidir.");
                return false;
            }
            if (cbGonderenAdres.SelectedIndex < 0)
            {
                MessageBox.Show("Gönderici adresi seçilmelidir.");
                return false;
            }
            if (cbAliciAdres.SelectedIndex < 0)
            {
                MessageBox.Show("Alýcý adresi seçilmelidir.");
                return false;
            }

            // Ayný müþteri ise adres tipleri farklý olmalý
            if (cbGonderen.SelectedValue is int gId && cbAlici.SelectedValue is int aId && gId == aId)
            {
                var gAdresTip = cbGonderenAdres.Text?.Trim();
                var aAdresTip = cbAliciAdres.Text?.Trim();
                if (string.Equals(gAdresTip, aAdresTip, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Gönderici ve alýcý ayný müþteri ise farklý adres tipi seçilmelidir (Ev / Ýþ).");
                    return false;
                }
            }

            // Boyut alaný
            if (string.IsNullOrWhiteSpace(tbBoyut.Text))
            {
                MessageBox.Show("Boyut alaný boþ býrakýlamaz. Örnek: 30x20x10");
                return false;
            }
            if (tbBoyut.Text.Length > 50)
            {
                MessageBox.Show("Boyut alaný en fazla 50 karakter olmalýdýr.");
                return false;
            }
            // Basit format kontrolü (LxWxH)
            var rx = new Regex("^\n            (?:[ ]*)?(\\d+(?:[.,]\\d+)?)[xX](\\d+(?:[.,]\\d+)?)[xX](\\d+(?:[.,]\\d+)?)(?:[ ]*)?$");
            if (!rx.IsMatch(tbBoyut.Text.Replace(" ", "")))
            {
                MessageBox.Show("Boyut formatý geçersiz. Örnek: 30x20x10");
                return false;
            }

            // Aðýrlýk
            if (nudAgirlik.Value <= 0)
            {
                MessageBox.Show("Aðýrlýk 0'dan büyük olmalýdýr.");
                return false;
            }

            // Teslimat tipi
            if (cbTeslimatTip.SelectedIndex < 0)
            {
                MessageBox.Show("Teslimat tipi seçilmelidir.");
                return false;
            }

            // Tarihler
            if (dtpGonderiTarih.Value.Date > dtpTahminiTeslim.Value.Date)
            {
                MessageBox.Show("Tahmini teslim tarihi gönderi tarihinden önce olamaz.");
                return false;
            }

            // Ücret / indirim / ek masraf
            if (nudUcret.Value < 0)
            {
                MessageBox.Show("Ücret negatif olamaz.");
                return false;
            }
            if (nudIndirim.Value < 0)
            {
                MessageBox.Show("Ýndirim negatif olamaz.");
                return false;
            }
            if (nudEkMasraf.Value < 0)
            {
                MessageBox.Show("Ek masraf negatif olamaz.");
                return false;
            }
            if (nudIndirim.Value > nudUcret.Value)
            {
                MessageBox.Show("Ýndirim tutarý ücret tutarýný geçemez.");
                return false;
            }

            return true;
        }
    }
}
