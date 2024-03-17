using Dominio.Service;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators
{
    public interface IValidador
    {
       void VerificarUsuario();
        void ValidarComando(IMessage comando);
        void Validar(IMessage comando);
        IList<string> Permisos { get; }
      
    }
}
