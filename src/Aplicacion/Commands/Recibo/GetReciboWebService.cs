using Aplicacion.Dtos;
using Dominio.Service;

namespace Aplicacion.Commands.Recibo
{
    public class GetReciboWebService : IMessage
    {
        public int id { get; set; }
        public ResponseReciboDTO Recibo { get; set; }
    }
}
