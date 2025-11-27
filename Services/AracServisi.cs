using System;
using System.Linq;
using kargotakipsistemi.Entities;
using Microsoft.EntityFrameworkCore;

namespace kargotakipsistemi.Servisler
{
    public class AracServisi
    {
        private static readonly Random _rnd = new();
        private static readonly char[] _alfabe = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public Arac? KimlikleGetir(KtsContext ctx, int id) => ctx.Araclar.Find(id);

        public Arac Olustur(KtsContext ctx)
        {
            var a = new Arac();
            ctx.Araclar.Add(a);
            return a;
        }

        public void Sil(KtsContext ctx, int id)
        {
            var a = ctx.Araclar.Find(id);
            if (a != null)
            {
                ctx.Araclar.Remove(a);
            }
        }

        public IQueryable<object> IzgaraIcinProjeksiyon(KtsContext ctx)
        {
            return ctx.Araclar
                .Include(a => a.Sube)
                .Select(a => new
                {
                    a.AracId,
                    a.Plaka,
                    a.AracTip,
                    a.KapasiteKg,
                    a.GpsKodu,
                    a.Durum,
                    a.SubeId,
                    SubeAd = a.Sube != null ? a.Sube.SubeAd : ""
                });
        }
    }
}
