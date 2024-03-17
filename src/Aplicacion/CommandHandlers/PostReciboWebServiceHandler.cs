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
using static System.Net.WebRequestMethods;

namespace Aplicacion.CommandHandlers
{
    public class PostReciboWebServiceHandler : AbstractHandler<PostReciboWebService>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokeService;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IImportadorRepository importadorRepository;
        private readonly ISefinClient sefinClient;
        private readonly IConfiguration configuration;
        public PostReciboWebServiceHandler(IReciboRepository reciboRepository, IConfiguration configuration, ISefinClient sefinClient, IUsuarioRepository usuarioRepository, IMapper mapper, ITokenService tokeService,
           ICatalogoRepository catalogoRepository,
           IUnitOfWork unitOfWork, IImportadorRepository importadorRepository)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.sefinClient = sefinClient;
            this.tokeService = tokeService;
            this.usuarioRepository = usuarioRepository;
            this.catalogoRepository = catalogoRepository;
            this.unitOfWork = unitOfWork;
            this.importadorRepository = importadorRepository;
            this.configuration = configuration;
        }
        public override IResponse Handle(PostReciboWebService message)
        {
            ResponseReciboPostDTO response = new ResponseReciboPostDTO();
            var usuarioID = tokeService.GetIdUsuario();
            var ambiente = configuration.GetValue<string>("AppSettings:Environment");

           
            try {
                List<int> listaRecibosMalos = new List<int> {12400651};
                var rec = reciboRepository.GetById((int)message.recibo.NroRecibo);
                bool encontro = false;
                bool actualizo = false;
                if (rec != null)
                {
                    var cumple = ActualizaEstado(rec.Id);
                    if (cumple) {
                        
                        rec = reciboRepository.GetById((int)message.recibo.NroRecibo);
                        actualizo = true;
                    }
                    if ((rec.EstadoSefinId == 7 && rec.EstadoSenasaId == 6) || actualizo)
                    {

                        rec.ProcesarReciboWS(message.recibo.Comentario, message.recibo.puesto, usuarioID);
                        reciboRepository.Update(rec.Id, rec);
                        var reciboMpp = mapper.Map<Dominio.Models.ReciboResponse>(reciboRepository.GetById((int)message.recibo.NroRecibo));
                        if (ambiente.Equals("production"))
                        {
                            sefinClient.ProcessRecibo((uint)rec.Id);
                        }
                        if (reciboMpp != null)
                        {
                            reciboMpp.NroRecibo = reciboMpp.id;
                            reciboMpp.id = 0;
                            reciboMpp.ApiEstadoSefin = SeleccionarEstado(reciboMpp.EstadoSefinId);
                            reciboMpp.ApiEstadoSenasa = SeleccionarEstado(reciboMpp.EstadoSenasaId);
                            response.Entity = true;
                            response.Success = true;
                            response.Message = "";
                        }

                    }
                    else if (rec.EstadoSefinId == 8 && rec.EstadoSenasaId == 9)
                    {
                        foreach (int recibo in listaRecibosMalos) {
                            if (recibo == message.recibo.NroRecibo) {
                                response.Entity = true;
                                response.Success = true;
                                response.Message = "";
                                encontro = true;
                            }
                           
                        }
                        if(!encontro){
                            response.Entity = false;
                            response.Success = false;
                            response.Message = "El recibo " + rec.Id + " ya fue procesado";
                        }

                    }
                    else if (rec.EstadoSefinId == 6 && rec.EstadoSenasaId == 6)
                    {
                        response.Entity = false;
                        response.Success = false;
                        response.Message = "El recibo " + rec.Id + " no ha sido pagado";
                    }
                    else if (rec.EstadoSenasaId == 11)
                    {
                        response.Entity = false;
                        response.Success = false;
                        response.Message = "El recibo " + rec.Id + " esta en estado anulado";
                    }

                }
                else if (reciboRepository.TraerDelSistemaPagos((int)message.recibo.NroRecibo) != null)
                {
                    reciboRepository.ProcesarRecibos((int)message.recibo.NroRecibo, message.recibo.Comentario , 1);
                    response.Entity = true;
                    response.Success = true;
                    response.Message = "";
                }
                else
                {
                    response.Entity = false;
                    response.Success = false;
                    response.Message = "Recibo no encontrado ";
                }
            }
            catch(Exception ex)
            {

                response.Entity = false;
                response.Success = false;
                response.Message = ex.Message;

            }
            
            
            return mapper.Map<ResponseReciboPostDTO>(response);

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

        public bool ActualizaEstado(double NroRecibo) {
            var rec = reciboRepository.GetById((int)NroRecibo);
            var sefinRecibo = sefinClient.GetRecibo((uint)NroRecibo);
            if ((rec.EstadoSefinId==6 && rec.EstadoSenasaId == 6 ) && sefinRecibo.ApiEstado == "PAGADO") {
                rec.PagarRecibo(sefinRecibo.FechaMod, sefinRecibo.UsuarioModificacion);
                reciboRepository.Update(rec.Id, rec);
                return true;
            }
            return false;
        }
    }
}
