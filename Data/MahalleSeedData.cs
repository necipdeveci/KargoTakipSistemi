using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Data;

/// <summary>
/// Mahalle tablosu için her ilçeye ait 2 mahallenin baþlangýç verilerini ekler.
/// Migration sýrasýnda kullanýlýr.
/// Toplam: (81 il * 3 ilçe) * 2 mahalle = 486 kayýt
/// </summary>
public static class MahalleSeedData
{
    /// <summary>
    /// ModelBuilder üzerinden seed data ekler (Migration'da kullanýlýr)
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        var mahalleler = new List<Mahalle>();
        int mahalleIdCounter = 1;

        // Her ilçe için 2 mahalle oluþtur
        // Toplam 243 ilçe var (81 il * 3 ilçe)
        for (int ilceId = 1; ilceId <= 243; ilceId++)
        {
            for (int i = 1; i <= 2; i++)
            {
                mahalleler.Add(new Mahalle
                {
                    MahalleId = mahalleIdCounter++,
                    IlceId = ilceId,
                    MahalleAd = $"Mahalle {i}"
                });
            }
        }

        modelBuilder.Entity<Mahalle>().HasData(mahalleler);
    }
}
