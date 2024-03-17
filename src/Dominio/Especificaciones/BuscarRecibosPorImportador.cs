using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorIdArdalis : Specification<Recibo>
    {
        public BuscarRecibosPorIdArdalis(int idRecibo) {
            if (idRecibo != 0) {
                Query.Where(c => c.Id == idRecibo);
            }
        }
       
    }
}
