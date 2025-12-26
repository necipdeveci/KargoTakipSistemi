using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace kargotakipsistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
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
                    TeslimatTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                columns: new[] { "TarifeId", "Aciklama", "Aktif", "Birim", "Deger", "GecerlilikBaslangic", "GecerlilikBitis", "GuncellemeTarihi", "MaxDeger", "MinDeger", "OlusturulmaTarihi", "Oncelik", "TarifeAdi", "TarifeTuru", "TeslimatTipi" },
                values: new object[,]
                {
                    { 1, "0-30 kg arası standart ağırlık tarifesi. Genel kargo gönderileri için geçerlidir.", true, "TL/kg", 15.50m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 30.0m, 0.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Standart Ağırlık Tarifesi (0-30 kg)", "AgirlikTarife", null },
                    { 2, "0.5 m³ üzeri hacimli paketler için ek ücret uygulanır.", true, "TL", 75.00m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 0.5m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hacim Eşik Tarifesi (0.5 m³ üzeri)", "HacimEkUcret", null },
                    { 3, "Normal teslimat süresi, ek ücret uygulanmaz (x1.0)", true, "Çarpan", 1.0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Standart Teslimat (3-5 İş Günü)", "TeslimatCarpan", "Standart Teslimat" },
                    { 4, "Hızlandırılmış teslimat, %35 ek ücret (x1.35)", true, "Çarpan", 1.35m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Hızlı Teslimat (1-2 İş Günü)", "TeslimatCarpan", "Hızlı Teslimat" },
                    { 5, "Aynı gün içinde teslimat, %75 ek ücret (x1.75)", true, "Çarpan", 1.75m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Aynı Gün Teslimat", "TeslimatCarpan", "Aynı Gün" },
                    { 6, "Randevulu teslimat hizmeti, %25 ek ücret (x1.25)", true, "Çarpan", 1.25m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Randevulu Teslimat", "TeslimatCarpan", "Randevulu Teslimat" }
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
                table: "Musteriler",
                columns: new[] { "MusteriId", "Ad", "DogumTarihi", "Mail", "Notlar", "Soyad", "Tel" },
                values: new object[,]
                {
                    { 1, "Kemal", new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "kemal.yilmaz@email.com", "VIP müşteri, hızlı teslimat tercih eder", "Yılmaz", "05301111111" },
                    { 2, "Selin", new DateTime(1988, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "selin.demir@email.com", "Kurumsal müşteri, toplu gönderi yapar", "Demir", "05302222222" },
                    { 3, "Barış", new DateTime(1995, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "baris.kaya@email.com", "Düzenli müşteri, aylık ortalama 10 gönderi", "Kaya", "05303333333" },
                    { 4, "Aylin", new DateTime(1992, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "aylin.celik@email.com", "E-ticaret müşterisi, standart teslimat tercih eder", "Çelik", "05304444444" },
                    { 5, "Cem", new DateTime(1987, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "cem.arslan@email.com", "Yeni müşteri, ilk gönderi deneyimi", "Arslan", "05305555555" },
                    { 6, "Deniz", new DateTime(1993, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "deniz.polat@email.com", "Hassas ürün gönderileri, ekstra özen gerektirir", "Polat", "05306666666" },
                    { 7, "Ebru", new DateTime(1991, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "ebru.yildiz@email.com", "Randevulu teslimat tercih eder", "Yıldız", "05307777777" },
                    { 8, "Furkan", new DateTime(1989, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "furkan.ozkan@email.com", "Kurumsal anlaşma var, indirimli fiyat uygulanır", "Özkan", "05308888888" }
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

            migrationBuilder.InsertData(
                table: "Subeler",
                columns: new[] { "SubeId", "AcikAdres", "CalismaSaatleri", "Email", "IlId", "IlceId", "Kapasite", "SubeAd", "SubeTip", "Tel" },
                values: new object[,]
                {
                    { 1, "Atatürk Mahallesi, Cumhuriyet Caddesi No:123 Kat:5 D:12", "24 Saat Açık", "istanbul@kargosistemi.com", 34, 100, 5000, "İstanbul Merkez Şube", "Merkez", "02121234567" },
                    { 2, "Kızılay Mahallesi, İnönü Bulvarı No:456 Kat:3", "08:00 - 17:00", "ankara@kargosistemi.com", 6, 16, 3000, "Ankara Bölge Şube", "Şube", "03129876543" },
                    { 3, "Alsancak Mahallesi, Kordon Sokak No:789", "09:00 - 18:00", "izmir@kargosistemi.com", 35, 103, 2000, "İzmir Liman Şube", "Dağıtım Noktası", "02325554433" }
                });

            migrationBuilder.InsertData(
                table: "Adresler",
                columns: new[] { "AdresId", "AcikAdres", "AdresBaslik", "AdresTipi", "Aktif", "BinaAdi", "Daire", "EkAciklama", "IlId", "IlceId", "KapiNo", "Kat", "MahalleId", "MusteriId", "PersonelId", "PostaKodu" },
                values: new object[,]
                {
                    { 53, "Kemal Mahallesi, Yılmaz Sokak No:10", "Kemal Ev Adresi", "Ev", true, "Yılmaz Konakları", "1", "Kemal Yılmaz ev adresi", 34, 100, "10", "2", 199, 1, null, "340010" },
                    { 54, "İş Merkezi, Yılmaz Plaza Kat:1 No:5", "Kemal İş Adresi", "Is", true, "Yılmaz Plaza", "10", "Kemal Yılmaz iş adresi", 6, 16, "5", "1", 31, 1, null, "60011" },
                    { 55, "Selin Mahallesi, Demir Sokak No:20", "Selin Ev Adresi", "Ev", true, "Demir Konakları", "2", "Selin Demir ev adresi", 6, 16, "20", "3", 31, 2, null, "60020" },
                    { 56, "İş Merkezi, Demir Plaza Kat:2 No:10", "Selin İş Adresi", "Is", true, "Demir Plaza", "20", "Selin Demir iş adresi", 35, 103, "10", "2", 205, 2, null, "350021" },
                    { 57, "Barış Mahallesi, Kaya Sokak No:30", "Barış Ev Adresi", "Ev", true, "Kaya Konakları", "3", "Barış Kaya ev adresi", 35, 103, "30", "4", 205, 3, null, "350030" },
                    { 58, "İş Merkezi, Kaya Plaza Kat:3 No:15", "Barış İş Adresi", "Is", true, "Kaya Plaza", "30", "Barış Kaya iş adresi", 34, 100, "15", "3", 199, 3, null, "340031" },
                    { 59, "Aylin Mahallesi, Çelik Sokak No:40", "Aylin Ev Adresi", "Ev", true, "Çelik Konakları", "4", "Aylin Çelik ev adresi", 34, 100, "40", "5", 199, 4, null, "340040" },
                    { 60, "İş Merkezi, Çelik Plaza Kat:4 No:20", "Aylin İş Adresi", "Is", true, "Çelik Plaza", "40", "Aylin Çelik iş adresi", 6, 16, "20", "4", 31, 4, null, "60041" },
                    { 61, "Cem Mahallesi, Arslan Sokak No:50", "Cem Ev Adresi", "Ev", true, "Arslan Konakları", "5", "Cem Arslan ev adresi", 6, 16, "50", "1", 31, 5, null, "60050" },
                    { 62, "İş Merkezi, Arslan Plaza Kat:5 No:25", "Cem İş Adresi", "Is", true, "Arslan Plaza", "50", "Cem Arslan iş adresi", 35, 103, "25", "5", 205, 5, null, "350051" },
                    { 63, "Deniz Mahallesi, Polat Sokak No:60", "Deniz Ev Adresi", "Ev", true, "Polat Konakları", "6", "Deniz Polat ev adresi", 35, 103, "60", "2", 205, 6, null, "350060" },
                    { 64, "İş Merkezi, Polat Plaza Kat:6 No:30", "Deniz İş Adresi", "Is", true, "Polat Plaza", "60", "Deniz Polat iş adresi", 34, 100, "30", "6", 199, 6, null, "340061" },
                    { 65, "Ebru Mahallesi, Yıldız Sokak No:70", "Ebru Ev Adresi", "Ev", true, "Yıldız Konakları", "7", "Ebru Yıldız ev adresi", 34, 100, "70", "3", 199, 7, null, "340070" },
                    { 66, "İş Merkezi, Yıldız Plaza Kat:7 No:35", "Ebru İş Adresi", "Is", true, "Yıldız Plaza", "70", "Ebru Yıldız iş adresi", 6, 16, "35", "7", 31, 7, null, "60071" },
                    { 67, "Furkan Mahallesi, Özkan Sokak No:80", "Furkan Ev Adresi", "Ev", true, "Özkan Konakları", "8", "Furkan Özkan ev adresi", 6, 16, "80", "4", 31, 8, null, "60080" },
                    { 68, "İş Merkezi, Özkan Plaza Kat:8 No:40", "Furkan İş Adresi", "Is", true, "Özkan Plaza", "80", "Furkan Özkan iş adresi", 35, 103, "40", "8", 205, 8, null, "350081" }
                });

            migrationBuilder.InsertData(
                table: "Araclar",
                columns: new[] { "AracId", "AracTip", "Durum", "GpsKodu", "KapasiteKg", "Plaka", "SubeId" },
                values: new object[,]
                {
                    { 1, "Kamyonet", "Aktif", "GPS010002", 1500m, "31ABC102", 1 },
                    { 2, "Panelvan", "Aktif", "GPS010003", 800m, "31ABC103", 1 },
                    { 3, "Tır", "Aktif", "GPS010004", 5000m, "31ABC104", 1 },
                    { 4, "Minibüs", "Aktif", "GPS010005", 1200m, "31ABC105", 1 },
                    { 5, "Otomobil", "Aktif", "GPS010006", 400m, "31ABC106", 1 },
                    { 6, "Motosiklet", "Aktif", "GPS010007", 50m, "31ABC107", 1 },
                    { 7, "Kamyonet", "Aktif", "GPS020008", 1500m, "32ABC108", 2 },
                    { 8, "Panelvan", "Aktif", "GPS020009", 800m, "32ABC109", 2 },
                    { 9, "Tır", "Aktif", "GPS020010", 5000m, "32ABC110", 2 },
                    { 10, "Minibüs", "Aktif", "GPS020011", 1200m, "32ABC111", 2 },
                    { 11, "Otomobil", "Aktif", "GPS020012", 400m, "32ABC112", 2 },
                    { 12, "Motosiklet", "Aktif", "GPS020013", 50m, "32ABC113", 2 },
                    { 13, "Kamyonet", "Aktif", "GPS030014", 1500m, "33ABC114", 3 },
                    { 14, "Panelvan", "Aktif", "GPS030015", 800m, "33ABC115", 3 },
                    { 15, "Tır", "Aktif", "GPS030016", 5000m, "33ABC116", 3 },
                    { 16, "Minibüs", "Aktif", "GPS030017", 1200m, "33ABC117", 3 },
                    { 17, "Otomobil", "Aktif", "GPS030018", 400m, "33ABC118", 3 },
                    { 18, "Motosiklet", "Aktif", "GPS030019", 50m, "33ABC119", 3 }
                });

            migrationBuilder.InsertData(
                table: "Personeller",
                columns: new[] { "PersonelId", "Ad", "Aktif", "AracId", "Cinsiyet", "DogumTarihi", "EhliyetSinifi", "IseGirisTarihi", "IstenCikisTarihi", "Maas", "Mail", "RolId", "Sifre", "Soyad", "SubeId", "Tel" },
                values: new object[,]
                {
                    { 1, "Admin", true, null, "Erkek", new DateTime(1985, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 25000m, "admin@com", 1, "1234", "Sistem Yöneticisi", 1, "05551234567" },
                    { 2, "Mehmet", true, null, "Kadın", new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9000m, "mehmet.yılmaz1@kargosistemi.com", 2, "1234", "Yılmaz 1", 1, "05551000003" },
                    { 3, "Ayşe", true, null, "Erkek", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9500m, "ayşe.demir1@kargosistemi.com", 3, "1234", "Demir 1", 1, "05551000004" },
                    { 4, "Fatma", true, null, "Kadın", new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10000m, "fatma.çelik1@kargosistemi.com", 4, "1234", "Çelik 1", 1, "05551000005" },
                    { 8, "Elif", true, null, "Kadın", new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 12000m, "elif.yıldız1@kargosistemi.com", 8, "1234", "Yıldız 1", 1, "05551000009" },
                    { 9, "Mustafa", true, null, "Erkek", new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 12500m, "mustafa.aydın1@kargosistemi.com", 9, "1234", "Aydın 1", 1, "05551000010" },
                    { 10, "Emine", true, null, "Kadın", new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13000m, "emine.şahin1@kargosistemi.com", 10, "1234", "Şahin 1", 1, "05551000011" },
                    { 11, "Hasan", true, null, "Erkek", new DateTime(1982, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13500m, "hasan.koç1@kargosistemi.com", 11, "1234", "Koç 1", 1, "05551000012" },
                    { 12, "Merve", true, null, "Kadın", new DateTime(1981, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14000m, "merve.kurt1@kargosistemi.com", 12, "1234", "Kurt 1", 1, "05551000013" },
                    { 13, "İbrahim", true, null, "Erkek", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14500m, "ibrahim.özdemir1@kargosistemi.com", 13, "1234", "Özdemir 1", 1, "05551000014" },
                    { 14, "Selin", true, null, "Kadın", new DateTime(1979, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15000m, "selin.çetin1@kargosistemi.com", 14, "1234", "Çetin 1", 1, "05551000015" },
                    { 15, "Burak", true, null, "Erkek", new DateTime(1978, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15500m, "burak.yavuz1@kargosistemi.com", 15, "1234", "Yavuz 1", 1, "05551000016" },
                    { 16, "Gamze", true, null, "Kadın", new DateTime(1977, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16000m, "gamze.özkan1@kargosistemi.com", 16, "1234", "Özkan 1", 1, "05551000017" },
                    { 17, "Emre", true, null, "Erkek", new DateTime(1976, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16500m, "emre.polat1@kargosistemi.com", 17, "1234", "Polat 1", 1, "05551000018" },
                    { 18, "Deniz", true, null, "Kadın", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 17000m, "deniz.şimşek1@kargosistemi.com", 18, "1234", "Şimşek 1", 1, "05551000019" },
                    { 19, "Can", true, null, "Erkek", new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9000m, "can.türk2@kargosistemi.com", 2, "1234", "Türk 2", 2, "05551000020" },
                    { 20, "Mehmet", true, null, "Kadın", new DateTime(1993, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9500m, "mehmet.yılmaz2@kargosistemi.com", 3, "1234", "Yılmaz 2", 2, "05551000021" },
                    { 21, "Ayşe", true, null, "Erkek", new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10000m, "ayşe.demir2@kargosistemi.com", 4, "1234", "Demir 2", 2, "05551000022" },
                    { 25, "Ahmet", true, null, "Erkek", new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 12000m, "ahmet.öztürk2@kargosistemi.com", 8, "1234", "Öztürk 2", 2, "05551000026" },
                    { 26, "Elif", true, null, "Kadın", new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 12500m, "elif.yıldız2@kargosistemi.com", 9, "1234", "Yıldız 2", 2, "05551000027" },
                    { 27, "Mustafa", true, null, "Erkek", new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13000m, "mustafa.aydın2@kargosistemi.com", 10, "1234", "Aydın 2", 2, "05551000028" },
                    { 28, "Emine", true, null, "Kadın", new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13500m, "emine.şahin2@kargosistemi.com", 11, "1234", "Şahin 2", 2, "05551000029" },
                    { 29, "Hasan", true, null, "Erkek", new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14000m, "hasan.koç2@kargosistemi.com", 12, "1234", "Koç 2", 2, "05551000030" },
                    { 30, "Merve", true, null, "Kadın", new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14500m, "merve.kurt2@kargosistemi.com", 13, "1234", "Kurt 2", 2, "05551000031" },
                    { 31, "İbrahim", true, null, "Erkek", new DateTime(1982, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15000m, "ibrahim.özdemir2@kargosistemi.com", 14, "1234", "Özdemir 2", 2, "05551000032" },
                    { 32, "Selin", true, null, "Kadın", new DateTime(1981, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15500m, "selin.çetin2@kargosistemi.com", 15, "1234", "Çetin 2", 2, "05551000033" },
                    { 33, "Burak", true, null, "Erkek", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16000m, "burak.yavuz2@kargosistemi.com", 16, "1234", "Yavuz 2", 2, "05551000034" },
                    { 34, "Gamze", true, null, "Kadın", new DateTime(1979, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16500m, "gamze.özkan2@kargosistemi.com", 17, "1234", "Özkan 2", 2, "05551000035" },
                    { 35, "Emre", true, null, "Erkek", new DateTime(1978, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 17000m, "emre.polat2@kargosistemi.com", 18, "1234", "Polat 2", 2, "05551000036" },
                    { 36, "Deniz", true, null, "Kadın", new DateTime(1977, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9000m, "deniz.şimşek3@kargosistemi.com", 2, "1234", "Şimşek 3", 3, "05551000037" },
                    { 37, "Can", true, null, "Erkek", new DateTime(1976, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9500m, "can.türk3@kargosistemi.com", 3, "1234", "Türk 3", 3, "05551000038" },
                    { 38, "Mehmet", true, null, "Kadın", new DateTime(1975, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10000m, "mehmet.yılmaz3@kargosistemi.com", 4, "1234", "Yılmaz 3", 3, "05551000039" },
                    { 42, "Zeynep", true, null, "Kadın", new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 12000m, "zeynep.arslan3@kargosistemi.com", 8, "1234", "Arslan 3", 3, "05551000043" },
                    { 43, "Ahmet", true, null, "Erkek", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 12500m, "ahmet.öztürk3@kargosistemi.com", 9, "1234", "Öztürk 3", 3, "05551000044" },
                    { 44, "Elif", true, null, "Kadın", new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13000m, "elif.yıldız3@kargosistemi.com", 10, "1234", "Yıldız 3", 3, "05551000045" },
                    { 45, "Mustafa", true, null, "Erkek", new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13500m, "mustafa.aydın3@kargosistemi.com", 11, "1234", "Aydın 3", 3, "05551000046" },
                    { 46, "Emine", true, null, "Kadın", new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14000m, "emine.şahin3@kargosistemi.com", 12, "1234", "Şahin 3", 3, "05551000047" },
                    { 47, "Hasan", true, null, "Erkek", new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14500m, "hasan.koç3@kargosistemi.com", 13, "1234", "Koç 3", 3, "05551000048" },
                    { 48, "Merve", true, null, "Kadın", new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15000m, "merve.kurt3@kargosistemi.com", 14, "1234", "Kurt 3", 3, "05551000049" },
                    { 49, "İbrahim", true, null, "Erkek", new DateTime(1984, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 15500m, "ibrahim.özdemir3@kargosistemi.com", 15, "1234", "Özdemir 3", 3, "05551000050" },
                    { 50, "Selin", true, null, "Kadın", new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16000m, "selin.çetin3@kargosistemi.com", 16, "1234", "Çetin 3", 3, "05551000051" },
                    { 51, "Burak", true, null, "Erkek", new DateTime(1982, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16500m, "burak.yavuz3@kargosistemi.com", 17, "1234", "Yavuz 3", 3, "05551000052" },
                    { 52, "Gamze", true, null, "Kadın", new DateTime(1981, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yok", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 17000m, "gamze.özkan3@kargosistemi.com", 18, "1234", "Özkan 3", 3, "05551000053" }
                });

            migrationBuilder.InsertData(
                table: "Adresler",
                columns: new[] { "AdresId", "AcikAdres", "AdresBaslik", "AdresTipi", "Aktif", "BinaAdi", "Daire", "EkAciklama", "IlId", "IlceId", "KapiNo", "Kat", "MahalleId", "MusteriId", "PersonelId", "PostaKodu" },
                values: new object[,]
                {
                    { 1, "Merkez Mahallesi, Ana Cadde No:1", "Admin Ev Adresi", "Ev", true, "Merkez Apartmanı", "12", "Sistem yöneticisi ev adresi", 34, 100, "1", "5", 199, null, 1, "34000" },
                    { 2, "Mehmet Mahallesi, Yılmaz 1 Sokak No:2", "Mehmet Ev Adresi", "Ev", true, "Yılmaz 1 Apt", "3", "Mehmet Yılmaz 1 ev adresi", 34, 100, "2", "3", 199, null, 2, "34000" },
                    { 3, "Ayşe Mahallesi, Demir 1 Sokak No:3", "Ayşe Ev Adresi", "Ev", true, "Demir 1 Apt", "4", "Ayşe Demir 1 ev adresi", 34, 100, "3", "4", 199, null, 3, "34000" },
                    { 4, "Fatma Mahallesi, Çelik 1 Sokak No:4", "Fatma Ev Adresi", "Ev", true, "Çelik 1 Apt", "5", "Fatma Çelik 1 ev adresi", 34, 100, "4", "5", 199, null, 4, "34000" },
                    { 8, "Elif Mahallesi, Yıldız 1 Sokak No:8", "Elif Ev Adresi", "Ev", true, "Yıldız 1 Apt", "9", "Elif Yıldız 1 ev adresi", 34, 100, "8", "9", 199, null, 8, "34000" },
                    { 9, "Mustafa Mahallesi, Aydın 1 Sokak No:9", "Mustafa Ev Adresi", "Ev", true, "Aydın 1 Apt", "10", "Mustafa Aydın 1 ev adresi", 34, 100, "9", "10", 199, null, 9, "34000" },
                    { 10, "Emine Mahallesi, Şahin 1 Sokak No:10", "Emine Ev Adresi", "Ev", true, "Şahin 1 Apt", "11", "Emine Şahin 1 ev adresi", 34, 100, "10", "1", 199, null, 10, "34000" },
                    { 11, "Hasan Mahallesi, Koç 1 Sokak No:11", "Hasan Ev Adresi", "Ev", true, "Koç 1 Apt", "12", "Hasan Koç 1 ev adresi", 34, 100, "11", "2", 199, null, 11, "34000" },
                    { 12, "Merve Mahallesi, Kurt 1 Sokak No:12", "Merve Ev Adresi", "Ev", true, "Kurt 1 Apt", "13", "Merve Kurt 1 ev adresi", 34, 100, "12", "3", 199, null, 12, "34000" },
                    { 13, "İbrahim Mahallesi, Özdemir 1 Sokak No:13", "İbrahim Ev Adresi", "Ev", true, "Özdemir 1 Apt", "14", "İbrahim Özdemir 1 ev adresi", 34, 100, "13", "4", 199, null, 13, "34000" },
                    { 14, "Selin Mahallesi, Çetin 1 Sokak No:14", "Selin Ev Adresi", "Ev", true, "Çetin 1 Apt", "15", "Selin Çetin 1 ev adresi", 34, 100, "14", "5", 199, null, 14, "34000" },
                    { 15, "Burak Mahallesi, Yavuz 1 Sokak No:15", "Burak Ev Adresi", "Ev", true, "Yavuz 1 Apt", "16", "Burak Yavuz 1 ev adresi", 34, 100, "15", "6", 199, null, 15, "34000" },
                    { 16, "Gamze Mahallesi, Özkan 1 Sokak No:16", "Gamze Ev Adresi", "Ev", true, "Özkan 1 Apt", "17", "Gamze Özkan 1 ev adresi", 34, 100, "16", "7", 199, null, 16, "34000" },
                    { 17, "Emre Mahallesi, Polat 1 Sokak No:17", "Emre Ev Adresi", "Ev", true, "Polat 1 Apt", "18", "Emre Polat 1 ev adresi", 34, 100, "17", "8", 199, null, 17, "34000" },
                    { 18, "Deniz Mahallesi, Şimşek 1 Sokak No:18", "Deniz Ev Adresi", "Ev", true, "Şimşek 1 Apt", "19", "Deniz Şimşek 1 ev adresi", 34, 100, "18", "9", 199, null, 18, "34000" },
                    { 19, "Can Mahallesi, Türk 2 Sokak No:19", "Can Ev Adresi", "Ev", true, "Türk 2 Apt", "20", "Can Türk 2 ev adresi", 6, 16, "19", "10", 31, null, 19, "6000" },
                    { 20, "Mehmet Mahallesi, Yılmaz 2 Sokak No:20", "Mehmet Ev Adresi", "Ev", true, "Yılmaz 2 Apt", "1", "Mehmet Yılmaz 2 ev adresi", 6, 16, "20", "1", 31, null, 20, "6000" },
                    { 21, "Ayşe Mahallesi, Demir 2 Sokak No:21", "Ayşe Ev Adresi", "Ev", true, "Demir 2 Apt", "2", "Ayşe Demir 2 ev adresi", 6, 16, "21", "2", 31, null, 21, "6000" },
                    { 25, "Ahmet Mahallesi, Öztürk 2 Sokak No:25", "Ahmet Ev Adresi", "Ev", true, "Öztürk 2 Apt", "6", "Ahmet Öztürk 2 ev adresi", 6, 16, "25", "6", 31, null, 25, "6000" },
                    { 26, "Elif Mahallesi, Yıldız 2 Sokak No:26", "Elif Ev Adresi", "Ev", true, "Yıldız 2 Apt", "7", "Elif Yıldız 2 ev adresi", 6, 16, "26", "7", 31, null, 26, "6000" },
                    { 27, "Mustafa Mahallesi, Aydın 2 Sokak No:27", "Mustafa Ev Adresi", "Ev", true, "Aydın 2 Apt", "8", "Mustafa Aydın 2 ev adresi", 6, 16, "27", "8", 31, null, 27, "6000" },
                    { 28, "Emine Mahallesi, Şahin 2 Sokak No:28", "Emine Ev Adresi", "Ev", true, "Şahin 2 Apt", "9", "Emine Şahin 2 ev adresi", 6, 16, "28", "9", 31, null, 28, "6000" },
                    { 29, "Hasan Mahallesi, Koç 2 Sokak No:29", "Hasan Ev Adresi", "Ev", true, "Koç 2 Apt", "10", "Hasan Koç 2 ev adresi", 6, 16, "29", "10", 31, null, 29, "6000" },
                    { 30, "Merve Mahallesi, Kurt 2 Sokak No:30", "Merve Ev Adresi", "Ev", true, "Kurt 2 Apt", "11", "Merve Kurt 2 ev adresi", 6, 16, "30", "1", 31, null, 30, "6000" },
                    { 31, "İbrahim Mahallesi, Özdemir 2 Sokak No:31", "İbrahim Ev Adresi", "Ev", true, "Özdemir 2 Apt", "12", "İbrahim Özdemir 2 ev adresi", 6, 16, "31", "2", 31, null, 31, "6000" },
                    { 32, "Selin Mahallesi, Çetin 2 Sokak No:32", "Selin Ev Adresi", "Ev", true, "Çetin 2 Apt", "13", "Selin Çetin 2 ev adresi", 6, 16, "32", "3", 31, null, 32, "6000" },
                    { 33, "Burak Mahallesi, Yavuz 2 Sokak No:33", "Burak Ev Adresi", "Ev", true, "Yavuz 2 Apt", "14", "Burak Yavuz 2 ev adresi", 6, 16, "33", "4", 31, null, 33, "6000" },
                    { 34, "Gamze Mahallesi, Özkan 2 Sokak No:34", "Gamze Ev Adresi", "Ev", true, "Özkan 2 Apt", "15", "Gamze Özkan 2 ev adresi", 6, 16, "34", "5", 31, null, 34, "6000" },
                    { 35, "Emre Mahallesi, Polat 2 Sokak No:35", "Emre Ev Adresi", "Ev", true, "Polat 2 Apt", "16", "Emre Polat 2 ev adresi", 6, 16, "35", "6", 31, null, 35, "6000" },
                    { 36, "Deniz Mahallesi, Şimşek 3 Sokak No:36", "Deniz Ev Adresi", "Ev", true, "Şimşek 3 Apt", "17", "Deniz Şimşek 3 ev adresi", 35, 103, "36", "7", 205, null, 36, "35000" },
                    { 37, "Can Mahallesi, Türk 3 Sokak No:37", "Can Ev Adresi", "Ev", true, "Türk 3 Apt", "18", "Can Türk 3 ev adresi", 35, 103, "37", "8", 205, null, 37, "35000" },
                    { 38, "Mehmet Mahallesi, Yılmaz 3 Sokak No:38", "Mehmet Ev Adresi", "Ev", true, "Yılmaz 3 Apt", "19", "Mehmet Yılmaz 3 ev adresi", 35, 103, "38", "9", 205, null, 38, "35000" },
                    { 42, "Zeynep Mahallesi, Arslan 3 Sokak No:42", "Zeynep Ev Adresi", "Ev", true, "Arslan 3 Apt", "3", "Zeynep Arslan 3 ev adresi", 35, 103, "42", "3", 205, null, 42, "35000" },
                    { 43, "Ahmet Mahallesi, Öztürk 3 Sokak No:43", "Ahmet Ev Adresi", "Ev", true, "Öztürk 3 Apt", "4", "Ahmet Öztürk 3 ev adresi", 35, 103, "43", "4", 205, null, 43, "35000" },
                    { 44, "Elif Mahallesi, Yıldız 3 Sokak No:44", "Elif Ev Adresi", "Ev", true, "Yıldız 3 Apt", "5", "Elif Yıldız 3 ev adresi", 35, 103, "44", "5", 205, null, 44, "35000" },
                    { 45, "Mustafa Mahallesi, Aydın 3 Sokak No:45", "Mustafa Ev Adresi", "Ev", true, "Aydın 3 Apt", "6", "Mustafa Aydın 3 ev adresi", 35, 103, "45", "6", 205, null, 45, "35000" },
                    { 46, "Emine Mahallesi, Şahin 3 Sokak No:46", "Emine Ev Adresi", "Ev", true, "Şahin 3 Apt", "7", "Emine Şahin 3 ev adresi", 35, 103, "46", "7", 205, null, 46, "35000" },
                    { 47, "Hasan Mahallesi, Koç 3 Sokak No:47", "Hasan Ev Adresi", "Ev", true, "Koç 3 Apt", "8", "Hasan Koç 3 ev adresi", 35, 103, "47", "8", 205, null, 47, "35000" },
                    { 48, "Merve Mahallesi, Kurt 3 Sokak No:48", "Merve Ev Adresi", "Ev", true, "Kurt 3 Apt", "9", "Merve Kurt 3 ev adresi", 35, 103, "48", "9", 205, null, 48, "35000" },
                    { 49, "İbrahim Mahallesi, Özdemir 3 Sokak No:49", "İbrahim Ev Adresi", "Ev", true, "Özdemir 3 Apt", "10", "İbrahim Özdemir 3 ev adresi", 35, 103, "49", "10", 205, null, 49, "35000" },
                    { 50, "Selin Mahallesi, Çetin 3 Sokak No:50", "Selin Ev Adresi", "Ev", true, "Çetin 3 Apt", "11", "Selin Çetin 3 ev adresi", 35, 103, "50", "1", 205, null, 50, "35000" },
                    { 51, "Burak Mahallesi, Yavuz 3 Sokak No:51", "Burak Ev Adresi", "Ev", true, "Yavuz 3 Apt", "12", "Burak Yavuz 3 ev adresi", 35, 103, "51", "2", 205, null, 51, "35000" },
                    { 52, "Gamze Mahallesi, Özkan 3 Sokak No:52", "Gamze Ev Adresi", "Ev", true, "Özkan 3 Apt", "13", "Gamze Özkan 3 ev adresi", 35, 103, "52", "3", 205, null, 52, "35000" }
                });

            migrationBuilder.InsertData(
                table: "Gonderiler",
                columns: new[] { "GonderiId", "Agirlik", "AliciAdresId", "AliciId", "Boyut", "EkMasraf", "GonderenAdresId", "GonderenId", "GonderiTarihi", "GuncellemeTarihi", "IadeDurumu", "IndirimTutar", "IptalTarihi", "KayitTarihi", "KuryeId", "TahminiTeslimTarihi", "TakipNo", "TeslimEdilenKisi", "TeslimTarihi", "TeslimatTipi", "Ucret" },
                values: new object[,]
                {
                    { 1, 3.5m, 54, 1, "21x16x11", 0m, 53, 1, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000001", "", null, "Hızlı Teslimat", 60m },
                    { 2, 5.5m, 55, 2, "27x20x14", 0m, 53, 1, new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000002", "", null, "Aynı Gün", 91m },
                    { 3, 6.5m, 57, 3, "28x21x15", 0m, 53, 1, new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000003", "", null, "Randevulu Teslimat", 99m },
                    { 4, 7.5m, 59, 4, "29x22x16", 0m, 53, 1, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000004", "", null, "Standart Teslimat", 107m },
                    { 5, 8.5m, 61, 5, "30x23x17", 0m, 53, 1, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000005", "", null, "Hızlı Teslimat", 115m },
                    { 6, 9.5m, 63, 6, "31x24x18", 25m, 53, 1, new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000006", "", null, "Aynı Gün", 123m },
                    { 7, 10.5m, 65, 7, "32x25x19", 0m, 53, 1, new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000007", "", null, "Randevulu Teslimat", 131m },
                    { 8, 11.5m, 67, 8, "33x26x20", 0m, 53, 1, new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000008", "", null, "Standart Teslimat", 139m },
                    { 9, 11.5m, 56, 2, "29x24x19", 0m, 55, 2, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 10m, null, new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000009", "", null, "Hızlı Teslimat", 140m },
                    { 10, 13.5m, 53, 1, "35x28x22", 0m, 55, 2, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000010", "", null, "Aynı Gün", 155m },
                    { 11, 14.5m, 57, 3, "36x29x23", 0m, 55, 2, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000011", "", null, "Randevulu Teslimat", 163m },
                    { 12, 15.5m, 59, 4, "37x30x24", 25m, 55, 2, new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000012", "", null, "Standart Teslimat", 171m },
                    { 13, 16.5m, 61, 5, "38x31x25", 0m, 55, 2, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000013", "", null, "Hızlı Teslimat", 179m },
                    { 14, 17.5m, 63, 6, "39x32x26", 0m, 55, 2, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000014", "", null, "Aynı Gün", 187m },
                    { 15, 3.5m, 65, 7, "40x33x27", 0m, 55, 2, new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000015", "", null, "Randevulu Teslimat", 195m },
                    { 16, 4.5m, 67, 8, "41x34x28", 0m, 55, 2, new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000016", "", null, "Standart Teslimat", 203m },
                    { 17, 9.5m, 58, 3, "37x32x27", 0m, 57, 3, new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000017", "", null, "Hızlı Teslimat", 220m },
                    { 18, 6.5m, 53, 1, "43x36x30", 25m, 57, 3, new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000018", "", null, "Aynı Gün", 219m },
                    { 19, 7.5m, 55, 2, "44x37x31", 0m, 57, 3, new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000019", "", null, "Randevulu Teslimat", 227m },
                    { 20, 8.5m, 59, 4, "45x38x32", 0m, 57, 3, new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000020", "", null, "Standart Teslimat", 235m },
                    { 21, 9.5m, 61, 5, "46x39x33", 0m, 57, 3, new DateTime(2025, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000021", "", null, "Hızlı Teslimat", 243m },
                    { 22, 10.5m, 63, 6, "47x40x34", 0m, 57, 3, new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000022", "", null, "Aynı Gün", 251m },
                    { 23, 11.5m, 65, 7, "48x41x35", 0m, 57, 3, new DateTime(2025, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000023", "", null, "Randevulu Teslimat", 259m },
                    { 24, 12.5m, 67, 8, "49x42x36", 25m, 57, 3, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000024", "", null, "Standart Teslimat", 267m },
                    { 25, 7.5m, 60, 4, "45x40x35", 15m, 59, 4, new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000025", "", null, "Hızlı Teslimat", 300m },
                    { 26, 14.5m, 53, 1, "51x44x38", 0m, 59, 4, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000026", "", null, "Aynı Gün", 283m },
                    { 27, 15.5m, 55, 2, "52x45x39", 0m, 59, 4, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000027", "", null, "Randevulu Teslimat", 291m },
                    { 28, 16.5m, 57, 3, "53x46x40", 0m, 59, 4, new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000028", "", null, "Standart Teslimat", 299m },
                    { 29, 17.5m, 61, 5, "54x47x41", 0m, 59, 4, new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000029", "", null, "Hızlı Teslimat", 307m },
                    { 30, 3.5m, 63, 6, "55x48x42", 25m, 59, 4, new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000030", "", null, "Aynı Gün", 315m },
                    { 31, 4.5m, 65, 7, "56x49x43", 0m, 59, 4, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000031", "", null, "Randevulu Teslimat", 323m },
                    { 32, 5.5m, 67, 8, "57x50x44", 0m, 59, 4, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000032", "", null, "Standart Teslimat", 331m },
                    { 33, 5.5m, 62, 5, "53x48x43", 0m, 61, 5, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 10m, null, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000033", "", null, "Hızlı Teslimat", 380m },
                    { 34, 7.5m, 53, 1, "59x52x46", 0m, 61, 5, new DateTime(2025, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000034", "", null, "Aynı Gün", 347m },
                    { 35, 8.5m, 55, 2, "60x53x47", 0m, 61, 5, new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000035", "", null, "Randevulu Teslimat", 355m },
                    { 36, 9.5m, 57, 3, "61x54x48", 25m, 61, 5, new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000036", "", null, "Standart Teslimat", 363m },
                    { 37, 10.5m, 59, 4, "62x55x49", 0m, 61, 5, new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000037", "", null, "Hızlı Teslimat", 371m },
                    { 38, 11.5m, 63, 6, "63x56x50", 0m, 61, 5, new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000038", "", null, "Aynı Gün", 379m },
                    { 39, 12.5m, 65, 7, "64x57x51", 0m, 61, 5, new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000039", "", null, "Randevulu Teslimat", 387m },
                    { 40, 13.5m, 67, 8, "65x58x52", 0m, 61, 5, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000040", "", null, "Standart Teslimat", 395m },
                    { 41, 3.5m, 64, 6, "61x56x51", 0m, 63, 6, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000041", "", null, "Hızlı Teslimat", 460m },
                    { 42, 15.5m, 53, 1, "67x60x54", 25m, 63, 6, new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000042", "", null, "Aynı Gün", 411m },
                    { 43, 16.5m, 55, 2, "68x61x55", 0m, 63, 6, new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000043", "", null, "Randevulu Teslimat", 419m },
                    { 44, 17.5m, 57, 3, "69x62x56", 0m, 63, 6, new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000044", "", null, "Standart Teslimat", 427m },
                    { 45, 3.5m, 59, 4, "70x63x57", 0m, 63, 6, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000045", "", null, "Hızlı Teslimat", 435m },
                    { 46, 4.5m, 61, 5, "71x64x58", 0m, 63, 6, new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000046", "", null, "Aynı Gün", 443m },
                    { 47, 5.5m, 65, 7, "72x65x59", 0m, 63, 6, new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000047", "", null, "Randevulu Teslimat", 451m },
                    { 48, 6.5m, 67, 8, "73x66x60", 25m, 63, 6, new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000048", "", null, "Standart Teslimat", 459m },
                    { 49, 11.5m, 66, 7, "69x64x59", 0m, 65, 7, new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000049", "", null, "Hızlı Teslimat", 540m },
                    { 50, 8.5m, 53, 1, "75x68x62", 0m, 65, 7, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000050", "", null, "Aynı Gün", 475m },
                    { 51, 9.5m, 55, 2, "76x69x63", 0m, 65, 7, new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000051", "", null, "Randevulu Teslimat", 483m },
                    { 52, 10.5m, 57, 3, "77x70x64", 0m, 65, 7, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000052", "", null, "Standart Teslimat", 491m },
                    { 53, 11.5m, 59, 4, "78x71x65", 0m, 65, 7, new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000053", "", null, "Hızlı Teslimat", 499m },
                    { 54, 12.5m, 61, 5, "79x72x66", 25m, 65, 7, new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000054", "", null, "Aynı Gün", 507m },
                    { 55, 13.5m, 63, 6, "80x73x67", 0m, 65, 7, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000055", "", null, "Randevulu Teslimat", 515m },
                    { 56, 14.5m, 67, 8, "81x74x68", 0m, 65, 7, new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000056", "", null, "Standart Teslimat", 523m },
                    { 57, 9.5m, 68, 8, "77x72x67", 0m, 67, 8, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 10m, null, new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000057", "", null, "Hızlı Teslimat", 620m },
                    { 58, 16.5m, 53, 1, "83x76x70", 0m, 67, 8, new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000058", "", null, "Aynı Gün", 539m },
                    { 59, 17.5m, 55, 2, "84x77x71", 0m, 67, 8, new DateTime(2025, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000059", "", null, "Randevulu Teslimat", 547m },
                    { 60, 3.5m, 57, 3, "85x78x72", 25m, 67, 8, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000060", "", null, "Standart Teslimat", 555m },
                    { 61, 4.5m, 59, 4, "86x79x73", 0m, 67, 8, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000061", "", null, "Hızlı Teslimat", 563m },
                    { 62, 5.5m, 61, 5, "87x80x74", 0m, 67, 8, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000062", "", null, "Aynı Gün", 571m },
                    { 63, 6.5m, 63, 6, "88x81x75", 0m, 67, 8, new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0m, null, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000063", "", null, "Randevulu Teslimat", 579m },
                    { 64, 7.5m, 65, 7, "89x82x76", 0m, 67, 8, new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 20m, null, new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "KTS00000064", "", null, "Standart Teslimat", 587m }
                });

            migrationBuilder.InsertData(
                table: "MusteriAdresleri",
                columns: new[] { "Id", "AdresId", "AdresTipi", "Aktif", "MusteriId" },
                values: new object[,]
                {
                    { 1, 53, "Ev", true, 1 },
                    { 2, 54, "Is", true, 1 },
                    { 3, 55, "Ev", true, 2 },
                    { 4, 56, "Is", true, 2 },
                    { 5, 57, "Ev", true, 3 },
                    { 6, 58, "Is", true, 3 },
                    { 7, 59, "Ev", true, 4 },
                    { 8, 60, "Is", true, 4 },
                    { 9, 61, "Ev", true, 5 },
                    { 10, 62, "Is", true, 5 },
                    { 11, 63, "Ev", true, 6 },
                    { 12, 64, "Is", true, 6 },
                    { 13, 65, "Ev", true, 7 },
                    { 14, 66, "Is", true, 7 },
                    { 15, 67, "Ev", true, 8 },
                    { 16, 68, "Is", true, 8 }
                });

            migrationBuilder.InsertData(
                table: "Personeller",
                columns: new[] { "PersonelId", "Ad", "Aktif", "AracId", "Cinsiyet", "DogumTarihi", "EhliyetSinifi", "IseGirisTarihi", "IstenCikisTarihi", "Maas", "Mail", "RolId", "Sifre", "Soyad", "SubeId", "Tel" },
                values: new object[,]
                {
                    { 5, "Ali", true, 4, "Erkek", new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10500m, "ali.kaya1@kargosistemi.com", 5, "1234", "Kaya 1", 1, "05551000006" },
                    { 6, "Zeynep", true, 5, "Kadın", new DateTime(1987, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 11000m, "zeynep.arslan1@kargosistemi.com", 6, "1234", "Arslan 1", 1, "05551000007" },
                    { 7, "Ahmet", true, 6, "Erkek", new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 11500m, "ahmet.öztürk1@kargosistemi.com", 7, "1234", "Öztürk 1", 1, "05551000008" },
                    { 22, "Fatma", true, 9, "Kadın", new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10500m, "fatma.çelik2@kargosistemi.com", 5, "1234", "Çelik 2", 2, "05551000023" },
                    { 23, "Ali", true, 10, "Erkek", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2020, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 11000m, "ali.kaya2@kargosistemi.com", 6, "1234", "Kaya 2", 2, "05551000024" },
                    { 24, "Zeynep", true, 11, "Kadın", new DateTime(1989, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 11500m, "zeynep.arslan2@kargosistemi.com", 7, "1234", "Arslan 2", 2, "05551000025" },
                    { 39, "Ayşe", true, 14, "Erkek", new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10500m, "ayşe.demir3@kargosistemi.com", 5, "1234", "Demir 3", 3, "05551000040" },
                    { 40, "Fatma", true, 15, "Kadın", new DateTime(1993, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 11000m, "fatma.çelik3@kargosistemi.com", 6, "1234", "Çelik 3", 3, "05551000041" },
                    { 41, "Ali", true, 16, "Erkek", new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 11500m, "ali.kaya3@kargosistemi.com", 7, "1234", "Kaya 3", 3, "05551000042" }
                });

            migrationBuilder.InsertData(
                table: "Adresler",
                columns: new[] { "AdresId", "AcikAdres", "AdresBaslik", "AdresTipi", "Aktif", "BinaAdi", "Daire", "EkAciklama", "IlId", "IlceId", "KapiNo", "Kat", "MahalleId", "MusteriId", "PersonelId", "PostaKodu" },
                values: new object[,]
                {
                    { 5, "Ali Mahallesi, Kaya 1 Sokak No:5", "Ali Ev Adresi", "Ev", true, "Kaya 1 Apt", "6", "Ali Kaya 1 ev adresi", 34, 100, "5", "6", 199, null, 5, "34000" },
                    { 6, "Zeynep Mahallesi, Arslan 1 Sokak No:6", "Zeynep Ev Adresi", "Ev", true, "Arslan 1 Apt", "7", "Zeynep Arslan 1 ev adresi", 34, 100, "6", "7", 199, null, 6, "34000" },
                    { 7, "Ahmet Mahallesi, Öztürk 1 Sokak No:7", "Ahmet Ev Adresi", "Ev", true, "Öztürk 1 Apt", "8", "Ahmet Öztürk 1 ev adresi", 34, 100, "7", "8", 199, null, 7, "34000" },
                    { 22, "Fatma Mahallesi, Çelik 2 Sokak No:22", "Fatma Ev Adresi", "Ev", true, "Çelik 2 Apt", "3", "Fatma Çelik 2 ev adresi", 6, 16, "22", "3", 31, null, 22, "6000" },
                    { 23, "Ali Mahallesi, Kaya 2 Sokak No:23", "Ali Ev Adresi", "Ev", true, "Kaya 2 Apt", "4", "Ali Kaya 2 ev adresi", 6, 16, "23", "4", 31, null, 23, "6000" },
                    { 24, "Zeynep Mahallesi, Arslan 2 Sokak No:24", "Zeynep Ev Adresi", "Ev", true, "Arslan 2 Apt", "5", "Zeynep Arslan 2 ev adresi", 6, 16, "24", "5", 31, null, 24, "6000" },
                    { 39, "Ayşe Mahallesi, Demir 3 Sokak No:39", "Ayşe Ev Adresi", "Ev", true, "Demir 3 Apt", "20", "Ayşe Demir 3 ev adresi", 35, 103, "39", "10", 205, null, 39, "35000" },
                    { 40, "Fatma Mahallesi, Çelik 3 Sokak No:40", "Fatma Ev Adresi", "Ev", true, "Çelik 3 Apt", "1", "Fatma Çelik 3 ev adresi", 35, 103, "40", "1", 205, null, 40, "35000" },
                    { 41, "Ali Mahallesi, Kaya 3 Sokak No:41", "Ali Ev Adresi", "Ev", true, "Kaya 3 Apt", "2", "Ali Kaya 3 ev adresi", 35, 103, "41", "2", 205, null, 41, "35000" }
                });

            migrationBuilder.InsertData(
                table: "GonderiDurumGecmisi",
                columns: new[] { "Id", "Aciklama", "AracId", "DurumAd", "GonderiId", "IlgiliKisiAd", "IlgiliKisiTel", "IslemBaslangicTarihi", "IslemBitisTarihi", "IslemSonucu", "IslemTipi", "PersonelId", "SonDurumMu", "SubeId", "Tarih", "TeslimatKodu" },
                values: new object[,]
                {
                    { 1, "Kemal Yılmaz tarafından kargoya verildi", null, "Kargoya Verildi", 1, "Kemal Yılmaz", "05301111111", new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 2, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 11, true, 1, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0001" },
                    { 2, "Şehirlerarası transfer işlemi", 2, "Transferde", 2, "Transfer Personeli 2", "05550000002", new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 22, true, 2, new DateTime(2025, 10, 3, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0002" },
                    { 3, "Şehirlerarası transfer işlemi", 3, "Transferde", 3, "Transfer Personeli 0", "05550000003", new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 23, true, 3, new DateTime(2025, 10, 4, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0003" },
                    { 4, "Şehirlerarası transfer işlemi", 4, "Transferde", 4, "Transfer Personeli 1", "05550000004", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 24, true, 1, new DateTime(2025, 10, 5, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0004" },
                    { 5, "Şehirlerarası transfer işlemi", 5, "Transferde", 5, "Transfer Personeli 2", "05550000005", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 25, true, 2, new DateTime(2025, 10, 6, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0005" },
                    { 6, "Şehirlerarası transfer işlemi", 6, "Transferde", 6, "Transfer Personeli 0", "05550000006", new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 26, true, 3, new DateTime(2025, 10, 7, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0006" },
                    { 7, "Şehirlerarası transfer işlemi", 7, "Transferde", 7, "Transfer Personeli 1", "05550000007", new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 27, true, 1, new DateTime(2025, 10, 8, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0007" },
                    { 8, "Şehirlerarası transfer işlemi", 8, "Transferde", 8, "Transfer Personeli 2", "05550000008", new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 28, true, 2, new DateTime(2025, 10, 9, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0008" },
                    { 9, "Selin Demir tarafından kargoya verildi", null, "Kargoya Verildi", 9, "Selin Demir", "05302222222", new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 10, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 19, true, 1, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0009" },
                    { 10, "Şehirlerarası transfer işlemi", 10, "Transferde", 10, "Transfer Personeli 1", "05550000010", new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 20, true, 1, new DateTime(2025, 10, 11, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0010" },
                    { 11, "Şehirlerarası transfer işlemi", 11, "Transferde", 11, "Transfer Personeli 2", "05550000011", new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 21, true, 3, new DateTime(2025, 10, 12, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0011" },
                    { 12, "Şehirlerarası transfer işlemi", 12, "Transferde", 12, "Transfer Personeli 0", "05550000012", new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 22, true, 1, new DateTime(2025, 10, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0012" },
                    { 13, "Şehirlerarası transfer işlemi", 13, "Transferde", 13, "Transfer Personeli 1", "05550000013", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 23, true, 2, new DateTime(2025, 10, 14, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0013" },
                    { 14, "Şehirlerarası transfer işlemi", 14, "Transferde", 14, "Transfer Personeli 2", "05550000014", new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 24, true, 3, new DateTime(2025, 10, 15, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0014" },
                    { 15, "Şehirlerarası transfer işlemi", 15, "Transferde", 15, "Transfer Personeli 0", "05550000015", new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 25, true, 1, new DateTime(2025, 10, 16, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0015" },
                    { 16, "Şehirlerarası transfer işlemi", 16, "Transferde", 16, "Transfer Personeli 1", "05550000016", new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 26, true, 2, new DateTime(2025, 10, 17, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0016" },
                    { 17, "Barış Kaya tarafından kargoya verildi", null, "Kargoya Verildi", 17, "Barış Kaya", "05303333333", new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 18, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 17, true, 1, new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0017" },
                    { 18, "Şehirlerarası transfer işlemi", 18, "Transferde", 18, "Transfer Personeli 0", "05550000018", new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 28, true, 1, new DateTime(2025, 10, 19, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0018" },
                    { 19, "Şehirlerarası transfer işlemi", 1, "Transferde", 19, "Transfer Personeli 1", "05550000019", new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 29, true, 2, new DateTime(2025, 10, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0019" },
                    { 20, "Şehirlerarası transfer işlemi", 2, "Transferde", 20, "Transfer Personeli 2", "05550000020", new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 20, true, 1, new DateTime(2025, 10, 21, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0020" },
                    { 21, "Şehirlerarası transfer işlemi", 3, "Transferde", 21, "Transfer Personeli 0", "05550000021", new DateTime(2025, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 21, true, 2, new DateTime(2025, 10, 22, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0021" },
                    { 22, "Şehirlerarası transfer işlemi", 4, "Transferde", 22, "Transfer Personeli 1", "05550000022", new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 22, true, 3, new DateTime(2025, 10, 23, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0022" },
                    { 23, "Şehirlerarası transfer işlemi", 5, "Transferde", 23, "Transfer Personeli 2", "05550000023", new DateTime(2025, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 23, true, 1, new DateTime(2025, 10, 24, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0023" },
                    { 24, "Şehirlerarası transfer işlemi", 6, "Transferde", 24, "Transfer Personeli 0", "05550000024", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 24, true, 2, new DateTime(2025, 10, 25, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0024" },
                    { 25, "Aylin Çelik tarafından kargoya verildi", null, "Kargoya Verildi", 25, "Aylin Çelik", "05304444444", new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 26, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 15, true, 1, new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0025" },
                    { 26, "Şehirlerarası transfer işlemi", 8, "Transferde", 26, "Transfer Personeli 2", "05550000026", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 26, true, 1, new DateTime(2025, 10, 27, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0026" },
                    { 27, "Şehirlerarası transfer işlemi", 9, "Transferde", 27, "Transfer Personeli 0", "05550000027", new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 27, true, 2, new DateTime(2025, 10, 28, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0027" },
                    { 28, "Şehirlerarası transfer işlemi", 10, "Transferde", 28, "Transfer Personeli 1", "05550000028", new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 29, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 28, true, 3, new DateTime(2025, 10, 29, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0028" },
                    { 29, "Şehirlerarası transfer işlemi", 11, "Transferde", 29, "Transfer Personeli 2", "05550000029", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 29, true, 2, new DateTime(2025, 10, 30, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0029" },
                    { 30, "Şehirlerarası transfer işlemi", 12, "Transferde", 30, "Transfer Personeli 0", "05550000030", new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 20, true, 3, new DateTime(2025, 10, 31, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0030" },
                    { 31, "Şehirlerarası transfer işlemi", 13, "Transferde", 31, "Transfer Personeli 1", "05550000031", new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 21, true, 1, new DateTime(2025, 11, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0031" },
                    { 32, "Şehirlerarası transfer işlemi", 14, "Transferde", 32, "Transfer Personeli 2", "05550000032", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 22, true, 2, new DateTime(2025, 11, 2, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0032" },
                    { 33, "Cem Arslan tarafından kargoya verildi", null, "Kargoya Verildi", 33, "Cem Arslan", "05305555555", new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 3, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 13, true, 1, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0033" },
                    { 34, "Şehirlerarası transfer işlemi", 16, "Transferde", 34, "Transfer Personeli 1", "05550000034", new DateTime(2025, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 24, true, 1, new DateTime(2025, 11, 4, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0034" },
                    { 35, "Şehirlerarası transfer işlemi", 17, "Transferde", 35, "Transfer Personeli 2", "05550000035", new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 25, true, 2, new DateTime(2025, 11, 5, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0035" },
                    { 36, "Şehirlerarası transfer işlemi", 18, "Transferde", 36, "Transfer Personeli 0", "05550000036", new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 26, true, 3, new DateTime(2025, 11, 6, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0036" },
                    { 37, "Şehirlerarası transfer işlemi", 1, "Transferde", 37, "Transfer Personeli 1", "05550000037", new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 27, true, 1, new DateTime(2025, 11, 7, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0037" },
                    { 38, "Şehirlerarası transfer işlemi", 2, "Transferde", 38, "Transfer Personeli 2", "05550000038", new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 28, true, 3, new DateTime(2025, 11, 8, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0038" },
                    { 39, "Şehirlerarası transfer işlemi", 3, "Transferde", 39, "Transfer Personeli 0", "05550000039", new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 29, true, 1, new DateTime(2025, 11, 9, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0039" },
                    { 40, "Şehirlerarası transfer işlemi", 4, "Transferde", 40, "Transfer Personeli 1", "05550000040", new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 20, true, 2, new DateTime(2025, 11, 10, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0040" },
                    { 41, "Deniz Polat tarafından kargoya verildi", null, "Kargoya Verildi", 41, "Deniz Polat", "05306666666", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 11, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 11, true, 1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0041" },
                    { 42, "Şehirlerarası transfer işlemi", 6, "Transferde", 42, "Transfer Personeli 0", "05550000042", new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 22, true, 1, new DateTime(2025, 11, 12, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0042" },
                    { 43, "Şehirlerarası transfer işlemi", 7, "Transferde", 43, "Transfer Personeli 1", "05550000043", new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 23, true, 2, new DateTime(2025, 11, 13, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0043" },
                    { 44, "Şehirlerarası transfer işlemi", 8, "Transferde", 44, "Transfer Personeli 2", "05550000044", new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 24, true, 3, new DateTime(2025, 11, 14, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0044" },
                    { 45, "Şehirlerarası transfer işlemi", 9, "Transferde", 45, "Transfer Personeli 0", "05550000045", new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 25, true, 1, new DateTime(2025, 11, 15, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0045" },
                    { 46, "Şehirlerarası transfer işlemi", 10, "Transferde", 46, "Transfer Personeli 1", "05550000046", new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 26, true, 2, new DateTime(2025, 11, 16, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0046" },
                    { 47, "Şehirlerarası transfer işlemi", 11, "Transferde", 47, "Transfer Personeli 2", "05550000047", new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 27, true, 1, new DateTime(2025, 11, 17, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0047" },
                    { 48, "Şehirlerarası transfer işlemi", 12, "Transferde", 48, "Transfer Personeli 0", "05550000048", new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 28, true, 2, new DateTime(2025, 11, 18, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0048" },
                    { 49, "Ebru Yıldız tarafından kargoya verildi", null, "Kargoya Verildi", 49, "Ebru Yıldız", "05307777777", new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 19, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 19, true, 1, new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0049" },
                    { 50, "Şehirlerarası transfer işlemi", 14, "Transferde", 50, "Transfer Personeli 2", "05550000050", new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 20, true, 1, new DateTime(2025, 11, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0050" },
                    { 51, "Şehirlerarası transfer işlemi", 15, "Transferde", 51, "Transfer Personeli 0", "05550000051", new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 21, true, 2, new DateTime(2025, 11, 21, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0051" },
                    { 52, "Şehirlerarası transfer işlemi", 16, "Transferde", 52, "Transfer Personeli 1", "05550000052", new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 22, true, 3, new DateTime(2025, 11, 22, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0052" },
                    { 53, "Şehirlerarası transfer işlemi", 17, "Transferde", 53, "Transfer Personeli 2", "05550000053", new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 23, true, 1, new DateTime(2025, 11, 23, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0053" },
                    { 54, "Şehirlerarası transfer işlemi", 18, "Transferde", 54, "Transfer Personeli 0", "05550000054", new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 24, true, 2, new DateTime(2025, 11, 24, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0054" },
                    { 55, "Şehirlerarası transfer işlemi", 1, "Transferde", 55, "Transfer Personeli 1", "05550000055", new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 25, true, 3, new DateTime(2025, 11, 25, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0055" },
                    { 56, "Şehirlerarası transfer işlemi", 2, "Transferde", 56, "Transfer Personeli 2", "05550000056", new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 26, true, 2, new DateTime(2025, 11, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0056" },
                    { 57, "Furkan Özkan tarafından kargoya verildi", null, "Kargoya Verildi", 57, "Furkan Özkan", "05308888888", new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 27, 2, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Hazırlık", 17, true, 1, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "TSL0057" },
                    { 58, "Şehirlerarası transfer işlemi", 4, "Transferde", 58, "Transfer Personeli 1", "05550000058", new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 28, true, 1, new DateTime(2025, 11, 28, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0058" },
                    { 59, "Şehirlerarası transfer işlemi", 5, "Transferde", 59, "Transfer Personeli 2", "05550000059", new DateTime(2025, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 29, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 29, true, 2, new DateTime(2025, 11, 29, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0059" },
                    { 60, "Şehirlerarası transfer işlemi", 6, "Transferde", 60, "Transfer Personeli 0", "05550000060", new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 20, true, 3, new DateTime(2025, 11, 30, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0060" },
                    { 61, "Şehirlerarası transfer işlemi", 7, "Transferde", 61, "Transfer Personeli 1", "05550000061", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 21, true, 1, new DateTime(2025, 12, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0061" },
                    { 62, "Şehirlerarası transfer işlemi", 8, "Transferde", 62, "Transfer Personeli 2", "05550000062", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 22, true, 2, new DateTime(2025, 12, 2, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0062" },
                    { 63, "Şehirlerarası transfer işlemi", 9, "Transferde", 63, "Transfer Personeli 0", "05550000063", new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarılı", "Transfer", 23, true, 3, new DateTime(2025, 12, 3, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0063" },
                    { 64, "Şehirlerarası transfer işlemi", 10, "Transferde", 64, "Transfer Personeli 1", "05550000064", new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), "Başarısız", "Transfer", 24, true, 1, new DateTime(2025, 12, 4, 6, 0, 0, 0, DateTimeKind.Unspecified), "TSL0064" }
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
