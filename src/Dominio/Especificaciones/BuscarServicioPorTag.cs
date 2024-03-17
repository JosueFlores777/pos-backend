using Ardalis.Specification;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarServicioPorTag : Specification<Servicio>
    {
        public BuscarServicioPorTag(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                Query.Where(c => c.Tag.ToLower().Contains(tag.ToLower()) && c.Activo == true);
            }

        }
    }
}

//BuscarServicioPorTag