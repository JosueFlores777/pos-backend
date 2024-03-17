using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class addUsuarioRegional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionalId",
                table: "recibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recibo_RegionalId",
                table: "recibo",
                column: "RegionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_RegionalId",
                table: "recibo",
                column: "RegionalId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_RegionalId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_recibo_RegionalId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "RegionalId",
                table: "recibo");
        }
    }
}
