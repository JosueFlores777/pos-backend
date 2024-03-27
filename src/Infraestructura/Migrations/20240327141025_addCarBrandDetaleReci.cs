using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class addCarBrandDetaleReci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "detalleRecibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModeloId",
                table: "detalleRecibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_detalleRecibo_MarcaId",
                table: "detalleRecibo",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_detalleRecibo_ModeloId",
                table: "detalleRecibo",
                column: "ModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_detalleRecibo_Catalogo_MarcaId",
                table: "detalleRecibo",
                column: "MarcaId",
                principalTable: "Catalogo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_detalleRecibo_Catalogo_ModeloId",
                table: "detalleRecibo",
                column: "ModeloId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalleRecibo_Catalogo_MarcaId",
                table: "detalleRecibo");

            migrationBuilder.DropForeignKey(
                name: "FK_detalleRecibo_Catalogo_ModeloId",
                table: "detalleRecibo");

            migrationBuilder.DropIndex(
                name: "IX_detalleRecibo_MarcaId",
                table: "detalleRecibo");

            migrationBuilder.DropIndex(
                name: "IX_detalleRecibo_ModeloId",
                table: "detalleRecibo");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "detalleRecibo");

            migrationBuilder.DropColumn(
                name: "ModeloId",
                table: "detalleRecibo");
        }
    }
}
