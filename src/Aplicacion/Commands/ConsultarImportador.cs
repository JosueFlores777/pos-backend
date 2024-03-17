using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands
{
   public class ConsultarImportador: IMessage
    {
        public string Identificador { get; set; }
    }
}
