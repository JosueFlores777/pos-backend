using Aplicacion.Commands;
using Aplicacion.Commands.Recibo;
using Aplicacion.Dtos;
using Aplicacion.Dtos.Servicio;
using AutoMapper;
using Dominio.Models;
using Microsoft.Extensions.Configuration;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using Dominio.Especificaciones;
using System.Linq.Dynamic.Core;

namespace Aplicacion.CommandHandlers
{
    public class GetReciboWebServiceHandler : AbstractHandler<GetReciboWebService>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokeService;
        private readonly IConfiguration configuration;
        private readonly IServicioRepository servicioRepository;
        private readonly ISefinClient sefinClient;
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IImportadorRepository importadorRepository;
        public GetReciboWebServiceHandler(IReciboRepository reciboRepository, IMapper mapper, ITokenService tokeService, IConfiguration configuration,
           ICatalogoRepository catalogoRepository, ISefinClient sefinClient, 
           IUnitOfWork unitOfWork, IImportadorRepository importadorRepository, IServicioRepository servicioRepository)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.tokeService = tokeService;
            this.servicioRepository = servicioRepository;
            this.sefinClient = sefinClient;
            this.catalogoRepository = catalogoRepository;
            this.unitOfWork = unitOfWork;
            this.importadorRepository = importadorRepository;
             
    }
        public override IResponse Handle(GetReciboWebService message)
        {

            UpdateRecibo(message.id);

            ResponseReciboDTO response = new ResponseReciboDTO();
            response.Entity = new List<ReciboResponse> { };
            response.Success = false;
            response.Message = "";
                        
            try {

                var consulta = reciboRepository.TraerDelSistemaPagos(message.id);

                var reciboViejo = mapper.Map<Dominio.Models.ReciboResponse>(consulta);



                var reciboMpp = mapper.Map<Dominio.Models.ReciboResponse>(reciboRepository.GetById(message.id));
                var include = new Includes<Dominio.Models.Recibo>(new[] { "DetalleRecibos", "DetalleRecibos.Servicio" });
                if (reciboMpp != null)
                {

                    var rec = reciboRepository.ReciboConDetalle(message.id);
                    reciboMpp.NroRecibo = reciboMpp.id;
                    reciboMpp.id = 0;
                    reciboMpp.ApiEstadoSefin = SeleccionarEstado(reciboMpp.EstadoSefinId);
                    reciboMpp.ApiEstadoSenasa = SeleccionarEstado(reciboMpp.EstadoSenasaId);
                    reciboMpp.Departamento = SelecionarDepartamento(rec.DetalleRecibos[0].Servicio.DepartamentoId);
                    string servicio = rec.DetalleRecibos[0].Servicio.Tag;
                    reciboMpp.Servicio = servicio.Replace('\t', ' ');

                    if (reciboMpp.MontoTotal - Math.Floor(reciboMpp.MontoTotal) < 0.01)
                    {
                        reciboMpp.MontoTotal = Math.Floor(reciboMpp.MontoTotal);
                    }

                    response.Entity.Add(reciboMpp);
                    response.Success = true;
                    response.Message = "";

                }
                else if (reciboViejo != null)
                {
                    if (!String.IsNullOrEmpty(reciboViejo.Comentario)) {
                        if (reciboViejo.Comentario.Contains("ByPass"))
                        {
                            string comentario = reciboViejo.Comentario.Replace("(ByPass) - 1", "");
                            reciboViejo.Comentario = comentario;
                        }
                        else if (reciboViejo.Comentario.Contains("-"))
                        {
                            string comentario = reciboViejo.Comentario.Replace(" - ", "");
                            reciboViejo.Comentario = comentario;
                        }
                    }

                    reciboViejo.NroRecibo = reciboViejo.id;
                    reciboViejo.id = 0;
                    reciboViejo.ApiEstadoSefin = SeleccionarEstado(reciboViejo.EstadoSefinId);
                    reciboViejo.ApiEstadoSenasa = SeleccionarEstado(reciboViejo.EstadoSenasaId);
                    reciboViejo.Departamento = "";
                    reciboViejo.Servicio = "";
             
                    response.Entity.Add(reciboViejo);
                    response.Success = true;
                    response.Message = "";
                }
                else
                {
                    response.Entity = null;
                    response.Success = false;
                    response.Message = "No se ha podido encontrar el recibo";
                }
            }
            catch (Exception e){
                response.Entity = null;
                response.Success = false;
                response.Message = e.Message;

            }
            


            return  mapper.Map<ResponseReciboDTO>(response);

        }
        private void UpdateRecibo(int id)
        {
            var reciboMpp = mapper.Map<Dominio.Models.ReciboResponse>(reciboRepository.GetById(id));
            if (reciboMpp != null){
                var recibo = reciboRepository.ReciboConDetalle(id);
                var sefinRecibo = sefinClient.GetRecibo((uint)recibo.Id);
                recibo.LastSync = DateTime.Now;

                if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboPagado))
                {
                    recibo.PagarRecibo(sefinRecibo.FechaMod, sefinRecibo.UsuarioModificacion);

                }
                else if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboEliminado))
                {
                    recibo.EliminarRecibo(sefinRecibo.FechaMod);
                }else if (sefinRecibo.ApiEstado == SeleccionarEstado(Recibo.EstadoReciboProcesado) && recibo.EstadoSefinId != Recibo.EstadoReciboProcesado)
                {

                    recibo.ProcesarReciboTemporal("Procesado Por Administrador", recibo.RegionalId, 1, sefinRecibo.FechaMod);

                }

                reciboRepository.Update(recibo.Id, recibo);
            }
        }

        private string SelecionarDepartamento(int departamentoId) {
            var cat =catalogoRepository.GetById(departamentoId);
            return cat.Nombre;
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
            else if (id == Dominio.Models.Recibo.EstadoReciboSolicitado)
            {
                return "SOLICITADO";
            }
            return "";
        }

        

    }
}
