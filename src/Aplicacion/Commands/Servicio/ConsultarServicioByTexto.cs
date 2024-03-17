using Dominio.Repositories.Extenciones;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands.Servicio
{
    public class ConsultarServicioByTexto : QueryStringParameters, IMessage
    {
        public string Nombre { get; set; }
    }
}
