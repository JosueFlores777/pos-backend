using Dominio.Repositories.Extenciones;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands
{
    public class ConsultarRecibosDashboardUsuarioExterno : QueryStringParameters, IMessage
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int AreaId { get; set; }
        public int RegionalId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
