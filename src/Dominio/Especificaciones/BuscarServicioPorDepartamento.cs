using Ardalis.Specification;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarServicioPorDepartamento : Specification<Servicio>
    {
        public BuscarServicioPorDepartamento(int idDepartamento)
        {
            if (idDepartamento != 0)
            {
                Query.Where(c => c.DepartamentoId == idDepartamento && c.Activo == true);
            }

        }
    }
}

//BuscarServicioPorTag