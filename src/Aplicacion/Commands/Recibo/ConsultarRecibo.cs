using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands
{
   public class ConsultarRecibo: IMessage
    {
        public int Id { get; set; }
    }
}
