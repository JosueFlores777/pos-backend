using Aplicacion.Dtos;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands.Recibo
{
    public class GenerarReciboWebService : IMessage
    {
        public ReciboWebService Recibo { get; set; }
    }
}
