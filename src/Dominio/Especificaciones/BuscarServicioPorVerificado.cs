using Ardalis.Specification;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarServicioPorVerificado : Specification<Servicio>
    {
        public BuscarServicioPorVerificado(int verificado)
        {
            if (verificado == 1)
            {
                Query.Where(c => c.Verificado == true && c.Activo == true);
            }
            else if (verificado == 3) {
                Query.Where(c => c.Verificado == false && c.Activo == true);
            }
        }
    }
}
