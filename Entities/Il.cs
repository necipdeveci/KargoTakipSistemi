namespace kargotakipsistemi.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Il
    {
        public int IlId { get; set; }
        [Required, MaxLength(50)]
        public string IlAd { get; set; }
        public string IlIdVeAd => $"{IlId} - {IlAd}";
        public ICollection<Ilce> Ilceler { get; set; }
        public ICollection<Adres> Adresler { get; set; }
        public ICollection<Sube> Subeler { get; set; }
    }
}