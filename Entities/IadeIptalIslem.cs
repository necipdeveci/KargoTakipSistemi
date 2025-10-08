namespace kargotakipsistemi.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class IadeIptalIslem
    {
        public int Id { get; set; }
        public int GonderiId { get; set; }
        public int MusteriId { get; set; }
        [Required, MaxLength(10)]
        public string IslemTipi { get; set; }
        public decimal? Tutar { get; set; }
        public DateTime Tarih { get; set; }
        [MaxLength(100)]
        public string Neden { get; set; }
        [MaxLength(20)]
        public string IadeTipi { get; set; }
        [MaxLength(30)]
        public string IslemDurumu { get; set; }
        [MaxLength(30)]
        public string IslemSonucu { get; set; }
        [MaxLength(255)]
        public string Aciklama { get; set; }
        [MaxLength(255)]
        public string RedNedeni { get; set; }
        [MaxLength(255)]
        public string DosyaLinki { get; set; }
        public int? SubeId { get; set; }
        public int? PersonelId { get; set; }
        public int? IslemBaslatanId { get; set; }
        public int? IslemOnaylayanId { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
        public DateTime? IslemBaslangicTarihi { get; set; }
        public DateTime? IslemBitisTarihi { get; set; }
        public Gonderi Gonderi { get; set; }
        public Musteri Musteri { get; set; }
        public Sube Sube { get; set; }
        public Personel Personel { get; set; }
    }
}