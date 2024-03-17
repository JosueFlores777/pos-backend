using Dominio.Repositories.Extenciones;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands
{
    public class ConsultarRecibosGestion : QueryStringParameters, IMessage
    {
        public bool Reporte { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int AreaId { get; set; }
        public int RegionalId { get; set; }
        public int idEstadoSefin { get; set; }
        public int idEstadoSenasa { get; set; }
        public string nombreRazon { get; set; } 
    }
}
