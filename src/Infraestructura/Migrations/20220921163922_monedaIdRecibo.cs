using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class monedaIdRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonedaId",
                table: "recibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recibo_MonedaId",
                table: "recibo",
                column: "MonedaId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_MonedaId",
                table: "recibo",
                column: "MonedaId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_MonedaId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_recibo_MonedaId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "MonedaId",
                table: "recibo");
        }
    }
}
