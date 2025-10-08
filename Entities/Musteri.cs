namespace kargotakipsistemi.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Musteri
    {
        public int MusteriId { get; set; }
        [Required, MaxLength(50)]
        public string Ad { get; set; }
        [Required, MaxLength(50)]
        public string Soyad { get; set; }
        [Required, MaxLength(100)]
        public string Mail { get; set; }
        [Required, MaxLength(15)]
        public string Tel { get; set; }
        public DateTime? DogumTarihi { get; set; }
        [MaxLength(255)]
        public string Notlar { get; set; }
        public ICollection<Adres> Adresler { get; set; }
        public ICollection<MusteriAdres> MusteriAdresleri { get; set; }
        public ICollection<Gonderi> GonderilerGonderen { get; set; }
        public ICollection<Gonderi> GonderilerAlici { get; set; }
        public ICollection<MusteriDestek> MusteriDestekleriGonderen { get; set; }
        public ICollection<MusteriDestek> MusteriDestekleriAlici { get; set; }
        public ICollection<OdemeFatura> OdemeFaturalari { get; set; }
        public ICollection<IadeIptalIslem> IadeIptalIslemleri { get; set; }
    }
}