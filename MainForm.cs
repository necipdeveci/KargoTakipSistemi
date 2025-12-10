using kargotakipsistemi.Dogrulamalar;
using kargotakipsistemi.Entities;
using kargotakipsistemi.Forms;
using kargotakipsistemi.Servisler;
using kargotakipsistemi.Yardimcilar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace kargotakipsistemi;



public partial class MainForm : Form
{
    // Alanlar & Servis örnekleri
    private readonly PersonelServisi _personelServisi = new();
    private readonly SubeServisi _subeServisi = new();
    private readonly AracServisi _aracServisi = new();
    private readonly MusteriServisi _musteriServisi = new();
    private readonly GonderiServisi _gonderiServisi = new();
    private readonly DinamikUcretHesaplamaServisi _ucretServisi = new(); // VERİTABANI TABANLI HESAPLAMA

    private int? secilenPersonelId = null;
    private int? secilenSubeId = null;
    private int? secilenAracId = null;
    private int? secilenMusteriId = null;
    private int? secilenGonderiId = null;

    // Ücret hesaplama durum alanları
    private bool _ucretManuelDegisti = false; // Kullanıcı ücret alanını elle güncelledi mi?
    private bool _fiyatHesaplamaCalisiyor = false; // Hesaplama re-entrancy koruması

    // Tüm seçimleri tek seferde sıfırlamak için yardımcı metot
    private void SecimleriResetle()
    {
        secilenPersonelId = null;
        secilenSubeId = null;
        secilenAracId = null;
        secilenMusteriId = null;
        secilenGonderiId = null;
    }



private List<TabPage> _allTabPages;
private List<TabPage> _allTabPages2; // tabControl2 için

    // MainForm.cs içinde ApplyRoleBasedTabPermissions metodunu bulun ve şu değişiklikleri yapın:

    private void ApplyRoleBasedTabPermissions()
    {
        // TabControl1 kontrolü
        if (tabControl1 == null) return;

        // İlk çağırmada tüm sekmeleri sakla
        if (_allTabPages == null)
            _allTabPages = tabControl1.TabPages.Cast<TabPage>().ToList();

        // tabControl2 için de sakla (Operasyon sekmesi içinde)
        if (_allTabPages2 == null && tabControl2 != null)
            _allTabPages2 = tabControl2.TabPages.Cast<TabPage>().ToList();

        // Eğer CurrentUser yoksa tüm sekmeleri gizle (güvenlik-first)
        if (CurrentUser == null || CurrentUser.Rol?.RolAd == null)
        {
            tabControl1.TabPages.Clear();
            if (tabControl2 != null) tabControl2.TabPages.Clear();
            return;
        }

        string rolAd = CurrentUser.Rol.RolAd.Trim();

        // ======================================
        // ROL BAZLI SEKME İZİNLERİ
        // RolSeedData.cs dosyasındaki 18 rol tanımına göre yapılandırılmıştır
        // ======================================

        var roleToAllowedTabs = new Dictionary<string, RoleTabPermissions>(StringComparer.OrdinalIgnoreCase)
        {
            // ======================================
            // YÖNETİCİ ROLLERİ - Geniş Erişim
            // ======================================
            
            // Rol 1: Sistem Yöneticisi -> SADECE ANA İŞLEVSEL SEKMELERE ERİŞİM (tabPage4 ve tabPage5 KALDIRILDI)
            { "Sistem Yöneticisi", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageOperasyon", "tabPageMYonetim", "tabPage4", "tabPage5"}, // tabPage4 ve tabPage5 kaldırıldı
                OperationTabs = _allTabPages2?.Select(t => t.Name).ToArray() ?? Array.Empty<string>()
            }},
            
            // Rol 2: Genel Müdür -> tabPage4 ve tabPage5 KALDIRILDI
            { "Genel Müdür", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageOperasyon", "tabPageMYonetim" }, // tabPage4 ve tabPage5 kaldırıldı
                OperationTabs = new[] { "tabPage6", "tabPage7", "tabPage8" }
            }},
            
            // Rol 3: Bölge Müdürü -> Gönderi + Operasyon (Tüm Alt Sekmeler) + Müşteri
            { "Bölge Müdürü", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageOperasyon", "tabPageMYonetim" },
                OperationTabs = new[] { "tabPage6", "tabPage7", "tabPage8" }
            }},
            
            // Rol 4: Şube Müdürü -> Gönderi + Operasyon (Personel, Araç)
            { "Şube Müdürü", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageOperasyon", "tabPageMYonetim" },
                OperationTabs = new[] { "tabPage6", "tabPage8" } // Personel ve Araç
            }},
            
            // ======================================
            // OPERASYON ROLLERİ - Alan İşlemleri
            // ======================================
            
            // Rol 5: Kurye -> Sadece Gönderi Takibi
            { "Kurye", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi" },
                OperationTabs = Array.Empty<string>()
            }},
            
            // Rol 6: Dağıtım Sorumlusu -> Gönderi + Operasyon (Personel, Araç)
            { "Dağıtım Sorumlusu", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage6", "tabPage8" }
            }},
            
            // Rol 7: Transfer Personeli -> Gönderi + Şube Bilgileri
            { "Transfer Personeli", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage7" } // Sadece Şube
            }},
            
            // Rol 8: Depo Görevlisi -> Sadece Gönderi Kabul/Çıkış
            { "Depo Görevlisi", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi" },
                OperationTabs = Array.Empty<string>()
            }},
            
            // ======================================
            // MÜŞTERİ HİZMETLERİ ROLLERİ
            // ======================================
            
            // Rol 9: Müşteri Hizmetleri -> Gönderi + Müşteri
            { "Müşteri Hizmetleri", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageMYonetim" },
                OperationTabs = Array.Empty<string>()
            }},
            
            // Rol 10: Kargo Kabul -> Gönderi + Müşteri
            { "Kargo Kabul", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageMYonetim" },
                OperationTabs = Array.Empty<string>()
            }},
            
            // Rol 11: Çağrı Merkezi -> Gönderi + Müşteri
            { "Çağrı Merkezi", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageMYonetim" },
                OperationTabs = Array.Empty<string>()
            }},
            
            // ======================================
            // DESTEK VE YÖNETİM ROLLERİ
            // ======================================
            
            // Rol 12: Muhasebe -> Gönderi + Müşteri (Fatura İçin)
            { "Muhasebe", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageMYonetim" },
                OperationTabs = Array.Empty<string>()
            }},
            
            // Rol 13: İnsan Kaynakları -> Sadece Personel Yönetimi
            { "İnsan Kaynakları", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage6" } // Sadece Personel
            }},
            
            // Rol 14: Filo Yöneticisi -> Operasyon (Personel, Araç)
            { "Filo Yöneticisi", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage6", "tabPage8" } // Personel ve Araç
            }},
            
            // Rol 15: IT Destek -> Operasyon (Personel, Şube) - Sistem Ayarları
            { "IT Destek", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage6", "tabPage7" }
            }},
            
            // Rol 16: Kalite Kontrol -> Gönderi + Müşteri + Personel
            { "Kalite Kontrol", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageGonderi", "tabPageMYonetim", "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage6" }
            }},
            
            // Rol 17: Güvenlik -> Operasyon (Personel, Araç Giriş/Çıkış)
            { "Güvenlik", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage6", "tabPage8" }
            }},
            
            // Rol 18: Eğitim Koordinatörü -> Sadece Personel
            { "Eğitim Koordinatörü", new RoleTabPermissions
            {
                MainTabs = new[] { "tabPageOperasyon" },
                OperationTabs = new[] { "tabPage6" }
            }}
        };

        // Ana TabControl (tabControl1) temizle ve izinleri uygula
        tabControl1.TabPages.Clear();

        if (roleToAllowedTabs.TryGetValue(rolAd, out var permissions))
        {
            // Ana sekmeleri ekle (tabPage4 ve tabPage5 HİÇBİR ROLDE YOK, OTOMATIK GİZLİ KALACAK)
            foreach (var name in permissions.MainTabs.Distinct())
            {
                var tp = _allTabPages.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));
                if (tp != null)
                    tabControl1.TabPages.Add(tp);
            }

            // Operasyon alt sekmelerini ekle (eğer Operasyon sekmesi varsa)
            if (tabControl2 != null && _allTabPages2 != null)
            {
                tabControl2.TabPages.Clear();
                foreach (var name in permissions.OperationTabs.Distinct())
                {
                    var tp = _allTabPages2.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));
                    if (tp != null)
                        tabControl2.TabPages.Add(tp);
                }
            }
        }
        else
        {
            // Rol tanımlı değilse uyarı ver
            MessageBox.Show($"'{rolAd}' rolü için sekme izni tanımlı değil.\nLütfen sistem yöneticisi ile iletişime geçin.",
                "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Boş sekme uyarısı
        if (tabControl1.TabPages.Count == 0)
        {
            MessageBox.Show($"'{rolAd}' rolü için görüntülenebilir ana sekme bulunmamaktadır.",
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

// Yardımcı sınıf - Rol izinlerini tutmak için
private class RoleTabPermissions
{
    public string[] MainTabs { get; set; } = Array.Empty<string>();
    public string[] OperationTabs { get; set; } = Array.Empty<string>();
}

    private Personel CurrentUser;
    public MainForm(Personel currentUser)
    {
        CurrentUser = currentUser;
        InitializeComponent();
        cb_subeIl.SelectedIndexChanged += cb_subeIl_SelectedIndexChanged;
        cb_aracSube.SelectedIndexChanged += cb_aracSube_SelectedIndexChanged;
        // Gönderi: müşteri seçince adresleri doldur
        cb_gonderiGonderen.SelectedIndexChanged += cb_gonderiGonderen_SelectedIndexChanged;
        cb_gonderiAlici.SelectedIndexChanged += cb_gonderiAlici_SelectedIndexChanged;
        // Adres combolarındaki değişimleri izle (kural uygula)
        cb_gonderiGonderenAdres.SelectedIndexChanged += GonderiAdresCombo_SelectedIndexChanged;
        cb_gonderiAliciAdres.SelectedIndexChanged += GonderiAdresCombo_SelectedIndexChanged;
        // Gönderi grid selection
        dgv_gonderiler.SelectionChanged += dgv_gonderiler_SelectionChanged;
        // Personel bölümünde rol değişince araç listesi rol/subeye göre güncellensin
        cb_personelRol.SelectedIndexChanged += cb_personelRol_SelectedIndexChanged;
        cb_personelSube.SelectedIndexChanged += cb_personelSube_SelectedIndexChanged;
        // Müşteri grid seçim değişimi
        dgv_musteriler.SelectionChanged += dgv_musteriler_SelectionChanged;
        // Canlı müşteri filtreleme
        tb_musteriFiltre.TextChanged += tb_musteriFiltre_TextChanged;

        // Ücretlendirme için ilgili eventler
        tb_gonderiBoyut.TextChanged += GonderiFiyatBilesenDegisti;
        nud_gonderiAgirlik.ValueChanged += GonderiFiyatBilesenDegisti;
        cb_gonderiTeslimatTip.SelectedIndexChanged += GonderiFiyatBilesenDegisti;
        nud_gonderiIndirim.ValueChanged += GonderiFiyatBilesenDegisti;
        nud_gonderiEkMasraf.ValueChanged += GonderiFiyatBilesenDegisti;
        nud_gonderiUcret.ValueChanged += GonderiUcretManuelDegisti;

        // NumericUpDown maksimumları (varsayılan 100 olabilir, hesaplanan değerler taşmasın)
        nud_gonderiUcret.Maximum = 10000000;
        nud_gonderiIndirim.Maximum = 10000000;
        nud_gonderiEkMasraf.Maximum = 10000000;

        // Başlangıçta tüm kaydet butonları Kaydet modunda
        btn_personelKaydet.Text = "Kaydet";
        btn_musteriKaydet.Text = "Kaydet";
        btn_subeKaydet.Text = "Kaydet";
        btn_aracKaydet.Text = "Kaydet";
        btn_gonderiOlustur.Text = "Kaydet";
    }

    // -------------------------------------------------- ÜCRET HESAPLAMA --------------------------------------------------
    private void GonderiUcretManuelDegisti(object? sender, EventArgs e)
    {
        if (_fiyatHesaplamaCalisiyor) return; // otomatik hesaplamada tetiklendiyse yok say
        _ucretManuelDegisti = true; // Kullanıcı ücreti elle değiştirdi
        GonderiToplamFiyatGuncelle();
    }

    private void GonderiFiyatBilesenDegisti(object? sender, EventArgs e)
    {
        GonderiFiyatHesaplaVeGuncelle();
    }

    private void GonderiToplamFiyatGuncelle()
    {
        decimal toplam = _ucretServisi.ToplamFiyatHesapla(
            nud_gonderiUcret.Value,
            nud_gonderiIndirim.Value,
            nud_gonderiEkMasraf.Value);
        tb_gonderiToplamFiyat.Text = toplam.ToString("0.00");
    }

    private void GonderiFiyatHesaplaVeGuncelle()
    {
        try
        {
            _fiyatHesaplamaCalisiyor = true;

            // Mevcut manuel değerleri kontrol et
            decimal? mevcutIndirim = nud_gonderiIndirim.Value > 0 ? nud_gonderiIndirim.Value : null;
            decimal? mevcutEkMasraf = nud_gonderiEkMasraf.Value > 0 ? nud_gonderiEkMasraf.Value : null;

            // Ücret hesaplama servisini kullan
            var detay = _ucretServisi.UcretHesapla(
                nud_gonderiAgirlik.Value,
                tb_gonderiBoyut.Text,
                cb_gonderiTeslimatTip.SelectedItem?.ToString() ?? "Standart",
                mevcutIndirim,
                mevcutEkMasraf
            );

            // Ek masraf önerisini uygula (kullanıcı manual girmediyse)
            if (!detay.EkMasrafManuelMi)
            {
                nud_gonderiEkMasraf.Value = Math.Min(nud_gonderiEkMasraf.Maximum, detay.EkMasraf);
            }

            // Ücret alanını güncelle (kullanıcı manuel değiştirmediyse)
            if (!_ucretManuelDegisti)
            {
                nud_gonderiUcret.Value = Math.Min(nud_gonderiUcret.Maximum, detay.HamUcret);
            }

            // İndirim önerisini uygula (kullanıcı manuel girmediyse)
            if (!detay.IndirimManuelMi && detay.Indirim > 0)
            {
                nud_gonderiIndirim.Value = Math.Min(nud_gonderiIndirim.Maximum, detay.Indirim);
            }
        }
        finally
        {
            _fiyatHesaplamaCalisiyor = false;
            GonderiToplamFiyatGuncelle();
        }
    }

    private void cb_subeIl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_subeIl.SelectedValue != null)
        {
            int ilId;
            if (cb_subeIl.SelectedValue is int)
                ilId = (int)cb_subeIl.SelectedValue;
            else if (cb_subeIl.SelectedValue is Il il)
                ilId = il.IlId;
            else
                throw new InvalidOperationException("Seçilen değer beklenen türde değil.");

            VeriBaglamaServisi.KomboyaBagla(cb_subeIlce,
                ctx => ctx.Ilceler.Where(x => x.IlId == ilId),
                "IlceAd", "IlceId");
        }
    }

    // -------------------------------------------------- MÜŞTERİ BÖLÜMÜ --------------------------------------------------
    private void dgv_musteriler_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_musteriler.SelectedRows.Count > 0)
        {
            var row = dgv_musteriler.SelectedRows[0];
            secilenMusteriId = row.Cells["MusteriId"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["MusteriId"].Value) : (int?)null;
            tb_musteriAd.Text = row.Cells["Ad"].Value?.ToString() ?? string.Empty;
            tb_musteriSoyad.Text = row.Cells["Soyad"].Value?.ToString() ?? string.Empty;
            tb_musteriEposta.Text = row.Cells["Mail"].Value?.ToString() ?? string.Empty;
            tb_musteriTel.Text = row.Cells["Tel"].Value?.ToString() ?? string.Empty;
            tb_musteriNot.Text = row.Cells["Notlar"].Value?.ToString() ?? string.Empty;
            dtp_musteriDogumTarih.Value = row.Cells["DogumTarihi"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["DogumTarihi"].Value) : DateTime.Now;
            btnMusterAdresYonet.Enabled = secilenMusteriId.HasValue;

            // Kaydet buton metni: seçim var -> Güncelle
            btn_musteriKaydet.Text = "Güncelle";
        }
        else
        {
            secilenMusteriId = null;
            btnMusterAdresYonet.Enabled = false;
            // Seçim yok -> Kaydet modu
            btn_musteriKaydet.Text = "Kaydet";
        }
    }

    private void btn_musteriKaydet_Click(object sender, EventArgs e)
    {
        if (!MusteriDogrulayici.Dogrula(tb_musteriAd, tb_musteriSoyad, tb_musteriEposta, tb_musteriTel, dtp_musteriDogumTarih, tb_musteriNot))
            return;

        using (var ctx = new KtsContext())
        {
            Musteri musteri;
            bool guncelleme = secilenMusteriId.HasValue;
            if (guncelleme)
            {
                musteri = _musteriServisi.KimlikleGetir(ctx, secilenMusteriId!.Value);
                if (musteri == null)
                {
                    MessageBox.Show("Müşteri bulunamadı.");
                    return;
                }
            }
            else
            {
                musteri = _musteriServisi.Olustur(ctx);
            }

            musteri.Ad = tb_musteriAd.Text;
            musteri.Soyad = tb_musteriSoyad.Text;
            musteri.Mail = tb_musteriEposta.Text;
            musteri.Tel = tb_musteriTel.Text;
            musteri.Notlar = tb_musteriNot.Text;
            musteri.DogumTarihi = dtp_musteriDogumTarih.Value;

            ctx.SaveChanges();
            SecimleriResetle(); // Seçilen ID'leri hemen sıfırla
            MessageBox.Show(guncelleme ? "Müşteri güncellendi." : "Müşteri eklendi.");
            VeriBaglamaServisi.IzgaraBagla(dgv_musteriler, c => _musteriServisi.IzgaraIcinProjeksiyon(c));
        }

        KontrolleriTemizle(tb_musteriAd, tb_musteriSoyad, tb_musteriEposta, tb_musteriTel, tb_musteriNot, dtp_musteriDogumTarih);
        btnMusterAdresYonet.Enabled = false;
        btn_musteriKaydet.Text = "Kaydet"; // Kaydet moduna dön
    }

    private void btn_musteriKayitSil_Click(object sender, EventArgs e)
    {
        if (!secilenMusteriId.HasValue)
        {
            MessageBox.Show("Lütfen silmek için bir müşteri seçin.");
            return;
        }
        if (!OnayYardimcisi.SilmeOnayi("Müşteri Sil", "Seçili müşteriyi silmek istediğinize emin misiniz?"))
            return;

        using (var ctx = new KtsContext())
        {
            _musteriServisi.Sil(ctx, secilenMusteriId.Value);
            ctx.SaveChanges();
            SecimleriResetle(); // Silme sonrası ID'leri sıfırla
            VeriBaglamaServisi.IzgaraBagla(dgv_musteriler, c => _musteriServisi.IzgaraIcinProjeksiyon(c));
        }

        KontrolleriTemizle(tb_musteriAd, tb_musteriSoyad, tb_musteriEposta, tb_musteriTel, tb_musteriNot, dtp_musteriDogumTarih);
        btnMusterAdresYonet.Enabled = false;
        btn_musteriKaydet.Text = "Kaydet";
    }

    private void btn_musteriFormTemizle_Click(object sender, EventArgs e)
    {
        SecimleriResetle();
        KontrolleriTemizle(tb_musteriAd, tb_musteriSoyad, tb_musteriEposta, tb_musteriTel, tb_musteriNot, dtp_musteriDogumTarih);
        btnMusterAdresYonet.Enabled = false;
        VeriBaglamaServisi.IzgaraBagla(dgv_musteriler, c => _musteriServisi.IzgaraIcinProjeksiyon(c));
        btn_musteriKaydet.Text = "Kaydet";
    }

    private void btnMusterAdresYonet_Click(object sender, EventArgs e)
    {
        if (!secilenMusteriId.HasValue)
        {
            MessageBox.Show("Adres yönetimi için önce müşteri seçin.");
            return;
        }
        var form = new AdresEkleForm(secilenMusteriId.Value, "Musteri");
        form.ShowDialog();
        VeriBaglamaServisi.IzgaraBagla(dgv_musteriler, c => _musteriServisi.IzgaraIcinProjeksiyon(c));
    }

    // -------------------------------------------------- ARAÇ BÖLÜMÜ --------------------------------------------------
    private void cb_aracSube_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_aracSube.SelectedValue is int subeId)
        {
            using (var ctx = new KtsContext())
            {
                var sube = ctx.Subeler.Find(subeId);
                if (sube != null)
                {
                    // GPS artık otomatik değil
                }
            }
        }
    }

    private void cb_personelRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
    }

    private void cb_personelSube_SelectedIndexChanged(object sender, EventArgs e)
    {
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
    }

    private void KontrolleriTemizle(params Control[] controls) => KontrolYardimcisi.Temizle(controls);

    private void MainForm_Load(object sender, EventArgs e)
    {
        _allTabPages = tabControl1.TabPages.Cast<TabPage>().ToList();

        this.BackColor = Color.FromArgb(242, 242, 242);

        splitContainer1.BackColor = Color.FromArgb(234, 228, 213);
        
        // DataGridView başlık stilleri - dgv_gonderiler
        dgv_gonderiler.BackgroundColor = Color.FromArgb(182, 176, 159);
        dgv_gonderiler.BorderStyle = BorderStyle.None;
        dgv_gonderiler.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        dgv_gonderiler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv_gonderiler.EnableHeadersVisualStyles = false;
        dgv_gonderiler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(182, 176, 159);
        dgv_gonderiler.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        
        // DataGridView başlık stilleri - dgv_seciliGonderiDetay
        dgv_seciliGonderiDetay.BackgroundColor = Color.FromArgb(182, 176, 159);
        dgv_seciliGonderiDetay.BorderStyle = BorderStyle.None;
        dgv_seciliGonderiDetay.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        dgv_seciliGonderiDetay.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv_seciliGonderiDetay.EnableHeadersVisualStyles = false;
        dgv_seciliGonderiDetay.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(182, 176, 159);
        dgv_seciliGonderiDetay.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        
        // DataGridView başlık stilleri - dgv_musteriler
        dgv_musteriler.BackgroundColor = Color.FromArgb(182, 176, 159);
        dgv_musteriler.BorderStyle = BorderStyle.None;
        dgv_musteriler.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        dgv_musteriler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv_musteriler.EnableHeadersVisualStyles = false;
        dgv_musteriler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(182, 176, 159);
        dgv_musteriler.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        
        // DataGridView başlık stilleri - dgv_personeller
        dgv_personeller.BackgroundColor = Color.FromArgb(182, 176, 159);
        dgv_personeller.BorderStyle = BorderStyle.None;
        dgv_personeller.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        dgv_personeller.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv_personeller.EnableHeadersVisualStyles = false;
        dgv_personeller.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(182, 176, 159);
        dgv_personeller.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        
        // DataGridView başlık stilleri - dgv_subeler
        dgv_subeler.BackgroundColor = Color.FromArgb(182, 176, 159);
        dgv_subeler.BorderStyle = BorderStyle.None;
        dgv_subeler.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        dgv_subeler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv_subeler.EnableHeadersVisualStyles = false;
        dgv_subeler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(182, 176, 159);
        dgv_subeler.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        
        // DataGridView başlık stilleri - dgv_araclar
        dgv_araclar.BackgroundColor = Color.FromArgb(182, 176, 159);
        dgv_araclar.BorderStyle = BorderStyle.None;
        dgv_araclar.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        dgv_araclar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgv_araclar.EnableHeadersVisualStyles = false;
        dgv_araclar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(182, 176, 159);
        dgv_araclar.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;


        cb_gonderiGonderen.BackColor = Color.FromArgb(182, 176, 159);
        cb_gonderiGonderenAdres.BackColor = Color.FromArgb(182, 176, 159);
        cb_gonderiAlici.BackColor = Color.FromArgb(182, 176, 159);
        cb_gonderiAliciAdres.BackColor = Color.FromArgb(182, 176, 159);

        cb_gonderiGonderen.ForeColor = Color.FromArgb(0, 0, 0);
        cb_gonderiGonderenAdres.ForeColor = Color.FromArgb(0, 0, 0);
        cb_gonderiAlici.ForeColor = Color.FromArgb(0, 0, 0);
        cb_gonderiAliciAdres.ForeColor = Color.FromArgb(0, 0, 0);

        cb_gonderiGonderen.FlatStyle = FlatStyle.Flat;
        cb_gonderiGonderenAdres.FlatStyle = FlatStyle.Flat;
        cb_gonderiAlici.FlatStyle = FlatStyle.Flat;
        cb_gonderiAliciAdres.FlatStyle = FlatStyle.Flat;

        tb_gonderiTakipNo.BackColor = Color.FromArgb(182, 176, 159);
        nud_gonderiAgirlik.BackColor = Color.FromArgb(182, 176, 159);
        tb_gonderiBoyut.BackColor = Color.FromArgb(182, 176, 159);
        cb_gonderiTeslimatTip.BackColor = Color.FromArgb(182, 176, 159);
        cb_gonderiCikisSube.BackColor = Color.FromArgb(182, 176, 159);
        cb_gonderiAtananKurye.BackColor = Color.FromArgb(182, 176, 159);
        dtp_gonderiTarih.BackColor = Color.FromArgb(182, 176, 159);
        dtp_gonderiTahminiTeslimTarih.BackColor = Color.FromArgb(182, 176, 159);


        tb_gonderiTakipNo.BorderStyle = BorderStyle.FixedSingle;
        nud_gonderiAgirlik.BorderStyle = BorderStyle.FixedSingle;
        tb_gonderiBoyut.BorderStyle = BorderStyle.FixedSingle;
        cb_gonderiTeslimatTip.FlatStyle = FlatStyle.Flat;
        cb_gonderiCikisSube.FlatStyle = FlatStyle.Flat;
        cb_gonderiAtananKurye.FlatStyle = FlatStyle.Flat;

        nud_gonderiUcret.BackColor = Color.FromArgb(182, 176, 159);
        nud_gonderiEkMasraf.BackColor = Color.FromArgb(182, 176, 159);
        nud_gonderiIndirim.BackColor = Color.FromArgb(182, 176, 159);
        tb_gonderiToplamFiyat.BackColor = Color.FromArgb(182, 176, 159);
        btn_gonderiTarifeYonetim.BackColor = Color.FromArgb(182, 176, 159);

        
        nud_gonderiUcret.BorderStyle = BorderStyle.FixedSingle;
        nud_gonderiEkMasraf.BorderStyle = BorderStyle.FixedSingle;
        nud_gonderiIndirim.BorderStyle = BorderStyle.FixedSingle;
        tb_gonderiToplamFiyat.BorderStyle = BorderStyle.FixedSingle;

        btn_gonderiKayitSil.FlatStyle = FlatStyle.Flat;
        btn_gonderiOlustur.FlatStyle = FlatStyle.Flat;
        btn_gonderiFormTemizle.FlatStyle = FlatStyle.Flat;
        btn_gonderiSurecYonetim.FlatStyle = FlatStyle.Flat;

        btn_gonderiKayitSil.BackColor = Color.FromArgb(182, 176, 159);
        btn_gonderiFormTemizle.BackColor = Color.FromArgb(182, 176, 159);
        btn_gonderiOlustur.BackColor = Color.FromArgb(182, 176, 159);
        btn_gonderiSurecYonetim.BackColor = Color.FromArgb(182, 176, 159);

        tb_filtreTakipNo.BackColor = Color.FromArgb(182, 176, 159);
        tb_filtreTeslimAlanAdSoyad.BackColor = Color.FromArgb(182, 176, 159);
        cb_filtreIdTipSec.BackColor = Color.FromArgb(182, 176, 159);
        tb_filtreId.BackColor = Color.FromArgb(182, 176, 159);
        tb_filtreIdAdSoyad.BackColor = Color.FromArgb(182, 176, 159);
        btn_gonderiAra.BackColor = Color.FromArgb(182, 176, 159);

        tb_filtreTakipNo.BorderStyle = BorderStyle.FixedSingle;
        tb_filtreTeslimAlanAdSoyad.BorderStyle = BorderStyle.FixedSingle;
        cb_filtreIdTipSec.FlatStyle = FlatStyle.Flat;
        tb_filtreId.BorderStyle = BorderStyle.FixedSingle;
        tb_filtreIdAdSoyad.BorderStyle = BorderStyle.FixedSingle;
        btn_gonderiAra.FlatStyle = FlatStyle.Flat;

        dgv_gonderiler.BorderStyle = BorderStyle.None;
        dgv_seciliGonderiDetay.BorderStyle = BorderStyle.None;

        dgv_gonderiler.BackgroundColor = Color.FromArgb(182, 176, 159);
        dgv_seciliGonderiDetay.BackgroundColor = Color.FromArgb(182, 176, 159);

        splitContainer2.BackColor = Color.FromArgb(234, 228, 213);
        tb_musteriAd.BackColor = Color.FromArgb(182, 176, 159);
        tb_musteriSoyad.BackColor = Color.FromArgb(182, 176, 159);
        tb_musteriTel.BackColor = Color.FromArgb(182, 176, 159);
        tb_musteriEposta.BackColor = Color.FromArgb(182, 176, 159);
        tb_musteriNot.BackColor = Color.FromArgb(182, 176, 159);
        btnMusterAdresYonet.BackColor = Color.FromArgb(182, 176, 159);
        btn_musteriKaydet.BackColor = Color.FromArgb(182, 176, 159);
        btn_musteriKayitSil.BackColor = Color.FromArgb(182, 176, 159);
        btn_musteriFormTemizle.BackColor = Color.FromArgb(182, 176, 159);

        tb_musteriFiltre.BackColor = Color.FromArgb(182, 176, 159);

        dgv_musteriler.BorderStyle = BorderStyle.None;
        dgv_musteriler.BackgroundColor = Color.FromArgb(182, 176, 159);

        splitContainer3.BackColor = Color.FromArgb(234, 228, 213);
        cb_personelFiltre.BackColor = Color.FromArgb(182, 176, 159);
        dgv_personeller.BorderStyle = BorderStyle.None;
        dgv_personeller.BackgroundColor = Color.FromArgb(182, 176, 159);

        btn_personelAra.BackColor = Color.FromArgb(182, 176, 159);
        tb_personelAd.BackColor = Color.FromArgb(182, 176, 159);
        tb_personelSoyad.BackColor = Color.FromArgb(182, 176, 159);
        tb_personelTel.BackColor = Color.FromArgb(182, 176, 159);
        tb_personelMail.BackColor = Color.FromArgb(182, 176, 159);
        tb_personelSifre.BackColor = Color.FromArgb(182, 176, 159);
        cb_personelRol.BackColor = Color.FromArgb(182, 176, 159);
        cb_personelArac.BackColor = Color.FromArgb(182, 176, 159);
        cb_personelSube.BackColor = Color.FromArgb(182, 176, 159);
        cb_personelCinsiyet.BackColor = Color.FromArgb(182, 176, 159);
        nud_personelMaas.BackColor = Color.FromArgb(182, 176, 159);
        btn_tempSifre.BackColor = Color.FromArgb(182, 176, 159);
        cb_personelEhliyet.BackColor = Color.FromArgb(182, 176, 159);
        btn_personelKaydet.BackColor = Color.FromArgb(182, 176, 159);
        btn_personelKayitSil.BackColor = Color.FromArgb(182, 176, 159);
        btn_personelFormTemizle.BackColor = Color.FromArgb(182, 176, 159);

        splitContainer4.BackColor = Color.FromArgb(234, 228, 213);
        cb_subeFiltre.BackColor = Color.FromArgb(182, 176, 159);
        dgv_subeler.BorderStyle = BorderStyle.None;
        dgv_subeler.BackgroundColor = Color.FromArgb(182, 176, 159);
        btn_subeAra.BackColor = Color.FromArgb(182, 176, 159);
        tb_subeAd.BackColor = Color.FromArgb(182, 176, 159);
        cb_subeTip.BackColor = Color.FromArgb(182, 176, 159);
        tb_subeTel.BackColor = Color.FromArgb(182, 176, 159);
        tb_subeMail.BackColor = Color.FromArgb(182, 176, 159);
        cb_subeIl.BackColor = Color.FromArgb(182, 176, 159);
        cb_subeIlce.BackColor = Color.FromArgb(182, 176, 159);
        nud_subeKapasite.BackColor = Color.FromArgb(182, 176, 159);
        cb_subeCalismaSaat.BackColor = Color.FromArgb(182, 176, 159);
        tbm_subeAcikAdres.BackColor = Color.FromArgb(182, 176, 159);
        btn_subeKaydet.BackColor = Color.FromArgb(182, 176, 159);
        btn_subeKayitSil.BackColor = Color.FromArgb(182, 176, 159);
        btn_subeFormTemizle.BackColor = Color.FromArgb(182, 176, 159);

        splitContainer5.BackColor = Color.FromArgb(234, 228, 213);
        cb_aracFiltre.BackColor = Color.FromArgb(182, 176, 159);
        dgv_araclar.BorderStyle = BorderStyle.None;
        dgv_araclar.BackgroundColor = Color.FromArgb(182, 176, 159);
        btn_aracAra.BackColor = Color.FromArgb(182, 176, 159);
        tb_aracPlaka.BackColor = Color.FromArgb(182, 176, 159);
        cb_aracTip.BackColor = Color.FromArgb(182, 176, 159);
        cb_aracSube.BackColor = Color.FromArgb(182, 176, 159);
        nud_aracKapasite.BackColor = Color.FromArgb(182, 176, 159);
        tb_aracGps.BackColor = Color.FromArgb(182, 176, 159);
        cb_aracDurum.BackColor = Color.FromArgb(182, 176, 159);
        btn_aracKaydet.BackColor = Color.FromArgb(182, 176, 159);
        btn_aracSil.BackColor = Color.FromArgb(182, 176, 159);
        btn_aracFormTemizle.BackColor = Color.FromArgb(182, 176, 159);





        VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
        VeriBaglamaServisi.KomboyaBagla(cb_personelRol, ctx => ctx.Roller, "RolAd", "RolId");
        cb_personelRol.SelectedIndex = cb_personelRol.Items.Count > 0 ? 0 : -1;
        VeriBaglamaServisi.KomboyaBagla(cb_personelSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        cb_personelSube.SelectedIndex = cb_personelSube.Items.Count > 0 ? 0 : -1;
        cb_personelCinsiyet.Items.Clear();
        cb_personelCinsiyet.Items.Add("Erkek");
        cb_personelCinsiyet.Items.Add("Kadın");
        cb_personelCinsiyet.SelectedIndex = 0;
        cb_personelEhliyet.Items.Clear();
        cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
        cb_personelEhliyet.SelectedIndex = -1;
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
        VeriBaglamaServisi.IzgaraBagla(dgv_musteriler, ctx => _musteriServisi.IzgaraIcinProjeksiyon(ctx));
        btnMusterAdresYonet.Enabled = false;
        VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
        VeriBaglamaServisi.KomboyaBagla(cb_subeIl, ctx => ctx.Iller, "IlIdVeAd", "IlId");
        SubeFiltreyiGuncelle();
        cb_subeTip.Items.Clear();
        cb_subeTip.Items.Add("Merkez");
        cb_subeTip.Items.Add("Şube");
        cb_subeTip.Items.Add("Dağıtım Noktası");
        cb_subeTip.Items.Add("Depo");
        cb_subeTip.Items.Add("Transfer Merkezi");
        cb_subeTip.Items.Add("Kargo Kabul");
        cb_subeTip.Items.Add("Teslimat Noktası");
        cb_subeCalismaSaat.Items.Clear();
        cb_subeCalismaSaat.Items.Add("08:00 - 17:00");
        cb_subeCalismaSaat.Items.Add("09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("10:00 - 19:00");
        cb_subeCalismaSaat.Items.Add("24 Saat Açık");
        cb_subeCalismaSaat.Items.Add("Hafta İçi 09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("Hafta Sonu Kapalı");
        cb_subeCalismaSaat.Items.Add("Hafta Sonu 10:00 - 16:00");
        VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
        VeriBaglamaServisi.KomboyaBagla(cb_aracSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        using (var context = new KtsContext())
        {
            var tipler = context.Araclar.Select(a => a.AracTip).Distinct().ToList();
            cb_aracFiltre.Items.Clear();
            cb_aracFiltre.Items.AddRange(tipler.ToArray());
        }
        cb_aracTip.Items.Clear();
        cb_aracTip.Items.Add("Kamyonet");
        cb_aracTip.Items.Add("Panelvan");
        cb_aracTip.Items.Add("Tır");
        cb_aracTip.Items.Add("Minibüs");
        cb_aracTip.Items.Add("Otomobil");
        cb_aracTip.Items.Add("Motosiklet");
        cb_aracTip.Items.Add("Diğer");
        cb_aracDurum.Items.Clear();
        cb_aracDurum.Items.Add("Aktif");
        cb_aracDurum.Items.Add("Bakımda");
        cb_aracDurum.Items.Add("Arızalı");
        cb_aracDurum.Items.Add("Pasif");
        cb_aracDurum.Items.Add("Serviste");
        cb_aracDurum.Items.Add("Kullanımda");

        // Personel filtre combobox'ını SADECE mevcut personel kayıtlarından (distinct) doldur
        using (var context = new KtsContext())
        {
            var personelTipleri = context.Personeller
                .Select(p => p.Rol.RolAd)
                .Where(rolAd => rolAd != null && rolAd != "")
                .Distinct()
                .ToList();
            cb_personelFiltre.Items.Clear();
            if (personelTipleri.Count > 0)
            {
                cb_personelFiltre.Items.AddRange(personelTipleri.Cast<object>().ToArray());
            }
        }

        // ID Tip Seç ComboBox'ını doldur
        cb_filtreIdTipSec.Items.Clear();
        cb_filtreIdTipSec.Items.Add("-");
        cb_filtreIdTipSec.Items.Add("Gönderen");
        cb_filtreIdTipSec.Items.Add("Alıcı");
        cb_filtreIdTipSec.Items.Add("Kurye");
        cb_filtreIdTipSec.SelectedIndex = 0;
        // Gönderi Combobox ve Grid bağlama
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiGonderen, ctx => ctx.Musteriler.OrderBy(m => m.Ad), nameof(Musteri.Ad), nameof(Musteri.MusteriId));
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiAlici, ctx => ctx.Musteriler.OrderBy(m => m.Ad), nameof(Musteri.Ad), nameof(Musteri.MusteriId));
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiCikisSube, ctx => ctx.Subeler.OrderBy(s => s.SubeAd), nameof(Sube.SubeAd), nameof(Sube.SubeId));
        // Kurye combosu sadece rolü Kurye olan aktif personellerle doldurulur (tüm şubelerden başlangıçta)
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiAtananKurye,
            ctx => ctx.Personeller.Include(p => p.Rol).Where(p => p.Aktif && p.Rol.RolAd == "Kurye").OrderBy(p => p.Ad),
            nameof(Personel.Ad), nameof(Personel.PersonelId));

        // Teslimat tipi combobox'ını veritabanındaki tarifelerden doldur
        TeslimatTipiCombosunuDoldur();

        VeriBaglamaServisi.IzgaraBagla(dgv_gonderiler, ctx => _gonderiServisi.IzgaraIcinProjeksiyon(ctx));
        // İlk yüklemede hesaplamayı tetikle
        GonderiFiyatHesaplaVeGuncelle();

        ApplyRoleBasedTabPermissions();
    }

    // Gönderen seçilince adresleri doldur
    private void cb_gonderiGonderen_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cb_gonderiGonderen.SelectedValue is int mid)
        {
            AdresCombosunuBagla(cb_gonderiGonderenAdres, mid);
        }
        AdresTipiFarkiniZorla();
        TakipNoHazirsaUretVeAyarla();
        GonderiFiyatHesaplaVeGuncelle();
    }
    private void cb_gonderiAlici_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cb_gonderiAlici.SelectedValue is int mid)
        {
            AdresCombosunuBagla(cb_gonderiAliciAdres, mid);
        }
        AdresTipiFarkiniZorla();
        TakipNoHazirsaUretVeAyarla();
        GonderiFiyatHesaplaVeGuncelle();
    }

    private void GonderiAdresCombo_SelectedIndexChanged(object? sender, EventArgs e)
    {
        AdresTipiFarkiniZorla();
        TakipNoHazirsaUretVeAyarla();
    }

    private void dgv_gonderiler_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgv_gonderiler.SelectedRows.Count > 0)
        {
            var row = dgv_gonderiler.SelectedRows[0];
            secilenGonderiId = row.Cells["GonderiId"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["GonderiId"].Value) : (int?)null;
            tb_gonderiTakipNo.Text = row.Cells["TakipNo"].Value?.ToString() ?? string.Empty;
            tb_gonderiBoyut.Text = row.Cells["Boyut"].Value?.ToString() ?? string.Empty;
            nud_gonderiAgirlik.Value = row.Cells["Agirlik"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Agirlik"].Value) : nud_gonderiAgirlik.Minimum;
            cb_gonderiTeslimatTip.SelectedItem = row.Cells["TeslimatTipi"].Value?.ToString() ?? null;
            dtp_gonderiTarih.Value = row.Cells["GonderiTarihi"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["GonderiTarihi"].Value) : DateTime.Now;
            dtp_gonderiTahminiTeslimTarih.Value = row.Cells["TahminiTeslimTarihi"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["TahminiTeslimTarihi"].Value) : DateTime.Now.AddDays(2);
            nud_gonderiUcret.Value = row.Cells["Ucret"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Ucret"].Value) : 0;
            nud_gonderiIndirim.Value = row.Cells["IndirimTutar"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["IndirimTutar"].Value) : 0;
            nud_gonderiEkMasraf.Value = row.Cells["EkMasraf"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["EkMasraf"].Value) : 0;

            // Veritabanından ilgili gönderiyi yükleyerek FK bilgilerini al
            if (secilenGonderiId.HasValue)
            {
                using var ctx = new KtsContext();
                var gonderi = ctx.Gonderiler
                    .Include(g => g.Gonderen)
                    .Include(g => g.Alici)
                    .Include(g => g.GonderenAdres)
                    .Include(g => g.AliciAdres)
                    .Include(g => g.Kurye)
                    .FirstOrDefault(g => g.GonderiId == secilenGonderiId.Value);

                if (gonderi != null)
                {
                    // Gönderen ComboBox
                    if (gonderi.GonderenId > 0)
                    {
                        cb_gonderiGonderen.SelectedValue = gonderi.GonderenId;
                        // Gönderen adreslerini doldur
                        if (gonderi.GonderenId > 0)
                            AdresCombosunuBagla(cb_gonderiGonderenAdres, gonderi.GonderenId);
                    }

                    // Alıcı ComboBox
                    if (gonderi.AliciId > 0)
                    {
                        cb_gonderiAlici.SelectedValue = gonderi.AliciId;
                        // Alıcı adreslerini doldur
                        if (gonderi.AliciId > 0)
                            AdresCombosunuBagla(cb_gonderiAliciAdres, gonderi.AliciId);
                    }

                    // Gönderen Adresi
                    if (gonderi.GonderenAdresId.HasValue)
                        cb_gonderiGonderenAdres.SelectedValue = gonderi.GonderenAdresId.Value;

                    // Alıcı Adresi
                    if (gonderi.AliciAdresId.HasValue)
                        cb_gonderiAliciAdres.SelectedValue = gonderi.AliciAdresId.Value;

                    // Çıkış Şubesi (önce kolonu kontrol et)
                    try
                    {
                        var cikisSubeCell = row.Cells["CikisSubeId"];
                        if (cikisSubeCell != null && cikisSubeCell.Value != DBNull.Value)
                        {
                            var cikisSubeId = Convert.ToInt32(cikisSubeCell.Value);
                            cb_gonderiCikisSube.SelectedValue = cikisSubeId;
                        }
                    }
                    catch
                    {
                        // CikisSubeId kolonu yoksa sessizce devam et
                    }

                    // Atanan Kurye
                    if (gonderi.KuryeId.HasValue)
                        cb_gonderiAtananKurye.SelectedValue = gonderi.KuryeId.Value;
                }
            }

            _ucretManuelDegisti = true; // seçili kaydı yüklerken hesaplamayı override etme
            GonderiToplamFiyatGuncelle();

            // Seçili gönderi detay panelini güncelle
            GonderiDetayPaneliniGuncelle();

            // Kaydet buton metni: seçim var -> Güncelle
            btn_gonderiOlustur.Text = "Güncelle";
        }
        else
        {
            secilenGonderiId = null;
            // Paneli temizle
            dgv_seciliGonderiDetay.DataSource = null;
            // Seçim yok -> Kaydet modu
            btn_gonderiOlustur.Text = "Kaydet";
        }
    }

    private void btn_gonderiOlustur_Click(object? sender, EventArgs e)
    {
        // MainForm.cs içinde btn_gonderiOlustur_Click metodunda GonderiDogrulayici.Dogrula çağrısı:
        if (!kargotakipsistemi.Dogrulamalar.GonderiDogrulayici.Dogrula(
            cb_gonderiGonderen,
            cb_gonderiGonderenAdres,
            cb_gonderiAlici,
            cb_gonderiAliciAdres,
            tb_gonderiBoyut,
            nud_gonderiAgirlik,
            cb_gonderiTeslimatTip,
            dtp_gonderiTarih,
            dtp_gonderiTahminiTeslimTarih, // <-- eksik olan parametre
            nud_gonderiUcret,
            nud_gonderiIndirim,
            nud_gonderiEkMasraf)) // <-- eksik olan parametre eklendi
        {
            return; // doğrulama başarısız
        }

        // Zorunlu seçimler ve adres kısıtı kontrolü (ek güvenlik)
        if (!GonderiZorunluSecimlerTam())
        {
            MessageBox.Show("Gönderici/alıcı ve adres seçimlerini tamamlayın.");
            return;
        }
        if (!IsAdresKisitSaglandi())
        {
            MessageBox.Show("Aynı müşteri için farklı adresler seçmalısınız.");
            return;
        }

        using var ctx = new KtsContext();
        Gonderi g;
        bool guncelleme = secilenGonderiId.HasValue;
        if (guncelleme)
        {
            g = _gonderiServisi.KimlikleGetir(ctx, secilenGonderiId!.Value);
            if (g == null)
            {
                MessageBox.Show("Gönderi bulunamadı.");
                return;
            }
        }
        else
        {
            g = _gonderiServisi.Olustur(ctx);
        }
        // Takip no üretimi
        if (string.IsNullOrWhiteSpace(tb_gonderiTakipNo.Text))
        {
            tb_gonderiTakipNo.Text = RasgeleTakipNo8Hane();
        }
        g.TakipNo = tb_gonderiTakipNo.Text;
        // FK'lar
        if (cb_gonderiGonderen.SelectedValue is int gid) g.GonderenId = gid;
        if (cb_gonderiAlici.SelectedValue is int aid) g.AliciId = aid;
        g.GonderenAdresId = cb_gonderiGonderenAdres.SelectedValue as int?;
        g.AliciAdresId = cb_gonderiAliciAdres.SelectedValue as int?;
        g.KuryeId = cb_gonderiAtananKurye.SelectedValue as int?;
        // Temel alanlar
        g.GonderiTarihi = dtp_gonderiTarih.Value;
        g.TahminiTeslimTarihi = dtp_gonderiTahminiTeslimTarih.Value;
        g.TeslimatTipi = cb_gonderiTeslimatTip.SelectedItem?.ToString();
        g.Agirlik = nud_gonderiAgirlik.Value;
        g.Boyut = tb_gonderiBoyut.Text;
        g.Ucret = nud_gonderiUcret.Value;
        g.IndirimTutar = nud_gonderiIndirim.Value;
        g.EkMasraf = nud_gonderiEkMasraf.Value;
        g.GuncellemeTarihi = DateTime.Now;
        g.TeslimEdilenKisi = string.Empty;
        GonderiToplamFiyatGuncelle();

        ctx.SaveChanges();
        SecimleriResetle();
        MessageBox.Show(guncelleme ? "Gönderi güncellendi." : "Gönderi eklendi.");
        VeriBaglamaServisi.IzgaraBagla(dgv_gonderiler, c => _gonderiServisi.IzgaraIcinProjeksiyon(c));
        _ucretManuelDegisti = false; // yeni işlem sonrası tekrar otomatik hesaplanabilir

        // Kaydet sonrası formu tamamen temizle ve buton yazısını resetle
        btn_gonderiFormTemizle_Click(sender, e);
        btn_gonderiOlustur.Text = "Kaydet";
    }

    private void btn_gonderiKayitSil_Click(object? sender, EventArgs e)
    {
        if (!secilenGonderiId.HasValue)
        {
            MessageBox.Show("Lütfen silmek için bir gönderi seçin.");
            return;
        }
        if (!OnayYardimcisi.SilmeOnayi("Gönderi Sil", "Seçili gönderiyi silmek istediğinize emin misiniz?"))
            return;

        using var ctx = new KtsContext();
        _gonderiServisi.Sil(ctx, secilenGonderiId.Value);
        ctx.SaveChanges();
        SecimleriResetle();
        VeriBaglamaServisi.IzgaraBagla(dgv_gonderiler, ctx => _gonderiServisi.IzgaraIcinProjeksiyon(ctx));
        btn_gonderiFormTemizle.PerformClick();
        MessageBox.Show("Gönderi silindi.");
        btn_gonderiOlustur.Text = "Kaydet";
    }

    private void btn_gonderiFormTemizle_Click(object? sender, EventArgs e)
    {
        SecimleriResetle();

        // Form kontrolleri temizleme
        KontrolleriTemizle(
            tb_gonderiTakipNo,
            tb_gonderiBoyut,
            cb_gonderiTeslimatTip,
            cb_gonderiGonderen,
            cb_gonderiGonderenAdres,
            cb_gonderiAlici,
            cb_gonderiAliciAdres,
            cb_gonderiAtananKurye,
            cb_gonderiCikisSube,
            nud_gonderiAgirlik,
            nud_gonderiUcret,
            nud_gonderiIndirim,
            nud_gonderiEkMasraf,
            tb_gonderiToplamFiyat,
            dtp_gonderiTarih,
            dtp_gonderiTahminiTeslimTarih
        );

        // Arama/Filtreleme kontrolleri temizleme
        KontrolleriTemizle(
            tb_filtreTakipNo,
            tb_filtreId,
            tb_filtreIdAdSoyad,
            tb_filtreTeslimAlanAdSoyad,
            cb_filtreIdTipSec
        );

        // Tarih filtrelerini bugüne ayarla
        dtp_filtreIlkTarih.Value = DateTime.Today;
        dtp_filtreSonTarih.Value = DateTime.Today;

        // ID Tip Seç combobox'ını varsayılan değere ayarla
        cb_filtreIdTipSec.SelectedIndex = 0; // "-" seçeneği

        // Grid'i yeniden yükle
        VeriBaglamaServisi.IzgaraBagla(dgv_gonderiler, ctx => _gonderiServisi.IzgaraIcinProjeksiyon(ctx));

        // Teslimat tipi combobox'ını veritabanından yeniden doldur
        TeslimatTipiCombosunuDoldur();

        // Müşteriler/kurye/şube combolarını yeniden bağla
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiGonderen, ctx => ctx.Musteriler.OrderBy(m => m.Ad), nameof(Musteri.Ad), nameof(Musteri.MusteriId));
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiAlici, ctx => ctx.Musteriler.OrderBy(m => m.Ad), nameof(Musteri.Ad), nameof(Musteri.MusteriId));
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiCikisSube, ctx => ctx.Subeler.OrderBy(s => s.SubeAd), nameof(Sube.SubeAd), nameof(Sube.SubeId));
        VeriBaglamaServisi.KomboyaBagla(cb_gonderiAtananKurye,
            ctx => ctx.Personeller.Include(p => p.Rol).Where(p => p.Aktif && p.Rol.RolAd == "Kurye").OrderBy(p => p.Ad),
            nameof(Personel.Ad), nameof(Personel.PersonelId));

        _ucretManuelDegisti = false; // Temizle sonrası otomatik hesaplamaya izin ver
        GonderiFiyatHesaplaVeGuncelle();
        btn_gonderiOlustur.Text = "Kaydet";
    }

    private void btn_gonderiAra_Click(object? sender, EventArgs e)
    {
        using var ctx = new KtsContext();

        // Tüm gönderi sorgusunu başlat - TÜM ALANLARI GETİR
        IQueryable<Gonderi> query = ctx.Gonderiler
            .Include(g => g.Gonderen)
            .Include(g => g.Alici)
            .Include(g => g.Kurye)
            .Include(g => g.GonderenAdres)
            .Include(g => g.AliciAdres);

        bool filtreLandiMi = false;

        // GRUP 1: Takip No Filtresi (tek başına çalışır)
        var takipNo = tb_filtreTakipNo.Text.Trim();
        if (!string.IsNullOrEmpty(takipNo))
        {
            query = query.Where(g => EF.Functions.Like(g.TakipNo, "%" + takipNo + "%"));
            filtreLandiMi = true;
        }

        // GRUP 2: ID/Ad-Soyad Filtresi (birlikte çalışır, Grup 1'den bağımsız)
        var secilenTip = cb_filtreIdTipSec.SelectedItem?.ToString();
        var idText = tb_filtreId.Text.Trim();
        var adSoyad = tb_filtreIdAdSoyad.Text.Trim();

        // "-" seçiliyse veya seçim yoksa bu grubu atla
        bool grup2Aktif = !string.IsNullOrEmpty(secilenTip) &&
                          secilenTip != "-" &&
                          (!string.IsNullOrEmpty(idText) || !string.IsNullOrEmpty(adSoyad));

        if (grup2Aktif)
        {
            // ID ile filtreleme
            if (!string.IsNullOrEmpty(idText) && int.TryParse(idText, out int arananId))
            {
                switch (secilenTip)
                {
                    case "Gönderen":
                        query = query.Where(g => g.GonderenId == arananId);
                        break;
                    case "Alıcı":
                        query = query.Where(g => g.AliciId == arananId);
                        break;
                    case "Kurye":
                        query = query.Where(g => g.KuryeId == arananId);
                        break;
                }
            }
            // Ad-Soyad ile filtreleme
            else if (!string.IsNullOrEmpty(adSoyad))
            {
                switch (secilenTip)
                {
                    case "Gönderen":
                        query = query.Where(g =>
                            EF.Functions.Like(g.Gonderen.Ad + " " + g.Gonderen.Soyad, "%" + adSoyad + "%") ||
                            EF.Functions.Like(g.Gonderen.Ad, "%" + adSoyad + "%") ||
                            EF.Functions.Like(g.Gonderen.Soyad, "%" + adSoyad + "%"));
                        break;
                    case "Alıcı":
                        query = query.Where(g =>
                            EF.Functions.Like(g.Alici.Ad + " " + g.Alici.Soyad, "%" + adSoyad + "%") ||
                            EF.Functions.Like(g.Alici.Ad, "%" + adSoyad + "%") ||
                            EF.Functions.Like(g.Alici.Soyad, "%" + adSoyad + "%"));
                        break;
                    case "Kurye":
                        query = query.Where(g => g.Kurye != null && (
                            EF.Functions.Like(g.Kurye.Ad + " " + g.Kurye.Soyad, "%" + adSoyad + "%") ||
                            EF.Functions.Like(g.Kurye.Ad, "%" + adSoyad + "%") ||
                            EF.Functions.Like(g.Kurye.Soyad, "%" + adSoyad + "%")));
                        break;
                }
            }

            filtreLandiMi = true;
        }

        // GRUP 3: Teslim Alan Kişi Filtresi (tek başına çalışır)
        var teslimAlanAd = tb_filtreTeslimAlanAdSoyad.Text.Trim();
        if (!string.IsNullOrEmpty(teslimAlanAd))
        {
            query = query.Where(g =>
                g.TeslimEdilenKisi != null &&
                EF.Functions.Like(g.TeslimEdilenKisi, "%" + teslimAlanAd + "%"));
            filtreLandiMi = true;
        }

        // GRUP 4: Tarih Aralığı Filtresi
        if (!filtreLandiMi || dtp_filtreIlkTarih.Value.Date != DateTime.Today.Date ||
            dtp_filtreSonTarih.Value.Date != DateTime.Today.Date)
        {
            DateTime ilkTarih = dtp_filtreIlkTarih.Value.Date;
            DateTime sonTarih = dtp_filtreSonTarih.Value.Date.AddDays(1).AddSeconds(-1);

            query = query.Where(g => g.GonderiTarihi >= ilkTarih && g.GonderiTarihi <= sonTarih);
        }

        // Sonuçları TÜM ALANLARLA grid'e bağla
        var sonuclar = query
            .OrderByDescending(g => g.GonderiTarihi)
            .Select(g => new
            {
                g.GonderiId,
                g.TakipNo,
                GonderenAd = g.Gonderen.Ad + " " + g.Gonderen.Soyad,
                GonderenId = g.GonderenId,
                AliciAd = g.Alici.Ad + " " + g.Alici.Soyad,
                AliciId = g.AliciId,
                KuryeAd = g.Kurye != null ? g.Kurye.Ad + " " + g.Kurye.Soyad : "",
                KuryeId = g.KuryeId,
                GonderenAdres = g.GonderenAdres != null ? g.GonderenAdres.AcikAdres : "",
                AliciAdres = g.AliciAdres != null ? g.AliciAdres.AcikAdres : "",
                g.GonderiTarihi,
                g.TahminiTeslimTarihi,
                g.TeslimTarihi,
                g.TeslimatTipi,
                g.Agirlik,
                g.Boyut,
                g.Ucret,
                g.IndirimTutar,
                g.EkMasraf,
                ToplamUcret = g.Ucret - (g.IndirimTutar ?? 0m) + (g.EkMasraf ?? 0m),
                g.TeslimEdilenKisi,
                g.IadeDurumu,
                g.KayitTarihi,
                g.GuncellemeTarihi,
                g.IptalTarihi
            }).ToList();

        dgv_gonderiler.DataSource = sonuclar;

        if (sonuclar.Count == 0)
        {
            MessageBox.Show("Arama kriterlerine uygun gönderi bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // -------------------------------------------------- PERSONEL BÖLÜMÜ --------------------------------------------------
    private void dgv_personeller_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_personeller.SelectedRows.Count > 0)
        {
            var row = dgv_personeller.SelectedRows[0];
            secilenPersonelId = row.Cells["PersonelId"].Value != DBNull.Value
                ? Convert.ToInt32(row.Cells["PersonelId"].Value)
                : (int?)null;
            tb_personelAd.Text = row.Cells["Ad"].Value?.ToString() ?? "";
            tb_personelSoyad.Text = row.Cells["Soyad"].Value?.ToString() ?? "";
            tb_personelMail.Text = row.Cells["Mail"].Value?.ToString() ?? "";
            tb_personelTel.Text = row.Cells["Tel"].Value?.ToString() ?? "";

            string sifreMasked = row.Cells["Sifre"].Value?.ToString() ?? "";
            tb_personelSifre.Text = sifreMasked;
            tb_personelSifre.ReadOnly = false;

            string cinsiyet = row.Cells["Cinsiyet"].Value?.ToString();
            cb_personelCinsiyet.SelectedIndex = !string.IsNullOrEmpty(cinsiyet)
                ? cb_personelCinsiyet.Items.IndexOf(cinsiyet)
                : -1;

            var ehliyet = row.Cells["EhliyetSinifi"].Value?.ToString();
            if (!string.IsNullOrWhiteSpace(ehliyet) && cb_personelEhliyet.Items.Contains(ehliyet))
                cb_personelEhliyet.SelectedItem = ehliyet;
            else
                cb_personelEhliyet.SelectedIndex = -1;

            nud_personelMaas.Value = row.Cells["Maas"].Value != DBNull.Value
                ? Convert.ToDecimal(row.Cells["Maas"].Value)
                : nud_personelMaas.Minimum;

            dtp_personelIsegiris.Value = row.Cells["IseGirisTarihi"].Value != DBNull.Value
                ? Convert.ToDateTime(row.Cells["IseGirisTarihi"].Value)
                : DateTime.Now;

            var exitVal = row.Cells["IstenCikisTarihi"].Value;
            if (exitVal == null || exitVal == DBNull.Value)
            {
                dtp_personelIstencikis.Value = dtp_personelIstencikis.MinDate;
                dtp_personelIstencikis.Enabled = true;
            }
            else
            {
                DateTime dt = Convert.ToDateTime(exitVal);
                if (dt < dtp_personelIstencikis.MinDate)
                    dtp_personelIstencikis.Value = dtp_personelIstencikis.MinDate;
                else if (dt > dtp_personelIstencikis.MaxDate)
                    dtp_personelIstencikis.Value = dtp_personelIstencikis.MaxDate;
                else
                    dtp_personelIstencikis.Value = dt;
                dtp_personelIstencikis.Enabled = true;
            }

            ckb_personelAktif.Checked = row.Cells["Aktif"].Value != DBNull.Value
                && Convert.ToBoolean(row.Cells["Aktif"].Value);

            if (row.Cells["RolId"].Value != DBNull.Value)
                cb_personelRol.SelectedValue = Convert.ToInt32(row.Cells["RolId"].Value);
            else
                cb_personelRol.SelectedIndex = -1;

            if (row.Cells["SubeId"].Value != DBNull.Value)
                cb_personelSube.SelectedValue = Convert.ToInt32(row.Cells["SubeId"].Value);
            else
                cb_personelSube.SelectedIndex = -1;

            int tercihAracId = row.Cells["AracId"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["AracId"].Value) : 0;
            PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube, tercihAracId);

            btnPersonelAdresYonet.Enabled = secilenPersonelId.HasValue;

            // Kaydet buton metni: seçim var -> Güncelle
            btn_personelKaydet.Text = "Güncelle";
        }
        else
        {
            secilenPersonelId = null;
            btnPersonelAdresYonet.Enabled = false;
            dtp_personelIstencikis.Enabled = false;
            // Seçim yok -> Kaydet modu
            btn_personelKaydet.Text = "Kaydet";
        }
    }

    // Ehliyet combobox değerini doğrulama için TextBox proxy olarak üretir
    private TextBox EhliyetTextBoxProxy()
    {
        return new TextBox { Text = cb_personelEhliyet.SelectedItem?.ToString() ?? string.Empty };
    }

    private void btn_personelKaydet_Click(object sender, EventArgs e)
    {
        if (!PersonelDogrulayici.Dogrula(
            tb_personelAd,
            tb_personelSoyad,
            tb_personelMail,
            tb_personelSifre,
            tb_personelTel,
            cb_personelCinsiyet,
            cb_personelRol,
            cb_personelSube,
            EhliyetTextBoxProxy(),
            dtp_personelDogumTarih,
            cb_personelArac,
            nud_personelMaas))
        {
            return;
        }

        using (var context = new KtsContext())
        {
            Personel personel;
            bool guncelleme = secilenPersonelId.HasValue;
            if (guncelleme)
            {
                personel = _personelServisi.KimlikleGetir(context, secilenPersonelId.Value);
                if (personel == null)
                {
                    MessageBox.Show("Personel bulunamadı.");
                    return;
                }
            }
            else
            {
                personel = _personelServisi.Olustur(context, tb_personelSifre.Text);
            }

            // Basit alanlar
            personel.Ad = tb_personelAd.Text;
            personel.Soyad = tb_personelSoyad.Text;
            personel.Mail = tb_personelMail.Text;
            personel.Tel = tb_personelTel.Text;
            personel.EhliyetSinifi = cb_personelEhliyet.SelectedItem?.ToString();
            personel.DogumTarihi = dtp_personelDogumTarih.Value;
            personel.Maas = nud_personelMaas.Value;
            personel.Cinsiyet = cb_personelCinsiyet.SelectedItem?.ToString();

            // NAVIGATION nesnelerini bağlamak yerine sadece Foreign Key Id atıyoruz (tracking çakışmasını önler)
            if (cb_personelRol.SelectedValue is int rolId)
                personel.RolId = rolId;
            else if (cb_personelRol.SelectedItem is Rol rol)
                personel.RolId = rol.RolId;

            if (cb_personelSube.SelectedValue is int subeId)
                personel.SubeId = subeId;
            else if (cb_personelSube.SelectedItem is Sube sube)
                personel.SubeId = sube.SubeId;

            // Araç seçimi: 0 veya null ise FK null
            int aracIdDegeri = 0;
            if (cb_personelArac.SelectedValue is int val)
                aracIdDegeri = val;
            else if (cb_personelArac.SelectedItem is Arac arac)
                aracIdDegeri = arac.AracId;
            personel.AracId = aracIdDegeri > 0 ? aracIdDegeri : (int?)null;
            // personel.Arac navigation’a set yapmıyoruz.

            personel.IseGirisTarihi = dtp_personelIsegiris.Value;
            if (guncelleme)
            {
                personel.IstenCikisTarihi = dtp_personelIstencikis.Enabled ? dtp_personelIstencikis.Value : null;
            }
            personel.Aktif = ckb_personelAktif.Checked;

            context.SaveChanges();
            SecimleriResetle();
            MessageBox.Show(guncelleme ? "Personel güncellendi." : "Personel eklendi.");
            VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
        }

        // Filtre comboyu güncelle
        PersonelFiltreyiGuncelle();
        KontrolleriTemizle(
            tb_personelAd,
            tb_personelSoyad,
            tb_personelMail,
            tb_personelSifre,
            tb_personelTel,
            cb_personelEhliyet,
            dtp_personelDogumTarih, // düzeltildi - h eklendi
            nud_personelMaas,
            cb_personelCinsiyet,
            cb_personelRol,
            cb_personelSube,
            cb_personelArac,
            dtp_personelIsegiris,
            dtp_personelIstencikis,
            ckb_personelAktif,
            cb_personelFiltre
        );
        if (cb_personelEhliyet.Items.Count == 0)
        {
            cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
        }
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
        btn_personelKaydet.Text = "Kaydet"; // Kaydet moduna dön
    }

    private void btn_personelKayitSil_Click(object sender, EventArgs e)
    {
        if (secilenPersonelId.HasValue)
        {
            // Silme onayı
            if (!OnayYardimcisi.SilmeOnayi("Personel Sil", "Seçili personeli silmek istediğinize emin misiniz?"))
                return;

            using (var context = new KtsContext())
            {
                _personelServisi.Sil(context, secilenPersonelId.Value);
                context.SaveChanges();
                SecimleriResetle(); // Silme sonrası seçimleri sıfırla
                VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
                SubeFiltreyiGuncelle();
            }
            // Silme sonrası filtre comboyu güncelle
            PersonelFiltreyiGuncelle();
            KontrolleriTemizle(
                tb_personelAd,
                tb_personelSoyad,
                tb_personelMail,
                tb_personelSifre,
                tb_personelTel,
                cb_personelEhliyet,
                dtp_personelDogumTarih, // düzeltildi
                nud_personelMaas,
                cb_personelCinsiyet,
                cb_personelRol,
                cb_personelSube,
                cb_personelArac,
                dtp_personelIsegiris,
                dtp_personelIstencikis,
                ckb_personelAktif,
                cb_personelFiltre
            );
            // Ehliyet comboyu yeniden yükle (liste sabit ise sadece temizlemek yeterli)
            if (cb_personelEhliyet.Items.Count == 0)
            {
                cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
            }
            PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
            btn_personelKaydet.Text = "Kaydet";
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir personel seçin.");
        }
    }

    private void btn_subeKaydet_Click(object sender, EventArgs e)
    {
        using (var context = new KtsContext())
        {
            Sube sube;
            bool guncelleme = secilenSubeId.HasValue;
            if (guncelleme)
            {
                sube = _subeServisi.KimlikleGetir(context, secilenSubeId.Value);
                if (sube == null)
                {
                    MessageBox.Show("Şube bulunamadı.");
                    return;
                }
            }
            else
            {
                sube = _subeServisi.Olustur(context);
            }

            sube.SubeAd = tb_subeAd.Text;
            sube.SubeTip = cb_subeTip.SelectedItem?.ToString();
            sube.Tel = tb_subeTel.Text;
            sube.Email = tb_subeMail.Text;
            sube.AcikAdres = tbm_subeAcikAdres.Text;
            sube.CalismaSaatleri = cb_subeCalismaSaat.SelectedItem?.ToString();
            sube.Kapasite = (int?)nud_subeKapasite.Value;
            if (cb_subeIl.SelectedValue is int ilId)
                sube.IlId = ilId;
            else if (cb_subeIl.SelectedValue is Il il)
                sube.IlId = il.IlId;
            if (cb_subeIlce.SelectedValue is int ilceId)
                sube.IlceId = ilceId;
            else if (cb_subeIlce.SelectedItem is Ilce ilce)
                sube.IlceId = ilce.IlceId;

            context.SaveChanges();
            SecimleriResetle();
            MessageBox.Show(guncelleme ? "Şube güncellendi." : "Şube eklendi.");
            VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
        }
        SubeFiltreyiGuncelle();
        VeriBaglamaServisi.KomboyaBagla(cb_aracSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        KontrolleriTemizle(tb_subeAd, cb_subeTip, tb_subeTel, tb_subeMail, tbm_subeAcikAdres, cb_subeCalismaSaat, nud_subeKapasite, cb_subeIl, cb_subeIlce, cb_subeFiltre);
        btn_subeKaydet.Text = "Kaydet";
    }

    private void btn_subeKayitSil_Click(object sender, EventArgs e)
    {
        if (secilenSubeId.HasValue)
        {
            if (!OnayYardimcisi.SilmeOnayi("Şube Sil", "Seçili şubeyi silmek istediğinize emin misiniz?"))
                return;
            using (var context = new KtsContext())
            {
                _subeServisi.Sil(context, secilenSubeId.Value);
                context.SaveChanges();
                SecimleriResetle();
                VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
            }
            SubeFiltreyiGuncelle();
            KontrolleriTemizle(tb_subeAd, cb_subeTip, tb_subeTel, tb_subeMail, tbm_subeAcikAdres, cb_subeCalismaSaat, nud_subeKapasite, cb_subeIl, cb_subeIlce, cb_subeFiltre);
            btn_subeKaydet.Text = "Kaydet";
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir şube seçin.");
        }
    }

    private void btn_aracKaydet_Click(object sender, EventArgs e)
    {
        using (var context = new KtsContext())
        {
            Arac arac;
            bool guncelleme = secilenAracId.HasValue;
            if (guncelleme)
            {
                arac = _aracServisi.KimlikleGetir(context, secilenAracId.Value);
                if (arac == null)
                {
                    MessageBox.Show("Araç bulunamadı.");
                    return;
                }
            }
            else
            {
                arac = _aracServisi.Olustur(context);
            }

            arac.Plaka = tb_aracPlaka.Text;
            arac.AracTip = cb_aracTip.SelectedItem?.ToString();
            arac.KapasiteKg = nud_aracKapasite.Value;
            arac.GpsKodu = tb_aracGps.Text;
            arac.Durum = cb_aracDurum.SelectedItem?.ToString();
            if (cb_aracSube.SelectedValue is int subeId)
            {
                arac.SubeId = subeId;
                arac.Sube = context.Subeler.Find(subeId);
            }
            context.SaveChanges();
            SecimleriResetle();
            MessageBox.Show(guncelleme ? "Araç güncellendi." : "Araç eklendi.");
            VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
            var tipler = context.Araclar.Select(a => a.AracTip).Distinct().ToList();
            cb_aracFiltre.Items.Clear();
            cb_aracFiltre.Items.AddRange(tipler.ToArray());
        }
        KontrolleriTemizle(tb_aracPlaka, cb_aracTip, nud_aracKapasite, tb_aracGps, cb_aracDurum, cb_aracSube, cb_aracFiltre);
        btn_aracKaydet.Text = "Kaydet";
    }

    private void btn_aracSil_Click(object sender, EventArgs e)
    {
        if (secilenAracId.HasValue)
        {
            if (!OnayYardimcisi.SilmeOnayi("Araç Sil", "Seçili aracı silmek istediğinize emin misiniz?"))
                return;

            using (var context = new KtsContext())
            {
                _aracServisi.Sil(context, secilenAracId.Value);
                context.SaveChanges();
                SecimleriResetle(); // Silme sonrası sıfırla
                VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
                var tipler = context.Araclar
                    .Select(a => a.AracTip)
                    .Distinct()
                    .ToList();
                cb_aracFiltre.Items.Clear();
                cb_aracFiltre.Items.AddRange(tipler.ToArray());

            }
            KontrolleriTemizle(tb_aracPlaka, cb_aracTip, nud_aracKapasite, tb_aracGps, cb_aracDurum, cb_aracSube, cb_aracFiltre);
            btn_aracKaydet.Text = "Kaydet";
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir araç seçin.");
        }
    }

    // Mevcut personel kayıtlarından distinct rol adlarını çekip filtre comboboxını doldurur
    private void PersonelFiltreyiGuncelle()
    {
        using var context = new KtsContext();
        var personelTipleri = context.Personeller
            .Select(p => p.Rol.RolAd)
            .Where(rolAd => !string.IsNullOrEmpty(rolAd))
            .Distinct()
            .OrderBy(x => x)
            .ToList();
        cb_personelFiltre.Items.Clear();
        if (personelTipleri.Count > 0)
            cb_personelFiltre.Items.AddRange(personelTipleri.Cast<object>().ToArray());
        cb_personelFiltre.SelectedIndex = -1; // Seçimi sıfırla
    }

    // Şube filtre comboboxını mevcut şube kayıtlarındaki tiplerden (distinct) doldurur
    private void SubeFiltreyiGuncelle()
    {
        using var ctx = new KtsContext();
        var tipler = ctx.Subeler
            .Select(s => s.SubeTip)
            .Where(t => !string.IsNullOrEmpty(t))
            .Distinct()
            .OrderBy(t => t)
            .ToList();
        cb_subeFiltre.Items.Clear();
        if (tipler.Count > 0)
            cb_subeFiltre.Items.AddRange(tipler.Cast<object>().ToArray());
        cb_subeFiltre.SelectedIndex = -1; // Seçimi sıfırla
    }

    // Seçili gönderi detaylarını sağ paneldeki dgv_seciliGonderiDetay içinde gösterir
    private void GonderiDetayPaneliniGuncelle()
    {
        if (!secilenGonderiId.HasValue)
        {
            dgv_seciliGonderiDetay.DataSource = null;
            return;
        }

        using var ctx = new KtsContext();
        var gecmisListe = ctx.GonderiDurumGecmisi
            .AsNoTracking()
            .Where(x => x.GonderiId == secilenGonderiId.Value)
            .OrderByDescending(x => x.Tarih)
            .Select(x => new
            {
                x.Id,
                x.GonderiId,
                x.DurumAd,
                x.Aciklama,
                x.Tarih,
                x.IslemTipi,
                x.SonDurumMu,
                x.IslemSonucu,
                x.TeslimatKodu,
                x.IlgiliKisiAd,
                x.IlgiliKisiTel,
                x.SubeId,
                x.PersonelId,
                x.AracId,
                x.IslemBaslangicTarihi,
                x.IslemBitisTarihi
            })
            .ToList();

        dgv_seciliGonderiDetay.AutoGenerateColumns = true;
        dgv_seciliGonderiDetay.DataSource = gecmisListe;
        dgv_seciliGonderiDetay.ClearSelection();
    }

    // Tasarımcı tarafından bağlanan boş olay işleyicileri
    private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) { }
    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) { }
    private void label12_Click(object sender, EventArgs e) { }
    private void groupBox1_Enter(object sender, EventArgs e) { }
    private void label2_Click(object sender, EventArgs e) { }
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    private void dateTimePicker4_ValueChanged(object sender, EventArgs e) { }
    private void button29_Click(object sender, EventArgs e) { }
    private void label85_Click(object sender, EventArgs e) { }
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) { }
    private void btnPersonelAdresYonet_Click(object sender, EventArgs e)
    {
        var form = new AdresEkleForm(secilenPersonelId.Value, "Personel");
        form.ShowDialog();
    }
    private void btn_tempSifre_Click(object sender, EventArgs e)
    {
        // 4 haneli rastgele sayısal şifre üret (1000-9999 arası)
        Random rnd = new Random();
        string tempSifre = rnd.Next(1000, 10000).ToString();
        tb_personelSifre.Text = tempSifre;
    }
    private void btn_subeAra_Click(object sender, EventArgs e)
    {
        if (cb_subeFiltre.SelectedItem != null)
        {
            string seciliTip = cb_subeFiltre.SelectedItem.ToString();
            VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx =>
                _subeServisi.IzgaraIcinProjeksiyon(ctx).Where(s => (string)s.GetType().GetProperty("SubeTip")!.GetValue(s)! == seciliTip));
        }
        else
        {
            VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
        }
    }

    private void dgv_subeler_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_subeler.SelectedRows.Count > 0)
        {
            var row = dgv_subeler.SelectedRows[0];
            secilenSubeId = Convert.ToInt32(row.Cells["SubeId"].Value);
            tb_subeAd.Text = row.Cells["SubeAd"].Value?.ToString() ?? "";
            string tip = row.Cells["SubeTip"].Value?.ToString();
            cb_subeTip.SelectedItem = !string.IsNullOrEmpty(tip) ? tip : null;
            tb_subeTel.Text = row.Cells["Tel"].Value?.ToString() ?? "";
            tb_subeMail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            tbm_subeAcikAdres.Text = row.Cells["AcikAdres"].Value?.ToString() ?? "";
            cb_subeCalismaSaat.SelectedItem = row.Cells["CalismaSaatleri"].Value?.ToString() ?? null;
            nud_subeKapasite.Value = row.Cells["Kapasite"].Value != null ? Convert.ToDecimal(row.Cells["Kapasite"].Value) : 0;
            cb_subeIl.SelectedValue = row.Cells["IlId"].Value;
            cb_subeIlce.SelectedValue = row.Cells["IlceId"].Value;

            // Kaydet buton metni: seçim var -> Güncelle
            btn_subeKaydet.Text = "Güncelle";
        }
        else
        {
            secilenSubeId = null;
            btn_subeKaydet.Text = "Kaydet";
        }
    }

    private void btn_aracAra_Click(object sender, EventArgs e)
    {
        if (cb_aracFiltre.SelectedItem != null)
        {
            string seciliTip = cb_aracFiltre.SelectedItem.ToString();
            VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx =>
                _aracServisi.IzgaraIcinProjeksiyon(ctx).Where(a => (string)a.GetType().GetProperty("AracTip")!.GetValue(a)! == seciliTip));
        }
        else
        {
            VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
        }
    }

    private void dgv_araclar_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_araclar.SelectedRows.Count > 0)
        {
            var row = dgv_araclar.SelectedRows[0];
            secilenAracId = Convert.ToInt32(row.Cells["AracId"].Value);
            tb_aracPlaka.Text = row.Cells["Plaka"].Value?.ToString() ?? "";
            cb_aracTip.SelectedItem = row.Cells["AracTip"].Value?.ToString();
            nud_aracKapasite.Value = row.Cells["KapasiteKg"].Value != null ? Convert.ToDecimal(row.Cells["KapasiteKg"].Value) : 0;
            tb_aracGps.Text = row.Cells["GpsKodu"].Value?.ToString();
            cb_aracDurum.SelectedItem = row.Cells["Durum"].Value?.ToString();
            cb_aracSube.SelectedValue = row.Cells["SubeId"].Value;

            // Kaydet buton metni: seçim var -> Güncelle
            btn_aracKaydet.Text = "Güncelle";
        }
        else
        {
            secilenAracId = null;
            btn_aracKaydet.Text = "Kaydet";
        }
    }

    private void btn_aracFormTemizle_Click(object sender, EventArgs e)
    {
        SecimleriResetle();
        KontrolleriTemizle(tb_aracPlaka, cb_aracTip, nud_aracKapasite, tb_aracGps, cb_aracDurum, cb_aracSube, cb_aracFiltre);
        VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
        cb_aracTip.Items.Clear();
        cb_aracTip.Items.Add("Kamyonet");
        cb_aracTip.Items.Add("Panelvan");
        cb_aracTip.Items.Add("Tır");
        cb_aracTip.Items.Add("Minibüs");
        cb_aracTip.Items.Add("Otomobil");
        cb_aracTip.Items.Add("Motosiklet");
        cb_aracTip.Items.Add("Diğer");
        cb_aracDurum.Items.Clear();
        cb_aracDurum.Items.Add("Aktif");
        cb_aracDurum.Items.Add("Bakımda");
        cb_aracDurum.Items.Add("Arızalı");
        cb_aracDurum.Items.Add("Pasif");
        cb_aracDurum.Items.Add("Serviste");
        cb_aracDurum.Items.Add("Kullanımda");
        VeriBaglamaServisi.KomboyaBagla(cb_aracSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        btn_aracKaydet.Text = "Kaydet";
    }

    private void btn_subeFormTemizle_Click(object sender, EventArgs e)
    {
        SecimleriResetle();
        KontrolleriTemizle(tb_subeAd, cb_subeTip, tb_subeTel, tb_subeMail, tbm_subeAcikAdres, cb_subeCalismaSaat, nud_subeKapasite, cb_subeIl, cb_subeIlce, cb_subeFiltre);
        VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
        cb_subeTip.Items.Clear();
        cb_subeTip.Items.Add("Merkez");
        cb_subeTip.Items.Add("Şube");
        cb_subeTip.Items.Add("Dağıtım Noktası");
        cb_subeTip.Items.Add("Depo");
        cb_subeTip.Items.Add("Transfer merkezi");
        cb_subeTip.Items.Add("Kargo kabul");
        cb_subeTip.Items.Add("Teslimat noktası");
        cb_subeCalismaSaat.Items.Clear();
        cb_subeCalismaSaat.Items.Add("08:00 - 17:00");
        cb_subeCalismaSaat.Items.Add("09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("10:00 - 19:00");
        cb_subeCalismaSaat.Items.Add("24 saat açık");
        cb_subeCalismaSaat.Items.Add("Hafta içi 09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("Hafta sonu kapalı");
        cb_subeCalismaSaat.Items.Add("Hafta sonu 10:00 - 16:00");
        VeriBaglamaServisi.KomboyaBagla(cb_subeIl, ctx => ctx.Iller, "IlIdVeAd", "IlId");
        btn_subeKaydet.Text = "Kaydet";
    }

    private void btn_personelFormTemizle_Click(object sender, EventArgs e)
    {
        SecimleriResetle();
        KontrolleriTemizle(
            tb_personelAd,
            tb_personelSoyad,
            tb_personelMail,
            tb_personelSifre,
            tb_personelTel,
            cb_personelEhliyet,
            dtp_personelDogumTarih,
            cb_personelArac,
            nud_personelMaas,
            cb_personelCinsiyet,
            cb_personelRol,
            cb_personelSube,
            dtp_personelIsegiris,
            dtp_personelIstencikis,
            ckb_personelAktif,
            cb_personelFiltre
        );
        // Filtre comboyu yeniden doldur
        PersonelFiltreyiGuncelle();
        if (cb_personelEhliyet.Items.Count == 0)
        {
            cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
            cb_personelEhliyet.SelectedIndex = -1;
        }
        cb_personelCinsiyet.Items.Clear();
        cb_personelCinsiyet.Items.Add("Erkek");
        cb_personelCinsiyet.Items.Add("Kadın");
        cb_personelCinsiyet.SelectedIndex = 0;
        dtp_personelIstencikis.Enabled = false;
        VeriBaglamaServisi.KomboyaBagla(cb_personelRol, ctx => ctx.Roller, "RolAd", "RolId");
        VeriBaglamaServisi.KomboyaBagla(cb_personelSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
        VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
        btn_personelKaydet.Text = "Kaydet";
    }

    private void btn_personelAra_Click(object sender, EventArgs e)
    {
        if (cb_personelFiltre.SelectedItem != null)
        {
            string seciliRolAd = cb_personelFiltre.SelectedItem.ToString();
            VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx =>
                ctx.Personeller
                    .Include(p => p.Rol)
                    .Include(p => p.Sube)
                    .Include(p => p.Arac)
                    .Include(p => p.Adresler)
                    .Where(p => p.Rol != null && p.Rol.RolAd == seciliRolAd)
                    .Select(p => new
                    {
                        p.PersonelId,
                        p.Ad,
                        p.Soyad,
                        p.Mail,
                        Sifre = p.Sifre.Length > 4 ? new string('*', p.Sifre.Length) : p.Sifre,
                        p.Tel,
                        p.DogumTarihi,
                        p.Cinsiyet,
                        p.RolId,
                        RolAd = p.Rol != null ? p.Rol.RolAd : "",
                        p.SubeId,
                        SubeAd = p.Sube != null ? p.Sube.SubeAd : "",
                        p.AracId,
                        AracTip = p.Arac != null ? p.Arac.AracTip : "",
                        p.Aktif,
                        p.IseGirisTarihi,
                        p.IstenCikisTarihi,
                        p.Maas,
                        p.EhliyetSinifi,
                        Adresler = p.Adresler != null && p.Adresler.Any()
                            ? string.Join(", ", p.Adresler.Select(a => a.AdresBaslik))
                            : ""
                    }));
        }
        else
        {
            VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
        }
    }

    private void tb_musteriFiltre_TextChanged(object sender, EventArgs e)
    {
        MusteriFiltreUygula();
    }

    private void MusteriFiltreUygula()
    {
        var term = tb_musteriFiltre.Text.Trim();
        VeriBaglamaServisi.IzgaraBagla(dgv_musteriler, ctx =>
            string.IsNullOrEmpty(term)
                ? _musteriServisi.IzgaraIcinProjeksiyon(ctx)
                : ctx.Musteriler
                    .Where(m => EF.Functions.Like(m.Ad, "%" + term + "%")
                             || EF.Functions.Like(m.Soyad, "%" + term + "%")
                             || EF.Functions.Like(m.Ad + " " + m.Soyad, "%" + term + "%"))
                    .Select(m => new
                    {
                        m.MusteriId,
                        m.Ad,
                        m.Soyad,
                        m.Mail,
                        m.Tel,
                        m.DogumTarihi,
                        m.Notlar
                    }));
    }

    private void btn_musteriAra_Click(object sender, EventArgs e)
    {
        MusteriFiltreUygula();
    }

    private bool GonderiZorunluSecimlerTam()
    {
        return cb_gonderiGonderen.SelectedValue is int
            && cb_gonderiGonderenAdres.SelectedValue is int
            && cb_gonderiAlici.SelectedValue is int
            && cb_gonderiAliciAdres.SelectedValue is int;
    }

    private bool IsAdresKisitSaglandi()
    {
        var gMusteri = cb_gonderiGonderen.SelectedValue as int?;
        var aMusteri = cb_gonderiAlici.SelectedValue as int?;
        if (!gMusteri.HasValue || !aMusteri.HasValue) return false;
        var gAdres = cb_gonderiGonderenAdres.SelectedItem as Adres;
        var aAdres = cb_gonderiAliciAdres.SelectedItem as Adres;
        if (gMusteri.Value != aMusteri.Value) return true; // Farklı müşteriler için kısıtlama yok
        return gAdres != null && aAdres != null && !string.Equals(gAdres.AdresTipi, aAdres.AdresTipi, StringComparison.OrdinalIgnoreCase);
    }

    private void TakipNoHazirsaUretVeAyarla()
    {
        if (GonderiZorunluSecimlerTam() && IsAdresKisitSaglandi())
        {
            tb_gonderiTakipNo.Text = RasgeleTakipNo8Hane();
        }
    }

    private Adres AdresCombosuOlustur(int musteriId, string adresTipi)
    {
        var combo = new Adres();
        combo.AdresId = -1;
        combo.AdresTipi = adresTipi;
        combo.AdresBaslik = adresTipi + " Adresi";
        return combo;
    }

    private void AdresCombosunuBagla(ComboBox cb, int musteriId)
    {
        using var ctx = new KtsContext();
        var liste = ctx.Adresler
            .Where(a => a.MusteriId == musteriId && (a.AdresTipi == "Ev" || a.AdresTipi == "İş" || a.AdresTipi == "Is"))
            .ToList();
        cb.DataSource = liste;
        cb.DisplayMember = nameof(Adres.AdresTipi);
        cb.ValueMember = nameof(Adres.AdresId);
        cb.SelectedIndex = liste.Count > 0 ? 0 : -1;
    }

    private void AdresTipiFarkiniZorla()
    {
        var gMusteri = cb_gonderiGonderen.SelectedValue as int?;
        var aMusteri = cb_gonderiAlici.SelectedValue as int?;
        if (!gMusteri.HasValue || !aMusteri.HasValue) return;
        if (gMusteri.Value != aMusteri.Value) return;

        string gTip = (cb_gonderiGonderenAdres.SelectedItem as Adres)?.AdresTipi;
        string aTip = (cb_gonderiAliciAdres.SelectedItem as Adres)?.AdresTipi;
        if (string.IsNullOrEmpty(gTip) && string.IsNullOrEmpty(aTip)) return;

        using var ctx = new KtsContext();
        var tumAdresler = ctx.Adresler
            .Where(a => a.MusteriId == gMusteri.Value && (a.AdresTipi == "Ev" || a.AdresTipi == "İş" || a.AdresTipi == "Is"))
            .ToList();

        if (!string.IsNullOrEmpty(gTip))
        {
            var hedefTip = gTip.Equals("Ev", StringComparison.OrdinalIgnoreCase) ? "İş" : "Ev";
            var alternatifler = tumAdresler.Where(a => string.Equals(a.AdresTipi, hedefTip, StringComparison.OrdinalIgnoreCase)).ToList();
            cb_gonderiAliciAdres.DataSource = alternatifler;
            cb_gonderiAliciAdres.DisplayMember = nameof(Adres.AdresTipi);
            cb_gonderiAliciAdres.ValueMember = nameof(Adres.AdresId);
            cb_gonderiAliciAdres.SelectedIndex = alternatifler.Count > 0 ? 0 : -1;
        }
        else if (!string.IsNullOrEmpty(aTip))
        {
            var hedefTip = aTip.Equals("Ev", StringComparison.OrdinalIgnoreCase) ? "İş" : "Ev";
            var alternatifler = tumAdresler.Where(a => string.Equals(a.AdresTipi, hedefTip, StringComparison.OrdinalIgnoreCase)).ToList();
            cb_gonderiGonderenAdres.DataSource = alternatifler;
            cb_gonderiGonderenAdres.DisplayMember = nameof(Adres.AdresTipi);
            cb_gonderiGonderenAdres.ValueMember = nameof(Adres.AdresId);
            cb_gonderiGonderenAdres.SelectedIndex = alternatifler.Count > 0 ? 0 : -1;
        }
    }

    // 8 haneli rastgele takip no
    private string RasgeleTakipNo8Hane()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var rnd = new Random();
        return new string(Enumerable.Range(0, 8).Select(_ => chars[rnd.Next(chars.Length)]).ToArray());
    }

    // Şube değişince o şubedeki aktif kuryeleri listele ve rastgele birini seç
    private void cb_gonderiCikisSube_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cb_gonderiCikisSube.SelectedValue is int subeId)
        {
            using var ctx = new KtsContext();
            var kuryeler = ctx.Personeller
                .Include(p => p.Rol)
                .Where(p => p.SubeId == subeId && p.Aktif && p.Rol.RolAd == "Kurye")
                .OrderBy(p => p.Ad)
                .ToList();
            cb_gonderiAtananKurye.DataSource = kuryeler;
            cb_gonderiAtananKurye.DisplayMember = nameof(Personel.Ad);
            cb_gonderiAtananKurye.ValueMember = nameof(Personel.PersonelId);
            if (kuryeler.Count > 0)
            {
                var rnd = new Random();
                cb_gonderiAtananKurye.SelectedIndex = rnd.Next(kuryeler.Count);
            }
            else
            {
                cb_gonderiAtananKurye.SelectedIndex = -1;
            }
        }
    }

    private void btn_gonderiSurecYonetim_Click(object sender, EventArgs e)
    {
        if (!secilenGonderiId.HasValue)
        {
            MessageBox.Show("Lütfen süreç yönetimi için bir gönderi seçin.");
            return;
        }

        using var frm = new Forms.GonderiSurecForm(secilenGonderiId.Value);
        frm.ShowDialog(this);

        // Kapanışta ızgarayı tazele (durum değişmiş olabilir)
        VeriBaglamaServisi.IzgaraBagla(dgv_gonderiler, ctx => _gonderiServisi.IzgaraIcinProjeksiyon(ctx));
        // Detay panelini de tazele
        GonderiDetayPaneliniGuncelle();
    }

    private void nud_gonderiUcret_ValueChanged(object sender, EventArgs e)
    {

    }

    private void btn_gonderiTarifeYonetim_Click(object sender, EventArgs e)
    {
        // Tarife Yönetim formunu aç
        using var frm = new TarifeYonetimForm();
        frm.ShowDialog(this);

        // Form kapandıktan sonra ücret hesaplamayı yenile (tarife değişmiş olabilir)
        GonderiFiyatHesaplaVeGuncelle();

        MessageBox.Show("Tarife ayarları kapatıldı. Ücret hesaplama yenilendi.",
            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// Teslimat tipi combobox'ını veritabanındaki aktif tarifelerden doldur
    /// </summary>
    private void TeslimatTipiCombosunuDoldur()
    {
        using var ctx = new KtsContext();

        // Önce sadece TeslimatTipi ve Oncelik'i al, sonra grupla
        var teslimatTipleri = ctx.FiyatlandirmaTarifeler
            .Where(t => t.TarifeTuru == "TeslimatCarpan"
                     && t.Aktif
                     && (t.GecerlilikBitis == null || t.GecerlilikBitis > DateTime.Now)
                     && !string.IsNullOrEmpty(t.TeslimatTipi))
            .GroupBy(t => t.TeslimatTipi)
            .Select(g => new
            {
                TeslimatTipi = g.Key,
                Oncelik = g.Min(x => x.Oncelik)  // Aynı tip için en düşük önceliği al
            })
            .OrderBy(t => t.Oncelik)
            .ThenBy(t => t.TeslimatTipi)
            .ToList();

        cb_gonderiTeslimatTip.Items.Clear();

        if (teslimatTipleri.Any())
        {
            // Sadece TeslimatTipi değerlerini ComboBox'a ekle
            cb_gonderiTeslimatTip.Items.AddRange(
                teslimatTipleri.Select(t => t.TeslimatTipi).ToArray()
            );
            cb_gonderiTeslimatTip.SelectedIndex = 0;
        }
        else
        {
            // Fallback: Eğer veritabanında kayıt yoksa varsayılan değerler
            cb_gonderiTeslimatTip.Items.AddRange(new object[] { "Standart", "Hızlı", "Aynı Gün", "Randevulu" });
            cb_gonderiTeslimatTip.SelectedIndex = 0;
        }
    }

    private void dtp_gonderiTarih_ValueChanged(object sender, EventArgs e)
    {

    }
}
