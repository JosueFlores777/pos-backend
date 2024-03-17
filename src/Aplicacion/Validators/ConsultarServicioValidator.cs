using System.Collections.Generic;
using Aplicacion.Commands;
using Aplicacion.Commands.Servicio;
using Aplicacion.Services.Validaciones;
using FluentValidation;

namespace Aplicacion.Validators.Producto
{
    public class ConsultarServicioValidator : Validador<ConsultarServicio>
    {
        public ConsultarServicioValidator(IAutenticationHelper autenticationHelper) : base(autenticationHelper)
        {
            
        }

        public override IList<string> Permisos => new List<string> { };
    }
}
