using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Forms;

/// <summary>
/// Fiyatlandýrma tarifelerini yönetme formu.
/// Admin kullanýcýlar bu form ile dinamik tarifeleri ekleyebilir, düzenleyebilir veya silebilir.
/// </summary>
public partial class TarifeYonetimForm : Form
{
    private readonly KtsContext _context;
    private int? _secilenTarifeId = null;

    public TarifeYonetimForm()
    {
        InitializeComponent();
        _context = new KtsContext();
        FormLoad();
    }

    private void FormLoad()
    {
        // Tarife türü combo
        cb_tarifeTuru.Items.Clear();
        cb_tarifeTuru.Items.AddRange(new object[] 
        { 
            "AgirlikTarife", 
            "HacimEkUcret", 
            "TeslimatCarpan", 
            "EkMasrafEsik", 
            "IndirimEsik" 
        });
        cb_tarifeTuru.SelectedIndex = 0;

        // Filtre combo (Tümü seçeneði ile)
        cb_tarifeTuruFiltre.Items.Clear();
        cb_tarifeTuruFiltre.Items.Add("Tümü");
        cb_tarifeTuruFiltre.Items.AddRange(new object[] 
        { 
            "AgirlikTarife", 
            "HacimEkUcret", 
            "TeslimatCarpan", 
            "EkMasrafEsik", 
            "IndirimEsik" 
        });
        cb_tarifeTuruFiltre.SelectedIndex = 0;

        // Birim combo
        cb_birim.Items.Clear();
        cb_birim.Items.AddRange(new object[] { "TL/kg", "TL", "çarpan", "%" });
        cb_birim.SelectedIndex = 0;

        // Grid ayarlarý
        dgv_tarifeler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv_tarifeler.MultiSelect = false;
        dgv_tarifeler.SelectionChanged += Dgv_tarifeler_SelectionChanged;
        dgv_tarifeler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // Tarih varsayýlanlarý
        dtp_gecerlilikBaslangic.Value = DateTime.Now;
        ckb_suresizGecerli.Checked = true;
        ckb_suresizGecerli.CheckedChanged += (s, e) => 
        {
            dtp_gecerlilikBitis.Enabled = !ckb_suresizGecerli.Checked;
        };

        // Min/Max deðer checkbox'larý
        ckb_minDegerYok.CheckedChanged += (s, e) =>
        {
            nud_minDeger.Enabled = !ckb_minDegerYok.Checked;
            if (ckb_minDegerYok.Checked)
                nud_minDeger.Value = 0;
        };

        ckb_maxDegerYok.CheckedChanged += (s, e) =>
        {
            nud_maxDeger.Enabled = !ckb_maxDegerYok.Checked;
            if (ckb_maxDegerYok.Checked)
                nud_maxDeger.Value = 0;
        };

        TarifeleriYukle();
    }

    private void TarifeleriYukle(string? filtreTuru = null)
    {
        var query = _context.FiyatlandirmaTarifeler.AsQueryable();

        if (!string.IsNullOrEmpty(filtreTuru))
            query = query.Where(t => t.TarifeTuru == filtreTuru);

        var tarifeler = query
            .OrderBy(t => t.TarifeTuru)
            .ThenBy(t => t.Oncelik)
            .ThenBy(t => t.MinDeger)
            .Select(t => new
            {
                t.TarifeId,
                t.TarifeTuru,
                t.TarifeAdi,
                t.MinDeger,
                t.MaxDeger,
                t.Deger,
                t.Birim,
                t.Aktif,
                t.Oncelik,
                Gecerlilik = t.GecerlilikBitis == null 
                    ? "Süresiz" 
                    : $"{t.GecerlilikBaslangic:dd.MM.yyyy} - {t.GecerlilikBitis:dd.MM.yyyy}",
                t.Aciklama
            })
            .ToList();

        dgv_tarifeler.DataSource = tarifeler;
        dgv_tarifeler.ClearSelection();
    }

    private void Dgv_tarifeler_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgv_tarifeler.SelectedRows.Count > 0)
        {
            var row = dgv_tarifeler.SelectedRows[0];
            _secilenTarifeId = Convert.ToInt32(row.Cells["TarifeId"].Value);

            var tarife = _context.FiyatlandirmaTarifeler.Find(_secilenTarifeId);
            if (tarife != null)
            {
                cb_tarifeTuru.SelectedItem = tarife.TarifeTuru;
                tb_tarifeAdi.Text = tarife.TarifeAdi;
                nud_minDeger.Value = tarife.MinDeger ?? 0;
                ckb_minDegerYok.Checked = !tarife.MinDeger.HasValue;
                nud_maxDeger.Value = tarife.MaxDeger ?? 0;
                ckb_maxDegerYok.Checked = !tarife.MaxDeger.HasValue;
                nud_deger.Value = tarife.Deger;
                cb_birim.SelectedItem = tarife.Birim;
                ckb_aktif.Checked = tarife.Aktif;
                nud_oncelik.Value = tarife.Oncelik;
                dtp_gecerlilikBaslangic.Value = tarife.GecerlilikBaslangic;
                ckb_suresizGecerli.Checked = !tarife.GecerlilikBitis.HasValue;
                if (tarife.GecerlilikBitis.HasValue)
                    dtp_gecerlilikBitis.Value = tarife.GecerlilikBitis.Value;
                tb_aciklama.Text = tarife.Aciklama ?? string.Empty;

                btn_kaydet.Text = "Güncelle";
            }
        }
        else
        {
            _secilenTarifeId = null;
            btn_kaydet.Text = "Kaydet";
        }
    }

    private void Btn_kaydet_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tb_tarifeAdi.Text))
        {
            MessageBox.Show("Tarife adý zorunludur!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        FiyatlandirmaTarife tarife;
        bool yeniKayit = !_secilenTarifeId.HasValue;

        if (yeniKayit)
        {
            tarife = new FiyatlandirmaTarife();
        }
        else
        {
            tarife = _context.FiyatlandirmaTarifeler.Find(_secilenTarifeId.Value);
            if (tarife == null)
            {
                MessageBox.Show("Tarife bulunamadý!");
                return;
            }
            tarife.GuncellemeTarihi = DateTime.Now;
        }

        tarife.TarifeTuru = cb_tarifeTuru.SelectedItem?.ToString() ?? "AgirlikTarife";
        tarife.TarifeAdi = tb_tarifeAdi.Text.Trim();
        tarife.MinDeger = ckb_minDegerYok.Checked ? null : nud_minDeger.Value;
        tarife.MaxDeger = ckb_maxDegerYok.Checked ? null : nud_maxDeger.Value;
        tarife.Deger = nud_deger.Value;
        tarife.Birim = cb_birim.SelectedItem?.ToString();
        tarife.Aktif = ckb_aktif.Checked;
        tarife.Oncelik = (int)nud_oncelik.Value;
        tarife.GecerlilikBaslangic = dtp_gecerlilikBaslangic.Value;
        tarife.GecerlilikBitis = ckb_suresizGecerli.Checked ? null : dtp_gecerlilikBitis.Value;
        tarife.Aciklama = tb_aciklama.Text.Trim();

        if (yeniKayit)
            _context.FiyatlandirmaTarifeler.Add(tarife);

        _context.SaveChanges();
        MessageBox.Show(yeniKayit ? "Tarife eklendi!" : "Tarife güncellendi!", "Baþarýlý", 
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        TarifeleriYukle();
        FormuTemizle();
    }

    private void Btn_sil_Click(object? sender, EventArgs e)
    {
        if (!_secilenTarifeId.HasValue)
        {
            MessageBox.Show("Lütfen silinecek tarifeyi seçin!");
            return;
        }

        var onay = MessageBox.Show("Seçili tarifeyi silmek istediðinize emin misiniz?", 
            "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (onay == DialogResult.Yes)
        {
            var tarife = _context.FiyatlandirmaTarifeler.Find(_secilenTarifeId.Value);
            if (tarife != null)
            {
                _context.FiyatlandirmaTarifeler.Remove(tarife);
                _context.SaveChanges();
                MessageBox.Show("Tarife silindi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TarifeleriYukle();
                FormuTemizle();
            }
        }
    }

    private void Btn_temizle_Click(object? sender, EventArgs e)
    {
        FormuTemizle();
    }

    private void Btn_filtre_Click(object? sender, EventArgs e)
    {
        string? filtre = cb_tarifeTuruFiltre.SelectedItem?.ToString();
        
        // "Tümü" veya boþ ise filtre yok
        if (string.IsNullOrEmpty(filtre) || filtre == "Tümü")
        {
            TarifeleriYukle(null);
        }
        else
        {
            TarifeleriYukle(filtre);
        }
    }

    private void FormuTemizle()
    {
        _secilenTarifeId = null;
        tb_tarifeAdi.Clear();
        nud_minDeger.Value = 0;
        nud_maxDeger.Value = 0;
        nud_deger.Value = 0;
        nud_oncelik.Value = 0;
        ckb_minDegerYok.Checked = false;
        ckb_maxDegerYok.Checked = false;
        ckb_aktif.Checked = true;
        ckb_suresizGecerli.Checked = true;
        tb_aciklama.Clear();
        btn_kaydet.Text = "Kaydet";
        dgv_tarifeler.ClearSelection();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context?.Dispose();
        }
        base.Dispose(disposing);
    }
}
