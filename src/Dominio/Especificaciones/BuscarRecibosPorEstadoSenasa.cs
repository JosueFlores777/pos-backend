using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorEstadoSenasa : Specification<Recibo>
    {
        public BuscarRecibosPorEstadoSenasa(int idEstadoSenasa) {
            if (idEstadoSenasa != 0) {
                Query.Where(c => c.EstadoSenasaId == idEstadoSenasa);
            }
        }
       
    }
}
