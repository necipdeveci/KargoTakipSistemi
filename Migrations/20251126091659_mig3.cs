using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kargotakipsistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdresId",
                table: "Personeller");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdresId",
                table: "Personeller",
                type: "int",
                nullable: true);
        }
    }
}
