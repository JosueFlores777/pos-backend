using Ardalis.Specification;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarServicioPorCategoriaId : Specification<Servicio>
    {
        public BuscarServicioPorCategoriaId(int categoriaId)
        {
            if (categoriaId != 0)
            {
                Query.Where(c => c.CategoriaId == categoriaId && c.Activo == true);
            }
            
           
        }
    }
}
