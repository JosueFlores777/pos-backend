using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class addCarBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_cliente_ImportadorId",
                table: "recibo");

            migrationBuilder.DropTable(
                name: "RangoCobros");

            migrationBuilder.DropIndex(
                name: "IX_recibo_ImportadorId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "ImportadorId",
                table: "recibo");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "recibo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "cliente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModeloId",
                table: "cliente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recibo_ClienteId",
                table: "recibo",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_MarcaId",
                table: "cliente",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_ModeloId",
                table: "cliente",
                column: "ModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_cliente_Catalogo_MarcaId",
                table: "cliente",
                column: "MarcaId",
                principalTable: "Catalogo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cliente_Catalogo_ModeloId",
                table: "cliente",
                column: "ModeloId",
                principalTable: "Catalogo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_cliente_ClienteId",
                table: "recibo",
                column: "ClienteId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cliente_Catalogo_MarcaId",
                table: "cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_cliente_Catalogo_ModeloId",
                table: "cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_recibo_cliente_ClienteId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_recibo_ClienteId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_cliente_MarcaId",
                table: "cliente");

            migrationBuilder.DropIndex(
                name: "IX_cliente_ModeloId",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "ModeloId",
                table: "cliente");

            migrationBuilder.AddColumn<int>(
                name: "ImportadorId",
                table: "recibo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RangoCobros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasePeso = table.Column<int>(type: "int", nullable: true),
                    BaseTarifa = table.Column<double>(type: "float", nullable: true),
                    Excedente = table.Column<bool>(type: "bit", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Monto = table.Column<double>(type: "float", nullable: true),
                    PorCada = table.Column<int>(type: "int", nullable: true),
                    ServicioId = table.Column<int>(type: "int", nullable: false),
                    ValorMaximo = table.Column<int>(type: "int", nullable: true),
                    ValorMinimo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangoCobros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangoCobros_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recibo_ImportadorId",
                table: "recibo",
                column: "ImportadorId");

            migrationBuilder.CreateIndex(
                name: "IX_RangoCobros_ServicioId",
                table: "RangoCobros",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_cliente_ImportadorId",
                table: "recibo",
                column: "ImportadorId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
