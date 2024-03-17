using Aplicacion.Dtos;
using Aplicacion.Services.Comandos;
using Dominio.Service;

namespace Aplicacion.CommandHandlers
{
    public abstract class AbstractHandler <T>: ICommandHandler<T> where T:IMessage
    {
        public abstract IResponse Handle(T message);
        

        public IResponse ejecutar(IMessage message)
        {
            return Handle((T)message);
        }
    }

}
