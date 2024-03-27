using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;
using Dominio.Models;

namespace Dominio.Especificaciones
{
    public class BuscarRecibosPorImportador : Specification<Recibo>
    {
        public BuscarRecibosPorImportador(int idImportador) {
            if (idImportador != 0) {
                Query.Where(c => c.ClienteId == idImportador);
            }
        }
       
    }
}
