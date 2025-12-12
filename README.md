# Kargo Takip Sistemi (KTS)

Bu proje, modern lojistik ve kargo operasyonlarýný yönetmek amacýyla geliþtirilmiþ kapsamlý bir **Windows Forms (.NET 9)** uygulamasýdýr. Kargo kabulünden teslimatýna, personel yönetiminden araç filosuna kadar tüm süreçleri dijital ortamda takip etmeyi saðlar.

## ?? Proje Hakkýnda

Kargo Takip Sistemi, nesne yönelimli programlama (OOP) prensiplerine ve katmanlý mimariye uygun olarak tasarlanmýþtýr. Proje, veritabaný iþlemlerini **Entity Framework Core** kullanarak gerçekleþtirir ve dinamik bir yapýya sahiptir.

### Temel Özellikler

*   **?? Rol Bazlý Yetkilendirme (RBAC):**
    *   Sistemde 18 farklý personel rolü tanýmlýdýr (Örn: Sistem Yöneticisi, Genel Müdür, Þube Müdürü, Kurye, Müþteri Hizmetleri).
    *   Her rolün eriþebileceði sekmeler ve yapabileceði iþlemler dinamik olarak kýsýtlanmýþtýr.

*   **?? Gönderi Yönetimi:**
    *   Gönderi oluþturma, düzenleme ve silme.
    *   Gönderici ve alýcý müþteri seçimi, adres yönetimi.
    *   Barkod/Takip numarasý üretimi.
    *   Gönderi durum takibi (Hazýrlanýyor, Yolda, Teslim Edildi vb.).
    *   Detaylý gönderi arama ve filtreleme.

*   **?? Dinamik Fiyatlandýrma:**
    *   Gönderi aðýrlýðý, boyutu (Desi), teslimat tipi ve ek hizmetlere göre otomatik fiyat hesaplama.
    *   Veritabaný tabanlý tarife yönetimi (`DinamikUcretHesaplamaServisi`).

*   **?? Personel ve Müþteri Yönetimi:**
    *   Personel kayýt, rol atama ve þube iliþkilendirme.
    *   Müþteri veritabaný, iletiþim bilgileri ve adres defteri.

*   **?? Operasyonel Yönetim:**
    *   **Þube Yönetimi:** Þubelerin konumu, kapasitesi ve çalýþma saatleri.
    *   **Araç Filosu:** Araçlarýn durumu, kapasitesi ve þube atamalarý.

## ?? Teknolojiler ve Mimari

Proje **C# 13** ve **.NET 9** altyapýsý üzerine inþa edilmiþtir.

*   **Platform:** Windows Forms (WinForms)
*   **Veri Eriþimi:** Entity Framework Core (Code-First Yaklaþýmý)
*   **Veritabaný:** Microsoft SQL Server (Varsayýlan yapýlandýrma)
*   **Mimari Yapý:**
    *   `Entities`: Veritabaný tablolarýný temsil eden varlýk sýnýflarý.
    *   `Services`: Ýþ mantýðýný (Business Logic) içeren servis katmaný.
    *   `Forms`: Kullanýcý arayüzü (UI) katmaný.
    *   `Data`: Veritabaný baðlamý (Context) ve tohum veriler (Seed Data).
    *   `Validators`: Veri tutarlýlýðýný saðlayan doðrulama sýnýflarý.

## ?? Proje Yapýsý

```text
KargoTakipSistemi/
??? Entities/           # Veritabaný modelleri (Gonderi, Personel, Sube vb.)
??? Services/           # Ýþ mantýðý servisleri (GonderiServisi, PersonelServisi vb.)
??? Forms/              # Uygulama formlarý (MainForm, LoginForm vb.)
??? Data/               # DbContext ve Seed Data sýnýflarý
??? Validators/         # Giriþ doðrulama mantýklarý
??? Helpers/            # Yardýmcý araçlar (Þifreleme, UI kontrolleri)
??? Migrations/         # Veritabaný migrasyon dosyalarý
```

## ?? Kurulum ve Çalýþtýrma

1.  **Projeyi Ýndirin:**
    Bu depoyu yerel makinenize klonlayýn.

2.  **Veritabanýný Hazýrlayýn:**
    Proje *Code-First* yaklaþýmýný kullanýr. `Package Manager Console` üzerinden aþaðýdaki komutu çalýþtýrarak veritabanýný oluþturun:
    ```powershell
    Update-Database
    ```
    *(Not: `appsettings.json` veya `KtsContext.cs` içerisindeki baðlantý dizesinin (Connection String) yerel SQL sunucunuza uygun olduðundan emin olun.)*

3.  **Projeyi Derleyin:**
    Visual Studio 2022 (veya daha yeni bir sürüm) kullanarak çözümü derleyin.

4.  **Çalýþtýrýn:**
    Projeyi baþlatýn. Ýlk açýlýþta `KapsamliSeedData` sayesinde veritabanýna örnek veriler (Roller, Ýller, Örnek Personeller) otomatik olarak eklenecektir.

## ?? Kullaným

Uygulama açýldýðýnda **Giriþ Ekraný (LoginForm)** karþýlar.
Veritabaný oluþturulurken eklenen varsayýlan yönetici veya personel hesaplarý ile giriþ yapabilirsiniz.

*   **Yönetici Paneli:** Tüm sekmelere (Gönderi, Müþteri, Operasyon, Raporlar) eriþim saðlar.
*   **Kurye Giriþi:** Sadece kendisine atanan gönderileri ve teslimat ekranlarýný görür.

---
*Geliþtirici: Necip Deveci*
