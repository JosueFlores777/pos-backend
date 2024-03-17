using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators
{
    class ConsultarCatalogoValidator : Validador<ConsultarCatalogo>
    {
        public ConsultarCatalogoValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
            RuleFor(x => x.Tipo).NotEmpty();
        }
        public override IList<string> Permisos => new List<string> ();
    }
}
