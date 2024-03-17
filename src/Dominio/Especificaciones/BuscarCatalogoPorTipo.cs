using Ardalis.Specification;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{

    public class BuscarCatalogoPorTipo : Specification<Catalogo>
    {

        public BuscarCatalogoPorTipo(string tipo)
        {
            if (!string.IsNullOrWhiteSpace(tipo))
            {
                Query.Where(c => c.Tipo == tipo);
            }

        }

    }
}

