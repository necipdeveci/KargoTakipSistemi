using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Data;

/// <summary>
/// Il tablosu için Türkiye'nin 81 ilinin baþlangýç verilerini ekler.
/// Migration sýrasýnda kullanýlýr.
/// </summary>
public static class IlSeedData
{
    /// <summary>
    /// ModelBuilder üzerinden seed data ekler (Migration'da kullanýlýr)
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        var iller = new[]
        {
            new Il { IlId = 1, IlAd = "Adana" },
            new Il { IlId = 2, IlAd = "Adýyaman" },
            new Il { IlId = 3, IlAd = "Afyonkarahisar" },
            new Il { IlId = 4, IlAd = "Aðrý" },
            new Il { IlId = 5, IlAd = "Amasya" },
            new Il { IlId = 6, IlAd = "Ankara" },
            new Il { IlId = 7, IlAd = "Antalya" },
            new Il { IlId = 8, IlAd = "Artvin" },
            new Il { IlId = 9, IlAd = "Aydýn" },
            new Il { IlId = 10, IlAd = "Balýkesir" },
            new Il { IlId = 11, IlAd = "Bilecik" },
            new Il { IlId = 12, IlAd = "Bingöl" },
            new Il { IlId = 13, IlAd = "Bitlis" },
            new Il { IlId = 14, IlAd = "Bolu" },
            new Il { IlId = 15, IlAd = "Burdur" },
            new Il { IlId = 16, IlAd = "Bursa" },
            new Il { IlId = 17, IlAd = "Çanakkale" },
            new Il { IlId = 18, IlAd = "Çankýrý" },
            new Il { IlId = 19, IlAd = "Çorum" },
            new Il { IlId = 20, IlAd = "Denizli" },
            new Il { IlId = 21, IlAd = "Diyarbakýr" },
            new Il { IlId = 22, IlAd = "Edirne" },
            new Il { IlId = 23, IlAd = "Elazýð" },
            new Il { IlId = 24, IlAd = "Erzincan" },
            new Il { IlId = 25, IlAd = "Erzurum" },
            new Il { IlId = 26, IlAd = "Eskiþehir" },
            new Il { IlId = 27, IlAd = "Gaziantep" },
            new Il { IlId = 28, IlAd = "Giresun" },
            new Il { IlId = 29, IlAd = "Gümüþhane" },
            new Il { IlId = 30, IlAd = "Hakkari" },
            new Il { IlId = 31, IlAd = "Hatay" },
            new Il { IlId = 32, IlAd = "Isparta" },
            new Il { IlId = 33, IlAd = "Mersin" },
            new Il { IlId = 34, IlAd = "Ýstanbul" },
            new Il { IlId = 35, IlAd = "Ýzmir" },
            new Il { IlId = 36, IlAd = "Kars" },
            new Il { IlId = 37, IlAd = "Kastamonu" },
            new Il { IlId = 38, IlAd = "Kayseri" },
            new Il { IlId = 39, IlAd = "Kýrklareli" },
            new Il { IlId = 40, IlAd = "Kýrþehir" },
            new Il { IlId = 41, IlAd = "Kocaeli" },
            new Il { IlId = 42, IlAd = "Konya" },
            new Il { IlId = 43, IlAd = "Kütahya" },
            new Il { IlId = 44, IlAd = "Malatya" },
            new Il { IlId = 45, IlAd = "Manisa" },
            new Il { IlId = 46, IlAd = "Kahramanmaraþ" },
            new Il { IlId = 47, IlAd = "Mardin" },
            new Il { IlId = 48, IlAd = "Muðla" },
            new Il { IlId = 49, IlAd = "Muþ" },
            new Il { IlId = 50, IlAd = "Nevþehir" },
            new Il { IlId = 51, IlAd = "Niðde" },
            new Il { IlId = 52, IlAd = "Ordu" },
            new Il { IlId = 53, IlAd = "Rize" },
            new Il { IlId = 54, IlAd = "Sakarya" },
            new Il { IlId = 55, IlAd = "Samsun" },
            new Il { IlId = 56, IlAd = "Siirt" },
            new Il { IlId = 57, IlAd = "Sinop" },
            new Il { IlId = 58, IlAd = "Sivas" },
            new Il { IlId = 59, IlAd = "Tekirdað" },
            new Il { IlId = 60, IlAd = "Tokat" },
            new Il { IlId = 61, IlAd = "Trabzon" },
            new Il { IlId = 62, IlAd = "Tunceli" },
            new Il { IlId = 63, IlAd = "Þanlýurfa" },
            new Il { IlId = 64, IlAd = "Uþak" },
            new Il { IlId = 65, IlAd = "Van" },
            new Il { IlId = 66, IlAd = "Yozgat" },
            new Il { IlId = 67, IlAd = "Zonguldak" },
            new Il { IlId = 68, IlAd = "Aksaray" },
            new Il { IlId = 69, IlAd = "Bayburt" },
            new Il { IlId = 70, IlAd = "Karaman" },
            new Il { IlId = 71, IlAd = "Kýrýkkale" },
            new Il { IlId = 72, IlAd = "Batman" },
            new Il { IlId = 73, IlAd = "Þýrnak" },
            new Il { IlId = 74, IlAd = "Bartýn" },
            new Il { IlId = 75, IlAd = "Ardahan" },
            new Il { IlId = 76, IlAd = "Iðdýr" },
            new Il { IlId = 77, IlAd = "Yalova" },
            new Il { IlId = 78, IlAd = "Karabük" },
            new Il { IlId = 79, IlAd = "Kilis" },
            new Il { IlId = 80, IlAd = "Osmaniye" },
            new Il { IlId = 81, IlAd = "Düzce" }
        };

        modelBuilder.Entity<Il>().HasData(iller);
    }
}
