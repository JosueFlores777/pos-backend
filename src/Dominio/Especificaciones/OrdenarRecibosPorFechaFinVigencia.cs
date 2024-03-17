using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class OrdenarRecibosPorFechaFinVigencia : Specification<Recibo>
    {
        public OrdenarRecibosPorFechaFinVigencia()
        {
            Query.OrderBy(c => c.FinVigencia);
        }

    }
}
