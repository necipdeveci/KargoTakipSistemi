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

    private void btn_adresKaydet_Click(object sender, EventArgs e)
    {
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
        }
    }
    private void btn_adresKayitSil_Click(object sender, EventArgs e)
    {

    }
    private void btn_adresFormTemizle_Click(object sender, EventArgs e)
    {

    }

    private void dgv_adresler_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_adresler.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_adresler.SelectedRows[0];
        }
    }

    private void AdresEkleForm_Load(object sender, EventArgs e)
    {
        //cb_adresTip
        cb_adresTip.Items.Clear();
        cb_adresTip.Items.Add("Ev");
        cb_adresTip.Items.Add("İş");
        cb_adresTip.Items.Add("Diğer");
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
