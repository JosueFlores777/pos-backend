﻿// <auto-generated />
using System;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestructura.Migrations
{
    [DbContext(typeof(RecibosContext))]
    [Migration("20221021021152_AdicionarMismoRecibo")]
    partial class AdicionarMismoRecibo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dominio.Models.CambioEstado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mensaje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReciboId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("ReciboId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("cambioEstado");
                });

            modelBuilder.Entity("Dominio.Models.Catalogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abreviatura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdPadre")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioActualiza")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioCrea")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Catalogo");
                });

            modelBuilder.Entity("Dominio.Models.Importador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AccesoAprobado")
                        .HasColumnType("bit");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CorreoEnviado")
                        .HasColumnType("bit");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaEnvioCorreo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identificador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MunicipioId")
                        .HasColumnType("int");

                    b.Property<int>("NacionalidadId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoIngreso")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("NacionalidadId");

                    b.ToTable("importador");
                });

            modelBuilder.Entity("Dominio.Models.Permiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Asignable")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsMenu")
                        .HasColumnType("bit");

                    b.Property<string>("Icono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<int?>("PermisoPadre")
                        .HasColumnType("int");

                    b.Property<bool>("TieneHijos")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permiso");
                });

            modelBuilder.Entity("Dominio.Models.RangoCobros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BasePeso")
                        .HasColumnType("int");

                    b.Property<double?>("BaseTarifa")
                        .HasColumnType("float");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Monto")
                        .HasColumnType("float");

                    b.Property<int?>("PorCada")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<int?>("ValorMaximo")
                        .HasColumnType("int");

                    b.Property<int?>("ValorMinimo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServicioId");

                    b.ToTable("RangoCobros");
                });

            modelBuilder.Entity("Dominio.Models.Recibo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoSefinId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoSenasaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaUtilizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinVigencia")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identificacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImportadorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InicioVigencia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastSync")
                        .HasColumnType("datetime2");

                    b.Property<int>("MonedaId")
                        .HasColumnType("int");

                    b.Property<double>("MontoTotal")
                        .HasColumnType("float");

                    b.Property<string>("NombreRazon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionalId")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<int>("TipoIdentificadorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioAsignadoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoSefinId");

                    b.HasIndex("EstadoSenasaId");

                    b.HasIndex("ImportadorId");

                    b.HasIndex("MonedaId");

                    b.HasIndex("RegionalId");

                    b.HasIndex("ServicioId");

                    b.HasIndex("TipoIdentificadorId");

                    b.HasIndex("UsuarioAsignadoId");

                    b.ToTable("recibo");
                });

            modelBuilder.Entity("Dominio.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Asignable")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("rol");
                });

            modelBuilder.Entity("Dominio.Models.RolPermiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PermisoId")
                        .HasColumnType("int");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermisoId");

                    b.HasIndex("RolId");

                    b.ToTable("rolPermiso");
                });

            modelBuilder.Entity("Dominio.Models.Servicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<bool>("AdicionarMismoServicio")
                        .HasColumnType("bit");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("MonedaId")
                        .HasColumnType("int");

                    b.Property<double?>("Monto")
                        .HasColumnType("float");

                    b.Property<string>("NombreServicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreSubServicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rubro")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoCobroId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoCobroUnidadesId")
                        .HasColumnType("int");

                    b.Property<int>("TipoServicioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("MonedaId");

                    b.HasIndex("TipoCobroId");

                    b.HasIndex("TipoServicioId");

                    b.ToTable("Servicio");
                });

            modelBuilder.Entity("Dominio.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<bool>("CambiarContrasena")
                        .HasColumnType("bit");

                    b.Property<string>("Contrasena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentificadorAcceso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("usuario");
                });

            modelBuilder.Entity("Dominio.Models.UsuarioRegional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RegionalId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegionalId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("usuarioRegional");
                });

            modelBuilder.Entity("Dominio.Models.UsuarioRol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("usuarioRol");
                });

            modelBuilder.Entity("Dominio.Models.CambioEstado", b =>
                {
                    b.HasOne("Dominio.Models.Catalogo", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Recibo", "Recibo")
                        .WithMany("Cambios")
                        .HasForeignKey("ReciboId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Models.Importador", b =>
                {
                    b.HasOne("Dominio.Models.Catalogo", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "Nacionalidad")
                        .WithMany()
                        .HasForeignKey("NacionalidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Models.RangoCobros", b =>
                {
                    b.HasOne("Dominio.Models.Servicio", "Servicio")
                        .WithMany("RangoCobros")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Models.Recibo", b =>
                {
                    b.HasOne("Dominio.Models.Catalogo", "EstadoSefin")
                        .WithMany()
                        .HasForeignKey("EstadoSefinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "EstadoSenasa")
                        .WithMany()
                        .HasForeignKey("EstadoSenasaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Importador", "Importador")
                        .WithMany()
                        .HasForeignKey("ImportadorId");

                    b.HasOne("Dominio.Models.Catalogo", "Moneda")
                        .WithMany()
                        .HasForeignKey("MonedaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "Regional")
                        .WithMany()
                        .HasForeignKey("RegionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "TipoIdentificador")
                        .WithMany()
                        .HasForeignKey("TipoIdentificadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Usuario", "UsuarioAsignado")
                        .WithMany()
                        .HasForeignKey("UsuarioAsignadoId");
                });

            modelBuilder.Entity("Dominio.Models.RolPermiso", b =>
                {
                    b.HasOne("Dominio.Models.Permiso", "Permiso")
                        .WithMany()
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Rol", "Rol")
                        .WithMany("Permisos")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Models.Servicio", b =>
                {
                    b.HasOne("Dominio.Models.Catalogo", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "Moneda")
                        .WithMany()
                        .HasForeignKey("MonedaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "TipoCobro")
                        .WithMany()
                        .HasForeignKey("TipoCobroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Catalogo", "TipoServicio")
                        .WithMany()
                        .HasForeignKey("TipoServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Models.Usuario", b =>
                {
                    b.HasOne("Dominio.Models.Catalogo", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId");
                });

            modelBuilder.Entity("Dominio.Models.UsuarioRegional", b =>
                {
                    b.HasOne("Dominio.Models.Catalogo", "Regional")
                        .WithMany()
                        .HasForeignKey("RegionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Usuario", "Usuario")
                        .WithMany("UsuarioRegional")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Models.UsuarioRol", b =>
                {
                    b.HasOne("Dominio.Models.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Models.Usuario", "Usuario")
                        .WithMany("Roles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
