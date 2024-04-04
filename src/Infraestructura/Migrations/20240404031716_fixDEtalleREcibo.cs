using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class fixDEtalleREcibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detalleRecibo_Catalogo_MarcaId",
                table: "detalleRecibo");


            migrationBuilder.DropForeignKey(
                name: "FK_detalleRecibo_Catalogo_ModeloId",
                table: "detalleRecibo");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "detalleRecibo");

            migrationBuilder.DropColumn(
                name: "ModeloId",
                table: "detalleRecibo");

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "recibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModeloId",
                table: "recibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recibo_MarcaId",
                table: "recibo",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_ModeloId",
                table: "recibo",
                column: "ModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_MarcaId",
                table: "recibo",
                column: "MarcaId",
                principalTable: "Catalogo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_ModeloId",
                table: "recibo",
                column: "ModeloId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_MarcaId",
                table: "recibo");

            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_ModeloId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_recibo_MarcaId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_recibo_ModeloId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "ModeloId",
                table: "recibo");

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "detalleRecibo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModeloId",
                table: "detalleRecibo",
                type: "int",
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detalleRecibo_Catalogo_ModeloId",
                table: "detalleRecibo",
                column: "ModeloId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
