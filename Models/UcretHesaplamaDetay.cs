using System;

namespace kargotakipsistemi.Models;

/// <summary>
/// Ücret hesaplama sonuç detaylarý.
/// Hem UcretHesaplamaServisi hem DinamikUcretHesaplamaServisi tarafýndan kullanýlýr.
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
