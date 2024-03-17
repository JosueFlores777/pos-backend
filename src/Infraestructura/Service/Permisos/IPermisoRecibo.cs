using Dominio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Infraestructura.Service.Permisos
{
   public interface IPermisoRecibo
    {
        Stream Getpermiso(Dominio.Models.Recibo recibo);
        Stream GetReportePDF(ReciboReporteConsolidado recibo);
        bool SePuedeUtilizar(int depenedencia);
    }
}
