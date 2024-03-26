using System.Collections.Generic;
using System.Linq;
using Aplicacion.Commands.Recibo;
using Aplicacion.Services.Validaciones;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using FluentValidation;

namespace Aplicacion.Validators
{
    public class PostReciboWebServiceValidator : Validador<PostReciboWebService>
    {
        private readonly ITokenService tokenService;
        private readonly IClienteRepository importRepo;
        private readonly IReciboRepository reciboRepository;
        public PostReciboWebServiceValidator(IReciboRepository reciboRepository, IAutenticationHelper autenticationHelper, ITokenService tokenService, IClienteRepository importRepo) : base(autenticationHelper)
        {

            this.tokenService = tokenService;
            this.importRepo = importRepo;
            this.reciboRepository = reciboRepository;
            RuleFor(x => x).Must(c => !string.IsNullOrEmpty(tokenService.GetIdUsuarioWebService())).WithMessage("No Autorizado");

        }
        private bool VerificarEstados(ReciboResponse recibo)
        {

            var reciboDb = reciboRepository.GetById((int)recibo.NroRecibo);
            if (reciboDb.EstadoSefinId == 7 && reciboDb.EstadoSenasaId == 6) {
                return true;
            }
            return false;
        }
        private bool VerificarRecibo(ReciboResponse recibo)
        {

            var reciboDb = reciboRepository.GetById((int)recibo.NroRecibo);
            if (reciboDb !=null)
            {
                return true;
            }
            return false;
        }
        public override IList<string> Permisos => new List<string> { };
    }
}
