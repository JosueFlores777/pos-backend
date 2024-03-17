using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;


namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorNombreRazon: Specification<Recibo>
    {
        public BuscarRecibosPorNombreRazon(string nombre) {
            if (nombre != "" && nombre != null) {
                Query.Where(c => c.NombreRazon.ToLower().Contains(nombre.ToLower()) );
            }
        }
       
    }
}
