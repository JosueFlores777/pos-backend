using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands.Consultas
{
    public class ConsultarCambiosEstadosRecibo : IMessage
    {
        public ConsultarCambiosEstadosRecibo(int reciboId)
        {
            this.reciboId = reciboId;
        }

        public int reciboId { get; }
    }
}
