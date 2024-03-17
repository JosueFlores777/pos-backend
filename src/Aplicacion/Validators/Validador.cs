using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using Dominio.Exceptions;
using Dominio.Service;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Aplicacion.Validators
{
    public abstract class Validador<T> : AbstractValidator<T>, IValidador
    {
        private readonly IAutenticationHelper autenticationHelper;

        public Validador(IAutenticationHelper autenticationHelper)
        {
            this.autenticationHelper = autenticationHelper;
        }
        public abstract IList< string> Permisos { get; }

        public void Validar(IMessage comando)
        {
            VerificarUsuario();
            ValidarComando(comando);
        }

        public void ValidarComando(IMessage comando)
        {
            var reult = Validate((T)comando);
            if (!reult.IsValid)
            {
                var errores = new List<string>();
                foreach (var failure in reult.Errors) {
                    errores.Add(HttpUtility.HtmlAttributeEncode(failure.ErrorMessage));
                        };
                if (errores.Count > 0) throw new HttpException(422, JsonConvert.SerializeObject(errores));
            }
        }

        public void VerificarUsuario()
        {
            autenticationHelper.Autenticado(this.Permisos);
        }
    }
}
