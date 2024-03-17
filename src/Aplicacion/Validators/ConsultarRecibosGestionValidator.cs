using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators
{
    class ConsultarRecibosGestionValidator : Validador<ConsultarRecibosGestion>
    {
        public ConsultarRecibosGestionValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
          
        }

        public override IList<string> Permisos => new List<string> {   };
    }
}
