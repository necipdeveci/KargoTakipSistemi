using System;
using System.Linq;
using kargotakipsistemi.Entities;
using Microsoft.EntityFrameworkCore;

namespace kargotakipsistemi.Servisler
{
    public class PersonelServisi
    {
        public Personel? KimlikleGetir(KtsContext ctx, int id) => ctx.Personeller.Find(id);

        public Personel Olustur(KtsContext ctx, string sifre)
        {
            var p = new Personel { Sifre = sifre };
            ctx.Personeller.Add(p);
            return p;
        }

        public void Sil(KtsContext ctx, int id)
        {
            var p = ctx.Personeller.Find(id);
            if (p != null)
            {
                ctx.Personeller.Remove(p);
            }
        }

        public IQueryable<object> IzgaraIcinProjeksiyon(KtsContext ctx)
        {
            return ctx.Personeller
                .Include(p => p.Rol)
                .Include(p => p.Sube)
                .Include(p => p.Arac)
                .Include(p => p.Adresler)
                .Select(p => new
                {
                    p.PersonelId,
                    p.Ad,
                    p.Soyad,
                    p.Mail,
                    Sifre = p.Sifre.Length > 4 ? new string('*', p.Sifre.Length) : p.Sifre,
                    p.Tel,
                    p.DogumTarihi,
                    p.Cinsiyet,
                    p.RolId,
                    RolAd = p.Rol != null ? p.Rol.RolAd : "",
                    p.SubeId,
                    SubeAd = p.Sube != null ? p.Sube.SubeAd : "",
                    p.AracId,
                    AracTip = p.Arac != null ? p.Arac.AracTip : "",
                    p.Aktif,
                    p.IseGirisTarihi,
                    p.IstenCikisTarihi,
                    p.Maas,
                    p.EhliyetSinifi,
                    Adresler = p.Adresler != null && p.Adresler.Any()
                        ? string.Join(", ", p.Adresler.Select(a => a.AdresBaslik))
                        : ""
                });
        }
    }
}
