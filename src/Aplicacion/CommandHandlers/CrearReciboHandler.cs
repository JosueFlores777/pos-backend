using System;
using System.Collections.Generic;
using System.Linq;
using Aplicacion.Commands;
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
    public class CrearReciboHandler : AbstractHandler<CrearRecibo>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokeService;
        private readonly IServicioRepository servicioRepository;
        private readonly ISefinClient sefinClient;
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IClienteRepository clienteRepository;
        private readonly IConfiguration configuration;
        public CrearReciboHandler(IServicioRepository servicioRepository, IConfiguration configuration, IReciboRepository reciboRepository, IMapper mapper, ITokenService tokeService, ISefinClient sefinClient,
            ICatalogoRepository catalogoRepository, 
            IUnitOfWork unitOfWork,  IClienteRepository importadorRepository)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.tokeService = tokeService;
            this.sefinClient = sefinClient;
            this.catalogoRepository = catalogoRepository;
            this.unitOfWork = unitOfWork;
            this.clienteRepository = importadorRepository;
            this.servicioRepository = servicioRepository;
            this.configuration = configuration;
        }

        public override IResponse Handle(CrearRecibo message)
        {
            var context = unitOfWork.GetContext();
            Random rnd = new Random();
            var ambiente = configuration.GetValue<string>("AppSettings:Environment");
            using (var transaction = context.Database.BeginTransaction())
            {

                var recibo = mapper.Map<Recibo>(message.Recibo);
                
                var importador = clienteRepository.Filter(new BuscarClienteAprobado(message.Recibo.Identificacion)).FirstOrDefault();
                if ( importador != null)
                {
                    recibo.NombreRazon = importador.Nombre;
                    recibo.ClienteId = importador.Id;

                } else {
                    recibo.ClienteId = 0;
                }
                if (ambiente.Equals("production"))
                {
                    recibo.Id = (int)CrearReciboSefin(message.Recibo);
                }
                else {
                    recibo.Id = rnd.Next(108900, 118900);
                }
                    
                var areaId = 0;
                foreach (var item in recibo.DetalleRecibos)
                {
                    var servicio = servicioRepository.GetById(item.ServicioId);
                    areaId = servicio.AreaId;
                }
                recibo.AreaId = areaId;

                recibo.RegionalId= message.Recibo.RegionalId == 0 ? null : message.Recibo.RegionalId;
                recibo.DescuentoId = message.Recibo.DescuentoId == 0 ? null : message.Recibo.DescuentoId;
                recibo.Inicializar();
                reciboRepository.Create(recibo);
                context.SaveChanges();
                transaction.Commit();

                var reciboResponse = reciboRepository.ReciboConDetalle(recibo.Id);
                var respuesta = mapper.Map<DtoRecibo>(reciboResponse);
                return respuesta;
                
            }

        }

        private string GetIdentificador(int idIdentificador) {
            if (idIdentificador == 5) {
                return "RTN";
            } else if(idIdentificador == 4)
            {
                return "PAS";
            }
            else if (idIdentificador == 3)
            {
                return "ID";
            }
            return "";
        }

        private uint CrearReciboSefin(DtoRecibo recibo) {

            string rubro = "";
            string descripcionRubro = "";

            List<SefinArticulo> sefinArticulo = new List<SefinArticulo>();
            SefinArticulo Articulo = new SefinArticulo();

            List<SefinRubro> listasefinRubro = new List<SefinRubro>();
            SefinRubro sefinRubro = new SefinRubro();

            //Crear Lista de Articulos
            foreach (var item in recibo.DetalleRecibos)
            {
                if (item.Servicio.Rubro == 12199)
                {
                    //Tasas Varias
                    rubro = "12199";
                    descripcionRubro = "Tasas Varias";
                    Articulo = new SefinArticulo()
                    {
                        Articulo = "3",
                        Descripcion = "Otros",
                        Monto = recibo.MontoTotal
                    };
                    
                }
                else if(item.Servicio.Rubro == 12121) {
                    //Emision de Constancias
                    rubro = "12121";
                    descripcionRubro = "Emisión, Constancias, Certificaciones y Otros"; 
                    Articulo = null;
                }else if (item.Servicio.Rubro == 12499)
                {
                    //multas Y Penas
                    rubro = "12499";
                    descripcionRubro = "Multas y Penas Diversas";
                    Articulo =null; 
                }else if (item.Servicio.Rubro == 12806)
                {
                    //multas Y Penas
                    rubro = "12806";
                    descripcionRubro = "Devoluciones de Ejercicios Fisc. Anteriores";
                    Articulo = null;
                }else if (item.Servicio.Rubro == 15104)
                {
                    //multas Y Penas
                    rubro = "15104";
                    descripcionRubro = "Venta de Artículos y Mat. Diversos";
                    Articulo = null;
                }

            }

            sefinArticulo.Add(Articulo);

            sefinRubro = new SefinRubro()
            {
                Rubro = rubro,
                Descripcion = descripcionRubro,
                Monto = recibo.MontoTotal,
                Articulos = sefinArticulo,
            };
            listasefinRubro.Add(sefinRubro);

            SefinRecibo sefinRecibo = new SefinRecibo()
            {
                TipoIdentificacion = GetIdentificador(recibo.TipoIdentificadorId),
                DescripcionIdentificacion = construirIdentificaion(recibo.Identificacion, recibo.TipoIdentificadorId),
                NombreRazon = recibo.NombreRazon,
                Institucion = "145",
                DescripcionInstitucion = "Servicio Nacional de Sanidad e Inocuidad Agroalimentaria",
                Monto = recibo.MontoTotal,
                Rubros = listasefinRubro,
            };
            var nroRecibo = sefinClient.InsertRecibo(sefinRecibo);
            return nroRecibo;
        }

        public string construirIdentificaion(string identificacion, int tipoIdentificaion)
        {

            if (tipoIdentificaion ==5) {
                //RTN
                var x = identificacion.Insert(4,"-");
                var y = x.Insert(9, "-");
                var z = y.Insert(15,"-");
                return z;
            }
            else if (tipoIdentificaion ==4) {
                //PASAPORTE
                return identificacion;
            }
            else if (tipoIdentificaion == 3)
            {
                //DNI
                var x = identificacion.Insert(4, "-");
                var y = x.Insert(9, "-");
                return y;
            }

            return identificacion;
        }

    }
    
}
