using kargotakipsistemi.Entities;
using System;
using System.Linq;
using System.Windows.Forms;
using kargotakipsistemi.Servisler;
using kargotakipsistemi.Yardimcilar;
using kargotakipsistemi.Dogrulamalar;
using System.Drawing;

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

        var (success, guncelleme) = AdresFormServisi.KaydetAdres(
            _referansId,
            _referansTipi,
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
            ckb_adresAktif
        );
        if (!success) return;

        MessageBox.Show(guncelleme ? "Adres güncellendi." : "Adres eklendi.");

        if (_referansId.HasValue)
        {
            if (_referansTipi == "Personel")
            {
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
            }
            else
            {
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

        // Kayıt sonrası yeni ekleme için adres tipi seçeneklerini güncelle
        AdresFormServisi.GuncelleAdresTipiSecenekleri(cb_adresTip, btn_adresKaydet, _referansId, _referansTipi);
    }

    private void btn_adresKayitSil_Click(object sender, EventArgs e)
    {
        if (dgv_adresler.SelectedRows.Count > 0)
        {
            var sonuc = MessageBox.Show("Seçili adresi silmek istediğinize emin misiniz?", "Adres Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sonuc == DialogResult.Yes)
            {
                var silindi = AdresFormServisi.SilSeciliAdres(_referansId, _referansTipi, dgv_adresler);
                if (silindi)
                {
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

                    // Silme sonrası ekleme seçeneklerini yeniden değerlendir
                    AdresFormServisi.GuncelleAdresTipiSecenekleri(cb_adresTip, btn_adresKaydet, _referansId, _referansTipi);
                }
                else
                {
                    MessageBox.Show("Adres bulunamadı.");
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

        // Tipleri referans tipine ve mevcut kayıtlara göre yeniden doldur
        AdresFormServisi.GuncelleAdresTipiSecenekleri(cb_adresTip, btn_adresKaydet, _referansId, _referansTipi);

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

                // Güncelleme moduna geçir
                btn_adresKaydet.Enabled = true;
                btn_adresKaydet.Text = "Güncelle";
            }
        }
    }

    private void AdresEkleForm_Load(object sender, EventArgs e)
    {
        // Renk teması ayarları (MainForm ile tutarlı)
        this.BackColor = Color.FromArgb(242, 242, 242);

        // SplitContainer renklendirme
        splitContainer1.BackColor = Color.FromArgb(234, 228, 213);

        // DataGridView renklendirme
        dgv_adresler.BackgroundColor = Color.FromArgb(234, 228, 213);
        dgv_adresler.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        dgv_adresler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv_adresler.EnableHeadersVisualStyles = false;
        dgv_adresler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(182, 176, 159);
        dgv_adresler.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

        // ComboBox kontrolleri
        cb_adresTip.BackColor = Color.FromArgb(182, 176, 159);
        cb_adresTip.ForeColor = Color.FromArgb(0, 0, 0);
        cb_adresTip.FlatStyle = FlatStyle.Flat;

        cb_adresIl.BackColor = Color.FromArgb(182, 176, 159);
        cb_adresIl.ForeColor = Color.FromArgb(0, 0, 0);
        cb_adresIl.FlatStyle = FlatStyle.Flat;

        cb_adresIlce.BackColor = Color.FromArgb(182, 176, 159);
        cb_adresIlce.ForeColor = Color.FromArgb(0, 0, 0);
        cb_adresIlce.FlatStyle = FlatStyle.Flat;

        cb_adresMahalle.BackColor = Color.FromArgb(182, 176, 159);
        cb_adresMahalle.ForeColor = Color.FromArgb(0, 0, 0);
        cb_adresMahalle.FlatStyle = FlatStyle.Flat;

        // TextBox kontrolleri
        tb_adresBaslik.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresBaslik.BorderStyle = BorderStyle.FixedSingle;

        tb_adresPostaKodu.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresPostaKodu.BorderStyle = BorderStyle.FixedSingle;

        tb_adresKapiNo.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresKapiNo.BorderStyle = BorderStyle.FixedSingle;

        tb_adresBinaAd.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresBinaAd.BorderStyle = BorderStyle.FixedSingle;

        tb_adresKat.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresKat.BorderStyle = BorderStyle.FixedSingle;

        tb_adresDaire.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresDaire.BorderStyle = BorderStyle.FixedSingle;

        tb_adresAciklama.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresAciklama.BorderStyle = BorderStyle.FixedSingle;

        tb_adresAcikAdres.BackColor = Color.FromArgb(182, 176, 159);
        tb_adresAcikAdres.BorderStyle = BorderStyle.FixedSingle;

        // Button kontrolleri
        btn_adresKaydet.BackColor = Color.FromArgb(182, 176, 159);
        btn_adresKaydet.FlatStyle = FlatStyle.Flat;

        btn_adresKayitSil.BackColor = Color.FromArgb(182, 176, 159);
        btn_adresKayitSil.FlatStyle = FlatStyle.Flat;

        btn_adresFormTemizle.BackColor = Color.FromArgb(182, 176, 159);
        btn_adresFormTemizle.FlatStyle = FlatStyle.Flat;

        // Tipleri referans tipine ve mevcut kayıtlara göre doldur
        AdresFormServisi.GuncelleAdresTipiSecenekleri(cb_adresTip, btn_adresKaydet, _referansId, _referansTipi);

        VeriBaglamaServisi.KomboyaBagla(cb_adresIl, ctx => ctx.Iller, "IlAd", "IlId");

        if (_referansId.HasValue)
        {
            if (_referansTipi == "Personel")
            {
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
                this.Text = "Personel Adresleri";
            }
            else
            {
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
                this.Text = "Müşteri Adresleri";

            }
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
