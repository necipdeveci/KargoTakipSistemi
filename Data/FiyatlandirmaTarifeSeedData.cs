using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using System;

namespace kargotakipsistemi.Data;

/// <summary>
/// FiyatlandirmaTarife tablosu için baþlangýç verilerini ekler.
/// Migration sýrasýnda otomatik olarak veritabanýna eklenir.
/// Toplam 18 farklý tarife kategorisi içerir.
/// </summary>
public static class FiyatlandirmaTarifeSeedData
{
    /// <summary>
    /// ModelBuilder üzerinden seed data ekler (Migration'da kullanýlýr)
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        var baseTarih = new DateTime(2024, 1, 1);

        // ======================================
        // 1. AÐIRLIK TARÝFELERÝ (4 kategori)
        // ======================================
        modelBuilder.Entity<FiyatlandirmaTarife>().HasData(
            // Kategori 1: Çok Hafif Paketler (0-1 kg) - AKTÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 1,
                TarifeTuru = "AgirlikTarife",
                TarifeAdi = "Çok Hafif (0-1 kg)",
                MinDeger = 0.0m,
                MaxDeger = 1.0m,
                Deger = 30.0m,
                Birim = "TL/kg",
                Aktif = 1 == 1, // ? AKTÝF (true)
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Küçük paketler için temel ücret (minimum ücret uygulanýr)",
                OlusturulmaTarihi = baseTarih
            },
            
            // Kategori 2: Hafif Paketler (1.01-5 kg) - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 2,
                TarifeTuru = "AgirlikTarife",
                TarifeAdi = "Hafif (1-5 kg)",
                MinDeger = 1.01m,
                MaxDeger = 5.0m,
                Deger = 22.0m,
                Birim = "TL/kg",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 2,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Standart paketler için ekonomik tarife",
                OlusturulmaTarihi = baseTarih
            },
            
            // Kategori 3: Orta Aðýrlýk (5.01-20 kg) - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 3,
                TarifeTuru = "AgirlikTarife",
                TarifeAdi = "Orta (5-20 kg)",
                MinDeger = 5.01m,
                MaxDeger = 20.0m,
                Deger = 18.0m,
                Birim = "TL/kg",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 3,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Büyük paketler için indirimli fiyat",
                OlusturulmaTarihi = baseTarih
            },
            
            // Kategori 4: Aðýr Paketler (20+ kg) - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 4,
                TarifeTuru = "AgirlikTarife",
                TarifeAdi = "Aðýr (20+ kg)",
                MinDeger = 20.01m,
                MaxDeger = null,
                Deger = 15.0m,
                Birim = "TL/kg",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 4,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Toplu/aðýr kargolar için en uygun fiyat",
                OlusturulmaTarihi = baseTarih
            }
        );

        // ======================================
        // 2. HACÝM EK ÜCRETLERÝ (4 kategori)
        // ======================================
        modelBuilder.Entity<FiyatlandirmaTarife>().HasData(
            // Standart Hacim - Ek ücret yok - AKTÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 5,
                TarifeTuru = "HacimEkUcret",
                TarifeAdi = "Standart Hacim (0-20,000 cm³)",
                MinDeger = 0.0m,
                MaxDeger = 20000.0m,
                Deger = 0.0m,
                Birim = "TL",
                Aktif = 1 == 1, // ? AKTÝF (true)
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Normal boyutlu paketler - ek ücret uygulanmaz",
                OlusturulmaTarihi = baseTarih
            },
            
            // Orta Hacim - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 6,
                TarifeTuru = "HacimEkUcret",
                TarifeAdi = "Orta Hacim (20,001-50,000 cm³)",
                MinDeger = 20000.01m,
                MaxDeger = 50000.0m,
                Deger = 20.0m,
                Birim = "TL",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 2,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Orta hacimli paketler için ek ücret",
                OlusturulmaTarihi = baseTarih
            },
            
            // Büyük Hacim - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 7,
                TarifeTuru = "HacimEkUcret",
                TarifeAdi = "Büyük Hacim (50,001-100,000 cm³)",
                MinDeger = 50000.01m,
                MaxDeger = 100000.0m,
                Deger = 45.0m,
                Birim = "TL",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 3,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Hacimli paketler için özel iþlem ücreti",
                OlusturulmaTarihi = baseTarih
            },
            
            // Çok Büyük Hacim - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 8,
                TarifeTuru = "HacimEkUcret",
                TarifeAdi = "Çok Büyük Hacim (100,000+ cm³)",
                MinDeger = 100000.01m,
                MaxDeger = null,
                Deger = 75.0m,
                Birim = "TL",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 4,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Ekstra büyük paketler için maksimum ek ücret",
                OlusturulmaTarihi = baseTarih
            }
        );

        // ======================================
        // 3. TESLÝMAT TÝPÝ ÇARPANLARI (4 kategori)
        // ======================================
        modelBuilder.Entity<FiyatlandirmaTarife>().HasData(
            // Standart Teslimat - AKTÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 9,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Standart Teslimat",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.0m,
                Birim = "çarpan",
                Aktif = 1 == 1, // ? AKTÝF (true)
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "3-5 iþ günü - ek ücret yok (x1.0)",
                OlusturulmaTarihi = baseTarih
            },
            
            // Hýzlý Teslimat - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 10,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Hýzlý Teslimat",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.35m,
                Birim = "çarpan",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 2,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "1-2 iþ günü - %35 ek ücret (x1.35)",
                OlusturulmaTarihi = baseTarih
            },
            
            // Ayný Gün Teslimat - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 11,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Ayný Gün Teslimat",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.60m,
                Birim = "çarpan",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 3,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Ayný gün teslim - %60 ek ücret (x1.60)",
                OlusturulmaTarihi = baseTarih
            },
            
            // Randevulu Teslimat - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 12,
                TarifeTuru = "TeslimatCarpan",
                TarifeAdi = "Randevulu Teslimat",
                MinDeger = null,
                MaxDeger = null,
                Deger = 1.40m,
                Birim = "çarpan",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 4,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Belirli saat aralýðýnda teslimat - %40 ek ücret (x1.40)",
                OlusturulmaTarihi = baseTarih
            }
        );

        // ======================================
        // 4. EK MASRAF EÞÝKLERÝ (3 kategori)
        // ======================================
        modelBuilder.Entity<FiyatlandirmaTarife>().HasData(
            // Çok Aðýr Yük - AKTÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 13,
                TarifeTuru = "EkMasrafEsik",
                TarifeAdi = "Çok Aðýr Yük (40+ kg)",
                MinDeger = 40.0m,
                MaxDeger = null,
                Deger = 50.0m,
                Birim = "TL",
                Aktif = 1 == 1, // ? AKTÝF (true)
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "40 kg üzeri paketler için özel taþýma ücreti",
                OlusturulmaTarihi = baseTarih
            },
            
            // Ekstra Büyük Hacim - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 14,
                TarifeTuru = "EkMasrafEsik",
                TarifeAdi = "Ekstra Büyük Hacim (150,000+ cm³)",
                MinDeger = 150000.0m,
                MaxDeger = null,
                Deger = 100.0m,
                Birim = "TL",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 2,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Çok hacimli paketler için özel araç gereksinimi ücreti",
                OlusturulmaTarihi = baseTarih
            },
            
            // Kýrýlgan/Hassas Ürün - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 15,
                TarifeTuru = "EkMasrafEsik",
                TarifeAdi = "Kýrýlgan/Hassas Ürün",
                MinDeger = null,
                MaxDeger = null,
                Deger = 25.0m,
                Birim = "TL",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 3,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "Özel paketleme ve dikkatli taþýma gerektiren ürünler için",
                OlusturulmaTarihi = baseTarih
            }
        );

        // ======================================
        // 5. ÝNDÝRÝM EÞÝKLERÝ (3 kategori)
        // ======================================
        modelBuilder.Entity<FiyatlandirmaTarife>().HasData(
            // Orta Hacim Ýndirimi - AKTÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 16,
                TarifeTuru = "IndirimEsik",
                TarifeAdi = "Orta Hacim Ýndirimi (10-25 kg)",
                MinDeger = 10.0m,
                MaxDeger = 25.0m,
                Deger = 0.05m,
                Birim = "%",
                Aktif = 1 == 1, // ? AKTÝF (true)
                Oncelik = 3,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "10-25 kg arasý gönderiler için %5 indirim",
                OlusturulmaTarihi = baseTarih
            },
            
            // Toplu Gönderi Ýndirimi - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 17,
                TarifeTuru = "IndirimEsik",
                TarifeAdi = "Toplu Gönderi Ýndirimi (25-50 kg)",
                MinDeger = 25.01m,
                MaxDeger = 50.0m,
                Deger = 0.10m,
                Birim = "%",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 2,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "25-50 kg arasý toplu gönderiler için %10 indirim",
                OlusturulmaTarihi = baseTarih
            },
            
            // Kurumsal Müþteri Ýndirimi - PASÝF ?
            new FiyatlandirmaTarife
            {
                TarifeId = 18,
                TarifeTuru = "IndirimEsik",
                TarifeAdi = "Kurumsal Müþteri (50+ kg)",
                MinDeger = 50.01m,
                MaxDeger = null,
                Deger = 0.15m,
                Birim = "%",
                Aktif = 0 == 1, // ? PASÝF (false)
                Oncelik = 1,
                GecerlilikBaslangic = baseTarih,
                Aciklama = "50 kg üzeri kurumsal gönderiler için %15 indirim",
                OlusturulmaTarihi = baseTarih
            }
        );
    }
}
