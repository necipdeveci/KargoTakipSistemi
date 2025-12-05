using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kargotakipsistemi.Entities;

/// <summary>
/// Dinamik fiyatlandýrma tarifeleri tablosu.
/// Aðýrlýk, hacim, teslimat tipi gibi farklý tarife türlerini saklar.
/// </summary>
[Table("FiyatlandirmaTarifeler")]
public class FiyatlandirmaTarife
{
    [Key]
    public int TarifeId { get; set; }

    /// <summary>
    /// Tarife türü: "AgirlikTarife", "HacimEkUcret", "TeslimatCarpan", "EkMasrafEsik", "IndirimEsik"
    /// </summary>
    [Required, MaxLength(50)]
    public string TarifeTuru { get; set; } = string.Empty;

    /// <summary>
    /// Tarife adý/açýklamasý (örn: "0-1 kg Arasý", "Hýzlý Teslimat")
    /// </summary>
    [Required, MaxLength(100)]
    public string TarifeAdi { get; set; } = string.Empty;

    /// <summary>
    /// Minimum deðer (kg, cm³, vb.). Null ise alt sýnýr yok.
    /// </summary>
    public decimal? MinDeger { get; set; }

    /// <summary>
    /// Maksimum deðer (kg, cm³, vb.). Null ise üst sýnýr yok (sonsuz).
    /// </summary>
    public decimal? MaxDeger { get; set; }

    /// <summary>
    /// Ücret/Çarpan/Oran deðeri.
    /// - AgirlikTarife için: TL/kg birim fiyat
    /// - HacimEkUcret için: TL ek ücret
    /// - TeslimatCarpan için: Çarpan (1.25 = %25 artýþ)
    /// - IndirimEsik için: Ýndirim oraný (0.10 = %10)
    /// </summary>
    [Column(TypeName = "decimal(18,4)")]
    public decimal Deger { get; set; }

    /// <summary>
    /// Birim/Açýklama (örn: "TL/kg", "TL", "çarpan", "%")
    /// </summary>
    [MaxLength(20)]
    public string? Birim { get; set; }

    /// <summary>
    /// Teslimat Tipi (TeslimatCarpan tarife türü için kullanýlýr)
    /// Örn: "Standart Teslimat", "Hýzlý Teslimat", "Ayný Gün", "Randevulu"
    /// Diðer tarife türleri için null olabilir
    /// </summary>
    [MaxLength(50)]
    public string? TeslimatTipi { get; set; }

    /// <summary>
    /// Tarife aktif mi? Geçersiz tarifeleri silmeden pasif yapabiliriz.
    /// </summary>
    public bool Aktif { get; set; } = true;

    /// <summary>
    /// Geçerlilik baþlangýç tarihi
    /// </summary>
    public DateTime GecerlilikBaslangic { get; set; } = DateTime.Now;

    /// <summary>
    /// Geçerlilik bitiþ tarihi (null ise süresiz geçerli)
    /// </summary>
    public DateTime? GecerlilikBitis { get; set; }

    /// <summary>
    /// Sýralama önceliði (ayný tür içinde hangi tarife önce kontrol edilecek)
    /// </summary>
    public int Oncelik { get; set; } = 0;

    /// <summary>
    /// Ek notlar
    /// </summary>
    [MaxLength(500)]
    public string? Aciklama { get; set; }

    /// <summary>
    /// Kayýt oluþturulma tarihi
    /// </summary>
    public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

    /// <summary>
    /// Son güncellenme tarihi
    /// </summary>
    public DateTime? GuncellemeTarihi { get; set; }
}
