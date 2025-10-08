using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kargotakipsistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Iller",
                columns: table => new
                {
                    IlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iller", x => x.IlId);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    MusteriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notlar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.MusteriId);
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    IlceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlceAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_Ilceler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mahalleler",
                columns: table => new
                {
                    MahalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlceId = table.Column<int>(type: "int", nullable: false),
                    MahalleAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahalleler", x => x.MahalleId);
                    table.ForeignKey(
                        name: "FK_Mahalleler_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subeler",
                columns: table => new
                {
                    SubeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubeAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubeTip = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlceId = table.Column<int>(type: "int", nullable: false),
                    AcikAdres = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CalismaSaatleri = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subeler", x => x.SubeId);
                    table.ForeignKey(
                        name: "FK_Subeler_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subeler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Araclar",
                columns: table => new
                {
                    AracId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plaka = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AracTip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubeId = table.Column<int>(type: "int", nullable: false),
                    KapasiteKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Durum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GpsKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Araclar", x => x.AracId);
                    table.ForeignKey(
                        name: "FK_Araclar_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "SubeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cinsiyet = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    SubeId = table.Column<int>(type: "int", nullable: false),
                    AracId = table.Column<int>(type: "int", nullable: true),
                    AdresId = table.Column<int>(type: "int", nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    IseGirisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IstenCikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Maas = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EhliyetSinifi = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.PersonelId);
                    table.ForeignKey(
                        name: "FK_Personeller_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "AracId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Personeller_Roller_RolId",
                        column: x => x.RolId,
                        principalTable: "Roller",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personeller_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "SubeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adresler",
                columns: table => new
                {
                    AdresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresBaslik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AcikAdres = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlceId = table.Column<int>(type: "int", nullable: false),
                    MahalleId = table.Column<int>(type: "int", nullable: false),
                    PostaKodu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AdresTipi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: true),
                    PersonelId = table.Column<int>(type: "int", nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    KapiNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BinaAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Daire = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EkAciklama = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresler", x => x.AdresId);
                    table.ForeignKey(
                        name: "FK_Adresler_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adresler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adresler_Mahalleler_MahalleId",
                        column: x => x.MahalleId,
                        principalTable: "Mahalleler",
                        principalColumn: "MahalleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adresler_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Adresler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Gonderiler",
                columns: table => new
                {
                    GonderiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakipNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GonderenId = table.Column<int>(type: "int", nullable: false),
                    AliciId = table.Column<int>(type: "int", nullable: false),
                    GonderenAdresId = table.Column<int>(type: "int", nullable: true),
                    AliciAdresId = table.Column<int>(type: "int", nullable: true),
                    GonderiTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TahminiTeslimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeslimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeslimEdilenKisi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeslimatTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KuryeId = table.Column<int>(type: "int", nullable: true),
                    Agirlik = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Boyut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndirimTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EkMasraf = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IptalTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IadeDurumu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gonderiler", x => x.GonderiId);
                    table.ForeignKey(
                        name: "FK_Gonderiler_Adresler_AliciAdresId",
                        column: x => x.AliciAdresId,
                        principalTable: "Adresler",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gonderiler_Adresler_GonderenAdresId",
                        column: x => x.GonderenAdresId,
                        principalTable: "Adresler",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gonderiler_Musteriler_AliciId",
                        column: x => x.AliciId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gonderiler_Musteriler_GonderenId",
                        column: x => x.GonderenId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gonderiler_Personeller_KuryeId",
                        column: x => x.KuryeId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MusteriAdresleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    AdresTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriAdresleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusteriAdresleri_Adresler_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adresler",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MusteriAdresleri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GonderiDurumGecmisi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderiId = table.Column<int>(type: "int", nullable: false),
                    DurumAd = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemTipi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SonDurumMu = table.Column<bool>(type: "bit", nullable: false),
                    IslemSonucu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TeslimatKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IlgiliKisiAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IlgiliKisiTel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SubeId = table.Column<int>(type: "int", nullable: true),
                    PersonelId = table.Column<int>(type: "int", nullable: true),
                    AracId = table.Column<int>(type: "int", nullable: true),
                    IslemBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IslemBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GonderiDurumGecmisi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GonderiDurumGecmisi_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "AracId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GonderiDurumGecmisi_Gonderiler_GonderiId",
                        column: x => x.GonderiId,
                        principalTable: "Gonderiler",
                        principalColumn: "GonderiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GonderiDurumGecmisi_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GonderiDurumGecmisi_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "SubeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IadeIptalIslemleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderiId = table.Column<int>(type: "int", nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    IslemTipi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Neden = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IadeTipi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IslemDurumu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IslemSonucu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RedNedeni = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DosyaLinki = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SubeId = table.Column<int>(type: "int", nullable: true),
                    PersonelId = table.Column<int>(type: "int", nullable: true),
                    IslemBaslatanId = table.Column<int>(type: "int", nullable: true),
                    IslemOnaylayanId = table.Column<int>(type: "int", nullable: true),
                    OnayTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IslemBaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IslemBitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IadeIptalIslemleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IadeIptalIslemleri_Gonderiler_GonderiId",
                        column: x => x.GonderiId,
                        principalTable: "Gonderiler",
                        principalColumn: "GonderiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IadeIptalIslemleri_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IadeIptalIslemleri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_IadeIptalIslemleri_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "SubeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MusteriDestekleri",
                columns: table => new
                {
                    KayitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderenId = table.Column<int>(type: "int", nullable: false),
                    AliciId = table.Column<int>(type: "int", nullable: false),
                    GonderiId = table.Column<int>(type: "int", nullable: false),
                    Konu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kategori = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Kanal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: true),
                    CevapTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CozumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CozumDurumu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CozumAciklama = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IslemSonucu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IlgiliKisiAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IlgiliKisiTel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DosyaLinki = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriDestekleri", x => x.KayitId);
                    table.ForeignKey(
                        name: "FK_MusteriDestekleri_Gonderiler_GonderiId",
                        column: x => x.GonderiId,
                        principalTable: "Gonderiler",
                        principalColumn: "GonderiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MusteriDestekleri_Musteriler_AliciId",
                        column: x => x.AliciId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MusteriDestekleri_Musteriler_GonderenId",
                        column: x => x.GonderenId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MusteriDestekleri_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OdemeFaturalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderiId = table.Column<int>(type: "int", nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OdemeTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IndirimTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EkMasraf = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ParaBirimi = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VadeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Durum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OdemeDurumu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FaturaAdresId = table.Column<int>(type: "int", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OdemeNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AliciAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdemeFaturalari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdemeFaturalari_Adresler_FaturaAdresId",
                        column: x => x.FaturaAdresId,
                        principalTable: "Adresler",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OdemeFaturalari_Gonderiler_GonderiId",
                        column: x => x.GonderiId,
                        principalTable: "Gonderiler",
                        principalColumn: "GonderiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OdemeFaturalari_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_IlceId",
                table: "Adresler",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_IlId",
                table: "Adresler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_MahalleId",
                table: "Adresler",
                column: "MahalleId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_MusteriId",
                table: "Adresler",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_PersonelId",
                table: "Adresler",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_SubeId",
                table: "Araclar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_GonderiDurumGecmisi_AracId",
                table: "GonderiDurumGecmisi",
                column: "AracId");

            migrationBuilder.CreateIndex(
                name: "IX_GonderiDurumGecmisi_GonderiId",
                table: "GonderiDurumGecmisi",
                column: "GonderiId");

            migrationBuilder.CreateIndex(
                name: "IX_GonderiDurumGecmisi_PersonelId",
                table: "GonderiDurumGecmisi",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GonderiDurumGecmisi_SubeId",
                table: "GonderiDurumGecmisi",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Gonderiler_AliciAdresId",
                table: "Gonderiler",
                column: "AliciAdresId");

            migrationBuilder.CreateIndex(
                name: "IX_Gonderiler_AliciId",
                table: "Gonderiler",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_Gonderiler_GonderenAdresId",
                table: "Gonderiler",
                column: "GonderenAdresId");

            migrationBuilder.CreateIndex(
                name: "IX_Gonderiler_GonderenId",
                table: "Gonderiler",
                column: "GonderenId");

            migrationBuilder.CreateIndex(
                name: "IX_Gonderiler_KuryeId",
                table: "Gonderiler",
                column: "KuryeId");

            migrationBuilder.CreateIndex(
                name: "IX_IadeIptalIslemleri_GonderiId",
                table: "IadeIptalIslemleri",
                column: "GonderiId");

            migrationBuilder.CreateIndex(
                name: "IX_IadeIptalIslemleri_MusteriId",
                table: "IadeIptalIslemleri",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_IadeIptalIslemleri_PersonelId",
                table: "IadeIptalIslemleri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_IadeIptalIslemleri_SubeId",
                table: "IadeIptalIslemleri",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilceler_IlId",
                table: "Ilceler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Mahalleler_IlceId",
                table: "Mahalleler",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriAdresleri_AdresId",
                table: "MusteriAdresleri",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriAdresleri_MusteriId",
                table: "MusteriAdresleri",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriDestekleri_AliciId",
                table: "MusteriDestekleri",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriDestekleri_GonderenId",
                table: "MusteriDestekleri",
                column: "GonderenId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriDestekleri_GonderiId",
                table: "MusteriDestekleri",
                column: "GonderiId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriDestekleri_PersonelId",
                table: "MusteriDestekleri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_OdemeFaturalari_FaturaAdresId",
                table: "OdemeFaturalari",
                column: "FaturaAdresId");

            migrationBuilder.CreateIndex(
                name: "IX_OdemeFaturalari_GonderiId",
                table: "OdemeFaturalari",
                column: "GonderiId");

            migrationBuilder.CreateIndex(
                name: "IX_OdemeFaturalari_MusteriId",
                table: "OdemeFaturalari",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_AracId",
                table: "Personeller",
                column: "AracId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_RolId",
                table: "Personeller",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_SubeId",
                table: "Personeller",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subeler_IlceId",
                table: "Subeler",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Subeler_IlId",
                table: "Subeler",
                column: "IlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GonderiDurumGecmisi");

            migrationBuilder.DropTable(
                name: "IadeIptalIslemleri");

            migrationBuilder.DropTable(
                name: "MusteriAdresleri");

            migrationBuilder.DropTable(
                name: "MusteriDestekleri");

            migrationBuilder.DropTable(
                name: "OdemeFaturalari");

            migrationBuilder.DropTable(
                name: "Gonderiler");

            migrationBuilder.DropTable(
                name: "Adresler");

            migrationBuilder.DropTable(
                name: "Mahalleler");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "Araclar");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "Subeler");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropTable(
                name: "Iller");
        }
    }
}
