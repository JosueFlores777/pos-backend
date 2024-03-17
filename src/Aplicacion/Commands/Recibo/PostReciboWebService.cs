using Aplicacion.Dtos;
using Dominio.Models;
using Dominio.Service;


namespace Aplicacion.Commands.Recibo
{
    public class PostReciboWebService : IMessage
    {
        public ReciboResponse recibo { get; set; } 

    }
}
