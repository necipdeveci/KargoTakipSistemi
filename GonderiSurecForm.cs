// GonderiSurecForm.cs
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Forms
{
    public partial class GonderiSurecForm : System.Windows.Forms.Form
    {
        private readonly int _gonderiId;

        private Label lblTakipNo = new() { AutoSize = true };
        private Label lblGonderen = new() { AutoSize = true };
        private Label lblAlici = new() { AutoSize = true };
        private Label lblTeslimatTipi = new() { AutoSize = true };
        private Label lblAgirlik = new() { AutoSize = true };
        private Label lblBoyut = new() { AutoSize = true };
        private Label lblUcret = new() { AutoSize = true };
        private Label lblDurum = new() { AutoSize = true, Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold) };

        // ToolTip ve ErrorProvider
        private ToolTip tt = new() { AutoPopDelay = 8000, InitialDelay = 500, ReshowDelay = 300, ShowAlways = true };
        private ErrorProvider ep = new() { BlinkStyle = ErrorBlinkStyle.NeverBlink };

        // GonderiDurumGecmis giriş kontrolleri
        private ComboBox cbDurumAd = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };
        private TextBox tbAciklama = new() { Width = 300, MaxLength = 100, PlaceholderText = "Açıklama" };
        private ComboBox cbIslemTipi = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        private ComboBox cbIslemSonucu = new() { DropDownStyle = ComboBoxStyle.DropDownList };
        // Teslimat kodu artık readonly
        private TextBox tbTeslimatKodu = new() { Width = 160, MaxLength = 4, PlaceholderText = "Otomatik", ReadOnly = true }; 
        private TextBox tbIlgiliKisiAd = new() { Width = 220, MaxLength = 100 };
        private TextBox tbIlgiliKisiTel = new() { Width = 140, MaxLength = 15 };
        private ComboBox cbSube = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
        private ComboBox cbPersonel = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
        private ComboBox cbArac = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
        private DateTimePicker dtpIslemBaslangic = new() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy HH:mm", ShowUpDown = true, ShowCheckBox = true };
        private DateTimePicker dtpIslemBitis = new() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy HH:mm", ShowUpDown = true, ShowCheckBox = true };
        private DateTimePicker dtpDurumTarih = new() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy HH:mm", ShowUpDown = true, Value = DateTime.Now };
        private CheckBox ckSonDurum = new() { Text = "Son durum olarak işaretle", AutoSize = true };

        private TextBox tbTeslimEdilenKisi = new() { Width = 250, PlaceholderText = "Teslim alan kişi adı-soyadı", MaxLength = 100 };
        private DateTimePicker dtpTeslimTarih = new() { Value = DateTime.Now, Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy HH:mm", ShowUpDown = true };

        private Button btnDurumEkle = new() { Text = "Durum Kaydı Ekle", AutoSize = true };
        private Button btnTeslimEt = new() { Text = "Teslimi Tamamla", AutoSize = true };
        private Button btnKapat = new() { Text = "Kapat", AutoSize = true };

        public GonderiSurecForm(int gonderiId)
        {
            InitializeComponent(); // Designer ile birleştir
            _gonderiId = gonderiId;

            Text = "Gönderi Süreç Yönetimi";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(12);
            KeyPreview = true;
            
            // Renk teması ayarları (MainForm ile tutarlı)
            this.BackColor = Color.FromArgb(234, 228, 213);

            // ErrorProvider hedef form
            ep.ContainerControl = this;

            // Scroll panel: içeriği sarmalar ve taşarsa scrollbar gösterir
            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(234, 228, 213)
            };

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Top, // AutoScroll ile uyumlu; içerik yüksekliği kadar olacak
                ColumnCount = 2,
                AutoSize = true,
                BackColor = Color.FromArgb(234, 228, 213)
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            int NextRow()
            {
                int i = table.RowCount;
                table.RowCount++;
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                return i;
            }

            void row(string baslik, Control deger)
            {
                int r = NextRow();
                var lbl = new Label { Text = baslik, AutoSize = true, Padding = new Padding(0, 4, 8, 4) };
                table.Controls.Add(lbl, 0, r);
                table.Controls.Add(deger, 1, r);
            }

            SuspendLayout();
            table.SuspendLayout();
            
            // Kontrol stillerini ayarla
            cbDurumAd.BackColor = Color.FromArgb(182, 176, 159);
            cbDurumAd.FlatStyle = FlatStyle.Flat;
            
            tbAciklama.BackColor = Color.FromArgb(182, 176, 159);
            tbAciklama.BorderStyle = BorderStyle.FixedSingle;
            
            cbIslemTipi.BackColor = Color.FromArgb(182, 176, 159);
            cbIslemTipi.FlatStyle = FlatStyle.Flat;
            
            cbIslemSonucu.BackColor = Color.FromArgb(182, 176, 159);
            cbIslemSonucu.FlatStyle = FlatStyle.Flat;
            
            tbTeslimatKodu.BackColor = Color.FromArgb(182, 176, 159);
            tbTeslimatKodu.BorderStyle = BorderStyle.FixedSingle;
            
            tbIlgiliKisiAd.BackColor = Color.FromArgb(182, 176, 159);
            tbIlgiliKisiAd.BorderStyle = BorderStyle.FixedSingle;
            
            tbIlgiliKisiTel.BackColor = Color.FromArgb(182, 176, 159);
            tbIlgiliKisiTel.BorderStyle = BorderStyle.FixedSingle;
            
            cbSube.BackColor = Color.FromArgb(182, 176, 159);
            cbSube.FlatStyle = FlatStyle.Flat;
            
            cbPersonel.BackColor = Color.FromArgb(182, 176, 159);
            cbPersonel.FlatStyle = FlatStyle.Flat;
            
            cbArac.BackColor = Color.FromArgb(182, 176, 159);
            cbArac.FlatStyle = FlatStyle.Flat;
            
            dtpIslemBaslangic.BackColor = Color.FromArgb(182, 176, 159);
            dtpIslemBitis.BackColor = Color.FromArgb(182, 176, 159);
            dtpDurumTarih.BackColor = Color.FromArgb(182, 176, 159);
            
            tbTeslimEdilenKisi.BackColor = Color.FromArgb(182, 176, 159);
            tbTeslimEdilenKisi.BorderStyle = BorderStyle.FixedSingle;
            
            dtpTeslimTarih.BackColor = Color.FromArgb(182, 176, 159);
            
            btnDurumEkle.BackColor = Color.FromArgb(182, 176, 159);
            btnDurumEkle.FlatStyle = FlatStyle.Flat;
            
            btnTeslimEt.BackColor = Color.FromArgb(182, 176, 159);
            btnTeslimEt.FlatStyle = FlatStyle.Flat;
            
            btnKapat.BackColor = Color.FromArgb(182, 176, 159);
            btnKapat.FlatStyle = FlatStyle.Flat;

            row("Takip No:", lblTakipNo); // Zaten Label olduğu için readonly
            row("Gönderen:", lblGonderen);
            row("Alıcı:", lblAlici);
            row("Teslimat Tipi:", lblTeslimatTipi);
            row("Ağırlık (kg):", lblAgirlik);
            row("Boyut:", lblBoyut);
            row("Ücret (₺):", lblUcret);
            row("Durum:", lblDurum);

            // Durum giriş alanları
            cbDurumAd.Items.AddRange(new object[] { "Kargoya Verildi", "Şubede", "Transferde", "Dağıtımda", "Teslim Edildi", "İade", "İptal" });
            cbDurumAd.SelectedIndex = 0;
            cbIslemTipi.Items.AddRange(new object[] { "Hazırlık", "Transfer", "Dağıtım", "Teslim", "İade", "İptal" });
            cbIslemTipi.SelectedIndex = 0;
            cbIslemSonucu.Items.AddRange(new object[] { "Başarılı", "Başarısız", "Beklemede", "Kısmi" });
            cbIslemSonucu.SelectedIndex = 0;

            row("Durum Ad:", cbDurumAd);
            row("Açıklama:", tbAciklama);
            row("İşlem Tipi:", cbIslemTipi);
            row("İşlem Sonucu:", cbIslemSonucu);
            row("Durum Tarih:", dtpDurumTarih);
            row("Teslimat Kodu:", tbTeslimatKodu); // readonly textbox
            row("İlgili Kişi Ad:", tbIlgiliKisiAd);
            row("İlgili Kişi Tel:", tbIlgiliKisiTel);
            row("Şube:", cbSube);
            row("Personel:", cbPersonel);
            row("Araç:", cbArac);
            row("İşlem Başlangıç:", dtpIslemBaslangic);
            row("İşlem Bitiş:", dtpIslemBitis);
            row("Son Durum:", ckSonDurum);

            // Teslim paneli
            int rTeslim = NextRow();
            table.Controls.Add(new Label { Text = "Teslim Alan:", AutoSize = true, Padding = new Padding(0, 8, 8, 4) }, 0, rTeslim);
            var teslimPanel = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, BackColor = Color.FromArgb(234, 228, 213) };
            teslimPanel.Controls.Add(tbTeslimEdilenKisi);
            teslimPanel.Controls.Add(new Label { Text = "Tarih:", AutoSize = true, Padding = new Padding(8, 4, 4, 4) });
            teslimPanel.Controls.Add(dtpTeslimTarih);
            table.Controls.Add(teslimPanel, 1, rTeslim);

            // Butonlar
            var buttons = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0, 8, 0, 0), BackColor = Color.FromArgb(234, 228, 213) };
            buttons.Controls.Add(btnKapat);
            buttons.Controls.Add(btnTeslimEt);
            buttons.Controls.Add(btnDurumEkle);

            int rButtons = NextRow();
            table.SetColumnSpan(buttons, 2);
            table.Controls.Add(buttons, 0, rButtons);

            // Table'ı scroll panel içine ekle
            scrollPanel.Controls.Add(table);
            Controls.Add(scrollPanel);

            AcceptButton = btnDurumEkle;
            CancelButton = btnKapat;

            // ToolTips
            tt.SetToolTip(cbDurumAd, "Durum adı: gönderinin süreçteki aşaması.");
            tt.SetToolTip(tbAciklama, "Bu duruma dair kısa açıklama.");
            tt.SetToolTip(cbIslemTipi, "İşlem tipi: operasyon türü (Transfer/Dağıtım vb.).");
            tt.SetToolTip(cbIslemSonucu, "İşlem sonucu: Başarılı/Başarısız/Beklemede/Kısmi.");
            tt.SetToolTip(dtpDurumTarih, "Durumun gerçekleştiği tarih ve saat.");
            tt.SetToolTip(tbTeslimatKodu, "Otomatik üretilir ve değiştirilemez.");
            tt.SetToolTip(tbIlgiliKisiAd, "İlgili kişi adı (kurye/teslim alan/şube yetkilisi).");
            tt.SetToolTip(tbIlgiliKisiTel, "İlgili kişi telefon numarası.");
            tt.SetToolTip(cbSube, "İlgili şube.");
            tt.SetToolTip(cbPersonel, "İlgili personel.");
            tt.SetToolTip(cbArac, "İlgili araç.");
            tt.SetToolTip(dtpIslemBaslangic, "İşlem başlangıç zamanı (opsiyonel).");
            tt.SetToolTip(dtpIslemBitis, "İşlem bitiş zamanı (opsiyonel).");
            tt.SetToolTip(ckSonDurum, "Bu kaydı son durum olarak işaretler.");
            tt.SetToolTip(tbTeslimEdilenKisi, "Teslim alan kişi adı-soyadı.");
            tt.SetToolTip(dtpTeslimTarih, "Teslim tarihi ve saati.");

            // Eventler: teslimat kodu otomatik üretimi
            cbDurumAd.SelectedIndexChanged += (_, __) => OtomatikTeslimatKoduUret();
            cbIslemTipi.SelectedIndexChanged += (_, __) => OtomatikTeslimatKoduUret();
            tbIlgiliKisiAd.TextChanged += (_, __) => OtomatikTeslimatKoduUret();
            dtpDurumTarih.ValueChanged += (_, __) => OtomatikTeslimatKoduUret();

            Load += (_, __) => { YukuYukleVeAlanlariDoldur(); ReferansListeleriniDoldur(); };
            btnDurumEkle.Click += (_, __) => DurumKaydiEkle();
            btnTeslimEt.Click += (_, __) => TeslimiTamamla();
            btnKapat.Click += (_, __) => Close();

            table.ResumeLayout(false);
            table.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void OtomatikTeslimatKoduUret()
        {
            // Önceki alanlar doluysa ve kullanıcı manuel girmediyse 4 karakterlik kod üret
            bool alanlarDolu = cbDurumAd.SelectedItem != null
                                && cbIslemTipi.SelectedItem != null
                                && !string.IsNullOrWhiteSpace(tbIlgiliKisiAd.Text);
            bool manuelGirildi = !string.IsNullOrWhiteSpace(tbTeslimatKodu.Text); // ReadOnly ama yine de kontrol
            if (alanlarDolu && !manuelGirildi)
            {
                tbTeslimatKodu.Text = RasgeleKod4();
            }
        }

        private string RasgeleKod4()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rnd = new Random();
            return new string(Enumerable.Range(0, 4).Select(_ => chars[rnd.Next(chars.Length)]).ToArray());
        }

        private bool DurumAlanlariniDogrula()
        {
            ep.Clear();
            bool gecerli = true;

            if (cbDurumAd.SelectedIndex < 0)
            {
                ep.SetError(cbDurumAd, "Durum adı zorunludur.");
                gecerli = false;
            }
            if (string.IsNullOrWhiteSpace(tbAciklama.Text))
            {
                ep.SetError(tbAciklama, "Açıklama zorunludur.");
                gecerli = false;
            }
            if (cbIslemTipi.SelectedIndex < 0)
            {
                ep.SetError(cbIslemTipi, "İşlem tipi zorunludur.");
                gecerli = false;
            }
            if (cbIslemSonucu.SelectedIndex < 0)
            {
                ep.SetError(cbIslemSonucu, "İşlem sonucu zorunludur.");
                gecerli = false;
            }
            if (string.IsNullOrWhiteSpace(tbIlgiliKisiAd.Text))
            {
                ep.SetError(tbIlgiliKisiAd, "İlgili kişi adı zorunludur.");
                gecerli = false;
            }
            if (string.IsNullOrWhiteSpace(tbIlgiliKisiTel.Text) || !tbIlgiliKisiTel.Text.All(char.IsDigit) || tbIlgiliKisiTel.Text.Length < 7)
            {
                ep.SetError(tbIlgiliKisiTel, "Geçerli bir telefon girin (sadece rakam, min 7). ");
                gecerli = false;
            }

            // Varsa seçilebilir listeler için (kayıt yoksa zorunlu tutma)
            if (cbSube.Items.Count > 0 && cbSube.SelectedIndex < 0)
            {
                ep.SetError(cbSube, "Şube seçin.");
                gecerli = false;
            }
            if (cbPersonel.Items.Count > 0 && cbPersonel.SelectedIndex < 0)
            {
                ep.SetError(cbPersonel, "Personel seçin.");
                gecerli = false;
            }
            if (cbArac.Items.Count > 0 && cbArac.SelectedIndex < 0)
            {
                ep.SetError(cbArac, "Araç seçin.");
                gecerli = false;
            }

            // Tarih tutarlılığı
            if (dtpIslemBaslangic.Checked && dtpIslemBitis.Checked && dtpIslemBaslangic.Value > dtpIslemBitis.Value)
            {
                ep.SetError(dtpIslemBitis, "Bitiş, başlangıçtan önce olamaz.");
                gecerli = false;
            }

            // Teslimat kodu boşsa otomatik üret (readonly olduğu için sadece set)
            if (string.IsNullOrWhiteSpace(tbTeslimatKodu.Text))
            {
                tbTeslimatKodu.Text = RasgeleKod4();
            }

            if (!gecerli)
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurun.");
            }
            return gecerli;
        }

        private bool TeslimAlanlariniDogrula()
        {
            ep.Clear();
            if (string.IsNullOrWhiteSpace(tbTeslimEdilenKisi.Text))
            {
                ep.SetError(tbTeslimEdilenKisi, "Teslim alan kişi adı zorunludur.");
                MessageBox.Show("Teslim alan kişi adı zorunludur.");
                return false;
            }
            // Teslim işleminde telefon boşsa varsayılan üret
            if (string.IsNullOrWhiteSpace(tbIlgiliKisiTel.Text))
            {
                tbIlgiliKisiTel.Text = "0000000"; // en az 7 hane güvenli varsayılan
            }
            // Teslimat kodu boşsa üret (readonly sorun değil)
            if (string.IsNullOrWhiteSpace(tbTeslimatKodu.Text))
            {
                tbTeslimatKodu.Text = RasgeleKod4();
            }
            return true;
        }

        private void ReferansListeleriniDoldur()
        {
            using var ctx = new KtsContext();
            cbSube.DataSource = ctx.Subeler.OrderBy(s => s.SubeAd).ToList();
            cbSube.DisplayMember = nameof(Sube.SubeAd);
            cbSube.ValueMember = nameof(Sube.SubeId);
            cbSube.SelectedIndex = cbSube.Items.Count > 0 ? 0 : -1;

            cbPersonel.DataSource = ctx.Personeller.OrderBy(p => p.Ad).ToList();
            cbPersonel.DisplayMember = nameof(Personel.Ad);
            cbPersonel.ValueMember = nameof(Personel.PersonelId);
            cbPersonel.SelectedIndex = cbPersonel.Items.Count > 0 ? 0 : -1;

            cbArac.DataSource = ctx.Araclar.OrderBy(a => a.Plaka).ToList();
            cbArac.DisplayMember = nameof(Arac.Plaka);
            cbArac.ValueMember = nameof(Arac.AracId);
            cbArac.SelectedIndex = cbArac.Items.Count > 0 ? 0 : -1;
        }

        private void YukuYukleVeAlanlariDoldur()
        {
            using var ctx = new KtsContext();
            var g = ctx.Gonderiler
                .Include(x => x.Gonderen)
                .Include(x => x.Alici)
                .FirstOrDefault(x => x.GonderiId == _gonderiId);

            if (g == null)
            {
                MessageBox.Show("Gönderi bulunamadı.");
                Close();
                return;
            }

            lblTakipNo.Text = g.TakipNo;
            lblGonderen.Text = g.Gonderen != null ? $"{g.Gonderen.Ad} {g.Gonderen.Soyad}" : "-";
            lblAlici.Text = g.Alici != null ? $"{g.Alici.Ad} {g.Alici.Soyad}" : "-";
            lblTeslimatTipi.Text = g.TeslimatTipi ?? "-";
            lblAgirlik.Text = g.Agirlik.ToString("0.##");
            lblBoyut.Text = g.Boyut ?? "-";
            lblUcret.Text = g.Ucret.ToString("0.00");

            bool teslimEdildi = g.TeslimTarihi.HasValue;
            lblDurum.Text = teslimEdildi ? $"Teslim Edildi ({g.TeslimTarihi:dd.MM.yyyy HH:mm})" : "Teslim Bekliyor";
            lblDurum.ForeColor = teslimEdildi ? Color.SeaGreen : Color.DarkOrange;

            tbTeslimEdilenKisi.Text = g.TeslimEdilenKisi ?? string.Empty;
            dtpTeslimTarih.Value = g.TeslimTarihi ?? DateTime.Now;

            tbTeslimEdilenKisi.Enabled = !teslimEdildi;
            dtpTeslimTarih.Enabled = !teslimEdildi;
            btnTeslimEt.Enabled = !teslimEdildi;
        }

        private void DurumKaydiEkle()
        {
            if (!DurumAlanlariniDogrula()) return;

            var durumAd = cbDurumAd.SelectedItem?.ToString()?.Trim() ?? string.Empty;

            using var ctx = new KtsContext();
            var g = ctx.Gonderiler.FirstOrDefault(x => x.GonderiId == _gonderiId);
            if (g == null)
            {
                MessageBox.Show("Gönderi bulunamadı.");
                return;
            }

            var oncekiler = ctx.Set<GonderiDurumGecmis>()
                .Where(d => d.GonderiId == _gonderiId && d.SonDurumMu)
                .ToList();
            foreach (var d in oncekiler) d.SonDurumMu = false;

            // İstenen kural:
            // - Başlangıç her zaman gönderi oluşturulurken belirlenen tarih (GonderiTarihi)
            // - Eğer SonDurum işaretliyse bitiş: kullanıcı seçmişse o an, değilse durum tarihi
            DateTime? islemBaslangic = g.GonderiTarihi;
            DateTime? islemBitis = null;
            if (ckSonDurum.Checked)
            {
                islemBitis = dtpIslemBitis.Checked ? dtpIslemBitis.Value : dtpDurumTarih.Value;
            }

            var yeni = new GonderiDurumGecmis
            {
                GonderiId = _gonderiId,
                DurumAd = durumAd,
                Aciklama = tbAciklama.Text,
                Tarih = dtpDurumTarih.Value,
                IslemTipi = cbIslemTipi.SelectedItem?.ToString(),
                SonDurumMu = ckSonDurum.Checked,
                IslemSonucu = cbIslemSonucu.SelectedItem?.ToString(),
                TeslimatKodu = tbTeslimatKodu.Text,
                IlgiliKisiAd = tbIlgiliKisiAd.Text,
                IlgiliKisiTel = tbIlgiliKisiTel.Text,
                SubeId = cbSube.SelectedValue is int subeId ? subeId : (int?)null,
                PersonelId = cbPersonel.SelectedValue is int personelId ? personelId : (int?)null,
                AracId = cbArac.SelectedValue is int aracId ? aracId : (int?)null,
                IslemBaslangicTarihi = islemBaslangic,
                IslemBitisTarihi = islemBitis
            };

            ctx.Add(yeni);
            ctx.SaveChanges();
            MessageBox.Show("Durum kaydı eklendi.");
        }

        private void TeslimiTamamla()
        {
            if (!TeslimAlanlariniDogrula()) return;

            var adSoyad = (tbTeslimEdilenKisi.Text ?? string.Empty).Trim();
            var ilgiliTel = (tbIlgiliKisiTel.Text ?? string.Empty).Trim();

            using var ctx = new KtsContext();
            var g = ctx.Gonderiler.FirstOrDefault(x => x.GonderiId == _gonderiId);
            if (g == null)
            {
                MessageBox.Show("Gönderi bulunamadı.");
                return;
            }
            if (g.TeslimTarihi.HasValue)
            {
                MessageBox.Show("Bu gönderi zaten teslim edilmiş.");
                return;
            }

            g.TeslimEdilenKisi = adSoyad;
            g.TeslimTarihi = dtpTeslimTarih.Value;
            g.GuncellemeTarihi = DateTime.Now;

            var oncekiler = ctx.Set<GonderiDurumGecmis>()
                .Where(d => d.GonderiId == _gonderiId && d.SonDurumMu)
                .ToList();
            foreach (var d in oncekiler) d.SonDurumMu = false;

            var teslimDurumu = new GonderiDurumGecmis
            {
                GonderiId = _gonderiId,
                DurumAd = "Teslim Edildi",
                Aciklama = $"Teslim alan: {adSoyad}",
                Tarih = g.TeslimTarihi ?? DateTime.Now,
                IslemTipi = "Teslim",
                SonDurumMu = true,
                IslemSonucu = "Başarılı",
                IlgiliKisiAd = adSoyad,
                IlgiliKisiTel = string.IsNullOrWhiteSpace(ilgiliTel) ? "0000000" : ilgiliTel,
                TeslimatKodu = string.IsNullOrWhiteSpace(tbTeslimatKodu.Text) ? RasgeleKod4() : tbTeslimatKodu.Text,
                SubeId = cbSube.SelectedValue is int subeId ? subeId : (int?)null,
                PersonelId = cbPersonel.SelectedValue is int personelId ? personelId : (int?)null,
                AracId = cbArac.SelectedValue is int aracId ? aracId : (int?)null,
                // Başlangıç: gönderi oluşturma tarihi, Bitiş: teslim tarihi
                IslemBaslangicTarihi = g.GonderiTarihi,
                IslemBitisTarihi = g.TeslimTarihi ?? DateTime.Now
            };
            ctx.Add(teslimDurumu);

            ctx.SaveChanges();
            MessageBox.Show("Teslim işlemi tamamlandı.");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}