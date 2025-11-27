using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using System;
using System.Linq;
using System.Windows.Forms;
using kargotakipsistemi.Servisler;
using kargotakipsistemi.Yardimcilar;
using kargotakipsistemi.Dogrulamalar;

namespace kargotakipsistemi;

public partial class MainForm : Form
{
    // Alanlar & Servis örnekleri
    private readonly PersonelServisi _personelServisi = new();
    private readonly SubeServisi _subeServisi = new();
    private readonly AracServisi _aracServisi = new();

    private int? secilenPersonelId = null;
    private int? secilenSubeId = null;
    private int? secilenAracId = null;

    public MainForm()
    {
        InitializeComponent();
        cb_subeIl.SelectedIndexChanged += cb_subeIl_SelectedIndexChanged;
        // cb_aracSube handler'ı formun Araç bölümündeki tanımıyla çakışıyordu; aşağıda tanımlanan yönteme bağlanıyor
        cb_aracSube.SelectedIndexChanged += cb_aracSube_SelectedIndexChanged_Araç;

        // Personel bölümünde rol değişince araç listesi rol/subeye göre güncellensin
        cb_personelRol.SelectedIndexChanged += cb_personelRol_SelectedIndexChanged;
        cb_personelSube.SelectedIndexChanged += cb_personelSube_SelectedIndexChanged;
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

    // -------------------------------------------------- ARAÇ BÖLÜMÜ --------------------------------------------------
    private void cb_aracSube_SelectedIndexChanged_Araç(object sender, EventArgs e)
    {
        // Şube değiştiğinde GPS artık otomatik üretilmiyor; kullanıcı girecek
        if (cb_aracSube.SelectedValue is int subeId)
        {
            using (var ctx = new KtsContext())
            {
                var sube = ctx.Subeler.Find(subeId);
                if (sube != null)
                {
                    // tb_aracGps'i boş bırakıyoruz veya mevcut değere dokunmuyoruz
                }
            }
        }
    }

    // Personel bölümünde: Rol/Sube değişimlerinde araç comboboxını merkezi servisten doldur
    private void cb_personelRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
    }

    private void cb_personelSube_SelectedIndexChanged(object sender, EventArgs e)
    {
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
    }

    // Genel Yardımcı
    private void KontrolleriTemizle(params Control[] controls) => KontrolYardimcisi.Temizle(controls);

    // Form Load (ilk veri bağlama ve sabit listeler)
    private void MainForm_Load(object sender, EventArgs e)
    {
        // Personel başlangıç
        VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
        VeriBaglamaServisi.KomboyaBagla(cb_personelRol, ctx => ctx.Roller, "RolAd", "RolId");
        cb_personelRol.SelectedIndex = cb_personelRol.Items.Count > 0 ? 0 : -1;
        VeriBaglamaServisi.KomboyaBagla(cb_personelSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        cb_personelSube.SelectedIndex = cb_personelSube.Items.Count > 0 ? 0 : -1;
        cb_personelCinsiyet.Items.Clear();
        cb_personelCinsiyet.Items.Add("Erkek");
        cb_personelCinsiyet.Items.Add("Kadın");
        cb_personelCinsiyet.SelectedIndex = 0;
        // Ehliyet sınıfları combobox doldurma
        cb_personelEhliyet.Items.Clear();
        cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
        cb_personelEhliyet.SelectedIndex = -1;

        // Rol/Sube bağlandıktan sonra araç comboboxını ilk kez doldur (servisten)
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);

        // Şube başlangıç
        VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
        VeriBaglamaServisi.KomboyaBagla(cb_subeIl, ctx => ctx.Iller, "IlIdVeAd", "IlId");
        using (var context = new KtsContext())
        {
            var tipler = context.Subeler.Select(a => a.SubeTip).Distinct().ToList();
            cb_subeFiltre.Items.Clear();
            cb_subeFiltre.Items.AddRange(tipler.ToArray());
        }
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

        // Araç başlangıç
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

            // Ehliyet combobox seçimini değerle eşle
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
                dtp_personelIstencikis.Value = DateTime.Now;
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

            // Rol/Sube/Arac combolarını seçili satırdan gelen kimliklere göre ayarla
            if (row.Cells["RolId"].Value != DBNull.Value)
                cb_personelRol.SelectedValue = Convert.ToInt32(row.Cells["RolId"].Value);
            else
                cb_personelRol.SelectedIndex = -1;

            if (row.Cells["SubeId"].Value != DBNull.Value)
                cb_personelSube.SelectedValue = Convert.ToInt32(row.Cells["SubeId"].Value);
            else
                cb_personelSube.SelectedIndex = -1;

            // Rol/sube değişiminden sonra combobox yeniden bağlanacağı için Arac seçimini merkezi servis ile uygula
            int tercihAracId = row.Cells["AracId"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["AracId"].Value) : 0;
            PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube, tercihAracId);

            btnPersonelAdresYonet.Enabled = secilenPersonelId.HasValue;
        }
        else
        {
            secilenPersonelId = null;
            btnPersonelAdresYonet.Enabled = false;
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

            personel.Ad = tb_personelAd.Text;
            personel.Soyad = tb_personelSoyad.Text;
            personel.Mail = tb_personelMail.Text;
            personel.Tel = tb_personelTel.Text;
            personel.EhliyetSinifi = cb_personelEhliyet.SelectedItem?.ToString();
            personel.DogumTarihi = dtp_personelDogumTarih.Value;
            personel.Maas = nud_personelMaas.Value;
            personel.Cinsiyet = cb_personelCinsiyet.SelectedItem?.ToString();
            personel.Rol = cb_personelRol.SelectedItem as Rol;
            personel.Sube = cb_personelSube.SelectedItem as Sube;

            // Araç seçimi: 0 veya null ise veritabanında null kaydet
            int aracIdDegeri = 0;
            if (cb_personelArac.SelectedValue is int val)
                aracIdDegeri = val;
            else if (cb_personelArac.SelectedItem is Arac arac)
                aracIdDegeri = arac.AracId;

            personel.AracId = aracIdDegeri > 0 ? aracIdDegeri : (int?)null;
            personel.Arac = null; // FK üzerinden yönetiyoruz

            personel.IseGirisTarihi = dtp_personelIsegiris.Value;
            if (guncelleme)
            {
                personel.IstenCikisTarihi = dtp_personelIstencikis.Enabled ? dtp_personelIstencikis.Value : null;
            }
            personel.Aktif = ckb_personelAktif.Checked;

            context.SaveChanges();
            VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
        }

        // Formu temizle ve seçimleri sıfırla
        secilenPersonelId = null;
        KontrolleriTemizle(
            tb_personelAd,
            tb_personelSoyad,
            tb_personelMail,
            tb_personelSifre,
            tb_personelTel,
            cb_personelEhliyet,
            dtp_personelDogumTarih,
            nud_personelMaas,
            cb_personelCinsiyet,
            cb_personelRol,
            cb_personelSube,
            cb_personelArac,
            dtp_personelIsegiris,
            dtp_personelIstencikis,
            ckb_personelAktif
        );
        // Ehliyet comboyu yeniden yükle (liste sabit ise sadece temizlemek yeterli)
        if (cb_personelEhliyet.Items.Count == 0)
        {
            cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
        }
        // Rol/Sube bağlandıktan sonra araç comboboxını yeniden oluştur (servis ile)
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
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
                VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
                var tipler = context.Subeler.Select(s => s.SubeTip).Distinct().ToList();
                cb_subeFiltre.Items.Clear();
                cb_subeFiltre.Items.AddRange(tipler.ToArray());
            }
            // Silme sonrası form temizliği
            secilenPersonelId = null;
            KontrolleriTemizle(
                tb_personelAd,
                tb_personelSoyad,
                tb_personelMail,
                tb_personelSifre,
                tb_personelTel,
                cb_personelEhliyet,
                dtp_personelDogumTarih,
                nud_personelMaas,
                cb_personelCinsiyet,
                cb_personelRol,
                cb_personelSube,
                cb_personelArac,
                dtp_personelIsegiris,
                dtp_personelIstencikis,
                ckb_personelAktif
            );
            // Ehliyet comboyu yeniden yükle (liste sabit ise sadece temizlemek yeterli)
            if (cb_personelEhliyet.Items.Count == 0)
            {
                cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
            }
            PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
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
            if (secilenSubeId.HasValue)
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
            else if (cb_subeIl.SelectedItem is Il il)
                sube.IlId = il.IlId;
            if (cb_subeIlce.SelectedValue is int ilceId)
                sube.IlceId = ilceId;
            else if (cb_subeIlce.SelectedItem is Ilce ilce)
                sube.IlceId = ilce.IlceId;

            context.SaveChanges();
            VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
            var tipler = context.Subeler.Select(a => a.SubeTip).Distinct().ToList();
            cb_subeFiltre.Items.Clear();
            cb_subeFiltre.Items.AddRange(tipler.ToArray());
        }
        VeriBaglamaServisi.KomboyaBagla(cb_aracSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        // Formu temizle
        secilenSubeId = null;
        KontrolleriTemizle(tb_subeAd, cb_subeTip, tb_subeTel, tb_subeMail, tbm_subeAcikAdres, cb_subeCalismaSaat, nud_subeKapasite, cb_subeIl, cb_subeIlce, cb_subeFiltre);
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
                VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
                var tipler = context.Subeler.Select(a => a.SubeTip).Distinct().ToList();
                cb_subeFiltre.Items.Clear();
                cb_subeFiltre.Items.AddRange(tipler.ToArray());
            }
            // Silme sonrası form temizliği
            secilenSubeId = null;
            KontrolleriTemizle(tb_subeAd, cb_subeTip, tb_subeTel, tb_subeMail, tbm_subeAcikAdres, cb_subeCalismaSaat, nud_subeKapasite, cb_subeIl, cb_subeIlce, cb_subeFiltre);
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
            if (secilenAracId.HasValue)
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
            // GPS kullanıcıdan alınacak; otomatik üretim kaldırıldı
            arac.GpsKodu = tb_aracGps.Text;
            arac.Durum = cb_aracDurum.SelectedItem?.ToString();
            if (cb_aracSube.SelectedValue is int subeId)
            {
                arac.SubeId = subeId;
                arac.Sube = context.Subeler.Find(subeId);
            }
            context.SaveChanges();
            VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
            var tipler = context.Araclar.Select(a => a.AracTip).Distinct().ToList();
            cb_aracFiltre.Items.Clear();
            cb_aracFiltre.Items.AddRange(tipler.ToArray());
        }
        // Formu temizle
        secilenAracId = null;
        KontrolleriTemizle(tb_aracPlaka, cb_aracTip, nud_aracKapasite, tb_aracGps, cb_aracDurum, cb_aracSube, cb_aracFiltre);
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
                VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
                var tipler = context.Araclar
                    .Select(a => a.AracTip)
                    .Distinct()
                    .ToList();
                cb_aracFiltre.Items.Clear();
                cb_aracFiltre.Items.AddRange(tipler.ToArray());

            }
            // Silme sonrası form temizliği
            secilenAracId = null;
            KontrolleriTemizle(tb_aracPlaka, cb_aracTip, nud_aracKapasite, tb_aracGps, cb_aracDurum, cb_aracSube, cb_aracFiltre);
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir araç seçin.");
        }
    }

    // Tasarımcı tarafından bağlanan boş olay işleyicileri (uyumluluk için eklendi)
    private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) { }
    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) { }
    private void label12_Click(object sender, EventArgs e) { }
    private void groupBox1_Enter(object sender, EventArgs e) { }
    private void label2_Click(object sender, EventArgs e) { }
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    private void btnMusterAdresYonet_Click(object sender, EventArgs e) { }
    private void dateTimePicker4_ValueChanged(object sender, EventArgs e) { }
    private void button29_Click(object sender, EventArgs e) { }
    private void label85_Click(object sender, EventArgs e) { }
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) { }

    // Eksik olabilecek tasarımcı bağlı olay işleyicileri için güvenlik amaçlı boş tanımlar
    private void button2_Click(object sender, EventArgs e) { }
    private void btnPersonelAdresYonet_Click(object sender, EventArgs e)
    {
        var form = new AdresEkleForm(secilenPersonelId.Value, "Personel");
        form.ShowDialog();
    }
    private void btn_tempSifre_Click(object sender, EventArgs e) { }
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
        }
        else
        {
            secilenSubeId = null;
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
        }
        else
        {
            secilenAracId = null;
        }
    }

    private void btn_aracFormTemizle_Click(object sender, EventArgs e)
    {
        secilenAracId = null;
        KontrolleriTemizle(tb_aracPlaka, cb_aracTip, nud_aracKapasite, tb_aracGps, cb_aracDurum, cb_aracSube, cb_aracFiltre);
        // DGV araçları yeniden doldur
        VeriBaglamaServisi.IzgaraBagla(dgv_araclar, ctx => _aracServisi.IzgaraIcinProjeksiyon(ctx));
        // Araç Tipi sabit listeyi doldur
        cb_aracTip.Items.Clear();
        cb_aracTip.Items.Add("Kamyonet");
        cb_aracTip.Items.Add("Panelvan");
        cb_aracTip.Items.Add("Tır");
        cb_aracTip.Items.Add("Minibüs");
        cb_aracTip.Items.Add("Otomobil");
        cb_aracTip.Items.Add("Motosiklet");
        cb_aracTip.Items.Add("Diğer");
        // Durum sabit listeyi doldur
        cb_aracDurum.Items.Clear();
        cb_aracDurum.Items.Add("Aktif");
        cb_aracDurum.Items.Add("Bakımda");
        cb_aracDurum.Items.Add("Arızalı");
        cb_aracDurum.Items.Add("Pasif");
        cb_aracDurum.Items.Add("Serviste");
        cb_aracDurum.Items.Add("Kullanımda");
        // Şube combobox’ını veritabanından doldur
        VeriBaglamaServisi.KomboyaBagla(cb_aracSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
    }

    private void btn_subeFormTemizle_Click(object sender, EventArgs e)
    {
        secilenSubeId = null;
        KontrolleriTemizle(tb_subeAd, cb_subeTip, tb_subeTel, tb_subeMail, tbm_subeAcikAdres,
            cb_subeCalismaSaat, nud_subeKapasite, cb_subeIl, cb_subeIlce, cb_subeFiltre);
        // DGV şubeleri tekrar doldur
        VeriBaglamaServisi.IzgaraBagla(dgv_subeler, ctx => _subeServisi.IzgaraIcinProjeksiyon(ctx));
        // Şube tip seçeneklerini doldur (sabit liste)
        cb_subeTip.Items.Clear();
        cb_subeTip.Items.Add("Merkez");
        cb_subeTip.Items.Add("Şube");
        cb_subeTip.Items.Add("Dağıtım Noktası");
        cb_subeTip.Items.Add("Depo");
        cb_subeTip.Items.Add("Transfer Merkezi");
        cb_subeTip.Items.Add("Kargo Kabul");
        cb_subeTip.Items.Add("Teslimat Noktası");
        // Çalışma saatleri seçeneklerini doldur (sabit liste)
        cb_subeCalismaSaat.Items.Clear();
        cb_subeCalismaSaat.Items.Add("08:00 - 17:00");
        cb_subeCalismaSaat.Items.Add("09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("10:00 - 19:00");
        cb_subeCalismaSaat.Items.Add("24 Saat Açık");
        cb_subeCalismaSaat.Items.Add("Hafta İçi 09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("Hafta Sonu Kapalı");
        cb_subeCalismaSaat.Items.Add("Hafta Sonu 10:00 - 16:00");
        // İl ve İlçe combolarını doldur
        VeriBaglamaServisi.KomboyaBagla(cb_subeIl, ctx => ctx.Iller, "IlIdVeAd", "IlId");
    }

    private void btn_personelFormTemizle_Click(object sender, EventArgs e)
    {
        secilenPersonelId = null;
        KontrolleriTemizle(
            tb_personelAd,
            tb_personelSoyad,
            tb_personelMail,
            tb_personelSifre,
            tb_personelTel,
            cb_personelEhliyet,
            dtp_personelDogumTarih,
            nud_personelMaas,
            cb_personelCinsiyet,
            cb_personelRol,
            cb_personelSube,
            cb_personelArac,
            dtp_personelIsegiris,
            dtp_personelIstencikis,
            ckb_personelAktif
        );
        // Ehliyet comboyu yeniden yükle (liste sabit ise sadece temizlemek yeterli)
        if (cb_personelEhliyet.Items.Count == 0)
        {
            cb_personelEhliyet.Items.AddRange(new object[] { "A", "A1", "A2", "B", "BE", "C", "CE", "C1", "C1E", "D", "DE", "D1", "D1E", "F", "G", "M" });
            cb_personelEhliyet.SelectedIndex = -1;
        }
        // Cinsiyet, Rol ve Şube combolarını doldur
        cb_personelCinsiyet.Items.Clear();
        cb_personelCinsiyet.Items.Add("Erkek");
        cb_personelCinsiyet.Items.Add("Kadın");
        cb_personelCinsiyet.SelectedIndex = 0;
        dtp_personelIstencikis.Enabled = false;
        VeriBaglamaServisi.KomboyaBagla(cb_personelRol, ctx => ctx.Roller, "RolAd", "RolId");
        VeriBaglamaServisi.KomboyaBagla(cb_personelSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        // Araç comboboxını rol/subeye göre yeniden oluştur (servis ile)
        PersonelFormServisi.GuncelleAracCombosu(cb_personelArac, cb_personelRol, cb_personelSube);
        // DGV personelleri tekrar doldur
        VeriBaglamaServisi.IzgaraBagla(dgv_personeller, ctx => _personelServisi.IzgaraIcinProjeksiyon(ctx));
    }
}
