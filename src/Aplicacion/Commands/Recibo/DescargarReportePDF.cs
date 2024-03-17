using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands
{
   public class DescargarReportePDF : IMessage
    {
        public DescargarReportePDF(DateTime FechaInicio, DateTime FechaFin, bool Reporte, string nombreRazon)
        {
            this.Reporte = Reporte;
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
            this.nombreRazon = nombreRazon;
        }
        public string nombreRazon { get; set; }
        public bool Reporte { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
