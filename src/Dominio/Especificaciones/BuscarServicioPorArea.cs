using Ardalis.Specification;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarServicioPorArea : Specification<Servicio>
    {
        public BuscarServicioPorArea(int areaId)
        {
            if (areaId != 0)
            {
                Query.Where(c => c.AreaId == areaId  && c.Activo == true);
            }


        }
    }
}

