using Aplicacion.Dtos;
using Dominio.Service;

namespace Aplicacion.Commands
{
   public class CrearRecibo:IMessage
    {
        public DtoRecibo Recibo { get; set; }
    }
}
