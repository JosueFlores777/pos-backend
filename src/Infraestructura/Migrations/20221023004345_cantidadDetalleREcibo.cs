using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class cantidadDetalleREcibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadServicio",
                table: "recibo");

            migrationBuilder.AddColumn<int>(
                name: "CantidadServicio",
                table: "detalleRecibo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadServicio",
                table: "detalleRecibo");

            migrationBuilder.AddColumn<int>(
                name: "CantidadServicio",
                table: "recibo",
                type: "int",
                nullable: true);
        }
    }
}
