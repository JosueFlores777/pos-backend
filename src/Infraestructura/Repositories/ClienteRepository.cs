using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infraestructura.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private readonly RecibosContext dbContext;

        public ClienteRepository(RecibosContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Cliente GetClienteConCatalogo(ISpecification<Cliente> busqueda)
        {
            return dbContext.Set<Cliente>().AsNoTracking().Include(c=>c.Departamento).Include(c=>c.Municipio).FirstOrDefault(busqueda.Traer());
        }
    }
}
