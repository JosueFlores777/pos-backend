using Aplicacion.Commands;
using Aplicacion.Commands.Servicio;
using Aplicacion.Dtos;
using Aplicacion.Dtos.Servicio;
using AutoMapper;
using Dominio.Models;

using Dominio.Repositories;
using Dominio.Service;
using System;

namespace Aplicacion.CommandHandlers
{
    public class EditarServicioHandler : AbstractHandler<EditarServicio>
    {
        private readonly IServicioRepository servicioRepository;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository user;
        private readonly ITokenService tokenService;

        public EditarServicioHandler(IServicioRepository servicioRepository, IMapper mapper, IUsuarioRepository user, ITokenService tokenService)
        {
            this.servicioRepository = servicioRepository;
            this.mapper = mapper;
            this.user = user;
            this.tokenService = tokenService;
        }
        public override IResponse Handle(EditarServicio message)
        {
            var servicio = mapper.Map<Dominio.Models.Servicio>(message.ServicioCompleto);
            servicio.FechaModificacion = DateTime.Now;
            servicio.Inicializar();
            servicio.Codigo = message.ServicioCompleto.Codigo;
            if (message.ServicioCompleto.MetodoVerificacion) {
                servicio.Verificado = true;
            }
            servicioRepository.Update(servicio.Id, servicio);
            return new OkResponse();
        }
    }
}
