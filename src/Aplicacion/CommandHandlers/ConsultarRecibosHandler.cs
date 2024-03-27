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
using Microsoft.Extensions.Configuration;

namespace Aplicacion.CommandHandlers
{
    class ConsultarRecibosHandler : AbstractHandler<ConsultarRecibos>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly ITokenService tokeService;
        private readonly IClienteRepository cllienteRepository;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ISefinClient sefinClient;
        private readonly IConfiguration configuration;
        public ConsultarRecibosHandler(IReciboRepository  reciboRepository, ISefinClient sefinClient, IConfiguration configuration,
            ITokenService tokeService, IClienteRepository importadorRepository, IMapper mapper,IUsuarioRepository usuarioRepository) {
            this.reciboRepository = reciboRepository;
            this.tokeService = tokeService;
            this.cllienteRepository = importadorRepository;
            this.mapper = mapper;
            this.sefinClient = sefinClient;
            this.configuration = configuration;
            this.usuarioRepository = usuarioRepository;
        }
        public override IResponse Handle(ConsultarRecibos message)
        {
            var Async = true;
            var idImportador = 0;
            var usuario = usuarioRepository.GetById(tokeService.GetIdUsuario());
            var include = new Includes<Dominio.Models.Recibo>(new[] { "DetalleRecibos" , "DetalleRecibos.Servicio"  });
            var respuesta = reciboRepository.Paginar(message);
            var ambiente = configuration.GetValue<string>("AppSettings:Environment");
            if (usuario.TipoUsuario == Usuario.tipoCliente) {
                var importador = cllienteRepository.GetByIdEspecificacion(new BuscarClientePorIndentificador(tokeService.GetIdentificacionUsuario()));
                idImportador =  importador.Id;
            }
            while (Async == true)
            {
                Async = false;
                respuesta = reciboRepository.Specify(new BuscarRecibosPorImportador(idImportador))
                        .Specify(new BuscarRecibosPorEstadoSefin(message.idEstadoSefin))
                        .Specify(new BuscarRecibosPorEstadoSenasa(message.idEstadoSenasa))
                        .Specify(new BuscarRecibosPorId(message.numeroRecibo))
                        .Specify(new OrdenarRecibosPorId())
                        .Specify(include).Paginar(message);
                
                foreach (var recibo in respuesta)
                {
                    if ((recibo.EstadoSefinId == Recibo.EstadoReciboCreado || recibo.EstadoSefinId == Recibo.EstadoReciboPagado)  && DateTime.Now > recibo.LastSync.AddMinutes(5))
                    {
                        if (ambiente.Equals("production")) {
                            var sefinRecibo = sefinClient.GetRecibo((uint)recibo.Id);
                            recibo.LastSync = DateTime.Now;
                            if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboPagado))
                            {
                                recibo.PagarRecibo(sefinRecibo.FechaMod, sefinRecibo.UsuarioModificacion);
                                Async = true;
                            }
                            else if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboEliminado))
                            {

                                recibo.EliminarRecibo(sefinRecibo.FechaMod);
                                Async = true;
                            }else if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboProcesado) && recibo.EstadoSefinId != Recibo.EstadoReciboProcesado)
                            {

                                recibo.ProcesarReciboTemporal("Procesado Por Administrador", recibo.RegionalId, 1, sefinRecibo.FechaMod);

                            }
                        } 
                        
                        
                        reciboRepository.Update(recibo.Id, recibo); 
                    }
                }
            }
            return mapper.Map<DtoRecibosPaginados>(respuesta);
        }
        private string SeleccionarEstado(int id)
        {
            if (id == Dominio.Models.Recibo.EstadoReciboCreado)
            {
                return "CREADO";
            }
            else if (id == Dominio.Models.Recibo.EstadoReciboEliminado)
            {
                return "ELIMINADO";
            }
            else if (id == Dominio.Models.Recibo.EstadoReciboPagado)
            {
                return "PAGADO";
            }
            else if (id == Dominio.Models.Recibo.EstadoReciboProcesado)
            {
                return "PROCESADO";
            }
            else if (id == Dominio.Models.Recibo.EstadoReciboUtilizado)
            {
                return "UTILIZADO";
            }
            return "";
        }
    }
}
