using Aplicacion.Commands.Consultas;
using Aplicacion.Services.Validaciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators.Consultas
{
    public class ConsultarCambiosEstadosReciboValidator : Validador<ConsultarCambiosEstadosRecibo>
    {
        public ConsultarCambiosEstadosReciboValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
            
        }

        public override IList<string> Permisos => new List<string> {  };
    }
}
