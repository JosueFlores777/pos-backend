using Ardalis.Specification;
using Dominio.Models;
using System;

namespace Dominio.Especificaciones
{ 

    public class BuscarCatalogoPorPadre : Specification<Catalogo>
    {

        public BuscarCatalogoPorPadre(int? idPadre)
        {
            if (idPadre.HasValue && idPadre.Value>0)
            {
                Query.Where(c => c.IdPadre == idPadre);
            }

        }

    }

}
