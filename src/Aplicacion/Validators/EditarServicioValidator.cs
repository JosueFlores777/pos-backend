using Aplicacion.Commands.Servicio;
using Aplicacion.Services.Validaciones;
using Dominio.Repositories;
using Dominio.Service;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Aplicacion.Validators
{
    public class EditarServicioValidator : Validador<EditarServicio>
    {
        public EditarServicioValidator(IAutenticationHelper autenticationHelper, ITokenService tokenService, IUsuarioRepository user, IServicioRepository repository) : base(autenticationHelper)
        {

        }

        public override IList<string> Permisos => new List<string> { };
    }
}
