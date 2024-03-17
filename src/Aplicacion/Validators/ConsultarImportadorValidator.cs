using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators
{
    class ConsultarImportadorValidator : Validador<ConsultarImportador>
    {
        public ConsultarImportadorValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
            RuleFor(x => x.Identificador).NotEmpty();
        }

        public override IList<string> Permisos => new List<string> ();
    }
}
