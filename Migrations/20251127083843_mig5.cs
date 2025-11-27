using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kargotakipsistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gonderiler_Adresler_AliciAdresId",
                table: "Gonderiler");

            migrationBuilder.DropForeignKey(
                name: "FK_Gonderiler_Adresler_GonderenAdresId",
                table: "Gonderiler");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriDestekleri_Personeller_PersonelId",
                table: "MusteriDestekleri");

            migrationBuilder.DropForeignKey(
                name: "FK_OdemeFaturalari_Adresler_FaturaAdresId",
                table: "OdemeFaturalari");

            migrationBuilder.AddForeignKey(
                name: "FK_Gonderiler_Adresler_AliciAdresId",
                table: "Gonderiler",
                column: "AliciAdresId",
                principalTable: "Adresler",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gonderiler_Adresler_GonderenAdresId",
                table: "Gonderiler",
                column: "GonderenAdresId",
                principalTable: "Adresler",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriDestekleri_Personeller_PersonelId",
                table: "MusteriDestekleri",
                column: "PersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OdemeFaturalari_Adresler_FaturaAdresId",
                table: "OdemeFaturalari",
                column: "FaturaAdresId",
                principalTable: "Adresler",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gonderiler_Adresler_AliciAdresId",
                table: "Gonderiler");

            migrationBuilder.DropForeignKey(
                name: "FK_Gonderiler_Adresler_GonderenAdresId",
                table: "Gonderiler");

            migrationBuilder.DropForeignKey(
                name: "FK_MusteriDestekleri_Personeller_PersonelId",
                table: "MusteriDestekleri");

            migrationBuilder.DropForeignKey(
                name: "FK_OdemeFaturalari_Adresler_FaturaAdresId",
                table: "OdemeFaturalari");

            migrationBuilder.AddForeignKey(
                name: "FK_Gonderiler_Adresler_AliciAdresId",
                table: "Gonderiler",
                column: "AliciAdresId",
                principalTable: "Adresler",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Gonderiler_Adresler_GonderenAdresId",
                table: "Gonderiler",
                column: "GonderenAdresId",
                principalTable: "Adresler",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MusteriDestekleri_Personeller_PersonelId",
                table: "MusteriDestekleri",
                column: "PersonelId",
                principalTable: "Personeller",
                principalColumn: "PersonelId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OdemeFaturalari_Adresler_FaturaAdresId",
                table: "OdemeFaturalari",
                column: "FaturaAdresId",
                principalTable: "Adresler",
                principalColumn: "AdresId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
