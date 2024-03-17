using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Dtos.Servicio;
namespace Aplicacion.Dtos
{
    public class DtoReciboReporte : IResponse
    {

        public bool Reporte { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int TotalRecibos { get; set; }
        public int RecibosSinPagar { get; set; }
        public int RecibosPagados { get; set; }
        public int RecibosUtilizados { get; set; }
        public int RecibosPorVencer { get; set; }
        public double PorcentajeReciboPorProcesar { get; set; }
        public double MontoTotal { get; set; }
        public string nombreRazon { get; set; }
        public int AreaId { get; set; }
        public int RegionalId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
