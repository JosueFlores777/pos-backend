using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using Dominio.Models.Regla;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Validators
{
    class DescargarReciboPDFValidator: Validador<DescargarReciboPDF>
    {
        public DescargarReciboPDFValidator(IAutenticationHelper autenticationHelper, IReciboEstadoAprobado aprobadas) : base(autenticationHelper)
        {
            
        }
        public override IList<string> Permisos => new List<string> { };
    }
}