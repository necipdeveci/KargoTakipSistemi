using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using kargotakipsistemi.Models;

namespace kargotakipsistemi.Servisler;

/// <summary>
/// Veritabaný tabanlý dinamik gönderi ücretlendirme servisi.
/// Tüm tarife bilgileri FiyatlandirmaTarifeler tablosundan çekilir.
/// </summary>
public class DinamikUcretHesaplamaServisi
{
    private readonly KtsContext _context;

    public DinamikUcretHesaplamaServisi(KtsContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Parametresiz constructor - kendi context'ini oluþturur
    /// </summary>
    public DinamikUcretHesaplamaServisi() : this(new KtsContext())
    {
    }

    #region Ana Hesaplama Metotlarý

    /// <summary>
    /// Gönderi için toplam ücreti veritabanýndaki tarifelerden hesaplar.
    /// </summary>
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

        // 1) Aðýrlýk bazlý tarife
        detay.AgirlikTarife = AgirlikTarifeGetir(agirlikKg);
        detay.AgirlikMaliyeti = agirlikKg * detay.AgirlikTarife;

        // 2) Hacim bazlý ek ücret
        detay.Hacim = HacimHesapla(boyutMetni);
        detay.HacimEkUcret = HacimEkUcretGetir(detay.Hacim);

        // 3) Teslimat tipi çarpaný
        detay.TeslimatTipi = teslimatTipi;
        detay.TeslimatCarpani = TeslimatCarpaniGetir(teslimatTipi);

        // 4) Ham ücret hesaplama
        detay.HamUcret = (detay.AgirlikMaliyeti + detay.HacimEkUcret) * detay.TeslimatCarpani;
        detay.HamUcret = decimal.Round(detay.HamUcret, 2);

        // 5) Ek masraf (manuel veya otomatik)
        if (mevcutEkMasraf.HasValue && mevcutEkMasraf.Value > 0)
        {
            detay.EkMasraf = mevcutEkMasraf.Value;
            detay.EkMasrafManuelMi = true;
        }
        else
        {
            detay.EkMasraf = EkMasrafHesapla(agirlikKg, detay.Hacim);
            detay.EkMasrafManuelMi = false;
        }

        // 6) Ýndirim (manuel veya otomatik)
        if (mevcutIndirim.HasValue && mevcutIndirim.Value > 0)
        {
            detay.Indirim = mevcutIndirim.Value;
            detay.IndirimManuelMi = true;
        }
        else
        {
            detay.Indirim = IndirimHesapla(agirlikKg, detay.HamUcret);
            detay.Indirim = decimal.Round(detay.Indirim, 2);
            detay.IndirimManuelMi = false;
        }

        // 7) Toplam
        detay.ToplamUcret = detay.HamUcret - detay.Indirim + detay.EkMasraf;
        if (detay.ToplamUcret < 0) detay.ToplamUcret = 0;

        return detay;
    }

    /// <summary>
    /// Basit toplam hesaplama
    /// </summary>
    public decimal ToplamFiyatHesapla(decimal ucret, decimal indirim, decimal ekMasraf)
    {
        decimal toplam = ucret - indirim + ekMasraf;
        return toplam < 0 ? 0 : decimal.Round(toplam, 2);
    }

    #endregion

    #region Veritabanýndan Tarife Çekme Metotlarý

    /// <summary>
    /// Aðýrlýða göre kg baþýna tarifeyi veritabanýndan getirir
    /// </summary>
    private decimal AgirlikTarifeGetir(decimal agirlikKg)
    {
        var tarife = _context.FiyatlandirmaTarifeler
            .Where(t => t.Aktif 
                     && t.TarifeTuru == "AgirlikTarife"
                     && t.GecerlilikBaslangic <= DateTime.Now
                     && (t.GecerlilikBitis == null || t.GecerlilikBitis >= DateTime.Now)
                     && (t.MinDeger == null || agirlikKg >= t.MinDeger)
                     && (t.MaxDeger == null || agirlikKg < t.MaxDeger))
            .OrderBy(t => t.Oncelik)
            .ThenBy(t => t.MinDeger)
            .FirstOrDefault();

        return tarife?.Deger ?? 15m; // Varsayýlan: 15 TL/kg
    }

    /// <summary>
    /// Hacme göre ek ücreti veritabanýndan getirir
    /// </summary>
    private decimal HacimEkUcretGetir(decimal? hacim)
    {
        if (!hacim.HasValue) return 0m;

        var tarife = _context.FiyatlandirmaTarifeler
            .Where(t => t.Aktif 
                     && t.TarifeTuru == "HacimEkUcret"
                     && t.GecerlilikBaslangic <= DateTime.Now
                     && (t.GecerlilikBitis == null || t.GecerlilikBitis >= DateTime.Now)
                     && (t.MinDeger == null || hacim >= t.MinDeger)
                     && (t.MaxDeger == null || hacim < t.MaxDeger))
            .OrderBy(t => t.Oncelik)
            .ThenBy(t => t.MinDeger)
            .FirstOrDefault();

        return tarife?.Deger ?? 0m;
    }

    /// <summary>
    /// Teslimat tipine göre çarpaný veritabanýndan getirir
    /// </summary>
    private decimal TeslimatCarpaniGetir(string teslimatTipi)
    {
        if (string.IsNullOrWhiteSpace(teslimatTipi))
            teslimatTipi = "Standart";

        var tarife = _context.FiyatlandirmaTarifeler
            .Where(t => t.Aktif 
                     && t.TarifeTuru == "TeslimatCarpan"
                     && t.GecerlilikBaslangic <= DateTime.Now
                     && (t.GecerlilikBitis == null || t.GecerlilikBitis >= DateTime.Now)
                     && t.TarifeAdi == teslimatTipi)
            .OrderBy(t => t.Oncelik)
            .FirstOrDefault();

        return tarife?.Deger ?? 1.0m; // Varsayýlan: 1.0 (çarpan yok)
    }

    /// <summary>
    /// Aðýrlýk ve hacme göre ek masraflarý hesaplar
    /// </summary>
    private decimal EkMasrafHesapla(decimal agirlikKg, decimal? hacim)
    {
        decimal toplam = 0m;

        // Aðýr yük ek masrafý
        var agirYukTarife = _context.FiyatlandirmaTarifeler
            .Where(t => t.Aktif 
                     && t.TarifeTuru == "EkMasrafEsik"
                     && t.TarifeAdi.Contains("Aðýr")
                     && t.GecerlilikBaslangic <= DateTime.Now
                     && (t.GecerlilikBitis == null || t.GecerlilikBitis >= DateTime.Now)
                     && (t.MinDeger == null || agirlikKg > t.MinDeger))
            .OrderBy(t => t.Oncelik)
            .FirstOrDefault();

        if (agirYukTarife != null)
            toplam += agirYukTarife.Deger;

        // Büyük hacim ek masrafý
        if (hacim.HasValue)
        {
            var buyukHacimTarife = _context.FiyatlandirmaTarifeler
                .Where(t => t.Aktif 
                         && t.TarifeTuru == "EkMasrafEsik"
                         && t.TarifeAdi.Contains("Hacim")
                         && t.GecerlilikBaslangic <= DateTime.Now
                         && (t.GecerlilikBitis == null || t.GecerlilikBitis >= DateTime.Now)
                         && (t.MinDeger == null || hacim > t.MinDeger))
                .OrderBy(t => t.Oncelik)
                .FirstOrDefault();

            if (buyukHacimTarife != null)
                toplam += buyukHacimTarife.Deger;
        }

        return toplam;
    }

    /// <summary>
    /// Aðýrlýða göre indirim hesaplar
    /// </summary>
    private decimal IndirimHesapla(decimal agirlikKg, decimal hamUcret)
    {
        var indirimTarife = _context.FiyatlandirmaTarifeler
            .Where(t => t.Aktif 
                     && t.TarifeTuru == "IndirimEsik"
                     && t.GecerlilikBaslangic <= DateTime.Now
                     && (t.GecerlilikBitis == null || t.GecerlilikBitis >= DateTime.Now)
                     && (t.MinDeger == null || agirlikKg > t.MinDeger))
            .OrderByDescending(t => t.MinDeger) // En yüksek eþik önce
            .ThenBy(t => t.Oncelik)
            .FirstOrDefault();

        if (indirimTarife != null)
            return hamUcret * indirimTarife.Deger; // Deger: 0.10 = %10 indirim

        return 0m;
    }

    #endregion

    #region Hacim Hesaplama

    /// <summary>
    /// Boyut metninden hacim hesaplar (cm³).
    /// Format: "30x20x10" veya "30.5X20X10"
    /// </summary>
    public decimal? HacimHesapla(string metin)
    {
        if (string.IsNullOrWhiteSpace(metin))
            return null;

        var rx = new Regex(@"^\s*(\d+(?:[.,]\d+)?)\s*[xX]\s*(\d+(?:[.,]\d+)?)\s*[xX]\s*(\d+(?:[.,]\d+)?)\s*$");
        var m = rx.Match(metin.Trim());

        if (!m.Success)
            return null;

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

    #region Yardýmcý Metodlar

    /// <summary>
    /// Tüm aktif tarifeleri getirir (admin paneli için)
    /// </summary>
    public IQueryable<FiyatlandirmaTarife> TumTarifeleriGetir()
    {
        return _context.FiyatlandirmaTarifeler
            .Where(t => t.Aktif)
            .OrderBy(t => t.TarifeTuru)
            .ThenBy(t => t.Oncelik)
            .ThenBy(t => t.MinDeger);
    }

    /// <summary>
    /// Belirli bir türdeki tarifeleri getirir
    /// </summary>
    public IQueryable<FiyatlandirmaTarife> TarifeGetir(string tarifeTuru)
    {
        return _context.FiyatlandirmaTarifeler
            .Where(t => t.Aktif && t.TarifeTuru == tarifeTuru)
            .OrderBy(t => t.Oncelik)
            .ThenBy(t => t.MinDeger);
    }

    #endregion
}
