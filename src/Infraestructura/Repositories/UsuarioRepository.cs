using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using Infraestructura.Data;
using Infraestructura.Repositories.Extenciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infraestructura.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly RecibosContext dbContext;
        public UsuarioRepository(RecibosContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public Usuario GetByIdConRegionales(int id)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Include(c => c.UsuarioRegional).FirstOrDefault(e => e.Id == id);
        }
        public Usuario GetByIdConAreas(int id)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Include(c => c.UsuarioArea).FirstOrDefault(e => e.Id == id);
        }
        public Usuario GetUsuarioConRolPermiso(ISpecification<Usuario> busqueda)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Include("Roles.Rol.Permisos.Permiso").Include(c => c.Departamento).FirstOrDefault(busqueda.Traer());
        }
    }

}
