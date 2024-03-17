using Aplicacion.Commands.Recibo;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CommandHandlers
{
    public class AnularReciboHandler : AbstractHandler<AnularRecibo>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository user;
        private readonly ITokenService tokenService;
        public AnularReciboHandler(IReciboRepository reciboRepository, IMapper mapper, IUsuarioRepository user, ITokenService tokenService)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.user = user;
            this.tokenService = tokenService;
        }
        public override IResponse Handle(AnularRecibo message)
        {
            var rec = reciboRepository.GetById(message.idRecibo);

            rec.EliminarRecibo(DateTime.Now);

            reciboRepository.Update(rec.Id, rec);
            return new OkResponse();
        }
    }
}
