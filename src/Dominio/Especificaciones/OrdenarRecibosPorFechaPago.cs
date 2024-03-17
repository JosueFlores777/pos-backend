using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class OrdenarRecibosPorFechaPago : Specification<Recibo>
    {
        public OrdenarRecibosPorFechaPago()
        {
            Query.OrderBy(c => c.FechaPago);
        }

    }
}
