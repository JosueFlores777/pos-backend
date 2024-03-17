using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators
{
    class ConsultarRecibosDashboardUsuarioExternoValidator : Validador<ConsultarRecibosDashboardUsuarioExterno>
    {
        public ConsultarRecibosDashboardUsuarioExternoValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
          
        }

        public override IList<string> Permisos => new List<string> {   };
    }
}
