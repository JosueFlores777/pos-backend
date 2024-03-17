using Aplicacion.Commands.Recibo;
using Aplicacion.Services.Validaciones;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Commands;
using FluentValidation;
using System.Linq;
using Dominio.Especificaciones;
using Aplicacion.Dtos;

namespace Aplicacion.Validators
{
    public class ProcesarReciboValidator : Validador<ProcesarRecibo>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITokenService tokenService;
        private readonly IServicioRepository servicioRepository;
        private readonly ISefinClient sefinClient;
        public ProcesarReciboValidator(ISefinClient sefinClient, IAutenticationHelper autenticationHelper, IUsuarioRepository usuarioRepository, IServicioRepository servicioRepository, ITokenService tokenService, IUsuarioRepository user, IReciboRepository reciboRepository) : base(autenticationHelper)
        {
            RuleFor(x => x.Recibo.Id).NotEmpty().Must(c => ExisteRecibo(c)).WithMessage("El recibo debe estar pagado y creado");
            //RuleFor(x => x.Recibo.Id).NotEmpty().Must(c => ExisteReciboSefin(c)).WithMessage("El recibo no existe");
            RuleFor(x => x.Recibo).NotEmpty()
                .Must(c => MismaRegional(c)).WithMessage("Debes Pertenecer a la misma regional del recibo");
            RuleFor(x => x.Recibo).NotEmpty().Must(c=> EditRegional(c)).WithMessage("El Comentario es obligatorio");


            this.reciboRepository = reciboRepository;
            this.usuarioRepository = usuarioRepository;
            this.tokenService = tokenService;
            this.servicioRepository = servicioRepository;
            this.sefinClient = sefinClient;
        }
        private bool ExisteRecibo(int  reciboId)
        {
           
            var rec = reciboRepository.Filter(new BuscarReciboPorId(reciboId)).FirstOrDefault();
            if (rec.EstadoSefinId == 7 && rec.EstadoSenasaId == 6) {
                return true;
            }
            return false;
            
        }
        private bool EditRegional(DtoRecibo recibo) {
            if (String.IsNullOrEmpty(recibo.Comentario) && recibo.RegionalBool)
            {
                return true;
            }else if(!String.IsNullOrEmpty(recibo.Comentario)){
                return true;
            }
            return false;
        }
        private bool MismaRegional(DtoRecibo recibo)
        {
            var rec = reciboRepository.Filter(new BuscarReciboPorId(recibo.Id)).FirstOrDefault();
            var user = usuarioRepository.GetByIdConRegionales(tokenService.GetIdUsuario());
            if (recibo.RegionalBool) return true;
            foreach (Dominio.Models.UsuarioRegional regional in user.UsuarioRegional) {
                if (rec.RegionalId == regional.RegionalId) {
                    return true;
                }
            }
            return false;

        }
        
        public override IList<string> Permisos => new List<string> { };
    }
}