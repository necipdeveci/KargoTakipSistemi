using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;

namespace kargotakipsistemi.Servisler
{
    public class GonderiServisi
    {
        public Gonderi? KimlikleGetir(KtsContext ctx, int id)
        {
            return ctx.Gonderiler
                .Include(g => g.Gonderen)
                .Include(g => g.Alici)
                .Include(g => g.GonderenAdres)
                .Include(g => g.AliciAdres)
                .Include(g => g.Kurye)
                .FirstOrDefault(g => g.GonderiId == id);
        }

        public Gonderi Olustur(KtsContext ctx)
        {
            var g = new Gonderi
            {
                KayitTarihi = DateTime.Now,
                GonderiTarihi = DateTime.Now,
                TahminiTeslimTarihi = DateTime.Now.AddDays(2)
            };
            ctx.Gonderiler.Add(g);
            return g;
        }

        public void Sil(KtsContext ctx, int id)
        {
            var g = ctx.Gonderiler.Find(id);
            if (g != null)
                ctx.Gonderiler.Remove(g);
        }

        public IQueryable<object> IzgaraIcinProjeksiyon(KtsContext ctx)
        {
            return ctx.Gonderiler
                .Include(g => g.Gonderen)
                .Include(g => g.Alici)
                .Include(g => g.Kurye)
                .Select(g => new
                {
                    g.GonderiId,
                    g.TakipNo,
                    GonderenAd = g.Gonderen != null ? g.Gonderen.Ad + " " + g.Gonderen.Soyad : string.Empty,
                    AliciAd = g.Alici != null ? g.Alici.Ad + " " + g.Alici.Soyad : string.Empty,
                    KuryeAd = g.Kurye != null ? g.Kurye.Ad + " " + g.Kurye.Soyad : string.Empty,
                    g.GonderiTarihi,
                    g.TahminiTeslimTarihi,
                    g.TeslimTarihi,
                    g.TeslimatTipi,
                    g.Agirlik,
                    g.Boyut,
                    g.Ucret,
                    g.IndirimTutar,
                    g.EkMasraf
                });
        }

        public string YeniTakipNoUret(KtsContext ctx)
        {
            // Basit benzersiz üretim: tarih + rastgele 4 karakter
            string baseNo;
            var rnd = new Random();
            string chars() => new string(Enumerable.Range(0, 4).Select(_ => (char)('A' + rnd.Next(0, 26))).ToArray());
            do
            {
                baseNo = $"KTS{DateTime.UtcNow:yyyyMMddHHmmss}{chars()}";
            } while (ctx.Gonderiler.Any(g => g.TakipNo == baseNo));
            return baseNo;
        }
    }
}
