using Dominio.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dominio.Service
{
    public interface IPdfHelper
    {
        Stream Traerpermiso(Recibo recibo);
        Stream traerReporte(ReciboReporteConsolidado recibos);

    }
}
