using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kargotakipsistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Araclar_Subeler_SubeId",
                table: "Araclar");

            migrationBuilder.AddForeignKey(
                name: "FK_Araclar_Subeler_SubeId",
                table: "Araclar",
                column: "SubeId",
                principalTable: "Subeler",
                principalColumn: "SubeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Araclar_Subeler_SubeId",
                table: "Araclar");

            migrationBuilder.AddForeignKey(
                name: "FK_Araclar_Subeler_SubeId",
                table: "Araclar",
                column: "SubeId",
                principalTable: "Subeler",
                principalColumn: "SubeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
