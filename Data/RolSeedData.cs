using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Data;

/// <summary>
/// Rol tablosu için kargo takip sistemine özel rol tanýmlarýný ekler.
/// Migration sýrasýnda otomatik olarak veritabanýna eklenir.
/// Toplam 8 farklý rol kategorisi içerir.
/// </summary>
public static class RolSeedData
{
    /// <summary>
    /// ModelBuilder üzerinden seed data ekler (Migration'da kullanýlýr)
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>().HasData(
            // ======================================
            // YÖNETÝCÝ ROLLERÝ
            // ======================================
            
            // Rol 1: Sistem Yöneticisi
            new Rol
            {
                RolId = 1,
                RolAd = "Sistem Yöneticisi"
            },
            
            // Rol 2: Genel Müdür
            new Rol
            {
                RolId = 2,
                RolAd = "Genel Müdür"
            },
            
            // Rol 3: Bölge Müdürü
            new Rol
            {
                RolId = 3,
                RolAd = "Bölge Müdürü"
            },
            
            // Rol 4: Þube Müdürü
            new Rol
            {
                RolId = 4,
                RolAd = "Þube Müdürü"
            },
            
            // ======================================
            // OPERASYON ROLLERÝ
            // ======================================
            
            // Rol 5: Kurye (Teslimat Personeli)
            new Rol
            {
                RolId = 5,
                RolAd = "Kurye"
            },
            
            // Rol 6: Daðýtým Sorumlusu
            new Rol
            {
                RolId = 6,
                RolAd = "Daðýtým Sorumlusu"
            },
            
            // Rol 7: Transfer Personeli
            new Rol
            {
                RolId = 7,
                RolAd = "Transfer Personeli"
            },
            
            // Rol 8: Depo Görevlisi
            new Rol
            {
                RolId = 8,
                RolAd = "Depo Görevlisi"
            },
            
            // ======================================
            // MÜÞTERÝ HÝZMETLERÝ ROLLERÝ
            // ======================================
            
            // Rol 9: Müþteri Hizmetleri Temsilcisi
            new Rol
            {
                RolId = 9,
                RolAd = "Müþteri Hizmetleri"
            },
            
            // Rol 10: Kargo Kabul Görevlisi
            new Rol
            {
                RolId = 10,
                RolAd = "Kargo Kabul"
            },
            
            // Rol 11: Çaðrý Merkezi Operatörü
            new Rol
            {
                RolId = 11,
                RolAd = "Çaðrý Merkezi"
            },
            
            // ======================================
            // DESTEK VE YÖNETÝM ROLLERÝ
            // ======================================
            
            // Rol 12: Muhasebe Personeli
            new Rol
            {
                RolId = 12,
                RolAd = "Muhasebe"
            },
            
            // Rol 13: Ýnsan Kaynaklarý
            new Rol
            {
                RolId = 13,
                RolAd = "Ýnsan Kaynaklarý"
            },
            
            // Rol 14: Filo Yöneticisi (Araç Yönetimi)
            new Rol
            {
                RolId = 14,
                RolAd = "Filo Yöneticisi"
            },
            
            // Rol 15: IT Destek
            new Rol
            {
                RolId = 15,
                RolAd = "IT Destek"
            },
            
            // ======================================
            // ÖZEL GÖREV ROLLERÝ
            // ======================================
            
            // Rol 16: Kalite Kontrol Uzmaný
            new Rol
            {
                RolId = 16,
                RolAd = "Kalite Kontrol"
            },
            
            // Rol 17: Güvenlik Görevlisi
            new Rol
            {
                RolId = 17,
                RolAd = "Güvenlik"
            },
            
            // Rol 18: Eðitim Koordinatörü
            new Rol
            {
                RolId = 18,
                RolAd = "Eðitim Koordinatörü"
            }
        );
    }
}
