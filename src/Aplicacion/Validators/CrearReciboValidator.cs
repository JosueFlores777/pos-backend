using System.Collections.Generic;
using System.Linq;
using Aplicacion.Commands;
using Aplicacion.Dtos;
using Aplicacion.Services.Validaciones;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using FluentValidation;

namespace Aplicacion.Validators
{
    class CrearReciboValidator: Validador<CrearRecibo>
    {
        private readonly ITokenService tokenService;
        private readonly IImportadorRepository importRepo;

        public CrearReciboValidator(IAutenticationHelper autenticationHelper, ITokenService tokenService, IImportadorRepository importRepo) : base(autenticationHelper)
        {
          
            this.tokenService = tokenService;
            this.importRepo = importRepo;
            RuleFor(x => x.Recibo).NotEmpty();
            RuleFor(x => x.Recibo.DetalleRecibos).NotEmpty().WithMessage("Seleccione un servicio");
            RuleFor(x => x.Recibo).Must(c=> validate(c) ).WithMessage("El monto del Recibo debe ser mayor que 0.00");

        }

        public bool validate(DtoRecibo recibo)
        {
            if (recibo.MontoTotal <= 0 && recibo.DescuentoId != 12570)
            {
                return false;
            }
            return true;
        }
        public override IList<string> Permisos => new List<string> {  };
    }
}
