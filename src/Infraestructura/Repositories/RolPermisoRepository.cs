using Dominio.Models;
using Dominio.Repositories;
using Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Repositories
{
    public class RolPermisoRepository : GenericRepository<RolPermiso>, IRolPermisoRepository
    {
        public RolPermisoRepository(RecibosContext dbContext) : base(dbContext)
        {
        }
    }
}
