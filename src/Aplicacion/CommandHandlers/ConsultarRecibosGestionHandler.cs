using Aplicacion.Commands;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Especificaciones;
using Dominio.Models;

using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;


namespace Aplicacion.CommandHandlers
{
    class ConsultarRecibosGestionHandler : AbstractHandler<ConsultarRecibosGestion>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly ITokenService tokeService;
        private readonly IImportadorRepository importadorRepository;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ISefinClient sefinClient;
        public ConsultarRecibosGestionHandler(IReciboRepository  reciboRepository, ISefinClient sefinClient,
            ITokenService tokeService, IImportadorRepository importadorRepository, IMapper mapper,IUsuarioRepository usuarioRepository) {
            this.reciboRepository = reciboRepository;
            this.tokeService = tokeService;
            this.importadorRepository = importadorRepository;
            this.mapper = mapper;
            this.sefinClient = sefinClient;
            this.usuarioRepository = usuarioRepository;
        }
        public override IResponse Handle(ConsultarRecibosGestion message)
        {
            var estadoId = 0;
            var idImportador = 0;
            var usuario = usuarioRepository.GetById(tokeService.GetIdUsuario());
            var fecha1 = message.FechaInicio;
            var fecha2 = message.FechaFin;
            fecha2 = fecha2.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);
            var include = new Includes<Dominio.Models.Recibo>(new[] { "DetalleRecibos", "DetalleRecibos.Servicio" });

            if (usuario.TipoUsuario == Usuario.tipoImportador) {
                var importador = importadorRepository.GetByIdEspecificacion(new BuscarImportadorPorIndentificador(tokeService.GetIdentificacionUsuario()));
                idImportador =  importador.Id;
            }
            if (message.Reporte)
            {
                estadoId = Recibo.EstadoReciboProcesado;
            }
            else {
                estadoId = Recibo.EstadoReciboPagado;
            }
            

            var respuesta = reciboRepository.Specify(new BuscarRecibosPorImportador(idImportador))
                        .Specify(new BuscarRecibosPorEstadoSefin(estadoId))
                        .Specify(new BuscarRecibosPorArea(message.AreaId))
                        .Specify(new BuscarRecibosPorRegional(message.RegionalId))
                        .Specify(new BuscarRecibosPorFecha(fecha1, fecha2))
                        .Specify(include)
                        .Specify(new OrdenarRecibosPorFechaFinVigencia())
                        .Paginar(message);

            if (message.Reporte)
            {
               respuesta = reciboRepository.Specify(new BuscarRecibosPorImportador(idImportador))
                        .Specify(new BuscarRecibosPorEstadoSefin(estadoId))
                        .Specify(new BuscarRecibosPorArea(message.AreaId))
                        .Specify(new BuscarRecibosPorRegional(message.RegionalId))
                        .Specify(new BuscarRecibosPorNombreRazon(message.nombreRazon))
                        .Specify(new BuscarRecibosPorFechaProcesado(fecha1, fecha2))
                        .Specify(include)
                        .Specify(new OrdenarRecibosPorFechaPago())
                        .Paginar(message);
            }

            return mapper.Map<DtoRecibosPaginados>(respuesta);
        }
        
    }
}
