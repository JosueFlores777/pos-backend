using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class AddServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiEstadoSefin",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "ApiEstadoSenasa",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "DependenciaId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "Puesto",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "Timbre",
                table: "recibo");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPago",
                table: "recibo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUtilizado",
                table: "recibo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ServicioId",
                table: "recibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoIdentificadorId",
                table: "recibo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreServicio = table.Column<int>(nullable: false),
                    NombreSubServicio = table.Column<int>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false),
                    TipoServicioId = table.Column<int>(nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    TipoCobroId = table.Column<int>(nullable: false),
                    Monto = table.Column<double>(nullable: true),
                    Rubro = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    SubCategoriaId = table.Column<int>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_TipoCobroId",
                        column: x => x.TipoCobroId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_TipoServicioId",
                        column: x => x.TipoServicioId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RangoCobros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioId = table.Column<int>(nullable: false),
                    ValorMinimo = table.Column<int>(nullable: false),
                    ValorMaximo = table.Column<int>(nullable: false),
                    TipoCobroUnidadesId = table.Column<int>(nullable: false),
                    Monto = table.Column<double>(nullable: true),
                    FechaRegistro = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_RangoCobros_Catalogo_TipoCobroUnidadesId",
                        column: x => x.TipoCobroUnidadesId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_recibo_ServicioId",
                table: "recibo",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_TipoIdentificadorId",
                table: "recibo",
                column: "TipoIdentificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_RangoCobros_ServicioId",
                table: "RangoCobros",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_RangoCobros_TipoCobroUnidadesId",
                table: "RangoCobros",
                column: "TipoCobroUnidadesId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_AreaId",
                table: "Servicio",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_CategoriaId",
                table: "Servicio",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_SubCategoriaId",
                table: "Servicio",
                column: "SubCategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_TipoCobroId",
                table: "Servicio",
                column: "TipoCobroId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_TipoServicioId",
                table: "Servicio",
                column: "TipoServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Servicio_ServicioId",
                table: "recibo",
                column: "ServicioId",
                principalTable: "Servicio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_TipoIdentificadorId",
                table: "recibo",
                column: "TipoIdentificadorId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Servicio_ServicioId",
                table: "recibo");

            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_TipoIdentificadorId",
                table: "recibo");

            migrationBuilder.DropTable(
                name: "RangoCobros");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_recibo_ServicioId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_recibo_TipoIdentificadorId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "FechaPago",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "FechaUtilizado",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "TipoIdentificadorId",
                table: "recibo");

            migrationBuilder.AddColumn<string>(
                name: "ApiEstadoSefin",
                table: "recibo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApiEstadoSenasa",
                table: "recibo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "recibo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "recibo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DependenciaId",
                table: "recibo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Puesto",
                table: "recibo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Timbre",
                table: "recibo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
