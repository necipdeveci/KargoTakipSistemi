using System.Windows.Forms;

namespace kargotakipsistemi.Yardimcilar
{
    public static class OnayYardimcisi
    {
        public static bool SilmeOnayi(string baslik, string mesaj)
        {
            var sonuc = MessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return sonuc == DialogResult.Yes;
        }
    }
}
