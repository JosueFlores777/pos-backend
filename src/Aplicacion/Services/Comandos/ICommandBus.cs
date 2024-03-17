using Aplicacion.Dtos;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Services.Comandos
{
    public interface ICommandBus
    {
        IResponse execute(IMessage comando);
    }
}
