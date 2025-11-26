namespace kargotakipsistemi.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Personel
    {
        public int PersonelId { get; set; }
        [Required, MaxLength(50)]
        public string Ad { get; set; }
        [Required, MaxLength(50)]
        public string Soyad { get; set; }
        [Required, MaxLength(100)]
        public string Mail { get; set; }
        [Required, MaxLength(128)]
        public string Sifre { get; set; }
        [Required, MaxLength(15)]
        public string Tel { get; set; }
        public DateTime? DogumTarihi { get; set; }
        [MaxLength(10)]
        public string Cinsiyet { get; set; }
        public int RolId { get; set; }
        public int SubeId { get; set; }
        public int? AracId { get; set; }
        public bool Aktif { get; set; } = true;
        public DateTime? IseGirisTarihi { get; set; }
        public DateTime? IstenCikisTarihi { get; set; }
        public decimal? Maas { get; set; }
        [MaxLength(5)]
        public string EhliyetSinifi { get; set; }
        public Rol Rol { get; set; }
        public Sube Sube { get; set; }
        public Arac Arac { get; set; }
        public ICollection<Adres> Adresler { get; set; }
        public ICollection<GonderiDurumGecmis> GonderiDurumGecmisi { get; set; }
        public ICollection<MusteriDestek> MusteriDestekleri { get; set; }
        public ICollection<IadeIptalIslem> IadeIptalIslemleri { get; set; }
    }
}