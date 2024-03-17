using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;

namespace Dominio.Especificaciones
{
    public class OrdenarRecibosPorId : Specification<Recibo>
    {
        public OrdenarRecibosPorId()
        {
            Query.OrderByDescending(c => c.Id); ;
        }
    }
}
