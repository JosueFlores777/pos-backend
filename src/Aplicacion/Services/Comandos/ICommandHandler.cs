using Aplicacion.Dtos;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services.Comandos
{
    public interface ICommandHandler<T> : ICommandHandler where T : IMessage
    {
        IResponse Handle(T message);
    }

    public interface ICommandHandler
    {
        IResponse ejecutar(IMessage message);
    }


}
