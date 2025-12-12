using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using System;

namespace kargotakipsistemi.Data;

/// <summary>
/// FiyatlandirmaTarife tablosu için baþlangýç verilerini ekler.
/// Migration sýrasýnda otomatik olarak veritabanýna eklenir.
/// Gerçekçi ve mantýklý fiyatlandýrma tarifeleri içerir.
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
            // 1. AÐIRLIK TARÝFESÝ
            // ======================================
            new FiyatlandirmaTarife
            {
                TarifeId = 1,
                TarifeTuru = "AgirlikTarife",
                TarifeAdi = "Standart Aðýrlýk Tarifesi (0-30 kg)",
                MinDeger = 0.0m,
                MaxDeger = 30.0m,
                Deger = 15.50m,
                Birim = "TL/kg",
                TeslimatTipi = null,
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "0-30 kg arasý standart aðýrlýk tarifesi. Genel kargo gönderileri için geçerlidir.",
                OlusturulmaTarihi = baseTarih
            },
            
            // ======================================
            // 2. HACÝM EK ÜCRETÝ
            // ======================================
            new FiyatlandirmaTarife
            {
                TarifeId = 2,
                TarifeTuru = "HacimEkUcret",
                TarifeAdi = "Hacim Eþik Tarifesi (0.5 m³ üzeri)",
                MinDeger = 0.5m,
                MaxDeger = null,
                Deger = 75.00m,
                Birim = "TL",
                TeslimatTipi = null,
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "0.5 m³ üzeri hacimli paketler için ek ücret uygulanýr.",
                OlusturulmaTarihi = baseTarih
            },
            
            // ======================================
            // 3. TESLÝMAT ÇARPANLARI
            // ======================================
            
            // 3.1. Standart Teslimat
            new FiyatlandirmaTarife
            {
                TarifeId = 3,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Standart Teslimat (3-5 Ýþ Günü)",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.0m,
                Birim = "Çarpan",
                TeslimatTipi = "Standart Teslimat",
                Aktif = true,
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Normal teslimat süresi, ek ücret uygulanmaz (x1.0)",
                OlusturulmaTarihi = baseTarih
            },
            
            // 3.2. Hýzlý Teslimat
            new FiyatlandirmaTarife
            {
                TarifeId = 4,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Hýzlý Teslimat (1-2 Ýþ Günü)",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.35m,
                Birim = "Çarpan",
                TeslimatTipi = "Hýzlý Teslimat",
                Aktif = true,
                Oncelik = 2,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Hýzlandýrýlmýþ teslimat, %35 ek ücret (x1.35)",
                OlusturulmaTarihi = baseTarih
            },
            
            // 3.3. Ayný Gün Teslimat
            new FiyatlandirmaTarife
            {
                TarifeId = 5,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Ayný Gün Teslimat",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.75m,
                Birim = "Çarpan",
                TeslimatTipi = "Ayný Gün",
                Aktif = true,
                Oncelik = 3,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Ayný gün içinde teslimat, %75 ek ücret (x1.75)",
                OlusturulmaTarihi = baseTarih
            },
            
            // 3.4. Randevulu Teslimat
            new FiyatlandirmaTarife
            {
                TarifeId = 6,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Randevulu Teslimat",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.25m,
                Birim = "Çarpan",
                TeslimatTipi = "Randevulu Teslimat",
                Aktif = true,
                Oncelik = 4,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Randevulu teslimat hizmeti, %25 ek ücret (x1.25)",
                OlusturulmaTarihi = baseTarih
            }
        );
    }
}
