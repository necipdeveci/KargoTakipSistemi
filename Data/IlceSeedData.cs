using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Data;

/// <summary>
/// Ilce tablosu için her il'e ait 3 ilçenin baþlangýç verilerini ekler.
/// Migration sýrasýnda kullanýlýr.
/// Toplam: 81 il * 3 ilçe = 243 kayýt
/// </summary>
public static class IlceSeedData
{
    /// <summary>
    /// ModelBuilder üzerinden seed data ekler (Migration'da kullanýlýr)
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        var ilceler = new List<Ilce>();
        int ilceIdCounter = 1;

        // Her il için 3 ilçe oluþtur
        for (int ilId = 1; ilId <= 81; ilId++)
        {
            for (int i = 1; i <= 3; i++)
            {
                ilceler.Add(new Ilce
                {
                    IlceId = ilceIdCounter++,
                    IlId = ilId,
                    IlceAd = $"Ýlçe {i}"
                });
            }
        }

        modelBuilder.Entity<Ilce>().HasData(ilceler);
    }
}
