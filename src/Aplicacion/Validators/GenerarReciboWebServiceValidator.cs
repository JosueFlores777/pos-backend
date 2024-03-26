using System.Collections.Generic;
using System.Linq;
using Aplicacion.Commands;
using Aplicacion.Commands.Recibo;
using Aplicacion.Dtos;
using Aplicacion.Services.Validaciones;

using Dominio.Repositories;
using Dominio.Service;
using FluentValidation;

namespace Aplicacion.Validators
{
    class GenerarReciboWebServiceValidator : Validador<GenerarReciboWebService>
    {
        private readonly ITokenService tokenService;
        private readonly IClienteRepository importRepo;

        public GenerarReciboWebServiceValidator(IAutenticationHelper autenticationHelper, ITokenService tokenService, IClienteRepository importRepo) : base(autenticationHelper)
        {
          
            this.tokenService = tokenService;
            this.importRepo = importRepo;
            RuleFor(x => x.Recibo).NotEmpty();
            RuleFor(x => x).Must(c => !string.IsNullOrEmpty(tokenService.GetIdUsuarioWebService())).WithMessage("No Autorizado");
            RuleFor(x => x.Recibo.MontoTotal).Must(c=> c > 0).WithMessage("El monto del Recibo debe ser mayor que 0.00");

        }
     

        public override IList<string> Permisos => new List<string> {  };
    }
}
