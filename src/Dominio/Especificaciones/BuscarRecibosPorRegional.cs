using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorRegional : Specification<Recibo>
    {
        public BuscarRecibosPorRegional(int id) {
            if (id != 0) {
                Query.Where(c => c.RegionalId == id);
            }
        }
       
    }
}
