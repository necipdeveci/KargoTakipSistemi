namespace kargotakipsistemi.Entities
{
    using System.ComponentModel.DataAnnotations;
    public class Adres
    {
        public int AdresId { get; set; }
        [Required, MaxLength(50)]
        public string AdresBaslik { get; set; }
        [Required, MaxLength(255)]
        public string AcikAdres { get; set; }
        public int IlId { get; set; }
        public int IlceId { get; set; }
        public int MahalleId { get; set; }
        [Required, MaxLength(10)]
        public string PostaKodu { get; set; }
        [MaxLength(30)]
        public string AdresTipi { get; set; }
        public int? MusteriId { get; set; }
        public int? PersonelId { get; set; }
        public bool Aktif { get; set; } = true;
        [MaxLength(10)]
        public string KapiNo { get; set; }
        [MaxLength(50)]
        public string BinaAdi { get; set; }
        [MaxLength(10)]
        public string Kat { get; set; }
        [MaxLength(10)]
        public string Daire { get; set; }
        [MaxLength(255)]
        public string EkAciklama { get; set; }
        public Il Il { get; set; }
        public Ilce Ilce { get; set; }
        public Mahalle Mahalle { get; set; }
        public Musteri Musteri { get; set; }
        public Personel Personel { get; set; }
    }
}