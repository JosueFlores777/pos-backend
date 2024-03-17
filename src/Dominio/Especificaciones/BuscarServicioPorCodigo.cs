using Ardalis.Specification;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarServicioPorCodigo : Specification<Servicio>
    {
        public BuscarServicioPorCodigo(string codigo)
        {
            if (!string.IsNullOrWhiteSpace(codigo))
            {
                Query.Where(c => c.Codigo.ToLower().Contains(codigo.ToLower()) && c.Activo == true);
            }

        }
    }
}

//BuscarServicioPorTag