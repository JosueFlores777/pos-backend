

using Dominio.Models;
using Dominio.Repositories;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Repositories
{
    public class UsuarioRolRepository : GenericRepository<UsuarioRol>, IUsuarioRolRepository
    {
        private readonly RecibosContext dbContext;

        public UsuarioRolRepository(RecibosContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }


        public IEnumerable<UsuarioRol> FilterWithDetalle(Func<UsuarioRol, bool> predicate)
        {
            var respultado = dbContext.Set<UsuarioRol>().AsNoTracking().Include("Usuario").Where(predicate);

            return respultado;
        }
    }
}
