using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands
{
   public class DescargarReciboPDF : IMessage
    {
        public int nroRecibo { get; set; }
    }
}
