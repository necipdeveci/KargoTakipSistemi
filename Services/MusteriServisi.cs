using System.Linq;
using kargotakipsistemi.Entities;
using Microsoft.EntityFrameworkCore;

namespace kargotakipsistemi.Servisler
{
    public class MusteriServisi
    {
        public Musteri? KimlikleGetir(KtsContext ctx, int id) => ctx.Musteriler.Find(id);

        public Musteri Olustur(KtsContext ctx)
        {
            var m = new Musteri();
            ctx.Musteriler.Add(m);
            return m;
        }

        public void Sil(KtsContext ctx, int id)
        {
            var m = ctx.Musteriler.Find(id);
            if (m != null)
            {
                ctx.Musteriler.Remove(m);
            }
        }

        public IQueryable<object> IzgaraIcinProjeksiyon(KtsContext ctx)
        {
            return ctx.Musteriler
                .Include(m => m.Adresler)
                .Select(m => new
                {
                    m.MusteriId,
                    m.Ad,
                    m.Soyad,
                    m.Mail,
                    m.Tel,
                    m.DogumTarihi,
                    m.Notlar,
                    Adresler = m.Adresler != null && m.Adresler.Any()
                        ? string.Join(", ", m.Adresler.Select(a => a.AdresBaslik))
                        : ""
                });
        }
    }
}
