using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using kargotakipsistemi.Data;

namespace kargotakipsistemi
{
    public class KtsContext : DbContext
    {
        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Mahalle> Mahalleler { get; set; }
        public DbSet<Adres> Adresler { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Sube> Subeler { get; set; }
        public DbSet<Arac> Araclar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<MusteriAdres> MusteriAdresleri { get; set; }
        public DbSet<Gonderi> Gonderiler { get; set; }
        public DbSet<GonderiDurumGecmis> GonderiDurumGecmisi { get; set; }
        public DbSet<MusteriDestek> MusteriDestekleri { get; set; }
        public DbSet<OdemeFatura> OdemeFaturalari { get; set; }
        public DbSet<IadeIptalIslem> IadeIptalIslemleri { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<FiyatlandirmaTarife> FiyatlandirmaTarifeler { get; set; } // YENİ EKLENDI

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ktsdb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ilce - Il (Cascade)
            modelBuilder.Entity<Ilce>()
                .HasOne(i => i.Il)
                .WithMany(il => il.Ilceler)
                .HasForeignKey(i => i.IlId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mahalle - Ilce (Cascade)
            modelBuilder.Entity<Mahalle>()
                .HasOne(m => m.Ilce)
                .WithMany(ilce => ilce.Mahalleler)
                .HasForeignKey(m => m.IlceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Adres - Il (Restrict)
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Il)
                .WithMany(il => il.Adresler)
                .HasForeignKey(a => a.IlId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adres - Ilce (Restrict)
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Ilce)
                .WithMany(ilce => ilce.Adresler)
                .HasForeignKey(a => a.IlceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adres - Mahalle (Restrict)
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Mahalle)
                .WithMany(m => m.Adresler)
                .HasForeignKey(a => a.MahalleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adres - Musteri (Cascade - customer silinince adresleri sil)
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Musteri)
                .WithMany(m => m.Adresler)
                .HasForeignKey(a => a.MusteriId)
                .OnDelete(DeleteBehavior.Cascade);

            // Adres - Personel (Cascade - personel silinince adresleri sil)
            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Personel)
                .WithMany(p => p.Adresler)
                .HasForeignKey(a => a.PersonelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Sube - Il (Restrict)
            modelBuilder.Entity<Sube>()
                .HasOne(s => s.Il)
                .WithMany(il => il.Subeler)
                .HasForeignKey(s => s.IlId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sube - Ilce (Restrict)
            modelBuilder.Entity<Sube>()
                .HasOne(s => s.Ilce)
                .WithMany(ilce => ilce.Subeler)
                .HasForeignKey(s => s.IlceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Arac - Sube (Cascade)
            modelBuilder.Entity<Arac>()
                .HasOne(a => a.Sube)
                .WithMany(s => s.Araclar)
                .HasForeignKey(a => a.SubeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Personel - Rol (Restrict)
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.Rol)
                .WithMany(r => r.Personeller)
                .HasForeignKey(p => p.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Personel - Sube (Restrict)
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.Sube)
                .WithMany(s => s.Personeller)
                .HasForeignKey(p => p.SubeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Personel - Arac (SetNull - personnel can exist without vehicle)
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.Arac)
                .WithMany(a => a.Personeller)
                .HasForeignKey(p => p.AracId)
                .OnDelete(DeleteBehavior.SetNull);

            // MusteriAdres - Musteri (Cascade - address association belongs to customer)
            modelBuilder.Entity<MusteriAdres>()
                .HasOne(ma => ma.Musteri)
                .WithMany(m => m.MusteriAdresleri)
                .HasForeignKey(ma => ma.MusteriId)
                .OnDelete(DeleteBehavior.Cascade);

            // MusteriAdres - Adres (Restrict - address is independent)
            modelBuilder.Entity<MusteriAdres>()
                .HasOne(ma => ma.Adres)
                .WithMany()
                .HasForeignKey(ma => ma.AdresId)
                .OnDelete(DeleteBehavior.Restrict);

            // Gonderi - Musteri (Restrict - shipment should not delete with customer)
            modelBuilder.Entity<Gonderi>()
                .HasOne(g => g.Gonderen)
                .WithMany(m => m.GonderilerGonderen)
                .HasForeignKey(g => g.GonderenId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Gonderi>()
                .HasOne(g => g.Alici)
                .WithMany(m => m.GonderilerAlici)
                .HasForeignKey(g => g.AliciId)
                .OnDelete(DeleteBehavior.Restrict);

            // Gonderi - Adres (Restrict) => iki FK olduğundan SET NULL çoklu path hatasına sebep olur
            modelBuilder.Entity<Gonderi>()
                .HasOne(g => g.GonderenAdres)
                .WithMany()
                .HasForeignKey(g => g.GonderenAdresId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Gonderi>()
                .HasOne(g => g.AliciAdres)
                .WithMany()
                .HasForeignKey(g => g.AliciAdresId)
                .OnDelete(DeleteBehavior.Restrict);

            // Gonderi - Personel (Kurye) (SetNull - shipment can exist without courier)
            modelBuilder.Entity<Gonderi>()
                .HasOne(g => g.Kurye)
                .WithMany()
                .HasForeignKey(g => g.KuryeId)
                .OnDelete(DeleteBehavior.SetNull);

            // GonderiDurumGecmis - Gonderi (Cascade)
            modelBuilder.Entity<GonderiDurumGecmis>()
                .HasOne(gdg => gdg.Gonderi)
                .WithMany()
                .HasForeignKey(gdg => gdg.GonderiId)
                .OnDelete(DeleteBehavior.Cascade);

            // GonderiDurumGecmis - Sube (Restrict)
            modelBuilder.Entity<GonderiDurumGecmis>()
                .HasOne(gdg => gdg.Sube)
                .WithMany(s => s.GonderiDurumGecmisi)
                .HasForeignKey(gdg => gdg.SubeId)
                .OnDelete(DeleteBehavior.Restrict);

            // GonderiDurumGecmis - Personel (SetNull)
            modelBuilder.Entity<GonderiDurumGecmis>()
                .HasOne(gdg => gdg.Personel)
                .WithMany(p => p.GonderiDurumGecmisi)
                .HasForeignKey(gdg => gdg.PersonelId)
                .OnDelete(DeleteBehavior.SetNull);

            // GonderiDurumGecmis - Arac (SetNull)
            modelBuilder.Entity<GonderiDurumGecmis>()
                .HasOne(gdg => gdg.Arac)
                .WithMany(a => a.GonderiDurumGecmisi)
                .HasForeignKey(gdg => gdg.AracId)
                .OnDelete(DeleteBehavior.SetNull);

            // MusteriDestek - Musteri (Restrict)
            modelBuilder.Entity<MusteriDestek>()
                .HasOne(md => md.Gonderen)
                .WithMany(m => m.MusteriDestekleriGonderen)
                .HasForeignKey(md => md.GonderenId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MusteriDestek>()
                .HasOne(md => md.Alici)
                .WithMany(m => m.MusteriDestekleriAlici)
                .HasForeignKey(md => md.AliciId)
                .OnDelete(DeleteBehavior.Restrict);

            // MusteriDestek - Gonderi (Restrict)
            modelBuilder.Entity<MusteriDestek>()
                .HasOne(md => md.Gonderi)
                .WithMany()
                .HasForeignKey(md => md.GonderiId)
                .OnDelete(DeleteBehavior.Restrict);

            // OdemeFatura - Gonderi (Cascade)
            modelBuilder.Entity<OdemeFatura>()
                .HasOne(of => of.Gonderi)
                .WithMany()
                .HasForeignKey(of => of.GonderiId)
                .OnDelete(DeleteBehavior.Cascade);

            // OdemeFatura - Musteri (Restrict)
            modelBuilder.Entity<OdemeFatura>()
                .HasOne(of => of.Musteri)
                .WithMany(m => m.OdemeFaturalari)
                .HasForeignKey(of => of.MusteriId)
                .OnDelete(DeleteBehavior.Restrict);

            // OdemeFatura - Adres (Restrict)
            modelBuilder.Entity<OdemeFatura>()
                .HasOne(of => of.FaturaAdres)
                .WithMany()
                .HasForeignKey(of => of.FaturaAdresId)
                .OnDelete(DeleteBehavior.Restrict);

            // IadeIptalIslem - Gonderi (Cascade)
            modelBuilder.Entity<IadeIptalIslem>()
                .HasOne(ii => ii.Gonderi)
                .WithMany()
                .HasForeignKey(ii => ii.GonderiId)
                .OnDelete(DeleteBehavior.Cascade);

            // IadeIptalIslem - Musteri (Restrict)
            modelBuilder.Entity<IadeIptalIslem>()
                .HasOne(ii => ii.Musteri)
                .WithMany(m => m.IadeIptalIslemleri)
                .HasForeignKey(ii => ii.MusteriId)
                .OnDelete(DeleteBehavior.Restrict);

            // IadeIptalIslem - Sube (Restrict)
            modelBuilder.Entity<IadeIptalIslem>()
                .HasOne(ii => ii.Sube)
                .WithMany(s => s.IadeIptalIslemleri)
                .HasForeignKey(ii => ii.SubeId)
                .OnDelete(DeleteBehavior.Restrict);

            // IadeIptalIslem - Personel (SetNull)
            modelBuilder.Entity<IadeIptalIslem>()
                .HasOne(ii => ii.Personel)
                .WithMany(p => p.IadeIptalIslemleri)
                .HasForeignKey(ii => ii.PersonelId)
                .OnDelete(DeleteBehavior.SetNull);

            // ======================================
            // FiyatlandirmaTarife - Bağımsız Tablo (İlişki Yok)
            // ======================================
            modelBuilder.Entity<FiyatlandirmaTarife>(entity =>
            {
                entity.ToTable("FiyatlandirmaTarifeler");
                
                entity.HasKey(e => e.TarifeId);

                entity.Property(e => e.TarifeTuru)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TarifeAdi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MinDeger)
                    .HasColumnType("decimal(18,4)");

                entity.Property(e => e.MaxDeger)
                    .HasColumnType("decimal(18,4)");

                entity.Property(e => e.Deger)
                    .IsRequired()
                    .HasColumnType("decimal(18,4)");

                entity.Property(e => e.Birim)
                    .HasMaxLength(20);

                entity.Property(e => e.Aktif)
                    .IsRequired()
                    .HasDefaultValue(true);

                entity.Property(e => e.GecerlilikBaslangic)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.GecerlilikBitis);

                entity.Property(e => e.Oncelik)
                    .IsRequired()
                    .HasDefaultValue(0);

                entity.Property(e => e.Aciklama)
                    .HasMaxLength(500);

                entity.Property(e => e.OlusturulmaTarihi)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.GuncellemeTarihi);

                // Index'ler - Performans için
                entity.HasIndex(e => new { e.TarifeTuru, e.Aktif, e.GecerlilikBaslangic })
                    .HasDatabaseName("IX_FiyatlandirmaTarifeler_TarifeTuru_Aktif_Gecerlilik");

                entity.HasIndex(e => new { e.TarifeTuru, e.MinDeger, e.MaxDeger })
                    .HasDatabaseName("IX_FiyatlandirmaTarifeler_TarifeTuru_DegerAraligi");
            });

            // ======================================
            // SEED DATA - Migration Sırasında Otomatik Veri Ekleme
            // ======================================
            
            // Roller - Kargo takip sistemi için kullanıcı rolleri (Toplam: 18 rol)
            RolSeedData.Seed(modelBuilder);
            
            // Türkiye'nin 81 ili
            IlSeedData.Seed(modelBuilder);
            
            // Her il için 3 ilçe (Toplam: 81 * 3 = 243)
            IlceSeedData.Seed(modelBuilder);
            
            // Her ilçe için 2 mahalle (Toplam: 243 * 2 = 486)
            MahalleSeedData.Seed(modelBuilder);
            
            // Fiyatlandırma tarifeleri (Toplam: 18 tarife - 5 kategori)
            FiyatlandirmaTarifeSeedData.Seed(modelBuilder);
        }
    }
}
