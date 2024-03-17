using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorFecha : Specification<Recibo>
    {
        public BuscarRecibosPorFecha(DateTime fecha1, DateTime fecha2) {
            if (fecha1 != null && fecha2 != null) {
                Query.Where(c => (c.FechaCreacion >= fecha1 && c.FechaCreacion <= fecha2));
            }
        }
       
    }
}
