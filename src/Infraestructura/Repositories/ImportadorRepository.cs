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
    public class ImportadorRepository : GenericRepository<Importador>, IImportadorRepository
    {
        private readonly RecibosContext dbContext;

        public ImportadorRepository(RecibosContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Importador GetImportadorConCatalogos(ISpecification<Importador> busqueda)
        {
            return dbContext.Set<Importador>().AsNoTracking().Include(c=>c.Departamento).Include(c=>c.Municipio).FirstOrDefault(busqueda.Traer());
        }
    }
}
