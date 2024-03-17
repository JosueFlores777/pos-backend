using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorArea : Specification<Recibo>
    {
        public BuscarRecibosPorArea(int id) {
            if (id != 0) {
                Query.Where(c => c.AreaId == id);
            }
        }
       
    }
}
