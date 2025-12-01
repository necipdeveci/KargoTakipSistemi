using System;
using System.Linq;
using System.Windows.Forms;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Servisler
{
    public static class PersonelFormServisi
    {
        // Rol koþulu kaldýrýldý: her zaman seçilen þubenin araç listesi + ilk sýrada "Araç Yok" sentinel gösterilir.
        public static void GuncelleAracCombosu(ComboBox cbArac, ComboBox cbRol, ComboBox cbSube, int? tercihEdilenAracId = null)
        {
            // Þube belirle
            int? subeId = null;
            if (cbSube.SelectedValue is int sid) subeId = sid;
            else if (cbSube.SelectedItem is Sube sube) subeId = sube.SubeId;

            using (var ctx = new KtsContext())
            {
                var liste = subeId.HasValue
                    ? ctx.Araclar.Where(a => a.SubeId == subeId.Value).ToList()
                    : ctx.Araclar.ToList();

                // Her zaman ilk eleman "Araç Yok"
                liste.Insert(0, new Arac { AracId = 0, AracTip = "Araç Yok" });

                cbArac.DisplayMember = "AracTip";
                cbArac.ValueMember = "AracId";
                cbArac.DataSource = liste;

                if (tercihEdilenAracId.HasValue && liste.Any(a => a.AracId == tercihEdilenAracId.Value))
                    cbArac.SelectedValue = tercihEdilenAracId.Value;
                else
                    cbArac.SelectedValue = 0; // Varsayýlan: Araç Yok
            }
        }
    }
}
