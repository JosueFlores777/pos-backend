using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class FisrtMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: true),
                    NombreServicio = table.Column<string>(nullable: true),
                    NombreSubServicio = table.Column<string>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: false),
                    TipoServicioId = table.Column<int>(nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    TipoCobroId = table.Column<int>(nullable: false),
                    DepartamentoId = table.Column<int>(nullable: false),
                    MonedaId = table.Column<int>(nullable: false),
                    Monto = table.Column<double>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    Verificado = table.Column<bool>(nullable: false),
                    AdicionarMismoServicio = table.Column<bool>(nullable: false),
                    Rubro = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Descuento = table.Column<bool>(nullable: false),
                    TipoCobroUnidadesId = table.Column<int>(nullable: true)
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
                        name: "FK_Servicio_Catalogo_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id"
                       );
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_MonedaId",
                        column: x => x.MonedaId,
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
                name: "recibo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificacion = table.Column<string>(nullable: true),
                    ClienteId = table.Column<int>(nullable: true),
                    NombreRazon = table.Column<string>(nullable: true),
                    TipoIdentificadorId = table.Column<int>(nullable: false),
                    MontoTotal = table.Column<double>(nullable: false),
                    MonedaId = table.Column<int>(nullable: false),
                    EstadoSenasaId = table.Column<int>(nullable: false),
                    EstadoSefinId = table.Column<int>(nullable: false),
                    UsuarioAsignadoId = table.Column<int>(nullable: true),
                    Comentario = table.Column<string>(nullable: true),
                    RegionalId = table.Column<int>(nullable: true),
                    DescuentoId = table.Column<int>(nullable: true),
                    AreaId = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    LastSync = table.Column<DateTime>(nullable: false),
                    FechaPago = table.Column<DateTime>(nullable: false),
                    FechaUtilizado = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    InicioVigencia = table.Column<DateTime>(nullable: true),
                    FinVigencia = table.Column<DateTime>(nullable: true),
                    Banco = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recibo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recibo_cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_DescuentoId",
                        column: x => x.DescuentoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_EstadoSefinId",
                        column: x => x.EstadoSefinId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_EstadoSenasaId",
                        column: x => x.EstadoSenasaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_RegionalId",
                        column: x => x.RegionalId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_TipoIdentificadorId",
                        column: x => x.TipoIdentificadorId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_recibo_usuario_UsuarioAsignadoId",
                        column: x => x.UsuarioAsignadoId,
                        principalTable: "usuario",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cambioEstado_recibo_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "recibo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_cambioEstado_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "detalleRecibo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReciboId = table.Column<int>(nullable: false),
                    ServicioId = table.Column<int>(nullable: false),
                    MarcaId = table.Column<int>(nullable: false),
                    ModeloId = table.Column<int>(nullable: false),
                    CantidadServicio = table.Column<int>(nullable: true),
                    Monto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalleRecibo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalleRecibo_Catalogo_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_detalleRecibo_Catalogo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Catalogo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_detalleRecibo_recibo_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "recibo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_detalleRecibo_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id");
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
                name: "IX_detalleRecibo_ReciboId",
                table: "detalleRecibo",
                column: "ReciboId");

            migrationBuilder.CreateIndex(
                name: "IX_detalleRecibo_ServicioId",
                table: "detalleRecibo",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_AreaId",
                table: "recibo",
                column: "AreaId");




            migrationBuilder.CreateIndex(
                name: "IX_Servicio_DepartamentoId",
                table: "Servicio",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_MonedaId",
                table: "Servicio",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_TipoCobroId",
                table: "Servicio",
                column: "TipoCobroId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_TipoServicioId",
                table: "Servicio",
                column: "TipoServicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cambioEstado");

            migrationBuilder.DropTable(
                name: "detalleRecibo");

            migrationBuilder.DropTable(
                name: "rolPermiso");

            migrationBuilder.DropTable(
                name: "UsuarioArea");

            migrationBuilder.DropTable(
                name: "usuarioRegional");

            migrationBuilder.DropTable(
                name: "usuarioRol");

            migrationBuilder.DropTable(
                name: "recibo");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "Catalogo");
        }
    }
}
