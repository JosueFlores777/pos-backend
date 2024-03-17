using Dominio.Especificaciones;
using Dominio.Repositories;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using Infraestructura.Data;
using Infraestructura.Repositories.Extenciones;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dominio.Models;

namespace Infraestructura.Repositories
{
    public class ServicioRepository : GenericRepository<Servicio>, IServicioRepository
    {
        private readonly RecibosContext dbContext;

        public ServicioRepository(RecibosContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Servicio GetServicio(int id)
        {
          
            return dbContext
                .Set<Servicio>().Include(c => c.RangoCobros).FirstOrDefault(c => c.Id == id);
        }
    }
}
