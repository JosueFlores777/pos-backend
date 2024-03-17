using System.Collections.Generic;
using System.Linq;
using Aplicacion.Commands.Recibo;
using Aplicacion.Dtos;
using Aplicacion.Services.Validaciones;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using FluentValidation;

namespace Aplicacion.Validators
{
    public class GetReciboWebServiceValidator : Validador<GetReciboWebService>
    {
        private readonly ITokenService tokenService;
        private readonly IImportadorRepository importRepo;
        private readonly IReciboRepository reciboRepository;
        public GetReciboWebServiceValidator(IAutenticationHelper autenticationHelper, IReciboRepository reciboRepository, ITokenService tokenService, IImportadorRepository importRepo) : base(autenticationHelper)
        {

            this.tokenService = tokenService;
            this.importRepo = importRepo;
            this.reciboRepository = reciboRepository;
            RuleFor(x => x.id).NotEmpty().WithMessage("Id Requerido");
            RuleFor(x => x).Must(c => !string.IsNullOrEmpty(tokenService.GetIdUsuarioWebService())).WithMessage("No Autorizado");

        }
        private bool VerificarRecibo(int recibo)
        {

            var reciboDb = reciboRepository.GetById(recibo);
            if (reciboDb != null)
            {
                return true;
            }
            return false;
        }

        public override IList<string> Permisos => new List<string> { };
    }

}
