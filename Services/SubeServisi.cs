using System.Linq;
using kargotakipsistemi.Entities;
using Microsoft.EntityFrameworkCore;

namespace kargotakipsistemi.Servisler
{
    public class SubeServisi
    {
        public Sube? KimlikleGetir(KtsContext ctx, int id) => ctx.Subeler.Find(id);

        public Sube Olustur(KtsContext ctx)
        {
            var s = new Sube();
            ctx.Subeler.Add(s);
            return s;
        }

        public void Sil(KtsContext ctx, int id)
        {
            var s = ctx.Subeler.Find(id);
            if (s != null)
            {
                ctx.Subeler.Remove(s);
            }
        }

        public IQueryable<object> IzgaraIcinProjeksiyon(KtsContext ctx)
        {
            return ctx.Subeler
                .Include(s => s.Araclar)
                .Select(s => new
                {
                    s.SubeId,
                    s.SubeAd,
                    s.SubeTip,
                    s.IlId,
                    s.IlceId,
                    s.AcikAdres,
                    s.Tel,
                    s.Email,
                    s.CalismaSaatleri,
                    s.Kapasite,
                    Araclar = s.Araclar != null && s.Araclar.Any()
                        ? string.Join(", ", s.Araclar.Select(a => a.AracTip))
                        : ""
                });
        }
    }
}
