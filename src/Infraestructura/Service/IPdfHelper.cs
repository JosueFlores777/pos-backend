using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using Infraestructura.Service.Permisos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infraestructura.Service
{
    public class PdfHelper : IPdfHelper
    {
        private readonly IEnumerable<IPermisoRecibo> permisos;

        public PdfHelper(IEnumerable<IPermisoRecibo> permisos) {
            this.permisos = permisos;

        }
        public Stream Traerpermiso(Dominio.Models.Recibo recibo)
        {
            Stream respuesta = null;
            foreach (var permiso in permisos)
            {
                   respuesta= permiso.Getpermiso(recibo);   
            }
            return respuesta;
        }
        public Stream traerReporte(Dominio.Models.ReciboReporteConsolidado recibo)
        {
            Stream respuesta = null;
            foreach (var permiso in permisos)
            {
                respuesta = permiso.GetReportePDF(recibo);
            }
            return respuesta;
        }
    }
}
