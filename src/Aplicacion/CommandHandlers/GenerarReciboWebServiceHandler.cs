using System;
using System.Collections.Generic;
using System.Linq;
using Aplicacion.Commands;
using Aplicacion.Commands.Recibo;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Models.Regla;
using Dominio.Repositories;
using Dominio.Service;
using Dominio.Service.Recibos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Aplicacion.CommandHandlers
{
    public class GenerarReciboWebServiceHandler : AbstractHandler<GenerarReciboWebService>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokeService;
        private readonly IServicioRepository servicioRepository;
        private readonly ISefinClient sefinClient;
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IImportadorRepository importadorRepository;

        public GenerarReciboWebServiceHandler(IServicioRepository servicioRepository, IConfiguration configuration, IReciboRepository reciboRepository, IMapper mapper, ITokenService tokeService, ISefinClient sefinClient,
            ICatalogoRepository catalogoRepository,
            IUnitOfWork unitOfWork, IImportadorRepository importadorRepository)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.tokeService = tokeService;
            this.sefinClient = sefinClient;
            this.catalogoRepository = catalogoRepository;
            this.unitOfWork = unitOfWork;
            this.importadorRepository = importadorRepository;
            this.servicioRepository = servicioRepository;
            this.configuration = configuration;
        }

        public override IResponse Handle(GenerarReciboWebService message)
        {
            var context = unitOfWork.GetContext();
            var ambiente = configuration.GetValue<string>("AppSettings:Environment");
            using (var transaction = context.Database.BeginTransaction())
            {
                SefinRecibo sefinRecibo2 = new SefinRecibo();
                ResponseReciboGenerarDTO response = new ResponseReciboGenerarDTO();
                List<SefinRecibo> listaSefin = new List<SefinRecibo>();
                SefinRecibo sefinRecibo = new SefinRecibo();
                DetalleRecibo detalles = new DetalleRecibo();
                List<DetalleRecibo> Listadetalles = new List<DetalleRecibo>();
                var areaId = 0;
                Recibo recibo = new Recibo();


                var importador = importadorRepository.Filter(new BuscarImportadorPorIndentificador(message.Recibo.Identificacion)).FirstOrDefault();
                if (importador != null)
                {
                    recibo.NombreRazon = importador.Nombre;
                    recibo.ImportadorId = importador.Id;

                }
                else
                {
                    recibo.ImportadorId = 0;
                    recibo.NombreRazon = message.Recibo.NombreRazon;
                }

                foreach (var item in message.Recibo.Servicios)
                {
                    var servicio = servicioRepository.GetById(item.Servicio);
                    areaId = servicio.AreaId;
                    detalles.ServicioId = item.Servicio;
                    detalles.Monto = item.Monto;
                    detalles.CantidadServicio = 1;

                }
                Listadetalles.Add(detalles);
                Random rnd = new Random();
                try
                {
                    if (ambiente.Equals("production"))
                    {
                        recibo.Id = (int)CrearReciboSefin(message.Recibo);
                    }
                    else
                    {
                        recibo.Id = rnd.Next(108900, 118900);
                    }
                    recibo.MontoTotal = message.Recibo.MontoTotal;


                    recibo.TipoIdentificadorId = selectIdentificaion(message.Recibo.TipoIdentificacion);
                    recibo.RegionalId = int.Parse(message.Recibo.Institucion);

                    recibo.Identificacion = message.Recibo.Identificacion;

                    recibo.DetalleRecibos = Listadetalles;

                    recibo.MonedaId = 64; // to-do por servicio
                    recibo.AreaId = areaId;
                    recibo.Inicializar();
                    reciboRepository.Create(recibo);
                    context.SaveChanges();
                    transaction.Commit();
                    sefinRecibo2 = sefinClient.GetRecibo((uint)recibo.Id);
                    response.Success = true;
                    response.Message = "";

                    listaSefin.Add(sefinRecibo2);
                    if (ambiente.Equals("production"))
                    {
                        response.Entity = listaSefin;
                    }
                    else
                    {
                        response.Entity = null;
                    }

                }
                catch (Exception e)
                {
                    response.Entity = null;
                    response.Message = e.Message;
                }



                var respuesta = mapper.Map<ResponseReciboGenerarDTO>(response);
                return respuesta;

            }

        }
        public string SeleccionarRubro(string rubro)
        {
            if (rubro == "12199")
            {
                return "12199 - Tasas Varias";
            }
            else if (rubro == "12121")
            {
                return "12121 - Emisión, Constancias, Certificaciones y Otros";
            }
            else if (rubro == "12499")
            {
                return "12499 - Multas y Penas Diversas";
            }
            else if (rubro == "12806")
            {
                return "12806 - Devoluciones de Ejercicios Fisc. Anteriores";
            }
            else if (rubro == "15104")
            {
                return "15104 - Venta de Artículos y Mat. Diversos";
            }
            return "";

        }
        private uint CrearReciboSefin(ReciboWebService recibo)
        {


            List<SefinArticulo> sefinArticulo = new List<SefinArticulo>();
            SefinArticulo Articulo = new SefinArticulo()
            {
                Articulo = "3",
                Descripcion = "Otros",
                Monto = recibo.MontoTotal
            };

            List<SefinRubro> listasefinRubro = new List<SefinRubro>();
            SefinRubro sefinRubro = new SefinRubro();

            //Crear Lista de Articulos


            sefinArticulo.Add(Articulo);

            sefinRubro = new SefinRubro()
            {
                Rubro = recibo.Rubro,
                Descripcion = SeleccionarRubro(recibo.Rubro),
                Monto = recibo.MontoTotal,
                Articulos = sefinArticulo,
            };
            listasefinRubro.Add(sefinRubro);

            SefinRecibo sefinRecibo = new SefinRecibo()
            {
                TipoIdentificacion = recibo.TipoIdentificacion,
                DescripcionIdentificacion = construirIdentificaion(recibo.Identificacion, recibo.TipoIdentificacion),
                NombreRazon = recibo.NombreRazon,
                Institucion = "145",
                DescripcionInstitucion = "Servicio Nacional de Sanidad e Inocuidad Agroalimentaria",
                Monto = recibo.MontoTotal,
                Rubros = listasefinRubro,
            };
            var nroRecibo = sefinClient.InsertRecibo(sefinRecibo);
            return nroRecibo;
        }

        public string construirIdentificaion(string identificacion, string tipoIdentificaion)
        {

            if (tipoIdentificaion == "RTN")
            {
                //RTN
                var x = identificacion.Insert(4, "-");
                var y = x.Insert(9, "-");
                var z = y.Insert(15, "-");
                return z;
            }
            else if (tipoIdentificaion == "PAS")
            {
                //PASAPORTE
                return identificacion;
            }
            else if (tipoIdentificaion == "ID")
            {
                //DNI
                var x = identificacion.Insert(4, "-");
                var y = x.Insert(9, "-");
                return y;
            }

            return identificacion;
        }

        public int selectIdentificaion(string tipoIdentificaion)
        {

            if (tipoIdentificaion == "RTN")
            {
                //RTN

                return 5;
            }
            else if (tipoIdentificaion == "PAS")
            {
                //PASAPORTE
                return 4;
            }
            else if (tipoIdentificaion == "ID")
            {
                //DNI

                return 3;
            }

            return 0;
        }

    }

}
