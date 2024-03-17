using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public interface ISpecification<Entidad>
    {
        Func<Entidad, bool> Traer();
    }
}
