using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class addMonedaServicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonedaId",
                table: "Servicio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_MonedaId",
                table: "Servicio",
                column: "MonedaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_MonedaId",
                table: "Servicio",
                column: "MonedaId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_MonedaId",
                table: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_MonedaId",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "MonedaId",
                table: "Servicio");
        }
    }
}
