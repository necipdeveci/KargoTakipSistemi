using System;
using System.Linq;
using System.Windows.Forms;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Servisler
{
    public static class PersonelFormServisi
    {
        // Rol ve þubeye göre araç comboboxýný doldurur; "Araç Yok" (AracId=0) sentinel ekler.
        public static void GuncelleAracCombosu(ComboBox cbArac, ComboBox cbRol, ComboBox cbSube, int? tercihEdilenAracId = null)
        {
            string? rolAd = (cbRol.SelectedItem as Rol)?.RolAd;

            int? subeId = null;
            if (cbSube.SelectedValue is int sid) subeId = sid;
            else if (cbSube.SelectedItem is Sube sube) subeId = sube.SubeId;

            bool aracsizRol = !string.IsNullOrWhiteSpace(rolAd) &&
                              (rolAd.Contains("Müdür", StringComparison.OrdinalIgnoreCase) ||
                               rolAd.Contains("Yönetici", StringComparison.OrdinalIgnoreCase));

            if (aracsizRol)
            {
                cbArac.DisplayMember = "AracTip";
                cbArac.ValueMember = "AracId";
                cbArac.DataSource = new[] { new Arac { AracId = 0, AracTip = "Araç Yok" } };
                cbArac.SelectedValue = 0;
                return;
            }

            using (var ctx = new KtsContext())
            {
                var liste = subeId.HasValue
                    ? ctx.Araclar.Where(a => a.SubeId == subeId.Value).ToList()
                    : ctx.Araclar.ToList();

                liste.Insert(0, new Arac { AracId = 0, AracTip = "Araç Yok" });

                cbArac.DisplayMember = "AracTip";
                cbArac.ValueMember = "AracId";
                cbArac.DataSource = liste;

                if (tercihEdilenAracId.HasValue)
                    cbArac.SelectedValue = tercihEdilenAracId.Value;
                else
                    cbArac.SelectedValue = 0;
            }
        }
    }
}
