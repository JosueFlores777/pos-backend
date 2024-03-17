using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorFechaProcesado : Specification<Recibo>
    {
        public BuscarRecibosPorFechaProcesado(DateTime fecha1, DateTime fecha2) {
            if (fecha1 != null && fecha2 != null) {
                Query.Where(c => (c.FechaPago >= fecha1 && c.FechaPago <= fecha2));
            }
        }
       
    }
}
