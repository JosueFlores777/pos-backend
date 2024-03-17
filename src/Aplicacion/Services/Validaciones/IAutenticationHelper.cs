using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Services.Validaciones
{
    public interface IAutenticationHelper
    {
         void Autenticado(IList<string> permisos);
    }
}
