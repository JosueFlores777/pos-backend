using Aplicacion.Commands;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace Aplicacion.CommandHandlers
{
    public class ConsultarReciboHandler : AbstractHandler<ConsultarRecibo>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly ISefinClient sefinClient;
        private readonly IConfiguration configuration;

        public ConsultarReciboHandler(IReciboRepository reciboRepository, IMapper mapper, ISefinClient sefinClient, IConfiguration configuration)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.sefinClient = sefinClient;
            this.configuration = configuration;
        }

        public override IResponse Handle(ConsultarRecibo message)
        {
            //GETTING RECIBO FROM SEFIN CLIENT
            var recibo = reciboRepository.ReciboConDetalle(message.Id);
            var ambiente = configuration.GetValue<string>("AppSettings:Environment");
            if (ambiente.Equals("production")){
                var sefinRecibo = sefinClient.GetRecibo((uint)recibo.Id);
                recibo.LastSync = DateTime.Now;
                if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboPagado))
                {
                    recibo.PagarRecibo(sefinRecibo.FechaMod, sefinRecibo.UsuarioModificacion);
                }
                else if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboEliminado))
                {

                    recibo.EliminarRecibo(sefinRecibo.FechaMod);

                } else if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboProcesado) && recibo.EstadoSefinId != Recibo.EstadoReciboProcesado) {

                    recibo.ProcesarReciboTemporal("Procesado Por Administrador",recibo.RegionalId,1,sefinRecibo.FechaMod);

                }
            }
            reciboRepository.Update(recibo.Id, recibo);

            recibo = reciboRepository.ReciboConDetalle(message.Id);
            var respuesta = mapper.Map<DtoRecibo>(recibo);
            return respuesta;
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
