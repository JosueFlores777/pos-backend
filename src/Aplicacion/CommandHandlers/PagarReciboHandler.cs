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
    public class PagarReciboHandler : AbstractHandler<PagarRecibo>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository user;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;
        public PagarReciboHandler(IReciboRepository reciboRepository, IConfiguration configuration, IMapper mapper, IUsuarioRepository user, ITokenService tokenService)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.user = user;
            this.tokenService = tokenService;
            this.configuration = configuration;
        }
        public override IResponse Handle(PagarRecibo message)
        {
            var ambiente = configuration.GetValue<string>("AppSettings:Environment");
            var rec = reciboRepository.GetById(message.idRecibo);
            if (ambiente.Equals("test"))
            {
                rec.PagarRecibo(DateTime.Now, "BAC TEST");
            }
            reciboRepository.Update(rec.Id, rec);
            return new OkResponse();
        }
    }
}
