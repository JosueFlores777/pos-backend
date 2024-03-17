using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "recibo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiEstadoSefin = table.Column<string>(nullable: true),
                    ApiEstadoSenasa = table.Column<string>(nullable: true),
                    ImportadorId = table.Column<int>(nullable: false),
                    Identificacion = table.Column<string>(nullable: true),
                    NombreRazon = table.Column<string>(nullable: true),
                    EstadoSenasaId = table.Column<int>(nullable: false),
                    EstadoSefinId = table.Column<int>(nullable: true),
                    EstadoSefinaId = table.Column<int>(nullable: false),
                    Puesto = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    Departamento = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    MontoTotal = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    InicioVigencia = table.Column<DateTime>(nullable: true),
                    FinVigencia = table.Column<DateTime>(nullable: true),
                    DependenciaId = table.Column<int>(nullable: true),
                    UsuarioAsignadoId = table.Column<int>(nullable: true),
                    Timbre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recibo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_EstadoSefinId",
                        column: x => x.EstadoSefinId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_EstadoSenasaId",
                        column: x => x.EstadoSenasaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recibo_importador_ImportadorId",
                        column: x => x.ImportadorId,
                        principalTable: "importador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recibo_usuario_UsuarioAsignadoId",
                        column: x => x.UsuarioAsignadoId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

           

            migrationBuilder.CreateTable(
                name: "cambioEstado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReciboId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Mensaje = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cambioEstado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cambioEstado_Catalogo_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cambioEstado_recibo_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "recibo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cambioEstado_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cambioEstado_EstadoId",
                table: "cambioEstado",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_cambioEstado_ReciboId",
                table: "cambioEstado",
                column: "ReciboId");

            migrationBuilder.CreateIndex(
                name: "IX_cambioEstado_UsuarioId",
                table: "cambioEstado",
                column: "UsuarioId");


            migrationBuilder.CreateIndex(
                name: "IX_recibo_EstadoSefinId",
                table: "recibo",
                column: "EstadoSefinId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_EstadoSenasaId",
                table: "recibo",
                column: "EstadoSenasaId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_ImportadorId",
                table: "recibo",
                column: "ImportadorId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_UsuarioAsignadoId",
                table: "recibo",
                column: "UsuarioAsignadoId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cambioEstado");

            migrationBuilder.DropTable(
                name: "recibo");

        }
    }
}
