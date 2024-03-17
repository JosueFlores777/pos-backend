using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class excedenteRangoCobro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excendente",
                table: "Servicio");

            migrationBuilder.AddColumn<bool>(
                name: "Excedente",
                table: "RangoCobros",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excedente",
                table: "RangoCobros");

            migrationBuilder.AddColumn<bool>(
                name: "Excendente",
                table: "Servicio",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
