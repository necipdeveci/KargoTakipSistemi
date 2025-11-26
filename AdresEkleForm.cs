using kargotakipsistemi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


    private void VeriCek(DataGridView dgv, Func<KtsContext, IQueryable<Adres>> sorgu)
    {
        using (var context = new KtsContext())
        {
            var liste = sorgu(context)
                .Select(a => new
                {
                    a.AdresId,
                    a.AdresBaslik,
                    a.AcikAdres,
                    Il = a.Il != null ? a.Il.IlAd : "",
                    Ilce = a.Ilce != null ? a.Ilce.IlceAd : "",
                    Mahalle = a.Mahalle != null ? a.Mahalle.MahalleAd : "",
                    a.PostaKodu,
                    a.AdresTipi,
                    a.KapiNo,
                    a.BinaAdi,
                    a.Kat,
                    a.Daire,
                    a.EkAciklama,
                    a.Aktif
                })
                .ToList();
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = liste;
        }
    }
    private void ComboBoxVeriCek<T>(ComboBox cb, Func<KtsContext, IQueryable<T>> sorgu, string displayMember, string valueMember) where T : class
    {
        using (var context = new KtsContext())
        {
            cb.DataSource = sorgu(context).ToList();
            cb.DisplayMember = displayMember;
            cb.ValueMember = valueMember;
        }
    }

    //formu temizler
    private void KontrolleriTemizle(params Control[] controls)
    {
        foreach (var control in controls)
        {
            if (control is TextBox tb)
                tb.Clear();
            else if (control is ComboBox cb)
                cb.SelectedIndex = -1;
            else if (control is NumericUpDown nud)
                nud.Value = nud.Minimum;
            else if (control is CheckBox ckb)
                ckb.Checked = false;
            else if (control is DateTimePicker dtp)
                dtp.Value = DateTime.Now;
        }
    }

    private bool AdresAlanlariGecerliMi()
    {
        // Adres Başlığı
        if (string.IsNullOrWhiteSpace(tb_adresBaslik.Text) || tb_adresBaslik.Text.Length > 50)
        {
            MessageBox.Show("Adres başlığı zorunlu ve en fazla 50 karakter olmalıdır.");
            return false;
        }
        // Adres Tipi
        if (cb_adresTip.SelectedIndex < 0)
        {
            MessageBox.Show("Adres tipi seçilmelidir.");
            return false;
        }
        // İl
        if (cb_adresIl.SelectedIndex < 0)
        {
            MessageBox.Show("İl seçilmelidir.");
            return false;
        }
        // İlçe
        if (cb_adresIlce.SelectedIndex < 0)
        {
            MessageBox.Show("İlçe seçilmelidir.");
            return false;
        }
        // Mahalle
        if (cb_adresMahalle.SelectedIndex < 0)
        {
            MessageBox.Show("Mahalle seçilmelidir.");
            return false;
        }
        // Kapı No (isteğe bağlı, ama karakter sınırı var)
        if (tb_adresKapiNo.Text.Length > 10)
        {
            MessageBox.Show("Kapı no en fazla 10 karakter olmalıdır.");
            return false;
        }
        // Bina Adı (isteğe bağlı, karakter sınırı var)
        if (tb_adresBinaAd.Text.Length > 50)
        {
            MessageBox.Show("Bina adı en fazla 50 karakter olmalıdır.");
            return false;
        }
        // Kat (isteğe bağlı, karakter sınırı var)
        if (tb_adresKat.Text.Length > 10)
        {
            MessageBox.Show("Kat en fazla 10 karakter olmalıdır.");
            return false;
        }
        // Daire (isteğe bağlı, karakter sınırı var)
        if (tb_adresDaire.Text.Length > 10)
        {
            MessageBox.Show("Daire en fazla 10 karakter olmalıdır.");
            return false;
        }
        // Posta Kodu (isteğe bağlı, karakter sınırı var)
        if (tb_adresPostaKodu.Text.Length > 10)
        {
            MessageBox.Show("Posta kodu en fazla 10 karakter olmalıdır.");
            return false;
        }
        // Açık Adres
        if (string.IsNullOrWhiteSpace(tb_adresAcikAdres.Text) || tb_adresAcikAdres.Text.Length > 255)
        {
            MessageBox.Show("Açık adres zorunlu ve en fazla 255 karakter olmalıdır.");
            return false;
        }
        // Ek Açıklama (isteğe bağlı, karakter sınırı var)
        if (tb_adresAciklama.Text.Length > 255)
        {
            MessageBox.Show("Ek açıklama en fazla 255 karakter olmalıdır.");
            return false;
        }

        return true;
    }
    private void btn_adresKaydet_Click(object sender, EventArgs e)
    {
        if (!AdresAlanlariGecerliMi())
            return;
        using (var context = new KtsContext())
        {
            Adres adres = null;
            if (_referansId.HasValue)
            {
                if (_referansTipi == "Personel")
                    adres = context.Adresler.SingleOrDefault(a => a.PersonelId == _referansId);
                else
                    adres = context.Adresler.SingleOrDefault(a => a.MusteriId == _referansId);
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
            adres.AdresTipi = cb_adresTip.SelectedItem?.ToString();
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
                    VeriCek(dgv_adresler, ctx => ctx.Adresler.Where(a => a.PersonelId == _referansId));
                else
                    VeriCek(dgv_adresler, ctx => ctx.Adresler.Where(a => a.MusteriId == _referansId));
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

                        // Listeyi güncelle
                        if (_referansId.HasValue)
                        {
                            if (_referansTipi == "Personel")
                                VeriCek(dgv_adresler, ctx => ctx.Adresler.Where(a => a.PersonelId == _referansId));
                            else
                                VeriCek(dgv_adresler, ctx => ctx.Adresler.Where(a => a.MusteriId == _referansId));
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
        KontrolleriTemizle(
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
    }

    private void dgv_adresler_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_adresler.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_adresler.SelectedRows[0];
            tb_adresBaslik.Text = seciliSatir.Cells["AdresBaslik"].Value?.ToString();
            tb_adresPostaKodu.Text = seciliSatir.Cells["PostaKodu"].Value?.ToString();
            cb_adresTip.SelectedItem = seciliSatir.Cells["AdresTipi"].Value?.ToString();
            cb_adresIl.SelectedItem = seciliSatir.Cells["Il"].Value?.ToString();
            cb_adresIlce.SelectedItem = seciliSatir.Cells["Ilce"].Value?.ToString();
            cb_adresMahalle.SelectedItem = seciliSatir.Cells["Mahalle"].Value?.ToString();
            tb_adresKapiNo.Text = seciliSatir.Cells["KapiNo"].Value?.ToString();
            tb_adresBinaAd.Text = seciliSatir.Cells["BinaAdi"].Value?.ToString();
            tb_adresKat.Text = seciliSatir.Cells["Kat"].Value?.ToString();
            tb_adresDaire.Text = seciliSatir.Cells["Daire"].Value?.ToString();
            tb_adresAciklama.Text = seciliSatir.Cells["EkAciklama"].Value?.ToString();
            tb_adresAcikAdres.Text = seciliSatir.Cells["AcikAdres"].Value?.ToString();
            ckb_adresAktif.Checked = Convert.ToBoolean(seciliSatir.Cells["Aktif"].Value);
        }
    }

    private void AdresEkleForm_Load(object sender, EventArgs e)
    {
        //cb_adresTip
        cb_adresTip.Items.Clear();
        cb_adresTip.Items.Add("Ev");
        cb_adresTip.Items.Add("İş");
        cb_adresTip.SelectedIndex = 0;

        //cb_adresIl
        ComboBoxVeriCek(cb_adresIl, ctx => ctx.Iller, "IlAd", "IlId");

        //dgv_adresler
        if (_referansId.HasValue)
        {
            if (_referansTipi == "Personel")
                VeriCek(dgv_adresler, ctx => ctx.Adresler.Where(a => a.PersonelId == _referansId));
            else
                VeriCek(dgv_adresler, ctx => ctx.Adresler.Where(a => a.MusteriId == _referansId));
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
            ComboBoxVeriCek(cb_adresIlce, ctx => ctx.Ilceler.Where(x => x.IlId == secilenIlId), "IlceAd", "IlceId");
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
            ComboBoxVeriCek(cb_adresMahalle, ctx => ctx.Mahalleler.Where(x => x.IlceId == secilenIlceId), "MahalleAd", "MahalleId");
        }
    }
}
