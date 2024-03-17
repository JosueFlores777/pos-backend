using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;
namespace Aplicacion.Commands
{
    public class GetToken : IMessage
    {
        public string usuario { get; set; }
        public string password { get; set; }
    }
}
