﻿using System;
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_TipoCobroId",
                        column: x => x.TipoCobroId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Catalogo_TipoServicioId",
                        column: x => x.TipoServicioId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    IdentificadorAcceso = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    Contrasena = table.Column<string>(nullable: true),
                    DepartamentoId = table.Column<int>(nullable: true),
                    CambiarContrasena = table.Column<bool>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: true),
                    FechaActualizacion = table.Column<DateTime>(nullable: true),
                    TipoUsuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuario_Catalogo_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rolPermiso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(nullable: false),
                    PermisoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolPermiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rolPermiso_Permiso_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolPermiso_rol_RolId",
                        column: x => x.RolId,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificadorId = table.Column<int>(nullable: false),
                    Identificador = table.Column<string>(nullable: true),
                    TipoPersonaId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    NacionalidadId = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    DepartamentoId = table.Column<int>(nullable: false),
                    MunicipioId = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    MarcaId = table.Column<int>(nullable: false),
                    ModeloId = table.Column<int>(nullable: false),
                    TipoIngreso = table.Column<string>(nullable: true),
                    FechaRegistro = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    CorreoEnviado = table.Column<bool>(nullable: false),
                    FechaEnvioCorreo = table.Column<DateTime>(nullable: true),
                    CorreoVerificado = table.Column<bool>(nullable: false),
                    FechaVerificacionCorreo = table.Column<DateTime>(nullable: true),
                    AccesoAprobado = table.Column<bool>(nullable: false),
                    FechaAprobacionAcceso = table.Column<DateTime>(nullable: true),
                    UsuarioGentionId = table.Column<int>(nullable: true),
                    TokenVerificacion = table.Column<string>(nullable: true),
                    MotivoRechazo = table.Column<string>(nullable: true)
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
                        name: "FK_cliente_Catalogo_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_NacionalidadId",
                        column: x => x.NacionalidadId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_TipoIdentificadorId",
                        column: x => x.TipoIdentificadorId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_Catalogo_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cliente_usuario_UsuarioGentionId",
                        column: x => x.UsuarioGentionId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioArea_Catalogo_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioArea_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarioRegional",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionalId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioRegional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarioRegional_Catalogo_RegionalId",
                        column: x => x.RegionalId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarioRegional_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarioRol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarioRol_rol_RolId",
                        column: x => x.RolId,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarioRol_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_EstadoSenasaId",
                        column: x => x.EstadoSenasaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_recibo_Catalogo_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cambioEstado_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalleRecibo_Catalogo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalleRecibo_recibo_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "recibo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalleRecibo_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
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
                name: "IX_cliente_DepartamentoId",
                table: "cliente",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_MarcaId",
                table: "cliente",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_ModeloId",
                table: "cliente",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_MunicipioId",
                table: "cliente",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_NacionalidadId",
                table: "cliente",
                column: "NacionalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_TipoIdentificadorId",
                table: "cliente",
                column: "TipoIdentificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_TipoPersonaId",
                table: "cliente",
                column: "TipoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_UsuarioGentionId",
                table: "cliente",
                column: "UsuarioGentionId");

            migrationBuilder.CreateIndex(
                name: "IX_detalleRecibo_MarcaId",
                table: "detalleRecibo",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_detalleRecibo_ModeloId",
                table: "detalleRecibo",
                column: "ModeloId");

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
                name: "IX_recibo_ClienteId",
                table: "recibo",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_DescuentoId",
                table: "recibo",
                column: "DescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_EstadoSefinId",
                table: "recibo",
                column: "EstadoSefinId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_EstadoSenasaId",
                table: "recibo",
                column: "EstadoSenasaId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_MonedaId",
                table: "recibo",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_RegionalId",
                table: "recibo",
                column: "RegionalId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_TipoIdentificadorId",
                table: "recibo",
                column: "TipoIdentificadorId");

            migrationBuilder.CreateIndex(
                name: "IX_recibo_UsuarioAsignadoId",
                table: "recibo",
                column: "UsuarioAsignadoId");

            migrationBuilder.CreateIndex(
                name: "IX_rolPermiso_PermisoId",
                table: "rolPermiso",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_rolPermiso_RolId",
                table: "rolPermiso",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_AreaId",
                table: "Servicio",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_CategoriaId",
                table: "Servicio",
                column: "CategoriaId");

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

            migrationBuilder.CreateIndex(
                name: "IX_usuario_DepartamentoId",
                table: "usuario",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioArea_AreaId",
                table: "UsuarioArea",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioArea_UsuarioId",
                table: "UsuarioArea",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioRegional_RegionalId",
                table: "usuarioRegional",
                column: "RegionalId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioRegional_UsuarioId",
                table: "usuarioRegional",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioRol_RolId",
                table: "usuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarioRol_UsuarioId",
                table: "usuarioRol",
                column: "UsuarioId");
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