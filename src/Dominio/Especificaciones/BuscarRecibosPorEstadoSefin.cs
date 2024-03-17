using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorEstadoSefin : Specification<Recibo>
    {
        public BuscarRecibosPorEstadoSefin(int idEstadoSefin) {
            if (idEstadoSefin != 0) {
                Query.Where(c => c.EstadoSefinId == idEstadoSefin);
            }
        }
       
    }
}
