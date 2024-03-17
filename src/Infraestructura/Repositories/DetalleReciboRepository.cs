using Dominio.Models;
using Dominio.Repositories;
using Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Repositories
{
    public class DetalleReciboRepository : GenericRepository<DetalleRecibo>, IDetalleReciboRepository
    {
        public DetalleReciboRepository(RecibosContext dbContext) : base(dbContext)
        { 
        }
    }
}
