using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using System;

namespace kargotakipsistemi.Data;

/// <summary>
/// FiyatlandirmaTarife tablosu için baþlangýç verilerini ekler.
/// Migration sýrasýnda otomatik olarak veritabanýna eklenir.
/// Her tarife türü için 1 varsayýlan standart kayýt içerir.
/// </summary>
public static class FiyatlandirmaTarifeSeedData
{
    /// <summary>
    /// ModelBuilder üzerinden seed data ekler (Migration'da kullanýlýr)
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        var baseTarih = new DateTime(2024, 1, 1);

        modelBuilder.Entity<FiyatlandirmaTarife>().HasData(
            // ======================================
            // 1. AÐIRLIK TARÝFESÝ - Standart Aðýrlýk Tarifesi
            // ======================================
            new FiyatlandirmaTarife
            {
                TarifeId = 1,
                TarifeTuru = "AgirlikTarife",
                TarifeAdi = "Standart Aðýrlýk Tarifesi",
                MinDeger = 0.0m,
                MaxDeger = null,
                Deger = 25.0m,
                Birim = "TL/kg",
                TeslimatTipi = null,
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Tüm paketler için standart kg baþý ücret",
                OlusturulmaTarihi = baseTarih
            },
            
            // ======================================
            // 2. HACÝM EK ÜCRETÝ - Hacim Kontrolü Yok
            // ======================================
            new FiyatlandirmaTarife
            {
                TarifeId = 2,
                TarifeTuru = "HacimEkUcret",
                TarifeAdi = "Hacim Ek Ücreti Yok",
                MinDeger = 0.0m,
                MaxDeger = null,
                Deger = 0.0m,
                Birim = "TL",
                TeslimatTipi = null,
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Standart gönderilerde hacim ek ücreti uygulanmaz",
                OlusturulmaTarihi = baseTarih
            },
            
            // ======================================
            // 3. TESLÝMAT TÝPÝ ÇARPANI - Normal Teslimat
            // ======================================
            new FiyatlandirmaTarife
            {
                TarifeId = 3,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Normal Teslimat",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.0m,
                Birim = "Çarpan",
                TeslimatTipi = "Standart Teslimat",
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Standart teslimat süresi - ek ücret uygulanmaz (x1.0)",
                OlusturulmaTarihi = baseTarih
            },
            
            // ======================================
            // 4. EK MASRAF EÞÝÐÝ - Ek Masraf Yok
            // ======================================
            new FiyatlandirmaTarife
            {
                TarifeId = 4,
                TarifeTuru = "EkMasrafEsik",
                TarifeAdi = "Ek Masraf Uygulanmaz",
                MinDeger = null,
                MaxDeger = null,
                Deger = 0.0m,
                Birim = "TL",
                TeslimatTipi = null,
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Standart gönderi koþullarýnda ek masraf uygulanmaz",
                OlusturulmaTarihi = baseTarih
            },
            
            // ======================================
            // 5. ÝNDÝRÝM EÞÝÐÝ - Ýndirim Yok
            // ======================================
            new FiyatlandirmaTarife
            {
                TarifeId = 5,
                TarifeTuru = "IndirimEsik",
                TarifeAdi = "Ýndirim Uygulanmaz",
                MinDeger = null,
                MaxDeger = null,
                Deger = 0.0m,
                Birim = "%",
                TeslimatTipi = null,
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Standart fiyatlandýrma - indirim uygulanmaz",
                OlusturulmaTarihi = baseTarih
            }
        );
    }
}
