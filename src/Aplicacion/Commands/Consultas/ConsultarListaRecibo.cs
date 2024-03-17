using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands.Consultas
{
    public class ConsultarListaRecibo : IMessage
    {
        public ConsultarListaRecibo(DateTime FechaInicio, DateTime FechaFin, string NombreRazon)
        {
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
            this.NombreRazon = NombreRazon;
        }

        public int Mes { get; set; }
        public int Anio { get; set; }

        public string NombreRazon { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
