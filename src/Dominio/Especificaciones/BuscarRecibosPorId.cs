using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorId : Specification<Recibo>
    {
        public BuscarRecibosPorId(int id) {
            if (id != 0) {
                Query.Where(c => c.Id.ToString().Contains(id.ToString()));
            }
        }
       
    }
}
