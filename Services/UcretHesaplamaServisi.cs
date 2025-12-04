using System;
using System.Text.RegularExpressions;

namespace kargotakipsistemi.Servisler;

/// <summary>
/// Gönderi ücretlendirme hesaplamalarýný yöneten servis sýnýfý.
/// Aðýrlýk, hacim, teslimat tipi ve ek masraf/indirim hesaplamalarýný içerir.
/// </summary>
public class UcretHesaplamaServisi
{
    #region Tarife Sabitleri

    // Aðýrlýk Tarifeleri (TL/kg)
    private const decimal TARIFE_0_1_KG = 25m;
    private const decimal TARIFE_1_5_KG = 20m;
    private const decimal TARIFE_5_20_KG = 15m;
    private const decimal TARIFE_20_UZERI = 12m;

    // Hacim Ek Ücretleri (TL)
    private const decimal HACIM_EK_0_10000 = 0m;
    private const decimal HACIM_EK_10000_30000 = 15m;
    private const decimal HACIM_EK_30000_70000 = 35m;
    private const decimal HACIM_EK_70000_UZERI = 60m;

    // Teslimat Tipi Çarpanlarý
    private const decimal CARPAN_STANDART = 1.0m;
    private const decimal CARPAN_HIZLI = 1.25m;
    private const decimal CARPAN_AYNI_GUN = 1.50m;
    private const decimal CARPAN_RANDEVULU = 1.30m;

    // Ek Masraf Eþikleri
    private const decimal AGIR_YUK_ESIK = 30m; // kg
    private const decimal AGIR_YUK_MASRAF = 40m; // TL
    private const decimal BUYUK_HACIM_ESIK = 100000m; // cm³
    private const decimal BUYUK_HACIM_MASRAF = 80m; // TL

    // Ýndirim Eþikleri ve Oranlarý
    private const decimal INDIRIM_ESIK_1 = 10m; // kg
    private const decimal INDIRIM_ORAN_1 = 0.05m; // %5
    private const decimal INDIRIM_ESIK_2 = 20m; // kg
    private const decimal INDIRIM_ORAN_2 = 0.10m; // %10

    #endregion

    #region Ana Hesaplama Metotlarý

    /// <summary>
    /// Gönderi için toplam ücreti hesaplar.
    /// </summary>
    /// <param name="agirlikKg">Aðýrlýk (kilogram)</param>
    /// <param name="boyutMetni">Boyut metni (örn: "30x20x10")</param>
    /// <param name="teslimatTipi">Teslimat tipi ("Standart", "Hýzlý", vb.)</param>
    /// <param name="mevcutIndirim">Mevcut indirim tutarý (manuel girilmiþse)</param>
    /// <param name="mevcutEkMasraf">Mevcut ek masraf (manuel girilmiþse)</param>
    /// <returns>Hesaplanan ücret detaylarý</returns>
    public UcretHesaplamaDetay UcretHesapla(
        decimal agirlikKg,
        string boyutMetni,
        string teslimatTipi,
        decimal? mevcutIndirim = null,
        decimal? mevcutEkMasraf = null)
    {
        var detay = new UcretHesaplamaDetay();

        // Aðýrlýk kontrolü
        if (agirlikKg < 0) agirlikKg = 0;
        detay.Agirlik = agirlikKg;

        // Aðýrlýk bazlý maliyet
        detay.AgirlikTarife = AgirlikTarifeHesapla(agirlikKg);
        detay.AgirlikMaliyeti = agirlikKg * detay.AgirlikTarife;

        // Hacim bazlý ek ücret
        detay.Hacim = HacimHesapla(boyutMetni);
        detay.HacimEkUcret = HacimEkUcretHesapla(detay.Hacim);

        // Teslimat tipi çarpaný
        detay.TeslimatTipi = teslimatTipi;
        detay.TeslimatCarpani = TeslimatCarpaniHesapla(teslimatTipi);

        // Ham ücret hesaplama
        detay.HamUcret = (detay.AgirlikMaliyeti + detay.HacimEkUcret) * detay.TeslimatCarpani;
        detay.HamUcret = decimal.Round(detay.HamUcret, 2);

        // Ek masraf önerisi (manuel girilmediyse)
        if (mevcutEkMasraf.HasValue && mevcutEkMasraf.Value > 0)
        {
            detay.EkMasraf = mevcutEkMasraf.Value;
            detay.EkMasrafManuelMi = true;
        }
        else
        {
            detay.EkMasraf = EkMasrafOnerisiHesapla(agirlikKg, detay.Hacim);
            detay.EkMasrafManuelMi = false;
        }

        // Ýndirim önerisi (manuel girilmediyse)
        if (mevcutIndirim.HasValue && mevcutIndirim.Value > 0)
        {
            detay.Indirim = mevcutIndirim.Value;
            detay.IndirimManuelMi = true;
        }
        else
        {
            detay.Indirim = IndirimOnerisiHesapla(agirlikKg, detay.HamUcret);
            detay.Indirim = decimal.Round(detay.Indirim, 2);
            detay.IndirimManuelMi = false;
        }

        // Toplam hesaplama
        detay.ToplamUcret = detay.HamUcret - detay.Indirim + detay.EkMasraf;
        if (detay.ToplamUcret < 0) detay.ToplamUcret = 0;

        return detay;
    }

    /// <summary>
    /// Sadece toplam fiyatý hesaplar (ücret, indirim, ek masraf belli).
    /// </summary>
    public decimal ToplamFiyatHesapla(decimal ucret, decimal indirim, decimal ekMasraf)
    {
        decimal toplam = ucret - indirim + ekMasraf;
        return toplam < 0 ? 0 : decimal.Round(toplam, 2);
    }

    #endregion

    #region Bileþen Hesaplama Metotlarý

    /// <summary>
    /// Aðýrlýða göre kg baþýna tarifesini belirler.
    /// </summary>
    private decimal AgirlikTarifeHesapla(decimal agirlikKg)
    {
        return agirlikKg switch
        {
            <= 1m => TARIFE_0_1_KG,
            <= 5m => TARIFE_1_5_KG,
            <= 20m => TARIFE_5_20_KG,
            _ => TARIFE_20_UZERI
        };
    }

    /// <summary>
    /// Hacme göre ek ücreti hesaplar.
    /// </summary>
    private decimal HacimEkUcretHesapla(decimal? hacim)
    {
        if (!hacim.HasValue) return 0m;

        return hacim.Value switch
        {
            <= 10000m => HACIM_EK_0_10000,
            <= 30000m => HACIM_EK_10000_30000,
            <= 70000m => HACIM_EK_30000_70000,
            _ => HACIM_EK_70000_UZERI
        };
    }

    /// <summary>
    /// Teslimat tipine göre çarpaný belirler.
    /// </summary>
    private decimal TeslimatCarpaniHesapla(string teslimatTipi)
    {
        if (string.IsNullOrWhiteSpace(teslimatTipi))
            return CARPAN_STANDART;

        return teslimatTipi switch
        {
            "Standart" => CARPAN_STANDART,
            "Hýzlý" => CARPAN_HIZLI,
            "Ayný Gün" => CARPAN_AYNI_GUN,
            "Randevulu" => CARPAN_RANDEVULU,
            _ => CARPAN_STANDART
        };
    }

    /// <summary>
    /// Aðýrlýk ve hacme göre ek masraf önerisi hesaplar.
    /// </summary>
    private decimal EkMasrafOnerisiHesapla(decimal agirlikKg, decimal? hacim)
    {
        decimal onerilen = 0m;

        // Aðýr yük kontrolü
        if (agirlikKg > AGIR_YUK_ESIK)
            onerilen += AGIR_YUK_MASRAF;

        // Büyük hacim kontrolü
        if (hacim.HasValue && hacim.Value > BUYUK_HACIM_ESIK)
            onerilen += BUYUK_HACIM_MASRAF;

        return onerilen;
    }

    /// <summary>
    /// Aðýrlýða göre indirim önerisi hesaplar.
    /// </summary>
    private decimal IndirimOnerisiHesapla(decimal agirlikKg, decimal hamUcret)
    {
        if (agirlikKg > INDIRIM_ESIK_2)
            return hamUcret * INDIRIM_ORAN_2; // %10

        if (agirlikKg > INDIRIM_ESIK_1)
            return hamUcret * INDIRIM_ORAN_1; // %5

        return 0m;
    }

    /// <summary>
    /// Boyut metninden hacim hesaplar (cm³).
    /// Format: "30x20x10" veya "30.5X20X10"
    /// </summary>
    public decimal? HacimHesapla(string metin)
    {
        if (string.IsNullOrWhiteSpace(metin))
            return null;

        // Regex: üç sayýyý x veya X ile ayrýlmýþ þekilde yakalar
        var rx = new Regex(@"^\s*(\d+(?:[.,]\d+)?)\s*[xX]\s*(\d+(?:[.,]\d+)?)\s*[xX]\s*(\d+(?:[.,]\d+)?)\s*$");
        var m = rx.Match(metin.Trim());

        if (!m.Success)
            return null;

        // Virgül/nokta farkýný handle ederek parse et
        if (TryParseDecimal(m.Groups[1].Value, out var uzunluk) &&
            TryParseDecimal(m.Groups[2].Value, out var genislik) &&
            TryParseDecimal(m.Groups[3].Value, out var yukseklik))
        {
            if (uzunluk <= 0 || genislik <= 0 || yukseklik <= 0)
                return null;

            return uzunluk * genislik * yukseklik; // cm³
        }

        return null;
    }

    /// <summary>
    /// Decimal parse yapar (virgül/nokta toleranslý).
    /// </summary>
    private bool TryParseDecimal(string value, out decimal result)
    {
        string normalized = value.Replace(',', '.');
        return decimal.TryParse(
            normalized,
            System.Globalization.NumberStyles.Number,
            System.Globalization.CultureInfo.InvariantCulture,
            out result);
    }

    #endregion

    #region Tarife Bilgileri (Raporlama/UI için)

    /// <summary>
    /// Tüm tarife bilgilerini döndürür (UI'da gösterim için).
    /// </summary>
    public TarifeBilgileri TarifeBilgileriniGetir()
    {
        return new TarifeBilgileri
        {
            AgirlikTarifeleri = new[]
            {
                new TarifeDetay { MinDeger = 0m, MaxDeger = 1m, Ucret = TARIFE_0_1_KG, Birim = "TL/kg" },
                new TarifeDetay { MinDeger = 1.01m, MaxDeger = 5m, Ucret = TARIFE_1_5_KG, Birim = "TL/kg" },
                new TarifeDetay { MinDeger = 5.01m, MaxDeger = 20m, Ucret = TARIFE_5_20_KG, Birim = "TL/kg" },
                new TarifeDetay { MinDeger = 20.01m, MaxDeger = null, Ucret = TARIFE_20_UZERI, Birim = "TL/kg" }
            },
            HacimEkUcretleri = new[]
            {
                new TarifeDetay { MinDeger = 0m, MaxDeger = 10000m, Ucret = HACIM_EK_0_10000, Birim = "TL" },
                new TarifeDetay { MinDeger = 10001m, MaxDeger = 30000m, Ucret = HACIM_EK_10000_30000, Birim = "TL" },
                new TarifeDetay { MinDeger = 30001m, MaxDeger = 70000m, Ucret = HACIM_EK_30000_70000, Birim = "TL" },
                new TarifeDetay { MinDeger = 70001m, MaxDeger = null, Ucret = HACIM_EK_70000_UZERI, Birim = "TL" }
            },
            TeslimatCarpanlari = new[]
            {
                new TeslimatTarifeDetay { TeslimatTipi = "Standart", Carpan = CARPAN_STANDART },
                new TeslimatTarifeDetay { TeslimatTipi = "Hýzlý", Carpan = CARPAN_HIZLI },
                new TeslimatTarifeDetay { TeslimatTipi = "Ayný Gün", Carpan = CARPAN_AYNI_GUN },
                new TeslimatTarifeDetay { TeslimatTipi = "Randevulu", Carpan = CARPAN_RANDEVULU }
            }
        };
    }

    #endregion
}

#region Model Sýnýflarý

/// <summary>
/// Ücret hesaplama sonuç detaylarý.
/// </summary>
public class UcretHesaplamaDetay
{
    public decimal Agirlik { get; set; }
    public decimal AgirlikTarife { get; set; }
    public decimal AgirlikMaliyeti { get; set; }

    public decimal? Hacim { get; set; }
    public decimal HacimEkUcret { get; set; }

    public string TeslimatTipi { get; set; } = "Standart";
    public decimal TeslimatCarpani { get; set; }

    public decimal HamUcret { get; set; }
    public decimal EkMasraf { get; set; }
    public bool EkMasrafManuelMi { get; set; }

    public decimal Indirim { get; set; }
    public bool IndirimManuelMi { get; set; }

    public decimal ToplamUcret { get; set; }

    /// <summary>
    /// Hesaplama özeti (logging/debug için).
    /// </summary>
    public override string ToString()
    {
        return $"Aðýrlýk: {Agirlik}kg x {AgirlikTarife} TL/kg = {AgirlikMaliyeti} TL\n" +
               $"Hacim Ek: {HacimEkUcret} TL\n" +
               $"Teslimat ({TeslimatTipi}): x{TeslimatCarpani}\n" +
               $"Ham Ücret: {HamUcret} TL\n" +
               $"Ek Masraf: {EkMasraf} TL {(EkMasrafManuelMi ? "(Manuel)" : "(Otomatik)")}\n" +
               $"Ýndirim: {Indirim} TL {(IndirimManuelMi ? "(Manuel)" : "(Otomatik)")}\n" +
               $"TOPLAM: {ToplamUcret} TL";
    }
}

/// <summary>
/// Tüm tarife bilgileri (raporlama/UI için).
/// </summary>
public class TarifeBilgileri
{
    public TarifeDetay[] AgirlikTarifeleri { get; set; }
    public TarifeDetay[] HacimEkUcretleri { get; set; }
    public TeslimatTarifeDetay[] TeslimatCarpanlari { get; set; }
}

/// <summary>
/// Tek bir tarife detayý.
/// </summary>
public class TarifeDetay
{
    public decimal MinDeger { get; set; }
    public decimal? MaxDeger { get; set; } // null = sýnýrsýz
    public decimal Ucret { get; set; }
    public string Birim { get; set; }

    public string AralikMetni =>
        MaxDeger.HasValue
            ? $"{MinDeger}-{MaxDeger} ? {Ucret} {Birim}"
            : $"{MinDeger}+ ? {Ucret} {Birim}";
}

/// <summary>
/// Teslimat tipi tarife detayý.
/// </summary>
public class TeslimatTarifeDetay
{
    public string TeslimatTipi { get; set; }
    public decimal Carpan { get; set; }

    public string Metin => $"{TeslimatTipi}: x{Carpan}";
}

#endregion
