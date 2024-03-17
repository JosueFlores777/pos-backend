using Aplicacion.Commands.Consultas;
using Aplicacion.Dtos;
using Dominio.Models;
using Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacion.CommandHandlers.Consultas
{
    public class ConsultarListaReciboHandler : AbstractHandler<ConsultarListaRecibo>
    {
        private readonly IGenericRepository<Recibo> repository;
        private readonly IServicioRepository servicioRepository;
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IUsuarioRepository usuarioRepository;
        public ConsultarListaReciboHandler(IGenericRepository<Recibo> repository, IServicioRepository servicioRepository, ICatalogoRepository catalogoRepository, IUsuarioRepository usuarioRepository)
        {
            this.servicioRepository = servicioRepository;
            this.repository = repository;
            this.catalogoRepository = catalogoRepository;
            this.usuarioRepository = usuarioRepository;
        }
        public override IResponse Handle(ConsultarListaRecibo message)
        {
            var cont = 1;
            IList<DtoReciboExcel> dtoRecibos = new List<DtoReciboExcel> { };
            var fecha1 = message.FechaInicio;
            var fecha2 = message.FechaFin;
            fecha2 = fecha2.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);
            var lista = repository.Set()
                .Where(c => c.FechaPago >= fecha1 
                        && c.FechaPago <= fecha2 
                        && c.EstadoSefinId == 8 
                        && c.EstadoSenasaId != 11
                        && c.NombreRazon.ToLower().Contains(message.NombreRazon.ToLower()))
                .Include(c => c.DetalleRecibos)
                .ThenInclude(dr => dr.Servicio)
                .AsNoTracking()
                .OrderBy(c => c.FechaPago) // Ordenar por FechaPago en orden ascendente
                .ToList();

            var listaTasasVarias = new List<DtoReciboExcel> { };
            var listaConstancias = new List<DtoReciboExcel> { };
            var listaMultas = new List<DtoReciboExcel> { };
            var listaVentas = new List<DtoReciboExcel> { };
            var listaDevoluciones = new List<DtoReciboExcel> { };
            foreach (var recibo in lista)
            {

                var Rubro = recibo.DetalleRecibos.FirstOrDefault().Servicio.Rubro;

                DtoReciboExcel dtoRecibo = new DtoReciboExcel {
                    Codigo = Rubro,
                    Descripcion = SeleccionarRubro(Rubro),
                    Recibo = recibo.Id,
                    Regional = SeleccionarCatalogo(recibo.RegionalId),
                    Estado = SeleccionarCatalogo(recibo.EstadoSefinId),
                    NombreRazon = recibo.NombreRazon,
                    FechaCreacion = recibo.FechaCreacion.ToShortDateString(),
                    FechaPago = recibo.FechaPago.ToShortDateString(),
                    Banco = recibo.Banco,
                    Fecha = recibo.FechaUtilizado.ToShortDateString(),
                    Monto = recibo.MontoTotal,
                    Comision = CalcularComision(recibo.MontoTotal),
                    Total = recibo.MontoTotal - CalcularComision(recibo.MontoTotal),
                    UsuarioAsignado = SeleccionarUsuario(recibo.UsuarioAsignadoId),
                    Institucion = "145-SENASA"

                };
                if (Rubro == 12199) {
                    listaTasasVarias.Add(dtoRecibo);
                } else if (Rubro == 12121) {
                    listaConstancias.Add(dtoRecibo);
                } else if (Rubro == 12499) {
                    listaMultas.Add(dtoRecibo);
                }else if (Rubro == 12806){
                    listaDevoluciones.Add(dtoRecibo);
                }else if (Rubro == 15104){
                    listaVentas.Add(dtoRecibo);
                }
                cont++;
                
            }
            dtoRecibos = crearLista(listaTasasVarias, listaConstancias, listaMultas, listaDevoluciones, listaVentas);

            return new ConsultaListaReciboDto(dtoRecibos);
        }
        public string SeleccionarRubro(int rubro) {
            if (rubro == 12199)
            {
                return "Tasas Varias";
            }
            else if (rubro == 12121)
            {
                return "Emisión, Constancias, Certificaciones y Otros";
            }
            else if (rubro == 12499)
            {
                return "Multas y Penas Diversas";
            }
            else if (rubro == 12806)
            {
                return "Devoluciones de Ejercicios Fisc. Anteriores";
            }
            else if (rubro == 15104)
            {
                return "Venta de Artículos y Mat. Diversos";
            }
            return "";
            
        }

        public string SeleccionarCatalogo(int? idCatalogo)
        {
            var inst = catalogoRepository.GetById((int)idCatalogo);
            if (inst != null) {
                return inst.Nombre;
            }
            return "";
        }

        public string SeleccionarUsuario(int? isUser)
        {
            var user = usuarioRepository.GetById((int)isUser);
            if (user != null)
            {
                return user.Nombre;
            }
            return "";
        }
        public IList<DtoReciboExcel> crearLista(List<DtoReciboExcel> tasas, List<DtoReciboExcel> constancias, List<DtoReciboExcel>multas, List<DtoReciboExcel> devoluciones, List<DtoReciboExcel> ventas) {
            IList<DtoReciboExcel> dtoRecibos = new List<DtoReciboExcel> { };
            var cont = 1;
            foreach (var recibo in tasas)
            {
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
            }
            foreach (var recibo in constancias)
            {
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
            }
            foreach (var recibo in multas)
            {
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
            }
            foreach (var recibo in devoluciones)
            {
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
            }
            foreach (var recibo in ventas)
            {
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
            }

            return dtoRecibos;
        }
        public double CalcularComision(double monto)
        {
            return (monto * 0.0032);
        }
    }
}
