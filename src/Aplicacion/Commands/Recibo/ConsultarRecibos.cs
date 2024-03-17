using Dominio.Repositories.Extenciones;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands
{
    public class ConsultarRecibos : QueryStringParameters, IMessage
    {
        public int numeroRecibo { get; set; }
        public int idEstadoSefin { get; set; }
        public int idEstadoSenasa { get; set; }
    }
}
