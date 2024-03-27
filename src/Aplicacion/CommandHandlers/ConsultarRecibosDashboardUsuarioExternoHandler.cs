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
    class ConsultarRecibosDashboardUsuarioExternoHandler : AbstractHandler<ConsultarRecibosDashboardUsuarioExterno>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly ITokenService tokeService;
        private readonly IClienteRepository clienteRepository;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ISefinClient sefinClient;
        public ConsultarRecibosDashboardUsuarioExternoHandler(IReciboRepository  reciboRepository, ISefinClient sefinClient,
            ITokenService tokeService, IClienteRepository importadorRepository, IMapper mapper,IUsuarioRepository usuarioRepository) {
            this.reciboRepository = reciboRepository;
            this.tokeService = tokeService;
            this.clienteRepository = importadorRepository;
            this.mapper = mapper;
            this.sefinClient = sefinClient;
            this.usuarioRepository = usuarioRepository;
        }
        public override IResponse Handle(ConsultarRecibosDashboardUsuarioExterno message)
        {
            message.PageNumber = 1;
            message.PageSize = 9999;
            var idImportador = 0;

            var usuario = usuarioRepository.GetById(tokeService.GetIdUsuario());
            var fecha1 = message.FechaInicio;
            var fecha2 = message.FechaFin;
            fecha2 = fecha2.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);

            var contTotalRecibos = 0;
            var contTotalCreados = 0;
            var contTotalPagados = 0;
            var contTotalUtilizados = 0;
            var montoTotal = 0.0;
            var recibosPorVencer = 0;
            var PorcentajeTotalRecibosProcesados = 0.0;

            if (usuario.TipoUsuario == Usuario.tipoCliente) {
                var importador = clienteRepository.GetByIdEspecificacion(new BuscarClientePorIndentificador(tokeService.GetIdentificacionUsuario()));
                idImportador =  importador.Id;
            }
            var include = new Includes<Dominio.Models.Recibo>(new[] { "DetalleRecibos", "DetalleRecibos.Servicio" });

            var respuesta = reciboRepository.Specify(new BuscarRecibosPorImportador(idImportador))
                        .Specify(new BuscarRecibosPorEstadoSefin(7))
                        .Specify(new BuscarRecibosPorArea(message.AreaId))
                        .Specify(new BuscarRecibosPorRegional(message.RegionalId))
                        .Specify(new BuscarRecibosPorFecha(fecha1, fecha2))
                        .Specify(include)
                        .Specify(new OrdenarRecibosPorFechaFinVigencia())
                        .Paginar(message);

            foreach (var item in respuesta)
            {
                if (item.EstadoSefinId == Recibo.EstadoReciboCreado)
                {
                    contTotalCreados++;
                }
                else if (item.EstadoSefinId == Recibo.EstadoReciboPagado) {
                    contTotalPagados++;
                } else if (item.EstadoSefinId == Recibo.EstadoReciboProcesado) {
                    contTotalUtilizados++;
                    montoTotal += item.MontoTotal;
                }
                if (reciboPorVencer(item)) {
                    recibosPorVencer++;
                };
                contTotalRecibos++;
            }
            if (contTotalPagados!=0) {
                double a = (contTotalUtilizados + contTotalPagados);
                double b = (contTotalPagados / a);
                double x = b * 100;
                PorcentajeTotalRecibosProcesados =100- x;
            }if ( (contTotalUtilizados + contTotalPagados) == contTotalUtilizados && (contTotalUtilizados + contTotalPagados) != 0) {
                PorcentajeTotalRecibosProcesados = 100.00;
            }
            

            DtoReciboReporte dtoReciboReporte = new DtoReciboReporte();
            dtoReciboReporte.TotalRecibos = contTotalRecibos;
            dtoReciboReporte.RecibosPagados = contTotalPagados;
            dtoReciboReporte.RecibosSinPagar = contTotalCreados;
            dtoReciboReporte.RecibosUtilizados = contTotalUtilizados;
            dtoReciboReporte.MontoTotal = montoTotal;
            dtoReciboReporte.RecibosPorVencer = recibosPorVencer;
            dtoReciboReporte.PorcentajeReciboPorProcesar = PorcentajeTotalRecibosProcesados;



            return dtoReciboReporte;
        }
        public bool reciboPorVencer(Recibo recibo) {
            if (recibo.FinVigencia != null) {
                if (recibo.FinVigencia.Value.AddDays(-10) <= DateTime.Now && recibo.EstadoSefinId == Recibo.EstadoReciboPagado)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
