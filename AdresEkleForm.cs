using kargotakipsistemi.Entities;
using System;
using System.Linq;
using System.Windows.Forms;
using kargotakipsistemi.Servisler;
using kargotakipsistemi.Yardimcilar;
using kargotakipsistemi.Dogrulamalar;

namespace kargotakipsistemi;

public partial class AdresEkleForm : Form
{
    private int? _referansId;
    private string _referansTipi;

    public AdresEkleForm(int referansId, string referansTipi)
    {
        InitializeComponent();
        _referansId = referansId;
        _referansTipi = referansTipi;
    }

    private void btn_adresKaydet_Click(object sender, EventArgs e)
    {
        // Doğrulama
        var gecerli = AdresDogrulayici.Dogrula(
            tb_adresBaslik,
            cb_adresTip,
            cb_adresIl,
            cb_adresIlce,
            cb_adresMahalle,
            tb_adresKapiNo,
            tb_adresBinaAd,
            tb_adresKat,
            tb_adresDaire,
            tb_adresPostaKodu,
            tb_adresAcikAdres,
            tb_adresAciklama,
            _referansTipi
        );
        if (!gecerli) return;

        using (var context = new KtsContext())
        {
            Adres adres = null;

            // Personel: tek adres; Müşteri: Ev/İş upsert ve en fazla 2 kayıt
            if (_referansId.HasValue)
            {
                if (_referansTipi == "Personel")
                {
                    adres = context.Adresler.SingleOrDefault(a => a.PersonelId == _referansId);
                    var extra = context.Adresler.Where(a => a.PersonelId == _referansId && a.AdresId != (adres != null ? adres.AdresId : 0)).ToList();
                    if (extra.Count > 0)
                    {
                        context.Adresler.RemoveRange(extra);
                    }
                }
                else
                {
                    var secilenTip = cb_adresTip.SelectedItem?.ToString();
                    adres = context.Adresler.SingleOrDefault(a => a.MusteriId == _referansId && a.AdresTipi == secilenTip);

                    var mevcutAdresler = context.Adresler.Where(a => a.MusteriId == _referansId).ToList();
                    if (adres == null && mevcutAdresler.Count >= 2)
                    {
                        MessageBox.Show("Müşteriye en fazla iki adres atanabilir (Ev ve İş).");
                        return;
                    }
                }
            }

            if (adres == null)
            {
                adres = new Adres();
                if (_referansTipi == "Personel")
                    adres.PersonelId = _referansId;
                else
                    adres.MusteriId = _referansId;
                context.Adresler.Add(adres);
            }

            adres.AdresBaslik = tb_adresBaslik.Text;
            adres.PostaKodu = tb_adresPostaKodu.Text;
            // Personel için adres tipi her zaman "Ev"
            adres.AdresTipi = _referansTipi == "Personel" ? "Ev" : cb_adresTip.SelectedItem?.ToString();
            adres.IlId = (int)cb_adresIl.SelectedValue;
            adres.IlceId = (int)cb_adresIlce.SelectedValue;
            adres.MahalleId = (int)cb_adresMahalle.SelectedValue;
            adres.KapiNo = tb_adresKapiNo.Text;
            adres.BinaAdi = tb_adresBinaAd.Text;
            adres.Kat = tb_adresKat.Text;
            adres.Daire = tb_adresDaire.Text;
            adres.EkAciklama = tb_adresAciklama.Text;
            adres.AcikAdres = tb_adresAcikAdres.Text;
            adres.Aktif = ckb_adresAktif.Checked;

            context.SaveChanges();
            MessageBox.Show("Adres kaydedildi.");

            if (_referansId.HasValue)
            {
                if (_referansTipi == "Personel")
                    VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                        .Where(a => a.PersonelId == _referansId)
                        .Select(a => new
                        {
                            a.AdresId,
                            a.AdresBaslik,
                            a.AcikAdres,
                            Il = a.Il != null ? a.Il.IlAd : "",
                            Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                            Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                            a.AdresTipi,
                            a.Aktif
                        }));
                else
                    VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                        .Where(a => a.MusteriId == _referansId)
                        .Select(a => new
                        {
                            a.AdresId,
                            a.AdresBaslik,
                            a.AcikAdres,
                            Il = a.Il != null ? a.Il.IlAd : "",
                            Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                            Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                            a.AdresTipi,
                            a.Aktif
                        }));
            }
        }
    }

    private void btn_adresKayitSil_Click(object sender, EventArgs e)
    {
        if (dgv_adresler.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_adresler.SelectedRows[0];
            int adresId = Convert.ToInt32(seciliSatir.Cells["AdresId"].Value);

            var sonuc = MessageBox.Show("Seçili adresi silmek istediğinize emin misiniz?", "Adres Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                using (var context = new KtsContext())
                {
                    var adres = context.Adresler.Find(adresId);
                    if (adres != null)
                    {
                        context.Adresler.Remove(adres);
                        context.SaveChanges();
                        MessageBox.Show("Adres silindi.");

                        if (_referansId.HasValue)
                        {
                            if (_referansTipi == "Personel")
                                VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                                    .Where(a => a.PersonelId == _referansId)
                                    .Select(a => new
                                    {
                                        a.AdresId,
                                        a.AdresBaslik,
                                        a.AcikAdres,
                                        Il = a.Il != null ? a.Il.IlAd : "",
                                        Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                                        Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                                        a.AdresTipi,
                                        a.Aktif
                                    }));
                            else
                                VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                                    .Where(a => a.MusteriId == _referansId)
                                    .Select(a => new
                                    {
                                        a.AdresId,
                                        a.AdresBaslik,
                                        a.AcikAdres,
                                        Il = a.Il != null ? a.Il.IlAd : "",
                                        Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                                        Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                                        a.AdresTipi,
                                        a.Aktif
                                    }));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Adres bulunamadı.");
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir adres seçin.");
        }
    }

    private void btn_adresFormTemizle_Click(object sender, EventArgs e)
    {
        KontrolYardimcisi.Temizle(
            tb_adresBaslik,
            tb_adresPostaKodu,
            cb_adresTip,
            cb_adresIl,
            cb_adresIlce,
            cb_adresMahalle,
            tb_adresKapiNo,
            tb_adresBinaAd,
            tb_adresKat,
            tb_adresDaire,
            tb_adresAciklama,
            tb_adresAcikAdres,
            ckb_adresAktif
        );

        // Tipleri referans tipine göre yeniden doldur
        cb_adresTip.Items.Clear();
        if (_referansTipi == "Personel")
        {
            cb_adresTip.Items.Add("Ev");
        }
        else
        {
            cb_adresTip.Items.Add("Ev");
            cb_adresTip.Items.Add("İş");
        }
        cb_adresTip.SelectedIndex = 0;

        VeriBaglamaServisi.KomboyaBagla(cb_adresIl, ctx => ctx.Iller, "IlAd", "IlId");

        if (_referansId.HasValue)
        {
            if (_referansTipi == "Personel")
                VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                    .Where(a => a.PersonelId == _referansId)
                    .Select(a => new
                    {
                        a.AdresId,
                        a.AdresBaslik,
                        a.AcikAdres,
                        Il = a.Il != null ? a.Il.IlAd : "",
                        Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                        Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                        a.AdresTipi,
                        a.Aktif
                    }));
            else
                VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                    .Where(a => a.MusteriId == _referansId)
                    .Select(a => new
                    {
                        a.AdresId,
                        a.AdresBaslik,
                        a.AcikAdres,
                        Il = a.Il != null ? a.Il.IlAd : "",
                        Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                        Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                        a.AdresTipi,
                        a.Aktif
                    }));
        }
    }

    private void dgv_adresler_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_adresler.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_adresler.SelectedRows[0];
            int adresId = Convert.ToInt32(seciliSatir.Cells["AdresId"].Value);

            using (var ctx = new KtsContext())
            {
                var adres = ctx.Adresler.FirstOrDefault(a => a.AdresId == adresId);
                if (adres == null) return;

                tb_adresBaslik.Text = adres.AdresBaslik;
                tb_adresPostaKodu.Text = adres.PostaKodu;

                // Tipleri referans tipine göre doldur ve seç
                cb_adresTip.Items.Clear();
                if (_referansTipi == "Personel")
                {
                    cb_adresTip.Items.Add("Ev");
                    cb_adresTip.SelectedIndex = 0; // Personel için her zaman Ev
                }
                else
                {
                    cb_adresTip.Items.Add("Ev");
                    cb_adresTip.Items.Add("İş");
                    cb_adresTip.SelectedItem = string.IsNullOrWhiteSpace(adres.AdresTipi) ? "Ev" : adres.AdresTipi;
                }

                // Comboları projedeki servis ile yeniden bağla ve ID seç
                VeriBaglamaServisi.KomboyaBagla(cb_adresIlce, c => c.Ilceler.Where(x => x.IlId == adres.IlId), "IlceAd", "IlceId");
                VeriBaglamaServisi.KomboyaBagla(cb_adresMahalle, c => c.Mahalleler.Where(x => x.IlceId == adres.IlceId), "MahalleAd", "MahalleId");

                cb_adresIl.SelectedValue = adres.IlId;
                cb_adresIlce.SelectedValue = adres.IlceId;
                cb_adresMahalle.SelectedValue = adres.MahalleId;

                tb_adresKapiNo.Text = adres.KapiNo;
                tb_adresBinaAd.Text = adres.BinaAdi;
                tb_adresKat.Text = adres.Kat;
                tb_adresDaire.Text = adres.Daire;
                tb_adresAciklama.Text = adres.EkAciklama;
                tb_adresAcikAdres.Text = adres.AcikAdres;
                ckb_adresAktif.Checked = adres.Aktif;
            }
        }
    }

    private void AdresEkleForm_Load(object sender, EventArgs e)
    {
        // Tipleri referans tipine göre doldur
        cb_adresTip.Items.Clear();
        if (_referansTipi == "Personel")
        {
            cb_adresTip.Items.Add("Ev");
        }
        else
        {
            cb_adresTip.Items.Add("Ev");
            cb_adresTip.Items.Add("İş");
        }
        cb_adresTip.SelectedIndex = 0;

        VeriBaglamaServisi.KomboyaBagla(cb_adresIl, ctx => ctx.Iller, "IlAd", "IlId");

        if (_referansId.HasValue)
        {
            if (_referansTipi == "Personel")
                VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                    .Where(a => a.PersonelId == _referansId)
                    .Select(a => new
                    {
                        a.AdresId,
                        a.AdresBaslik,
                        a.AcikAdres,
                        Il = a.Il != null ? a.Il.IlAd : "",
                        Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                        Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                        a.AdresTipi,
                        a.Aktif
                    }));
            else
                VeriBaglamaServisi.IzgaraBagla(dgv_adresler, ctx => ctx.Adresler
                    .Where(a => a.MusteriId == _referansId)
                    .Select(a => new
                    {
                        a.AdresId,
                        a.AdresBaslik,
                        a.AcikAdres,
                        Il = a.Il != null ? a.Il.IlAd : "",
                        Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                        Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                        a.AdresTipi,
                        a.Aktif
                    }));
        }
    }

    private void cb_adresIl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_adresIl.SelectedValue != null)
        {
            int secilenIlId;
            if (cb_adresIl.SelectedValue is int)
                secilenIlId = (int)cb_adresIl.SelectedValue;
            else if (cb_adresIl.SelectedValue is Il il)
                secilenIlId = il.IlId;
            else
                throw new InvalidOperationException("Seçilen değer beklenen türde değil.");
            VeriBaglamaServisi.KomboyaBagla(cb_adresIlce, ctx => ctx.Ilceler.Where(x => x.IlId == secilenIlId), "IlceAd", "IlceId");
        }
    }

    private void cb_adresIlce_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_adresIlce.SelectedValue != null)
        {
            int secilenIlceId;
            if (cb_adresIlce.SelectedValue is int)
                secilenIlceId = (int)cb_adresIlce.SelectedValue;
            else if (cb_adresIlce.SelectedValue is Ilce ilce)
                secilenIlceId = ilce.IlceId;
            else
                throw new InvalidOperationException("Seçilen değer beklenen türde değil.");
            VeriBaglamaServisi.KomboyaBagla(cb_adresMahalle, ctx => ctx.Mahalleler.Where(x => x.IlceId == secilenIlceId), "MahalleAd", "MahalleId");
        }
    }
}
