using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositories.Extenciones
{
    public interface IConsulta
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
