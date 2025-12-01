using System;
using System.Linq;
using System.Windows.Forms;

namespace kargotakipsistemi.Servisler
{
    public static class AdresFormServisi
    {
        public static void GuncelleAdresTipiSecenekleri(ComboBox cbAdresTip, Button btnKaydet, int? referansId, string referansTipi)
        {
            cbAdresTip.Items.Clear();
            btnKaydet.Enabled = true;
            btnKaydet.Text = "Kaydet";

            if (referansTipi == "Personel")
            {
                cbAdresTip.Items.Add("Ev");
                cbAdresTip.SelectedIndex = 0;
                return;
            }

            if (!referansId.HasValue)
            {
                cbAdresTip.Items.Add("Ev");
                cbAdresTip.Items.Add("Ýþ");
                cbAdresTip.SelectedIndex = 0;
                return;
            }

            using (var ctx = new KtsContext())
            {
                var tipler = ctx.Adresler
                    .Where(a => a.MusteriId == referansId)
                    .Select(a => a.AdresTipi)
                    .ToList();

                bool evVar = tipler.Any(t => t == "Ev");
                bool isVar = tipler.Any(t => t == "Ýþ");

                if (evVar && !isVar)
                {
                    cbAdresTip.Items.Add("Ýþ");
                    cbAdresTip.SelectedIndex = 0;
                }
                else if (!evVar && isVar)
                {
                    cbAdresTip.Items.Add("Ev");
                    cbAdresTip.SelectedIndex = 0;
                }
                else if (!evVar && !isVar)
                {
                    cbAdresTip.Items.Add("Ev");
                    cbAdresTip.Items.Add("Ýþ");
                    cbAdresTip.SelectedIndex = 0;
                }
                else
                {
                    // Ev ve Ýþ zaten var => yeni ekleme yapýlmamalý
                    btnKaydet.Enabled = false;
                    btnKaydet.Text = "Güncelle";
                }
            }
        }

        public static (bool success, bool guncelleme) KaydetAdres(
            int? referansId,
            string referansTipi,
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
            TextBox tbAciklama,
            CheckBox ckbAktif)
        {
            using (var context = new KtsContext())
            {
                var secilenTip = referansTipi == "Personel" ? "Ev" : cbAdresTip.SelectedItem?.ToString();
                var mevcutAdresler = referansId.HasValue
                    ? context.Adresler.Where(a => (referansTipi == "Personel" ? a.PersonelId == referansId : a.MusteriId == referansId)).ToList()
                    : new System.Collections.Generic.List<Entities.Adres>();

                var adres = referansId.HasValue
                    ? (referansTipi == "Personel"
                        ? context.Adresler.SingleOrDefault(a => a.PersonelId == referansId)
                        : context.Adresler.SingleOrDefault(a => a.MusteriId == referansId && a.AdresTipi == secilenTip))
                    : null;

                bool guncelleme = adres != null;

                if (referansTipi != "Personel" && !guncelleme && mevcutAdresler.Count >= 2)
                {
                    MessageBox.Show("Müþteriye en fazla iki adres atanabilir (Ev ve Ýþ).");
                    return (false, false);
                }

                if (adres == null)
                {
                    adres = new Entities.Adres();
                    if (referansTipi == "Personel") adres.PersonelId = referansId; else adres.MusteriId = referansId;
                    context.Adresler.Add(adres);
                }

                adres.AdresBaslik = tbAdresBaslik.Text;
                adres.PostaKodu = tbPostaKodu.Text;
                adres.AdresTipi = referansTipi == "Personel" ? "Ev" : secilenTip;
                adres.IlId = (int)cbIl.SelectedValue;
                adres.IlceId = (int)cbIlce.SelectedValue;
                adres.MahalleId = (int)cbMahalle.SelectedValue;
                adres.KapiNo = tbKapiNo.Text;
                adres.BinaAdi = tbBinaAdi.Text;
                adres.Kat = tbKat.Text;
                adres.Daire = tbDaire.Text;
                adres.EkAciklama = tbAciklama.Text;
                adres.AcikAdres = tbAcikAdres.Text;
                adres.Aktif = ckbAktif.Checked;

                context.SaveChanges(); // Adres Id üretildi

                // Musteri için MusteriAdres kaydý ekle/güncelle
                if (referansTipi != "Personel" && adres.MusteriId.HasValue)
                {
                    var musterýAdres = context.MusteriAdresleri.SingleOrDefault(ma => ma.MusteriId == adres.MusteriId && ma.AdresId == adres.AdresId);
                    if (musterýAdres == null)
                    {
                        musterýAdres = new Entities.MusteriAdres
                        {
                            MusteriId = adres.MusteriId.Value,
                            AdresId = adres.AdresId,
                            AdresTipi = adres.AdresTipi,
                            Aktif = adres.Aktif
                        };
                        context.MusteriAdresleri.Add(musterýAdres);
                    }
                    else
                    {
                        musterýAdres.AdresTipi = adres.AdresTipi;
                        musterýAdres.Aktif = adres.Aktif;
                    }
                    context.SaveChanges();
                }

                return (true, guncelleme);
            }
        }

        public static bool SilSeciliAdres(int? referansId, string referansTipi, DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0) return false;
            var seciliSatir = dgv.SelectedRows[0];
            int adresId = Convert.ToInt32(seciliSatir.Cells["AdresId"].Value);

            using (var context = new KtsContext())
            {
                var adres = context.Adresler.Find(adresId);
                if (adres == null) return false;
                context.Adresler.Remove(adres);

                // Ýlgili MusteriAdres kaydýný da kaldýr (müþteri ise)
                if (referansTipi != "Personel" && adres.MusteriId.HasValue)
                {
                    var musterýAdres = context.MusteriAdresleri.SingleOrDefault(ma => ma.MusteriId == adres.MusteriId && ma.AdresId == adres.AdresId);
                    if (musterýAdres != null)
                        context.MusteriAdresleri.Remove(musterýAdres);
                }

                context.SaveChanges();
                return true;
            }
        }
    }
}
