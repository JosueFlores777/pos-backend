using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class nuevosCamposServicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AliasServicio",
                table: "Servicio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Servicio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AliasServicio",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Servicio");
        }
    }
}
