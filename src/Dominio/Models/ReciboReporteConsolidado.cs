using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dominio.Models
{
    public class ReciboReporteConsolidado : IEntity
    {
        public int Id { get; set; }
        public IList<ReciboReportePDF> reciboReportes { get; set; }
        public string Mes { get; set; }
        public int TotalRecibosTasas { get; set; }
        public int TotalRecibosEmision { get; set; }
        public int TotalRecibosDevolucion { get; set; }
        public int TotalRecibosMultas { get; set; }
        public int TotalRecibosVentas { get; set; }
        public double MontoTotalRecibosTasas { get; set; }
        public double MontoTotalRecibosEmision { get; set; }
        public double MontoTotalRecibosDevolucion { get; set; }
        public double MontoTotalRecibosMultas { get; set; }
        public double MontoTotalRecibosVentas { get; set; }

    }
}
