using Dominio.Models;

using Dominio.Service;
using Microsoft.EntityFrameworkCore;
using System;


namespace Infraestructura.Data
{
    public class RecibosContext : DbContext
    {
        private readonly ITokenService tokenService;

        public RecibosContext(DbContextOptions<RecibosContext> options, ITokenService tokenService)
      : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.tokenService = tokenService;
        }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Recibo> recibo { get; set; }
        public DbSet<CambioEstado> cambioEstado { get; set; }
        public DbSet<DetalleRecibo> detalleRecibo { get; set; }
        public DbSet<UsuarioRol> usuarioRol { get; set; }
        public DbSet<RolPermiso> rolPermiso { get; set; }
        public DbSet<UsuarioRegional> usuarioRegional { get; set; }
        public DbSet<Rol> rol { get; set; }

        /// <summary>


        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is IEntityAuditable)
                {
                    var entity = (IEntityAuditable)changedEntity.Entity;

                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.FechaCreacion = DateTime.Now;
                        entity.UsuarioCreo = tokenService.GetIdUsuario();
                    }
                    if (changedEntity.State == EntityState.Modified)
                    {

                        changedEntity.Context.Entry(entity).Property(x => x.FechaCreacion).IsModified = false;
                        changedEntity.Context.Entry(entity).Property(x => x.UsuarioCreo).IsModified = false;
                        entity.FechaModificacion = DateTime.Now;
                        entity.UsuarioModifica = tokenService.GetIdUsuario();
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
