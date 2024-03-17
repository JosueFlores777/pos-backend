using Aplicacion.Commands.Recibo;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace Aplicacion.CommandHandlers
{
    public class ProcesarReciboHandler : AbstractHandler<ProcesarRecibo>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly ICorreoHelper correoHelper;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository user;
        private readonly ISefinClient sefinClient;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;

        public ProcesarReciboHandler(IReciboRepository reciboRepository, IConfiguration configuration, IMapper mapper, IUsuarioRepository user, ITokenService tokenService, ISefinClient sefinClient,ICorreoHelper correoHelper)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.correoHelper = correoHelper;
            this.user = user;
            this.tokenService = tokenService;
            this.configuration = configuration;
            this.sefinClient = sefinClient;
        }
        public override IResponse Handle(ProcesarRecibo message)
        {
            var ambiente = configuration.GetValue<string>("AppSettings:Environment");

            var rec = reciboRepository.GetById(message.Recibo.Id);
            var idUsuario = tokenService.GetIdUsuario();
            
            if (!message.Recibo.RegionalBool) {
                rec.ProcesarRecibo(message.Recibo.Comentario, idUsuario);
                if (ambiente.Equals("production"))
                {
                    sefinClient.ProcessRecibo((uint)rec.Id);
                }
            }
            rec.Comentario = message.Recibo.Comentario;
            rec.RegionalId = message.Recibo.RegionalId;
            reciboRepository.Update(rec.Id, rec);
            if (rec.ImportadorId != 0) {
                //correoHelper.EnviarPermisoCorreo(rec);
            }
            
            return new OkResponse();
        }
    }
}
