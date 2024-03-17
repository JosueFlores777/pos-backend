using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Services.Validaciones
{
    public interface IValidatorService
    {
        void AplicarValidador(IMessage message);
    }
}
