using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace kargotakipsistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiyatlandirmaTarifeler",
                columns: table => new
                {
                    TarifeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarifeTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TarifeAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinDeger = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    MaxDeger = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Deger = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Birim = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    GecerlilikBaslangic = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    GecerlilikBitis = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Oncelik = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiyatlandirmaTarifeler", x => x.TarifeId);
                });

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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adresler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
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
                    TeslimEdilenKisi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    IadeDurumu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
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
                        principalColumn: "PersonelId");
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

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Aktif", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[] { 1, "Küçük paketler için temel ücret (minimum ücret uygulanır)", true, "TL/kg", 30.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1.0m, 0.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Çok Hafif (0-1 kg)", "AgirlikTarife" });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[,]
                {
                    { 2, "Standart paketler için ekonomik tarife", "TL/kg", 22.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5.0m, 1.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Hafif (1-5 kg)", "AgirlikTarife" },
                    { 3, "Büyük paketler için indirimli fiyat", "TL/kg", 18.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20.0m, 5.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Orta (5-20 kg)", "AgirlikTarife" },
                    { 4, "Toplu/ağır kargolar için en uygun fiyat", "TL/kg", 15.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 20.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Ağır (20+ kg)", "AgirlikTarife" }
                });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Aktif", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[] { 5, "Normal boyutlu paketler - ek ücret uygulanmaz", true, "TL", 0.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20000.0m, 0.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Standart Hacim (0-20,000 cm³)", "HacimEkUcret" });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[,]
                {
                    { 6, "Orta hacimli paketler için ek ücret", "TL", 20.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 50000.0m, 20000.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Orta Hacim (20,001-50,000 cm³)", "HacimEkUcret" },
                    { 7, "Hacimli paketler için özel işlem ücreti", "TL", 45.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 100000.0m, 50000.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Büyük Hacim (50,001-100,000 cm³)", "HacimEkUcret" },
                    { 8, "Ekstra büyük paketler için maksimum ek ücret", "TL", 75.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 100000.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Çok Büyük Hacim (100,000+ cm³)", "HacimEkUcret" }
                });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Aktif", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[] { 9, "3-5 iş günü - ek ücret yok (x1.0)", true, "çarpan", 1.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Standart Teslimat", "TeslimatCarpan" });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[,]
                {
                    { 10, "1-2 iş günü - %35 ek ücret (x1.35)", "çarpan", 1.35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Hızlı Teslimat", "TeslimatCarpan" },
                    { 11, "Aynı gün teslim - %60 ek ücret (x1.60)", "çarpan", 1.60m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Aynı Gün Teslimat", "TeslimatCarpan" },
                    { 12, "Belirli saat aralığında teslimat - %40 ek ücret (x1.40)", "çarpan", 1.40m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Randevulu Teslimat", "TeslimatCarpan" }
                });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Aktif", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[] { 13, "40 kg üzeri paketler için özel taşıma ücreti", true, "TL", 50.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 40.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Çok Ağır Yük (40+ kg)", "EkMasrafEsik" });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[,]
                {
                    { 14, "Çok hacimli paketler için özel araç gereksinimi ücreti", "TL", 100.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 150000.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ekstra Büyük Hacim (150,000+ cm³)", "EkMasrafEsik" },
                    { 15, "Özel paketleme ve dikkatli taşıma gerektiren ürünler için", "TL", 25.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Kırılgan/Hassas Ürün", "EkMasrafEsik" }
                });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Aktif", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[] { 16, "10-25 kg arası gönderiler için %5 indirim", true, "%", 0.05m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 25.0m, 10.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Orta Hacim İndirimi (10-25 kg)", "IndirimEsik" });

            migrationBuilder.InsertData(
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeId", "Aciklama", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru" },
                values: new object[,]
                {
                    { 17, "25-50 kg arası toplu gönderiler için %10 indirim", "%", 0.10m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 50.0m, 25.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Toplu Gönderi İndirimi (25-50 kg)", "IndirimEsik" },
                    { 18, "50 kg üzeri kurumsal gönderiler için %15 indirim", "%", 0.15m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 50.01m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Kurumsal Müşteri (50+ kg)", "IndirimEsik" }
                });

            migrationBuilder.InsertData(
                table: "Iller",
                columns: new[] { "IlId", "IlAd" },
                values: new object[,]
                {
                    { 1, "Adana" },
                    { 2, "Adıyaman" },
                    { 3, "Afyonkarahisar" },
                    { 4, "Ağrı" },
                    { 5, "Amasya" },
                    { 6, "Ankara" },
                    { 7, "Antalya" },
                    { 8, "Artvin" },
                    { 9, "Aydın" },
                    { 10, "Balıkesir" },
                    { 11, "Bilecik" },
                    { 12, "Bingöl" },
                    { 13, "Bitlis" },
                    { 14, "Bolu" },
                    { 15, "Burdur" },
                    { 16, "Bursa" },
                    { 17, "Çanakkale" },
                    { 18, "Çankırı" },
                    { 19, "Çorum" },
                    { 20, "Denizli" },
                    { 21, "Diyarbakır" },
                    { 22, "Edirne" },
                    { 23, "Elazığ" },
                    { 24, "Erzincan" },
                    { 25, "Erzurum" },
                    { 26, "Eskişehir" },
                    { 27, "Gaziantep" },
                    { 28, "Giresun" },
                    { 29, "Gümüşhane" },
                    { 30, "Hakkari" },
                    { 31, "Hatay" },
                    { 32, "Isparta" },
                    { 33, "Mersin" },
                    { 34, "İstanbul" },
                    { 35, "İzmir" },
                    { 36, "Kars" },
                    { 37, "Kastamonu" },
                    { 38, "Kayseri" },
                    { 39, "Kırklareli" },
                    { 40, "Kırşehir" },
                    { 41, "Kocaeli" },
                    { 42, "Konya" },
                    { 43, "Kütahya" },
                    { 44, "Malatya" },
                    { 45, "Manisa" },
                    { 46, "Kahramanmaraş" },
                    { 47, "Mardin" },
                    { 48, "Muğla" },
                    { 49, "Muş" },
                    { 50, "Nevşehir" },
                    { 51, "Niğde" },
                    { 52, "Ordu" },
                    { 53, "Rize" },
                    { 54, "Sakarya" },
                    { 55, "Samsun" },
                    { 56, "Siirt" },
                    { 57, "Sinop" },
                    { 58, "Sivas" },
                    { 59, "Tekirdağ" },
                    { 60, "Tokat" },
                    { 61, "Trabzon" },
                    { 62, "Tunceli" },
                    { 63, "Şanlıurfa" },
                    { 64, "Uşak" },
                    { 65, "Van" },
                    { 66, "Yozgat" },
                    { 67, "Zonguldak" },
                    { 68, "Aksaray" },
                    { 69, "Bayburt" },
                    { 70, "Karaman" },
                    { 71, "Kırıkkale" },
                    { 72, "Batman" },
                    { 73, "Şırnak" },
                    { 74, "Bartın" },
                    { 75, "Ardahan" },
                    { 76, "Iğdır" },
                    { 77, "Yalova" },
                    { 78, "Karabük" },
                    { 79, "Kilis" },
                    { 80, "Osmaniye" },
                    { 81, "Düzce" }
                });

            migrationBuilder.InsertData(
                table: "Roller",
                columns: new[] { "RolId", "RolAd" },
                values: new object[,]
                {
                    { 1, "Sistem Yöneticisi" },
                    { 2, "Genel Müdür" },
                    { 3, "Bölge Müdürü" },
                    { 4, "Şube Müdürü" },
                    { 5, "Kurye" },
                    { 6, "Dağıtım Sorumlusu" },
                    { 7, "Transfer Personeli" },
                    { 8, "Depo Görevlisi" },
                    { 9, "Müşteri Hizmetleri" },
                    { 10, "Kargo Kabul" },
                    { 11, "Çağrı Merkezi" },
                    { 12, "Muhasebe" },
                    { 13, "İnsan Kaynakları" },
                    { 14, "Filo Yöneticisi" },
                    { 15, "IT Destek" },
                    { 16, "Kalite Kontrol" },
                    { 17, "Güvenlik" },
                    { 18, "Eğitim Koordinatörü" }
                });

            migrationBuilder.InsertData(
                table: "Ilceler",
                columns: new[] { "IlceId", "IlId", "IlceAd" },
                values: new object[,]
                {
                    { 1, 1, "İlçe 1" },
                    { 2, 1, "İlçe 2" },
                    { 3, 1, "İlçe 3" },
                    { 4, 2, "İlçe 1" },
                    { 5, 2, "İlçe 2" },
                    { 6, 2, "İlçe 3" },
                    { 7, 3, "İlçe 1" },
                    { 8, 3, "İlçe 2" },
                    { 9, 3, "İlçe 3" },
                    { 10, 4, "İlçe 1" },
                    { 11, 4, "İlçe 2" },
                    { 12, 4, "İlçe 3" },
                    { 13, 5, "İlçe 1" },
                    { 14, 5, "İlçe 2" },
                    { 15, 5, "İlçe 3" },
                    { 16, 6, "İlçe 1" },
                    { 17, 6, "İlçe 2" },
                    { 18, 6, "İlçe 3" },
                    { 19, 7, "İlçe 1" },
                    { 20, 7, "İlçe 2" },
                    { 21, 7, "İlçe 3" },
                    { 22, 8, "İlçe 1" },
                    { 23, 8, "İlçe 2" },
                    { 24, 8, "İlçe 3" },
                    { 25, 9, "İlçe 1" },
                    { 26, 9, "İlçe 2" },
                    { 27, 9, "İlçe 3" },
                    { 28, 10, "İlçe 1" },
                    { 29, 10, "İlçe 2" },
                    { 30, 10, "İlçe 3" },
                    { 31, 11, "İlçe 1" },
                    { 32, 11, "İlçe 2" },
                    { 33, 11, "İlçe 3" },
                    { 34, 12, "İlçe 1" },
                    { 35, 12, "İlçe 2" },
                    { 36, 12, "İlçe 3" },
                    { 37, 13, "İlçe 1" },
                    { 38, 13, "İlçe 2" },
                    { 39, 13, "İlçe 3" },
                    { 40, 14, "İlçe 1" },
                    { 41, 14, "İlçe 2" },
                    { 42, 14, "İlçe 3" },
                    { 43, 15, "İlçe 1" },
                    { 44, 15, "İlçe 2" },
                    { 45, 15, "İlçe 3" },
                    { 46, 16, "İlçe 1" },
                    { 47, 16, "İlçe 2" },
                    { 48, 16, "İlçe 3" },
                    { 49, 17, "İlçe 1" },
                    { 50, 17, "İlçe 2" },
                    { 51, 17, "İlçe 3" },
                    { 52, 18, "İlçe 1" },
                    { 53, 18, "İlçe 2" },
                    { 54, 18, "İlçe 3" },
                    { 55, 19, "İlçe 1" },
                    { 56, 19, "İlçe 2" },
                    { 57, 19, "İlçe 3" },
                    { 58, 20, "İlçe 1" },
                    { 59, 20, "İlçe 2" },
                    { 60, 20, "İlçe 3" },
                    { 61, 21, "İlçe 1" },
                    { 62, 21, "İlçe 2" },
                    { 63, 21, "İlçe 3" },
                    { 64, 22, "İlçe 1" },
                    { 65, 22, "İlçe 2" },
                    { 66, 22, "İlçe 3" },
                    { 67, 23, "İlçe 1" },
                    { 68, 23, "İlçe 2" },
                    { 69, 23, "İlçe 3" },
                    { 70, 24, "İlçe 1" },
                    { 71, 24, "İlçe 2" },
                    { 72, 24, "İlçe 3" },
                    { 73, 25, "İlçe 1" },
                    { 74, 25, "İlçe 2" },
                    { 75, 25, "İlçe 3" },
                    { 76, 26, "İlçe 1" },
                    { 77, 26, "İlçe 2" },
                    { 78, 26, "İlçe 3" },
                    { 79, 27, "İlçe 1" },
                    { 80, 27, "İlçe 2" },
                    { 81, 27, "İlçe 3" },
                    { 82, 28, "İlçe 1" },
                    { 83, 28, "İlçe 2" },
                    { 84, 28, "İlçe 3" },
                    { 85, 29, "İlçe 1" },
                    { 86, 29, "İlçe 2" },
                    { 87, 29, "İlçe 3" },
                    { 88, 30, "İlçe 1" },
                    { 89, 30, "İlçe 2" },
                    { 90, 30, "İlçe 3" },
                    { 91, 31, "İlçe 1" },
                    { 92, 31, "İlçe 2" },
                    { 93, 31, "İlçe 3" },
                    { 94, 32, "İlçe 1" },
                    { 95, 32, "İlçe 2" },
                    { 96, 32, "İlçe 3" },
                    { 97, 33, "İlçe 1" },
                    { 98, 33, "İlçe 2" },
                    { 99, 33, "İlçe 3" },
                    { 100, 34, "İlçe 1" },
                    { 101, 34, "İlçe 2" },
                    { 102, 34, "İlçe 3" },
                    { 103, 35, "İlçe 1" },
                    { 104, 35, "İlçe 2" },
                    { 105, 35, "İlçe 3" },
                    { 106, 36, "İlçe 1" },
                    { 107, 36, "İlçe 2" },
                    { 108, 36, "İlçe 3" },
                    { 109, 37, "İlçe 1" },
                    { 110, 37, "İlçe 2" },
                    { 111, 37, "İlçe 3" },
                    { 112, 38, "İlçe 1" },
                    { 113, 38, "İlçe 2" },
                    { 114, 38, "İlçe 3" },
                    { 115, 39, "İlçe 1" },
                    { 116, 39, "İlçe 2" },
                    { 117, 39, "İlçe 3" },
                    { 118, 40, "İlçe 1" },
                    { 119, 40, "İlçe 2" },
                    { 120, 40, "İlçe 3" },
                    { 121, 41, "İlçe 1" },
                    { 122, 41, "İlçe 2" },
                    { 123, 41, "İlçe 3" },
                    { 124, 42, "İlçe 1" },
                    { 125, 42, "İlçe 2" },
                    { 126, 42, "İlçe 3" },
                    { 127, 43, "İlçe 1" },
                    { 128, 43, "İlçe 2" },
                    { 129, 43, "İlçe 3" },
                    { 130, 44, "İlçe 1" },
                    { 131, 44, "İlçe 2" },
                    { 132, 44, "İlçe 3" },
                    { 133, 45, "İlçe 1" },
                    { 134, 45, "İlçe 2" },
                    { 135, 45, "İlçe 3" },
                    { 136, 46, "İlçe 1" },
                    { 137, 46, "İlçe 2" },
                    { 138, 46, "İlçe 3" },
                    { 139, 47, "İlçe 1" },
                    { 140, 47, "İlçe 2" },
                    { 141, 47, "İlçe 3" },
                    { 142, 48, "İlçe 1" },
                    { 143, 48, "İlçe 2" },
                    { 144, 48, "İlçe 3" },
                    { 145, 49, "İlçe 1" },
                    { 146, 49, "İlçe 2" },
                    { 147, 49, "İlçe 3" },
                    { 148, 50, "İlçe 1" },
                    { 149, 50, "İlçe 2" },
                    { 150, 50, "İlçe 3" },
                    { 151, 51, "İlçe 1" },
                    { 152, 51, "İlçe 2" },
                    { 153, 51, "İlçe 3" },
                    { 154, 52, "İlçe 1" },
                    { 155, 52, "İlçe 2" },
                    { 156, 52, "İlçe 3" },
                    { 157, 53, "İlçe 1" },
                    { 158, 53, "İlçe 2" },
                    { 159, 53, "İlçe 3" },
                    { 160, 54, "İlçe 1" },
                    { 161, 54, "İlçe 2" },
                    { 162, 54, "İlçe 3" },
                    { 163, 55, "İlçe 1" },
                    { 164, 55, "İlçe 2" },
                    { 165, 55, "İlçe 3" },
                    { 166, 56, "İlçe 1" },
                    { 167, 56, "İlçe 2" },
                    { 168, 56, "İlçe 3" },
                    { 169, 57, "İlçe 1" },
                    { 170, 57, "İlçe 2" },
                    { 171, 57, "İlçe 3" },
                    { 172, 58, "İlçe 1" },
                    { 173, 58, "İlçe 2" },
                    { 174, 58, "İlçe 3" },
                    { 175, 59, "İlçe 1" },
                    { 176, 59, "İlçe 2" },
                    { 177, 59, "İlçe 3" },
                    { 178, 60, "İlçe 1" },
                    { 179, 60, "İlçe 2" },
                    { 180, 60, "İlçe 3" },
                    { 181, 61, "İlçe 1" },
                    { 182, 61, "İlçe 2" },
                    { 183, 61, "İlçe 3" },
                    { 184, 62, "İlçe 1" },
                    { 185, 62, "İlçe 2" },
                    { 186, 62, "İlçe 3" },
                    { 187, 63, "İlçe 1" },
                    { 188, 63, "İlçe 2" },
                    { 189, 63, "İlçe 3" },
                    { 190, 64, "İlçe 1" },
                    { 191, 64, "İlçe 2" },
                    { 192, 64, "İlçe 3" },
                    { 193, 65, "İlçe 1" },
                    { 194, 65, "İlçe 2" },
                    { 195, 65, "İlçe 3" },
                    { 196, 66, "İlçe 1" },
                    { 197, 66, "İlçe 2" },
                    { 198, 66, "İlçe 3" },
                    { 199, 67, "İlçe 1" },
                    { 200, 67, "İlçe 2" },
                    { 201, 67, "İlçe 3" },
                    { 202, 68, "İlçe 1" },
                    { 203, 68, "İlçe 2" },
                    { 204, 68, "İlçe 3" },
                    { 205, 69, "İlçe 1" },
                    { 206, 69, "İlçe 2" },
                    { 207, 69, "İlçe 3" },
                    { 208, 70, "İlçe 1" },
                    { 209, 70, "İlçe 2" },
                    { 210, 70, "İlçe 3" },
                    { 211, 71, "İlçe 1" },
                    { 212, 71, "İlçe 2" },
                    { 213, 71, "İlçe 3" },
                    { 214, 72, "İlçe 1" },
                    { 215, 72, "İlçe 2" },
                    { 216, 72, "İlçe 3" },
                    { 217, 73, "İlçe 1" },
                    { 218, 73, "İlçe 2" },
                    { 219, 73, "İlçe 3" },
                    { 220, 74, "İlçe 1" },
                    { 221, 74, "İlçe 2" },
                    { 222, 74, "İlçe 3" },
                    { 223, 75, "İlçe 1" },
                    { 224, 75, "İlçe 2" },
                    { 225, 75, "İlçe 3" },
                    { 226, 76, "İlçe 1" },
                    { 227, 76, "İlçe 2" },
                    { 228, 76, "İlçe 3" },
                    { 229, 77, "İlçe 1" },
                    { 230, 77, "İlçe 2" },
                    { 231, 77, "İlçe 3" },
                    { 232, 78, "İlçe 1" },
                    { 233, 78, "İlçe 2" },
                    { 234, 78, "İlçe 3" },
                    { 235, 79, "İlçe 1" },
                    { 236, 79, "İlçe 2" },
                    { 237, 79, "İlçe 3" },
                    { 238, 80, "İlçe 1" },
                    { 239, 80, "İlçe 2" },
                    { 240, 80, "İlçe 3" },
                    { 241, 81, "İlçe 1" },
                    { 242, 81, "İlçe 2" },
                    { 243, 81, "İlçe 3" }
                });

            migrationBuilder.InsertData(
                table: "Mahalleler",
                columns: new[] { "MahalleId", "IlceId", "MahalleAd" },
                values: new object[,]
                {
                    { 1, 1, "Mahalle 1" },
                    { 2, 1, "Mahalle 2" },
                    { 3, 2, "Mahalle 1" },
                    { 4, 2, "Mahalle 2" },
                    { 5, 3, "Mahalle 1" },
                    { 6, 3, "Mahalle 2" },
                    { 7, 4, "Mahalle 1" },
                    { 8, 4, "Mahalle 2" },
                    { 9, 5, "Mahalle 1" },
                    { 10, 5, "Mahalle 2" },
                    { 11, 6, "Mahalle 1" },
                    { 12, 6, "Mahalle 2" },
                    { 13, 7, "Mahalle 1" },
                    { 14, 7, "Mahalle 2" },
                    { 15, 8, "Mahalle 1" },
                    { 16, 8, "Mahalle 2" },
                    { 17, 9, "Mahalle 1" },
                    { 18, 9, "Mahalle 2" },
                    { 19, 10, "Mahalle 1" },
                    { 20, 10, "Mahalle 2" },
                    { 21, 11, "Mahalle 1" },
                    { 22, 11, "Mahalle 2" },
                    { 23, 12, "Mahalle 1" },
                    { 24, 12, "Mahalle 2" },
                    { 25, 13, "Mahalle 1" },
                    { 26, 13, "Mahalle 2" },
                    { 27, 14, "Mahalle 1" },
                    { 28, 14, "Mahalle 2" },
                    { 29, 15, "Mahalle 1" },
                    { 30, 15, "Mahalle 2" },
                    { 31, 16, "Mahalle 1" },
                    { 32, 16, "Mahalle 2" },
                    { 33, 17, "Mahalle 1" },
                    { 34, 17, "Mahalle 2" },
                    { 35, 18, "Mahalle 1" },
                    { 36, 18, "Mahalle 2" },
                    { 37, 19, "Mahalle 1" },
                    { 38, 19, "Mahalle 2" },
                    { 39, 20, "Mahalle 1" },
                    { 40, 20, "Mahalle 2" },
                    { 41, 21, "Mahalle 1" },
                    { 42, 21, "Mahalle 2" },
                    { 43, 22, "Mahalle 1" },
                    { 44, 22, "Mahalle 2" },
                    { 45, 23, "Mahalle 1" },
                    { 46, 23, "Mahalle 2" },
                    { 47, 24, "Mahalle 1" },
                    { 48, 24, "Mahalle 2" },
                    { 49, 25, "Mahalle 1" },
                    { 50, 25, "Mahalle 2" },
                    { 51, 26, "Mahalle 1" },
                    { 52, 26, "Mahalle 2" },
                    { 53, 27, "Mahalle 1" },
                    { 54, 27, "Mahalle 2" },
                    { 55, 28, "Mahalle 1" },
                    { 56, 28, "Mahalle 2" },
                    { 57, 29, "Mahalle 1" },
                    { 58, 29, "Mahalle 2" },
                    { 59, 30, "Mahalle 1" },
                    { 60, 30, "Mahalle 2" },
                    { 61, 31, "Mahalle 1" },
                    { 62, 31, "Mahalle 2" },
                    { 63, 32, "Mahalle 1" },
                    { 64, 32, "Mahalle 2" },
                    { 65, 33, "Mahalle 1" },
                    { 66, 33, "Mahalle 2" },
                    { 67, 34, "Mahalle 1" },
                    { 68, 34, "Mahalle 2" },
                    { 69, 35, "Mahalle 1" },
                    { 70, 35, "Mahalle 2" },
                    { 71, 36, "Mahalle 1" },
                    { 72, 36, "Mahalle 2" },
                    { 73, 37, "Mahalle 1" },
                    { 74, 37, "Mahalle 2" },
                    { 75, 38, "Mahalle 1" },
                    { 76, 38, "Mahalle 2" },
                    { 77, 39, "Mahalle 1" },
                    { 78, 39, "Mahalle 2" },
                    { 79, 40, "Mahalle 1" },
                    { 80, 40, "Mahalle 2" },
                    { 81, 41, "Mahalle 1" },
                    { 82, 41, "Mahalle 2" },
                    { 83, 42, "Mahalle 1" },
                    { 84, 42, "Mahalle 2" },
                    { 85, 43, "Mahalle 1" },
                    { 86, 43, "Mahalle 2" },
                    { 87, 44, "Mahalle 1" },
                    { 88, 44, "Mahalle 2" },
                    { 89, 45, "Mahalle 1" },
                    { 90, 45, "Mahalle 2" },
                    { 91, 46, "Mahalle 1" },
                    { 92, 46, "Mahalle 2" },
                    { 93, 47, "Mahalle 1" },
                    { 94, 47, "Mahalle 2" },
                    { 95, 48, "Mahalle 1" },
                    { 96, 48, "Mahalle 2" },
                    { 97, 49, "Mahalle 1" },
                    { 98, 49, "Mahalle 2" },
                    { 99, 50, "Mahalle 1" },
                    { 100, 50, "Mahalle 2" },
                    { 101, 51, "Mahalle 1" },
                    { 102, 51, "Mahalle 2" },
                    { 103, 52, "Mahalle 1" },
                    { 104, 52, "Mahalle 2" },
                    { 105, 53, "Mahalle 1" },
                    { 106, 53, "Mahalle 2" },
                    { 107, 54, "Mahalle 1" },
                    { 108, 54, "Mahalle 2" },
                    { 109, 55, "Mahalle 1" },
                    { 110, 55, "Mahalle 2" },
                    { 111, 56, "Mahalle 1" },
                    { 112, 56, "Mahalle 2" },
                    { 113, 57, "Mahalle 1" },
                    { 114, 57, "Mahalle 2" },
                    { 115, 58, "Mahalle 1" },
                    { 116, 58, "Mahalle 2" },
                    { 117, 59, "Mahalle 1" },
                    { 118, 59, "Mahalle 2" },
                    { 119, 60, "Mahalle 1" },
                    { 120, 60, "Mahalle 2" },
                    { 121, 61, "Mahalle 1" },
                    { 122, 61, "Mahalle 2" },
                    { 123, 62, "Mahalle 1" },
                    { 124, 62, "Mahalle 2" },
                    { 125, 63, "Mahalle 1" },
                    { 126, 63, "Mahalle 2" },
                    { 127, 64, "Mahalle 1" },
                    { 128, 64, "Mahalle 2" },
                    { 129, 65, "Mahalle 1" },
                    { 130, 65, "Mahalle 2" },
                    { 131, 66, "Mahalle 1" },
                    { 132, 66, "Mahalle 2" },
                    { 133, 67, "Mahalle 1" },
                    { 134, 67, "Mahalle 2" },
                    { 135, 68, "Mahalle 1" },
                    { 136, 68, "Mahalle 2" },
                    { 137, 69, "Mahalle 1" },
                    { 138, 69, "Mahalle 2" },
                    { 139, 70, "Mahalle 1" },
                    { 140, 70, "Mahalle 2" },
                    { 141, 71, "Mahalle 1" },
                    { 142, 71, "Mahalle 2" },
                    { 143, 72, "Mahalle 1" },
                    { 144, 72, "Mahalle 2" },
                    { 145, 73, "Mahalle 1" },
                    { 146, 73, "Mahalle 2" },
                    { 147, 74, "Mahalle 1" },
                    { 148, 74, "Mahalle 2" },
                    { 149, 75, "Mahalle 1" },
                    { 150, 75, "Mahalle 2" },
                    { 151, 76, "Mahalle 1" },
                    { 152, 76, "Mahalle 2" },
                    { 153, 77, "Mahalle 1" },
                    { 154, 77, "Mahalle 2" },
                    { 155, 78, "Mahalle 1" },
                    { 156, 78, "Mahalle 2" },
                    { 157, 79, "Mahalle 1" },
                    { 158, 79, "Mahalle 2" },
                    { 159, 80, "Mahalle 1" },
                    { 160, 80, "Mahalle 2" },
                    { 161, 81, "Mahalle 1" },
                    { 162, 81, "Mahalle 2" },
                    { 163, 82, "Mahalle 1" },
                    { 164, 82, "Mahalle 2" },
                    { 165, 83, "Mahalle 1" },
                    { 166, 83, "Mahalle 2" },
                    { 167, 84, "Mahalle 1" },
                    { 168, 84, "Mahalle 2" },
                    { 169, 85, "Mahalle 1" },
                    { 170, 85, "Mahalle 2" },
                    { 171, 86, "Mahalle 1" },
                    { 172, 86, "Mahalle 2" },
                    { 173, 87, "Mahalle 1" },
                    { 174, 87, "Mahalle 2" },
                    { 175, 88, "Mahalle 1" },
                    { 176, 88, "Mahalle 2" },
                    { 177, 89, "Mahalle 1" },
                    { 178, 89, "Mahalle 2" },
                    { 179, 90, "Mahalle 1" },
                    { 180, 90, "Mahalle 2" },
                    { 181, 91, "Mahalle 1" },
                    { 182, 91, "Mahalle 2" },
                    { 183, 92, "Mahalle 1" },
                    { 184, 92, "Mahalle 2" },
                    { 185, 93, "Mahalle 1" },
                    { 186, 93, "Mahalle 2" },
                    { 187, 94, "Mahalle 1" },
                    { 188, 94, "Mahalle 2" },
                    { 189, 95, "Mahalle 1" },
                    { 190, 95, "Mahalle 2" },
                    { 191, 96, "Mahalle 1" },
                    { 192, 96, "Mahalle 2" },
                    { 193, 97, "Mahalle 1" },
                    { 194, 97, "Mahalle 2" },
                    { 195, 98, "Mahalle 1" },
                    { 196, 98, "Mahalle 2" },
                    { 197, 99, "Mahalle 1" },
                    { 198, 99, "Mahalle 2" },
                    { 199, 100, "Mahalle 1" },
                    { 200, 100, "Mahalle 2" },
                    { 201, 101, "Mahalle 1" },
                    { 202, 101, "Mahalle 2" },
                    { 203, 102, "Mahalle 1" },
                    { 204, 102, "Mahalle 2" },
                    { 205, 103, "Mahalle 1" },
                    { 206, 103, "Mahalle 2" },
                    { 207, 104, "Mahalle 1" },
                    { 208, 104, "Mahalle 2" },
                    { 209, 105, "Mahalle 1" },
                    { 210, 105, "Mahalle 2" },
                    { 211, 106, "Mahalle 1" },
                    { 212, 106, "Mahalle 2" },
                    { 213, 107, "Mahalle 1" },
                    { 214, 107, "Mahalle 2" },
                    { 215, 108, "Mahalle 1" },
                    { 216, 108, "Mahalle 2" },
                    { 217, 109, "Mahalle 1" },
                    { 218, 109, "Mahalle 2" },
                    { 219, 110, "Mahalle 1" },
                    { 220, 110, "Mahalle 2" },
                    { 221, 111, "Mahalle 1" },
                    { 222, 111, "Mahalle 2" },
                    { 223, 112, "Mahalle 1" },
                    { 224, 112, "Mahalle 2" },
                    { 225, 113, "Mahalle 1" },
                    { 226, 113, "Mahalle 2" },
                    { 227, 114, "Mahalle 1" },
                    { 228, 114, "Mahalle 2" },
                    { 229, 115, "Mahalle 1" },
                    { 230, 115, "Mahalle 2" },
                    { 231, 116, "Mahalle 1" },
                    { 232, 116, "Mahalle 2" },
                    { 233, 117, "Mahalle 1" },
                    { 234, 117, "Mahalle 2" },
                    { 235, 118, "Mahalle 1" },
                    { 236, 118, "Mahalle 2" },
                    { 237, 119, "Mahalle 1" },
                    { 238, 119, "Mahalle 2" },
                    { 239, 120, "Mahalle 1" },
                    { 240, 120, "Mahalle 2" },
                    { 241, 121, "Mahalle 1" },
                    { 242, 121, "Mahalle 2" },
                    { 243, 122, "Mahalle 1" },
                    { 244, 122, "Mahalle 2" },
                    { 245, 123, "Mahalle 1" },
                    { 246, 123, "Mahalle 2" },
                    { 247, 124, "Mahalle 1" },
                    { 248, 124, "Mahalle 2" },
                    { 249, 125, "Mahalle 1" },
                    { 250, 125, "Mahalle 2" },
                    { 251, 126, "Mahalle 1" },
                    { 252, 126, "Mahalle 2" },
                    { 253, 127, "Mahalle 1" },
                    { 254, 127, "Mahalle 2" },
                    { 255, 128, "Mahalle 1" },
                    { 256, 128, "Mahalle 2" },
                    { 257, 129, "Mahalle 1" },
                    { 258, 129, "Mahalle 2" },
                    { 259, 130, "Mahalle 1" },
                    { 260, 130, "Mahalle 2" },
                    { 261, 131, "Mahalle 1" },
                    { 262, 131, "Mahalle 2" },
                    { 263, 132, "Mahalle 1" },
                    { 264, 132, "Mahalle 2" },
                    { 265, 133, "Mahalle 1" },
                    { 266, 133, "Mahalle 2" },
                    { 267, 134, "Mahalle 1" },
                    { 268, 134, "Mahalle 2" },
                    { 269, 135, "Mahalle 1" },
                    { 270, 135, "Mahalle 2" },
                    { 271, 136, "Mahalle 1" },
                    { 272, 136, "Mahalle 2" },
                    { 273, 137, "Mahalle 1" },
                    { 274, 137, "Mahalle 2" },
                    { 275, 138, "Mahalle 1" },
                    { 276, 138, "Mahalle 2" },
                    { 277, 139, "Mahalle 1" },
                    { 278, 139, "Mahalle 2" },
                    { 279, 140, "Mahalle 1" },
                    { 280, 140, "Mahalle 2" },
                    { 281, 141, "Mahalle 1" },
                    { 282, 141, "Mahalle 2" },
                    { 283, 142, "Mahalle 1" },
                    { 284, 142, "Mahalle 2" },
                    { 285, 143, "Mahalle 1" },
                    { 286, 143, "Mahalle 2" },
                    { 287, 144, "Mahalle 1" },
                    { 288, 144, "Mahalle 2" },
                    { 289, 145, "Mahalle 1" },
                    { 290, 145, "Mahalle 2" },
                    { 291, 146, "Mahalle 1" },
                    { 292, 146, "Mahalle 2" },
                    { 293, 147, "Mahalle 1" },
                    { 294, 147, "Mahalle 2" },
                    { 295, 148, "Mahalle 1" },
                    { 296, 148, "Mahalle 2" },
                    { 297, 149, "Mahalle 1" },
                    { 298, 149, "Mahalle 2" },
                    { 299, 150, "Mahalle 1" },
                    { 300, 150, "Mahalle 2" },
                    { 301, 151, "Mahalle 1" },
                    { 302, 151, "Mahalle 2" },
                    { 303, 152, "Mahalle 1" },
                    { 304, 152, "Mahalle 2" },
                    { 305, 153, "Mahalle 1" },
                    { 306, 153, "Mahalle 2" },
                    { 307, 154, "Mahalle 1" },
                    { 308, 154, "Mahalle 2" },
                    { 309, 155, "Mahalle 1" },
                    { 310, 155, "Mahalle 2" },
                    { 311, 156, "Mahalle 1" },
                    { 312, 156, "Mahalle 2" },
                    { 313, 157, "Mahalle 1" },
                    { 314, 157, "Mahalle 2" },
                    { 315, 158, "Mahalle 1" },
                    { 316, 158, "Mahalle 2" },
                    { 317, 159, "Mahalle 1" },
                    { 318, 159, "Mahalle 2" },
                    { 319, 160, "Mahalle 1" },
                    { 320, 160, "Mahalle 2" },
                    { 321, 161, "Mahalle 1" },
                    { 322, 161, "Mahalle 2" },
                    { 323, 162, "Mahalle 1" },
                    { 324, 162, "Mahalle 2" },
                    { 325, 163, "Mahalle 1" },
                    { 326, 163, "Mahalle 2" },
                    { 327, 164, "Mahalle 1" },
                    { 328, 164, "Mahalle 2" },
                    { 329, 165, "Mahalle 1" },
                    { 330, 165, "Mahalle 2" },
                    { 331, 166, "Mahalle 1" },
                    { 332, 166, "Mahalle 2" },
                    { 333, 167, "Mahalle 1" },
                    { 334, 167, "Mahalle 2" },
                    { 335, 168, "Mahalle 1" },
                    { 336, 168, "Mahalle 2" },
                    { 337, 169, "Mahalle 1" },
                    { 338, 169, "Mahalle 2" },
                    { 339, 170, "Mahalle 1" },
                    { 340, 170, "Mahalle 2" },
                    { 341, 171, "Mahalle 1" },
                    { 342, 171, "Mahalle 2" },
                    { 343, 172, "Mahalle 1" },
                    { 344, 172, "Mahalle 2" },
                    { 345, 173, "Mahalle 1" },
                    { 346, 173, "Mahalle 2" },
                    { 347, 174, "Mahalle 1" },
                    { 348, 174, "Mahalle 2" },
                    { 349, 175, "Mahalle 1" },
                    { 350, 175, "Mahalle 2" },
                    { 351, 176, "Mahalle 1" },
                    { 352, 176, "Mahalle 2" },
                    { 353, 177, "Mahalle 1" },
                    { 354, 177, "Mahalle 2" },
                    { 355, 178, "Mahalle 1" },
                    { 356, 178, "Mahalle 2" },
                    { 357, 179, "Mahalle 1" },
                    { 358, 179, "Mahalle 2" },
                    { 359, 180, "Mahalle 1" },
                    { 360, 180, "Mahalle 2" },
                    { 361, 181, "Mahalle 1" },
                    { 362, 181, "Mahalle 2" },
                    { 363, 182, "Mahalle 1" },
                    { 364, 182, "Mahalle 2" },
                    { 365, 183, "Mahalle 1" },
                    { 366, 183, "Mahalle 2" },
                    { 367, 184, "Mahalle 1" },
                    { 368, 184, "Mahalle 2" },
                    { 369, 185, "Mahalle 1" },
                    { 370, 185, "Mahalle 2" },
                    { 371, 186, "Mahalle 1" },
                    { 372, 186, "Mahalle 2" },
                    { 373, 187, "Mahalle 1" },
                    { 374, 187, "Mahalle 2" },
                    { 375, 188, "Mahalle 1" },
                    { 376, 188, "Mahalle 2" },
                    { 377, 189, "Mahalle 1" },
                    { 378, 189, "Mahalle 2" },
                    { 379, 190, "Mahalle 1" },
                    { 380, 190, "Mahalle 2" },
                    { 381, 191, "Mahalle 1" },
                    { 382, 191, "Mahalle 2" },
                    { 383, 192, "Mahalle 1" },
                    { 384, 192, "Mahalle 2" },
                    { 385, 193, "Mahalle 1" },
                    { 386, 193, "Mahalle 2" },
                    { 387, 194, "Mahalle 1" },
                    { 388, 194, "Mahalle 2" },
                    { 389, 195, "Mahalle 1" },
                    { 390, 195, "Mahalle 2" },
                    { 391, 196, "Mahalle 1" },
                    { 392, 196, "Mahalle 2" },
                    { 393, 197, "Mahalle 1" },
                    { 394, 197, "Mahalle 2" },
                    { 395, 198, "Mahalle 1" },
                    { 396, 198, "Mahalle 2" },
                    { 397, 199, "Mahalle 1" },
                    { 398, 199, "Mahalle 2" },
                    { 399, 200, "Mahalle 1" },
                    { 400, 200, "Mahalle 2" },
                    { 401, 201, "Mahalle 1" },
                    { 402, 201, "Mahalle 2" },
                    { 403, 202, "Mahalle 1" },
                    { 404, 202, "Mahalle 2" },
                    { 405, 203, "Mahalle 1" },
                    { 406, 203, "Mahalle 2" },
                    { 407, 204, "Mahalle 1" },
                    { 408, 204, "Mahalle 2" },
                    { 409, 205, "Mahalle 1" },
                    { 410, 205, "Mahalle 2" },
                    { 411, 206, "Mahalle 1" },
                    { 412, 206, "Mahalle 2" },
                    { 413, 207, "Mahalle 1" },
                    { 414, 207, "Mahalle 2" },
                    { 415, 208, "Mahalle 1" },
                    { 416, 208, "Mahalle 2" },
                    { 417, 209, "Mahalle 1" },
                    { 418, 209, "Mahalle 2" },
                    { 419, 210, "Mahalle 1" },
                    { 420, 210, "Mahalle 2" },
                    { 421, 211, "Mahalle 1" },
                    { 422, 211, "Mahalle 2" },
                    { 423, 212, "Mahalle 1" },
                    { 424, 212, "Mahalle 2" },
                    { 425, 213, "Mahalle 1" },
                    { 426, 213, "Mahalle 2" },
                    { 427, 214, "Mahalle 1" },
                    { 428, 214, "Mahalle 2" },
                    { 429, 215, "Mahalle 1" },
                    { 430, 215, "Mahalle 2" },
                    { 431, 216, "Mahalle 1" },
                    { 432, 216, "Mahalle 2" },
                    { 433, 217, "Mahalle 1" },
                    { 434, 217, "Mahalle 2" },
                    { 435, 218, "Mahalle 1" },
                    { 436, 218, "Mahalle 2" },
                    { 437, 219, "Mahalle 1" },
                    { 438, 219, "Mahalle 2" },
                    { 439, 220, "Mahalle 1" },
                    { 440, 220, "Mahalle 2" },
                    { 441, 221, "Mahalle 1" },
                    { 442, 221, "Mahalle 2" },
                    { 443, 222, "Mahalle 1" },
                    { 444, 222, "Mahalle 2" },
                    { 445, 223, "Mahalle 1" },
                    { 446, 223, "Mahalle 2" },
                    { 447, 224, "Mahalle 1" },
                    { 448, 224, "Mahalle 2" },
                    { 449, 225, "Mahalle 1" },
                    { 450, 225, "Mahalle 2" },
                    { 451, 226, "Mahalle 1" },
                    { 452, 226, "Mahalle 2" },
                    { 453, 227, "Mahalle 1" },
                    { 454, 227, "Mahalle 2" },
                    { 455, 228, "Mahalle 1" },
                    { 456, 228, "Mahalle 2" },
                    { 457, 229, "Mahalle 1" },
                    { 458, 229, "Mahalle 2" },
                    { 459, 230, "Mahalle 1" },
                    { 460, 230, "Mahalle 2" },
                    { 461, 231, "Mahalle 1" },
                    { 462, 231, "Mahalle 2" },
                    { 463, 232, "Mahalle 1" },
                    { 464, 232, "Mahalle 2" },
                    { 465, 233, "Mahalle 1" },
                    { 466, 233, "Mahalle 2" },
                    { 467, 234, "Mahalle 1" },
                    { 468, 234, "Mahalle 2" },
                    { 469, 235, "Mahalle 1" },
                    { 470, 235, "Mahalle 2" },
                    { 471, 236, "Mahalle 1" },
                    { 472, 236, "Mahalle 2" },
                    { 473, 237, "Mahalle 1" },
                    { 474, 237, "Mahalle 2" },
                    { 475, 238, "Mahalle 1" },
                    { 476, 238, "Mahalle 2" },
                    { 477, 239, "Mahalle 1" },
                    { 478, 239, "Mahalle 2" },
                    { 479, 240, "Mahalle 1" },
                    { 480, 240, "Mahalle 2" },
                    { 481, 241, "Mahalle 1" },
                    { 482, 241, "Mahalle 2" },
                    { 483, 242, "Mahalle 1" },
                    { 484, 242, "Mahalle 2" },
                    { 485, 243, "Mahalle 1" },
                    { 486, 243, "Mahalle 2" }
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
                name: "IX_FiyatlandirmaTarifeler_TarifeTuru_Aktif_Gecerlilik",
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeTuru", "Aktif", "GecerlilikBaslangic" });

            migrationBuilder.CreateIndex(
                name: "IX_FiyatlandirmaTarifeler_TarifeTuru_DegerAraligi",
                table: "FiyatlandirmaTarifeler",
                columns: new[] { "TarifeTuru", "MinDeger", "MaxDeger" });

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
                name: "FiyatlandirmaTarifeler");

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
