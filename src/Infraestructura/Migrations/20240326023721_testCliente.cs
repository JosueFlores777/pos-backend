using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class testCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_importador_ImportadorId",
                table: "recibo");

            migrationBuilder.DropTable(
                name: "importador");

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    NacionalidadId = table.Column<int>(nullable: false),
                    Identificador = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    AccesoAprobado = table.Column<bool>(nullable: false),
                    DepartamentoId = table.Column<int>(nullable: false),
                    MunicipioId = table.Column<int>(nullable: false),
                    Celular = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    TipoIngreso = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    CorreoEnviado = table.Column<bool>(nullable: false),
                    FechaEnvioCorreo = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_NacionalidadId",
                        column: x => x.NacionalidadId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cliente_DepartamentoId",
                table: "cliente",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_MunicipioId",
                table: "cliente",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_NacionalidadId",
                table: "cliente",
                column: "NacionalidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_cliente_ImportadorId",
                table: "recibo",
                column: "ImportadorId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_cliente_ImportadorId",
                table: "recibo");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.CreateTable(
                name: "importador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccesoAprobado = table.Column<bool>(type: "bit", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoEnviado = table.Column<bool>(type: "bit", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEnvioCorreo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    NacionalidadId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoIngreso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_importador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_importador_Catalogo_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_importador_Catalogo_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_importador_Catalogo_NacionalidadId",
                        column: x => x.NacionalidadId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_importador_DepartamentoId",
                table: "importador",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_importador_MunicipioId",
                table: "importador",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_importador_NacionalidadId",
                table: "importador",
                column: "NacionalidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_importador_ImportadorId",
                table: "recibo",
                column: "ImportadorId",
                principalTable: "importador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
