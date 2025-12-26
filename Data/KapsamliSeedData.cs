using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using System;
using System.Linq;

namespace kargotakipsistemi.Data;

/// <summary>
/// Kapsamlı seed data oluşturur.
/// Personel, Şube, Araç, Müşteri, Adres ve Gönderi verilerini içerir.
/// </summary>
public static class KapsamliSeedData
{
    /// <summary>
    /// Model Builder üzerinden tüm seed data'yı ekler
    /// Doğru sıralama:
    /// 1. Iller
    /// 2. Ilceler
    /// 3. Mahalleler
    /// 4. Subeler
    /// 5. Araclar
    /// 6. Roller
    /// 7. Musteriler
    /// 8. Personeller
    /// 9. Adresler
    /// 10. Gonderiler
    /// 11. GonderiDurumGecmisi
    /// 12. MusteriAdresleri
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        var baseTarih = new DateTime(2024, 1, 1);

        // ======================================
        // 1. ADIM: İLLER (Bu veriler zaten LocationSeedData'da olabilir, gerekirse eklenebilir)
        // ======================================
        // LocationSeedData'nın yüklenmesini varsayıyoruz

        // ======================================
        // 2. ADIM: İLÇELER (Bu veriler zaten LocationSeedData'da olabilir, gerekirse eklenebilir)
        // ======================================
        // LocationSeedData'nın yüklenmesini varsayıyoruz

        // ======================================
        // 3. ADIM: MAHALLELER (Bu veriler zaten LocationSeedData'da olabilir, gerekirse eklenebilir)
        // ======================================
        // LocationSeedData'nın yüklenmesini varsayıyoruz

        // ======================================
        // 4. ADIM: ŞUBELER - 3 Adet Şube
        // ======================================
        modelBuilder.Entity<Sube>().HasData(
            // Şube 1: İstanbul Merkez
            new Sube
            {
                SubeId = 1,
                SubeAd = "İstanbul Merkez Şube",
                SubeTip = "Merkez",
                IlId = 34, // İstanbul
                IlceId = 100, // İlçe 1 (Varsayılan)
                AcikAdres = "Atatürk Mahallesi, Cumhuriyet Caddesi No:123 Kat:5 D:12",
                Tel = "02121234567",
                Email = "istanbul@kargosistemi.com",
                CalismaSaatleri = "24 Saat Açık",
                Kapasite = 5000
            },
            
            // Şube 2: Ankara Bölge Şube
            new Sube
            {
                SubeId = 2,
                SubeAd = "Ankara Bölge Şube",
                SubeTip = "Şube",
                IlId = 6, // Ankara
                IlceId = 16, // İlçe 1
                AcikAdres = "Kızılay Mahallesi, İnönü Bulvarı No:456 Kat:3",
                Tel = "03129876543",
                Email = "ankara@kargosistemi.com",
                CalismaSaatleri = "08:00 - 17:00",
                Kapasite = 3000
            },
            
            // Şube 3: İzmir Liman Şube
            new Sube
            {
                SubeId = 3,
                SubeAd = "İzmir Liman Şube",
                SubeTip = "Dağıtım Noktası",
                IlId = 35, // İzmir
                IlceId = 103, // İlçe 1
                AcikAdres = "Alsancak Mahallesi, Kordon Sokak No:789",
                Tel = "02325554433",
                Email = "izmir@kargosistemi.com",
                CalismaSaatleri = "09:00 - 18:00",
                Kapasite = 2000
            }
        );

        // ======================================
        // 5. ADIM: ARAÇLAR - Her Şube İçin Her Araç Tipi
        // ======================================
        // Araç tipleri: Kamyonet, Panelvan, Tır, Minibüs, Otomobil, Motosiklet
        
        int aracIdCounter = 1;
        var aracTipleri = new[] 
        { 
            ("Kamyonet", 1500m), 
            ("Panelvan", 800m), 
            ("Tır", 5000m), 
            ("Minibüs", 1200m), 
            ("Otomobil", 400m), 
            ("Motosiklet", 50m) 
        };

        var araclar = new System.Collections.Generic.List<Arac>();
        for (int subeId = 1; subeId <= 3; subeId++)
        {
            foreach (var (tip, kapasite) in aracTipleri)
            {
                araclar.Add(new Arac
                {
                    AracId = aracIdCounter++,
                    Plaka = $"{30 + subeId}ABC{100 + aracIdCounter}",
                    AracTip = tip,
                    SubeId = subeId,
                    KapasiteKg = kapasite,
                    Durum = "Aktif",
                    GpsKodu = $"GPS{subeId:D2}{aracIdCounter:D4}"
                });
            }
        }
        modelBuilder.Entity<Arac>().HasData(araclar.ToArray());

        // ======================================
        // 6. ADIM: ROLLER (Bu veriler zaten RolSeedData'da olabilir, gerekirse eklenebilir)
        // ======================================
        // RolSeedData'nın yüklenmesini varsayıyoruz

        // ======================================
        // 7. ADIM: MÜŞTERİLER - 8 Adet Müşteri
        // ======================================
        var musteriler = new[]
        {
            new Musteri
            {
                MusteriId = 1,
                Ad = "Kemal",
                Soyad = "Yılmaz",
                Mail = "kemal.yilmaz@email.com",
                Tel = "05301111111",
                DogumTarihi = new DateTime(1990, 3, 15),
                Notlar = "VIP müşteri, hızlı teslimat tercih eder"
            },
            new Musteri
            {
                MusteriId = 2,
                Ad = "Selin",
                Soyad = "Demir",
                Mail = "selin.demir@email.com",
                Tel = "05302222222",
                DogumTarihi = new DateTime(1988, 7, 22),
                Notlar = "Kurumsal müşteri, toplu gönderi yapar"
            },
            new Musteri
            {
                MusteriId = 3,
                Ad = "Barış",
                Soyad = "Kaya",
                Mail = "baris.kaya@email.com",
                Tel = "05303333333",
                DogumTarihi = new DateTime(1995, 11, 8),
                Notlar = "Düzenli müşteri, aylık ortalama 10 gönderi"
            },
            new Musteri
            {
                MusteriId = 4,
                Ad = "Aylin",
                Soyad = "Çelik",
                Mail = "aylin.celik@email.com",
                Tel = "05304444444",
                DogumTarihi = new DateTime(1992, 5, 19),
                Notlar = "E-ticaret müşterisi, standart teslimat tercih eder"
            },
            new Musteri
            {
                MusteriId = 5,
                Ad = "Cem",
                Soyad = "Arslan",
                Mail = "cem.arslan@email.com",
                Tel = "05305555555",
                DogumTarihi = new DateTime(1987, 9, 30),
                Notlar = "Yeni müşteri, ilk gönderi deneyimi"
            },
            new Musteri
            {
                MusteriId = 6,
                Ad = "Deniz",
                Soyad = "Polat",
                Mail = "deniz.polat@email.com",
                Tel = "05306666666",
                DogumTarihi = new DateTime(1993, 12, 5),
                Notlar = "Hassas ürün gönderileri, ekstra özen gerektirir"
            },
            new Musteri
            {
                MusteriId = 7,
                Ad = "Ebru",
                Soyad = "Yıldız",
                Mail = "ebru.yildiz@email.com",
                Tel = "05307777777",
                DogumTarihi = new DateTime(1991, 2, 14),
                Notlar = "Randevulu teslimat tercih eder"
            },
            new Musteri
            {
                MusteriId = 8,
                Ad = "Furkan",
                Soyad = "Özkan",
                Mail = "furkan.ozkan@email.com",
                Tel = "05308888888",
                DogumTarihi = new DateTime(1989, 6, 27),
                Notlar = "Kurumsal anlaşma var, indirimli fiyat uygulanır"
            }
        };
        modelBuilder.Entity<Musteri>().HasData(musteriler);

        // ======================================
        // 8. ADIM: PERSONELLER - Admin + Her Şube İçin Her Rol
        // ======================================
        // Roller: Genel Müdür, Bölge Müdürü, Şube Müdürü, Kurye, Dağıtım Sorumlusu,
        // Transfer Personeli, Depo Görevlisi, Müşteri Hizmetleri, Kargo Kabul, 
        // Çağrı Merkezi, Muhasebe, İnsan Kaynakları, Filo Yöneticisi, IT Destek,
        // Kalite Kontrol, Güvenlik, Eğitim Koordinatörü

        var personeller = new System.Collections.Generic.List<Personel>();
        
        // Admin (Sistem Yöneticisi)
        personeller.Add(new Personel
        {
            PersonelId = 1,
            Ad = "Admin",
            Soyad = "Sistem Yöneticisi",
            Mail = "admin@com",
            Sifre = "1234", // Basit şifre (production'da hash'lenmelidir)
            Tel = "05551234567",
            DogumTarihi = new DateTime(1985, 6, 15),
            Cinsiyet = "Erkek",
            RolId = 1, // Sistem Yöneticisi
            SubeId = 1, // Merkez şube
            AracId = null, // Admin aracı yok
            Aktif = true,
            IseGirisTarihi = baseTarih,
            IstenCikisTarihi = null,
            Maas = 25000m,
            EhliyetSinifi = "B"
        });

        int personelIdCounter = 2; // Admin = 1
        var personelAdlar = new[]
        {
            ("Mehmet", "Yılmaz"), ("Ayşe", "Demir"), ("Fatma", "Çelik"),
            ("Ali", "Kaya"), ("Zeynep", "Arslan"), ("Ahmet", "Öztürk"),
            ("Elif", "Yıldız"), ("Mustafa", "Aydın"), ("Emine", "Şahin"),
            ("Hasan", "Koç"), ("Merve", "Kurt"), ("İbrahim", "Özdemir"),
            ("Selin", "Çetin"), ("Burak", "Yavuz"), ("Gamze", "Özkan"),
            ("Emre", "Polat"), ("Deniz", "Şimşek"), ("Can", "Türk")
        };

        int personelIndex = 0;
        for (int subeId = 1; subeId <= 3; subeId++)
        {
            // Her şube için 18 rol (Admin hariç tüm roller)
            for (int rolId = 2; rolId <= 18; rolId++)
            {
                var (ad, soyad) = personelAdlar[personelIndex % personelAdlar.Length];
                
                // Kurye ve sürücü rolleri için araç atama
                int? atananAracId = null;
                if (rolId == 5 || rolId == 6 || rolId == 7) // Kurye, Dağıtım Sorumlusu, Transfer
                {
                    // Bu şubeye ait bir araç seç (şube bazlı indeks)
                    int aracIndexOffset = ((subeId - 1) * aracTipleri.Length) + 1; // Şube başlangıç araç ID'si
                    atananAracId = aracIndexOffset + ((personelIdCounter - 2) % aracTipleri.Length);
                }

                personeller.Add(new Personel
                {
                    PersonelId = personelIdCounter++,
                    Ad = ad,
                    Soyad = $"{soyad} {subeId}",
                    Mail = $"{ad.ToLower()}.{soyad.ToLower()}{subeId}@kargosistemi.com",
                    Sifre = "1234",
                    Tel = $"0555{1000000 + personelIdCounter:D7}",
                    DogumTarihi = baseTarih.AddYears(-30 - (personelIdCounter % 20)),
                    Cinsiyet = personelIdCounter % 2 == 0 ? "Erkek" : "Kadın",
                    RolId = rolId,
                    SubeId = subeId,
                    AracId = atananAracId,
                    Aktif = true,
                    IseGirisTarihi = baseTarih.AddDays(-365 * (personelIdCounter % 5)),
                    IstenCikisTarihi = null,
                    Maas = 8000m + (rolId * 500m),
                    EhliyetSinifi = atananAracId.HasValue ? "B" : "Yok"
                });
                personelIndex++;
            }
        }
        modelBuilder.Entity<Personel>().HasData(personeller.ToArray());

        // ======================================
        // 9. ADIM: ADRESLER (Personel + Müşteri Adresleri)
        // ======================================
        int adresIdCounter = 1;
        var adresler = new System.Collections.Generic.List<Adres>();
        
        // 9.1. Admin adresi
        adresler.Add(new Adres
        {
            AdresId = adresIdCounter++,
            AdresBaslik = "Admin Ev Adresi",
            AcikAdres = "Merkez Mahallesi, Ana Cadde No:1",
            IlId = 34, // İstanbul
            IlceId = 100,
            MahalleId = 199,
            PostaKodu = "34000",
            AdresTipi = "Ev",
            MusteriId = null,
            PersonelId = 1,
            Aktif = true,
            KapiNo = "1",
            BinaAdi = "Merkez Apartmanı",
            Kat = "5",
            Daire = "12",
            EkAciklama = "Sistem yöneticisi ev adresi"
        });

        // 9.2. Diğer personeller için adres
        foreach (var personel in personeller.Skip(1)) // Admin'i zaten ekledik
        {
            int ilId = personel.SubeId == 1 ? 34 : (personel.SubeId == 2 ? 6 : 35);
            int ilceId = personel.SubeId == 1 ? 100 : (personel.SubeId == 2 ? 16 : 103);
            int mahalleId = personel.SubeId == 1 ? 199 : (personel.SubeId == 2 ? 31 : 205);

            adresler.Add(new Adres
            {
                AdresId = adresIdCounter++,
                AdresBaslik = $"{personel.Ad} Ev Adresi",
                AcikAdres = $"{personel.Ad} Mahallesi, {personel.Soyad} Sokak No:{personel.PersonelId}",
                IlId = ilId,
                IlceId = ilceId,
                MahalleId = mahalleId,
                PostaKodu = $"{ilId}000",
                AdresTipi = "Ev",
                MusteriId = null,
                PersonelId = personel.PersonelId,
                Aktif = true,
                KapiNo = $"{personel.PersonelId}",
                BinaAdi = $"{personel.Soyad} Apt",
                Kat = $"{(personel.PersonelId % 10) + 1}",
                Daire = $"{(personel.PersonelId % 20) + 1}",
                EkAciklama = $"{personel.Ad} {personel.Soyad} ev adresi"
            });
        }

        // 9.3. Müşteri Adresleri - Her Müşteri İçin 2 Adres (Ev ve İş)
        for (int musteriId = 1; musteriId <= 8; musteriId++)
        {
            var musteri = musteriler[musteriId - 1];
            // Müşterileri farklı illere dağıt
            int ilId = (musteriId % 3) == 0 ? 35 : ((musteriId % 3) == 1 ? 34 : 6); // İzmir, İstanbul, Ankara
            int ilceId = ilId == 34 ? 100 : (ilId == 6 ? 16 : 103);
            int mahalleId = ilId == 34 ? 199 : (ilId == 6 ? 31 : 205);

            // Ev Adresi
            adresler.Add(new Adres
            {
                AdresId = adresIdCounter++,
                AdresBaslik = $"{musteri.Ad} Ev Adresi",
                AcikAdres = $"{musteri.Ad} Mahallesi, {musteri.Soyad} Sokak No:{musteriId * 10}",
                IlId = ilId,
                IlceId = ilceId,
                MahalleId = mahalleId,
                PostaKodu = $"{ilId}{musteriId:D3}0",
                AdresTipi = "Ev",
                MusteriId = musteriId,
                PersonelId = null,
                Aktif = true,
                KapiNo = $"{musteriId * 10}",
                BinaAdi = $"{musteri.Soyad} Konakları",
                Kat = $"{(musteriId % 5) + 1}",
                Daire = $"{musteriId}",
                EkAciklama = $"{musteri.Ad} {musteri.Soyad} ev adresi"
            });

            // İş Adresi (farklı il/ilçe)
            int isIlId = ilId == 34 ? 6 : (ilId == 6 ? 35 : 34); // Farklı il seç
            int isIlceId = isIlId == 34 ? 100 : (isIlId == 6 ? 16 : 103);
            int isMahalleId = isIlId == 34 ? 199 : (isIlId == 6 ? 31 : 205);

            adresler.Add(new Adres
            {
                AdresId = adresIdCounter++,
                AdresBaslik = $"{musteri.Ad} İş Adresi",
                AcikAdres = $"İş Merkezi, {musteri.Soyad} Plaza Kat:{musteriId} No:{musteriId * 5}",
                IlId = isIlId,
                IlceId = isIlceId,
                MahalleId = isMahalleId,
                PostaKodu = $"{isIlId}{musteriId:D3}1",
                AdresTipi = "Is", // Sistemde "İş" ve "Is" kontrolü yapılıyor
                MusteriId = musteriId,
                PersonelId = null,
                Aktif = true,
                KapiNo = $"{musteriId * 5}",
                BinaAdi = $"{musteri.Soyad} Plaza",
                Kat = $"{musteriId}",
                Daire = $"{musteriId * 10}",
                EkAciklama = $"{musteri.Ad} {musteri.Soyad} iş adresi"
            });
        }
        modelBuilder.Entity<Adres>().HasData(adresler.ToArray());

        // ======================================
        // 10. ADIM: GÖNDERİLER - Her Müşteri Kendine + Diğer Şehirlere Gönderi
        // ======================================
        int gonderiIdCounter = 1;
        var gonderiler = new System.Collections.Generic.List<Gonderi>();

        var teslimatTipleri = new[] { "Standart Teslimat", "Hızlı Teslimat", "Aynı Gün", "Randevulu Teslimat" };

        foreach (var musteri in musteriler)
        {
            // Müşterinin ev ve iş adreslerini bul
            var kendiEvAdres = adresler.First(a => a.MusteriId == musteri.MusteriId && a.AdresTipi == "Ev");
            var kendiIsAdres = adresler.First(a => a.MusteriId == musteri.MusteriId && (a.AdresTipi == "Is" || a.AdresTipi == "İş"));

            var baseTarih25 = new DateTime(2025, 10, 1);
            // 1. Kendine gönderi (ev -> iş)
            var kendiGonderi = new Gonderi
            {
                GonderiId = gonderiIdCounter,
                TakipNo = $"KTS{gonderiIdCounter:D8}",
                GonderenId = musteri.MusteriId,
                AliciId = musteri.MusteriId,
                GonderenAdresId = kendiEvAdres.AdresId,
                AliciAdresId = kendiIsAdres.AdresId,
                GonderiTarihi = baseTarih25.AddDays(gonderiIdCounter),
                TahminiTeslimTarihi = baseTarih25.AddDays(gonderiIdCounter + 3),
                TeslimTarihi = null,
                TeslimEdilenKisi = string.Empty,
                TeslimatTipi = teslimatTipleri[gonderiIdCounter % teslimatTipleri.Length],
                KuryeId = 2 + ((gonderiIdCounter - 1) % 3), // İlk 3 kurye arasında rotasyon
                Agirlik = 2.5m + (gonderiIdCounter % 10),
                Boyut = $"{20 + gonderiIdCounter}x{15 + gonderiIdCounter}x{10 + gonderiIdCounter}",
                Ucret = 50m + (gonderiIdCounter * 10m),
                IndirimTutar = gonderiIdCounter % 3 == 0 ? 10m : 0m,
                EkMasraf = gonderiIdCounter % 5 == 0 ? 15m : 0m,
                KayitTarihi = baseTarih25.AddDays(gonderiIdCounter - 1),
                GuncellemeTarihi = null,
                IptalTarihi = null,
                IadeDurumu = null
            };
            gonderiler.Add(kendiGonderi);
            gonderiIdCounter++;

            // 2. Diğer şehirlere gönderi (farklı il/ilçedeki müşterilere)
            var digerMusteriler = musteriler.Where(m => 
                m.MusteriId != musteri.MusteriId &&
                adresler.Any(a => a.MusteriId == m.MusteriId && 
                                       a.IlId != kendiEvAdres.IlId)).ToList();

            foreach (var alici in digerMusteriler)
            {
                var aliciEvAdres = adresler.First(a => a.MusteriId == alici.MusteriId && a.AdresTipi == "Ev");

                var gonderi = new Gonderi
                {
                    GonderiId = gonderiIdCounter,
                    TakipNo = $"KTS{gonderiIdCounter:D8}",
                    GonderenId = musteri.MusteriId,
                    AliciId = alici.MusteriId,
                    GonderenAdresId = kendiEvAdres.AdresId,
                    AliciAdresId = aliciEvAdres.AdresId,
                    GonderiTarihi = baseTarih25.AddDays(gonderiIdCounter),
                    TahminiTeslimTarihi = baseTarih25.AddDays(gonderiIdCounter + 4),
                    TeslimTarihi = null,
                    TeslimEdilenKisi = string.Empty,
                    TeslimatTipi = teslimatTipleri[gonderiIdCounter % teslimatTipleri.Length],
                    KuryeId = 2 + ((gonderiIdCounter - 1) % 3),
                    Agirlik = 3.5m + (gonderiIdCounter % 15),
                    Boyut = $"{25 + gonderiIdCounter}x{18 + gonderiIdCounter}x{12 + gonderiIdCounter}",
                    Ucret = 75m + (gonderiIdCounter * 8m),
                    IndirimTutar = gonderiIdCounter % 4 == 0 ? 20m : 0m,
                    EkMasraf = gonderiIdCounter % 6 == 0 ? 25m : 0m,
                    KayitTarihi = baseTarih25.AddDays(gonderiIdCounter - 1),
                    GuncellemeTarihi = null,
                    IptalTarihi = null,
                    IadeDurumu = null
                };
                gonderiler.Add(gonderi);
                gonderiIdCounter++;
            }
        }
        modelBuilder.Entity<Gonderi>().HasData(gonderiler.ToArray());

        // ======================================
        // 11. ADIM: GÖNDERİ DURUM GEÇMİŞİ
        // ======================================
        var gonderiDurumlar = new System.Collections.Generic.List<GonderiDurumGecmis>();
        int durumIdCounter = 1;

        foreach (var gonderi in gonderiler)
        {
            // İptal ve İade durumları hariç tüm gönderiler için durum oluştur
            var teslimatTipi = gonderi.TeslimatTipi;
            if (teslimatTipi != "İptal" && teslimatTipi != "İade")
            {
                var musteri = musteriler.First(m => m.MusteriId == gonderi.GonderenId);
                
                gonderiDurumlar.Add(new GonderiDurumGecmis
                {
                    Id = durumIdCounter++,
                    GonderiId = gonderi.GonderiId,
                    DurumAd = gonderi.GonderenId == gonderi.AliciId ? "Kargoya Verildi" : "Transferde",
                    Aciklama = gonderi.GonderenId == gonderi.AliciId 
                        ? $"{musteri.Ad} {musteri.Soyad} tarafından kargoya verildi"
                        : $"Şehirlerarası transfer işlemi",
                    Tarih = gonderi.GonderenId == gonderi.AliciId 
                        ? gonderi.GonderiTarihi 
                        : gonderi.GonderiTarihi.AddHours(6),
                    IslemTipi = gonderi.GonderenId == gonderi.AliciId ? "Hazırlık" : "Transfer",
                    SonDurumMu = true,
                    IslemSonucu = durumIdCounter % 2 == 0 ? "Başarılı" : "Başarısız",
                    TeslimatKodu = $"TSL{gonderi.GonderiId:D4}",
                    IlgiliKisiAd = gonderi.GonderenId == gonderi.AliciId 
                        ? $"{musteri.Ad} {musteri.Soyad}"
                        : $"Transfer Personeli {gonderi.GonderiId % 3}",
                    IlgiliKisiTel = gonderi.GonderenId == gonderi.AliciId 
                        ? musteri.Tel
                        : $"0555000{gonderi.GonderiId:D4}",
                    SubeId = gonderi.GonderenId == gonderi.AliciId 
                        ? 1 
                        : (adresler.First(a => a.AdresId == gonderi.AliciAdresId).IlId == 34 ? 1 : 
                           (adresler.First(a => a.AdresId == gonderi.AliciAdresId).IlId == 6 ? 2 : 3)),
                    PersonelId = gonderi.GonderenId == gonderi.AliciId 
                        ? 10 + (gonderi.GonderiId % 10)
                        : 20 + (gonderi.GonderiId % 10),
                    AracId = gonderi.GonderenId == gonderi.AliciId 
                        ? null
                        : ((gonderi.GonderiId - 1) % 18) + 1, // ✅ DÜZELTME: 1-18 arası değer döner
                    IslemBaslangicTarihi = gonderi.GonderiTarihi,
                    IslemBitisTarihi = gonderi.GonderenId == gonderi.AliciId 
                        ? gonderi.GonderiTarihi.AddHours(2)
                        : gonderi.GonderiTarihi.AddHours(12)
                });
            }
        }
        modelBuilder.Entity<GonderiDurumGecmis>().HasData(gonderiDurumlar.ToArray());

        // ======================================
        // 12. ADIM: MÜŞTERİ ADRES İLİŞKİLERİ
        // ======================================
        var musteriAdresIliskileri = new System.Collections.Generic.List<MusteriAdres>();
        int musteriAdresIdCounter = 1;

        for (int musteriId = 1; musteriId <= 8; musteriId++)
        {
            // Her müşterinin ev ve iş adreslerini bul
            var musteriAdresler = adresler.Where(a => a.MusteriId == musteriId).ToList();
            
            foreach (var adres in musteriAdresler)
            {
                musteriAdresIliskileri.Add(new MusteriAdres
                {
                    Id = musteriAdresIdCounter++,
                    MusteriId = musteriId,
                    AdresId = adres.AdresId,
                    AdresTipi = adres.AdresTipi,
                    Aktif = true
                });
            }
        }
        modelBuilder.Entity<MusteriAdres>().HasData(musteriAdresIliskileri.ToArray());
    }
}
