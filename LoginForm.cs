using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using kargotakipsistemi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace kargotakipsistemi
{
    public partial class LoginForm : Form                       
    {
        private int _failedLoginAttempts = 0;
        private DateTime? _lastFailedAttempt = null;
        private const int MAX_ATTEMPTS = 5;
        private const int LOCKOUT_MINUTES = 15;

        public LoginForm()
        {
            InitializeComponent();
            ConfigureLoginForm();
            ApplyCustomColors();
        }

        /// <summary>
        /// Form ayarlarını yapılandır
        /// </summary>
        private void ConfigureLoginForm()
        {
            // Şifre alanını gizle
            if (tb_loginSifre != null)
            {
                tb_loginSifre.PasswordChar = '●';
                tb_loginSifre.UseSystemPasswordChar = false;
            }

            // Enter tuşu ile giriş yapabilme
            this.AcceptButton = btn_login;

            // Form kapanma davranışı
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Form başlığı
            this.Text = "Kargo Takip Sistemi - Giriş";
        }

        /// <summary>
        /// MainForm ile aynı renk paletini uygula
        /// </summary>
        private void ApplyCustomColors()
        {
            // Ana form arka plan rengi
            this.BackColor = Color.FromArgb(242, 242, 242);

            // TextBox'lar için renk ayarları
            if (tb_loginEposta != null)
            {
                tb_loginEposta.BackColor = Color.FromArgb(182, 176, 159);
                tb_loginEposta.BorderStyle = BorderStyle.FixedSingle;
                tb_loginEposta.ForeColor = Color.FromArgb(0, 0, 0);
            }

            if (tb_loginSifre != null)
            {
                tb_loginSifre.BackColor = Color.FromArgb(182, 176, 159);
                tb_loginSifre.BorderStyle = BorderStyle.FixedSingle;
                tb_loginSifre.ForeColor = Color.FromArgb(0, 0, 0);
            }

            // Login butonu için renk ayarları
            if (btn_login != null)
            {
                btn_login.BackColor = Color.FromArgb(182, 176, 159);
                btn_login.FlatStyle = FlatStyle.Flat;
                btn_login.ForeColor = Color.Black;
                btn_login.FlatAppearance.BorderSize = 1;
                btn_login.FlatAppearance.BorderColor = Color.FromArgb(160, 154, 139);
                btn_login.Cursor = Cursors.Hand;
            }

            // Eğer varsa Label'lar için renk ayarları (MainForm standardı)
            foreach (Control control in this.Controls)
            {
                if (control is Label label)
                {
                    label.BackColor = Color.Transparent;
                    label.ForeColor = Color.Black;

                    // Özel label isimleri (MainForm konvansiyonuna uygun)
                    if (label.Name == "lbl_loginEposta" || label.Name == "label1")
                    {
                        label.Text = "E-posta:";
                    }
                    else if (label.Name == "lbl_loginSifre" || label.Name == "label2")
                    {
                        label.Text = "Şifre:";
                    }
                    else if (label.Name == "lbl_loginBaslik" || label.Name == "label3")
                    {
                        label.Text = "Kargo Takip Sistemi";
                        label.ForeColor = Color.FromArgb(60, 60, 60);
                    }
                }
                else if (control is Panel panel)
                {
                    panel.BackColor = Color.FromArgb(234, 228, 213);
                }
                else if (control is GroupBox groupBox)
                {
                    groupBox.BackColor = Color.FromArgb(234, 228, 213);
                    groupBox.ForeColor = Color.Black;
                }
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            // Kilitlenme kontrolü
            if (IsAccountLocked())
            {
                int remainingMinutes = GetRemainingLockoutMinutes();
                ShowError($"Çok fazla başarısız giriş denemesi yaptınız.\n" +
                         $"Lütfen {remainingMinutes} dakika sonra tekrar deneyin.");
                return;
            }

            // Kullanıcı girişlerini al ve temizle
            string mail = tb_loginEposta.Text.Trim();
            string sifre = tb_loginSifre.Text.Trim(); // Şifrede de Trim ekledik

            // Boş kontrol - Validasyon
            if (!ValidateInputs(mail, sifre))
            {
                return;
            }

            // Giriş butonunu devre dışı bırak (çift tıklama önleme)
            btn_login.Enabled = false;
            btn_login.Text = "Giriş yapılıyor...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Veritabanı bağlantısı
                using (var ctx = new KtsContext())
                {
                    // DEBUG: Tüm personelleri kontrol et
                    var tumPersoneller = ctx.Personeller.ToList();
                    System.Diagnostics.Debug.WriteLine($"Toplam Personel Sayısı: {tumPersoneller.Count}");
                    
                    // DEBUG: Girilen bilgileri kontrol et
                    System.Diagnostics.Debug.WriteLine($"Aranan Mail: [{mail}]");
                    System.Diagnostics.Debug.WriteLine($"Aranan Şifre: [{sifre}]");

                    // Personel kaydını ara - ToLower() ile case-insensitive arama (GERİ EKLENDİ)
                    var personel = ctx.Personeller
                        .Include(p => p.Rol) // Rol bilgisini eager loading ile yükle
                        .AsNoTracking() // Performans için tracking kapalı
                        .FirstOrDefault(p => p.Mail.ToLower() == mail.ToLower());

                    // DEBUG: Personel bulundu mu?
                    if (personel != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Personel Bulundu: {personel.Ad} {personel.Soyad}");
                        System.Diagnostics.Debug.WriteLine($"DB Mail: [{personel.Mail}]");
                        System.Diagnostics.Debug.WriteLine($"DB Şifre: [{personel.Sifre}]");
                        System.Diagnostics.Debug.WriteLine($"Aktif: {personel.Aktif}");
                        System.Diagnostics.Debug.WriteLine($"Rol: {personel.Rol?.RolAd ?? "NULL"}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("PERSONEL BULUNAMADI!");
                        // Veritabanındaki tüm mailleri göster
                        foreach (var p in tumPersoneller)
                        {
                            System.Diagnostics.Debug.WriteLine($"  - [{p.Mail}]");
                        }
                    }

                    if (personel == null ||
                        personel.Sifre != sifre || // Düz metin şifre kontrolü
                        !personel.Aktif)
                    {
                        // DEBUG: Hangi kontrol başarısız oldu?
                        if (personel == null)
                            System.Diagnostics.Debug.WriteLine("HATA: Personel NULL");
                        else if (personel.Sifre != sifre)
                            System.Diagnostics.Debug.WriteLine($"HATA: Şifre eşleşmiyor. Beklenen: [{personel.Sifre}], Girilen: [{sifre}]");
                        else if (!personel.Aktif)
                            System.Diagnostics.Debug.WriteLine("HATA: Personel aktif değil");

                        HandleFailedLogin();

                        // Genel hata mesajı - detay vermiyoruz
                        string errorMessage = "E-posta adresi veya şifre hatalı.\n" +
                                             "Lütfen bilgilerinizi kontrol edip tekrar deneyin.";

                        // Kalan deneme hakkı uyarısı
                        if (_failedLoginAttempts >= 3)
                        {
                            int remainingAttempts = MAX_ATTEMPTS - _failedLoginAttempts;
                            errorMessage += $"\n\nUyarı: {remainingAttempts} deneme hakkınız kaldı.";
                        }

                        ShowError(errorMessage);
                        return;
                    }

                    // Rol kontrolü (ekstra güvenlik)
                    if (personel.Rol == null)
                    {
                        ShowError("Hesabınızda bir yapılandırma hatası var.\n" +
                                 "Lütfen sistem yöneticisi ile iletişime geçin.");
                        return;
                    }

                    // Başarılı giriş
                    System.Diagnostics.Debug.WriteLine("GİRİŞ BAŞARILI!");
                    ResetFailedLoginAttempts();
                    OpenMainForm(personel);
                }
            }
            catch (DbUpdateException dbEx)
            {
                System.Diagnostics.Debug.WriteLine($"Veritabanı Hatası: {dbEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
                
                ShowError("Veritabanı bağlantı hatası oluştu.\n" +
                         "Lütfen internet bağlantınızı kontrol edin veya sistem yöneticisi ile iletişime geçin.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Genel Hata: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                
                ShowError("Beklenmeyen bir hata oluştu.\n" +
                         "Lütfen daha sonra tekrar deneyin veya sistem yöneticisi ile iletişime geçin.");
            }
            finally
            {
                // Butonu tekrar aktif et
                btn_login.Enabled = true;
                btn_login.Text = "Giriş Yap";
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Kullanıcı girişlerini doğrula
        /// </summary>
        private bool ValidateInputs(string mail, string sifre)
        {
            if (string.IsNullOrWhiteSpace(mail))
            {
                ShowError("Lütfen e-posta adresinizi girin.");
                tb_loginEposta.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(sifre))
            {
                ShowError("Lütfen şifrenizi girin.");
                tb_loginSifre.Focus();
                return false;
            }

            // Mail formatı kontrolü KALDIRILDI
            // Veritabanı kontrolü zaten yapılacak, burada format kontrolüne gerek yok
    
            return true;
        }

        /// <summary>
        /// Başarısız giriş denemesini kaydet
        /// </summary>
        private void HandleFailedLogin()
        {
            _failedLoginAttempts++;
            _lastFailedAttempt = DateTime.Now;
        }

        /// <summary>
        /// Başarılı giriş sonrası sayacı sıfırla
        /// </summary>
        private void ResetFailedLoginAttempts()
        {
            _failedLoginAttempts = 0;
            _lastFailedAttempt = null;
        }

        /// <summary>
        /// Hesap kilitli mi kontrol et
        /// </summary>
        private bool IsAccountLocked()
        {
            if (_failedLoginAttempts < MAX_ATTEMPTS)
                return false;

            if (_lastFailedAttempt == null)
                return false;

            var lockoutEnd = _lastFailedAttempt.Value.AddMinutes(LOCKOUT_MINUTES);

            if (DateTime.Now >= lockoutEnd)
            {
                // Kilitleme süresi doldu, sıfırla
                ResetFailedLoginAttempts();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kalan kilitleme süresini dakika olarak al
        /// </summary>
        private int GetRemainingLockoutMinutes()
        {
            if (_lastFailedAttempt == null)
                return 0;

            var lockoutEnd = _lastFailedAttempt.Value.AddMinutes(LOCKOUT_MINUTES);
            var remaining = lockoutEnd - DateTime.Now;

            return Math.Max(1, (int)Math.Ceiling(remaining.TotalMinutes));
        }

        /// <summary>
        /// Hata mesajı göster
        /// </summary>
        private void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Giriş Başarısız",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// MainForm'u aç ve LoginForm'u kapat
        /// </summary>
        private void OpenMainForm(Personel personel)
        {
            try
            {
                // MainForm'u oluştur ve göster
                var mainForm = new MainForm(personel);

                // LoginForm kapandığında uygulamayı sonlandır
                mainForm.FormClosed += (s, args) => Application.Exit();

                mainForm.Show();
                this.Hide(); // LoginForm'u gizle

                // Başarılı giriş mesajı (opsiyonel - gerekirse aktif edin)
                // MessageBox.Show($"Hoş geldiniz {personel.Ad} {personel.Soyad}!", "Başarılı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("Ana ekran açılırken bir hata oluştu.\n" +
                         "Lütfen uygulamayı yeniden başlatın.");
                this.Show(); // Hata durumunda LoginForm'u tekrar göster

                // Loglama (opsiyonel)
                // LogException(ex);
            }
        }

        /// <summary>
        /// Form kapanırken temizlik işlemleri
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Eğer MainForm açılmadıysa uygulamayı kapat
            if (Application.OpenForms.OfType<MainForm>().FirstOrDefault() == null)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Şifreyi SHA256 ile hash'le (güvenlik için)
        /// </summary>
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}