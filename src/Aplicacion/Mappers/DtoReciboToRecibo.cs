using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;

using Dominio.Repositories.Extensiones;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aplicacion.Mappers
{
    public class DtoReciboToRecibo : Profile
    {
        public DtoReciboToRecibo()
        {


            CreateMap<CambioEstado, CambioEstadoDto>().ReverseMap();

            CreateMap<ReciboResponse, Recibo>();
            CreateMap<Recibo, ReciboResponse>();
            CreateMap<DtoUsuario, Usuario>();
            CreateMap<Usuario, DtoUsuario>();
            CreateMap<Recibo, DtoRecibo>();
            CreateMap<DtoRecibo, Recibo>().ForMember(c => c.DetalleRecibos, f => f.MapFrom(c => GetServicios(c)));
            CreateMap<Recibo, DtoRecibo>().ForMember(c => c.DetalleRecibos, f => f.MapFrom(c => GetProductos(c)));
            CreateMap<IPagina<Recibo>, DtoRecibosPaginados>().ForMember(c => c.Metadata, f => f.MapFrom(c => Getmetadata(c)))
                        .ForMember(c => c.valores, f => f.MapFrom((g, orderDto, i, context) => GetList(g, context)));
         }

        public IList<DtoRecibo> GetList(IList<Recibo> recibos, ResolutionContext context)
        {
            var respuesta = new List<DtoRecibo>();
            foreach (var recibo in recibos) respuesta.Add(context.Mapper.Map<DtoRecibo>(recibo));
            return respuesta;
        }
        private List<DetalleRecibo> GetServicios(DtoRecibo recibo)
        {
            var lista = new List<DetalleRecibo>();
            foreach (var servicio in recibo.DetalleRecibos)
            {
                lista.Add(new DetalleRecibo
                {
                    ReciboId = recibo.Id,
                    CantidadServicio = servicio.CantidadServicio,
                    ServicioId = servicio.ServicioId,
                    Monto = servicio.Monto
                }); 
            }
            return lista;
        }
        private List<DtoDetalleRecibo> GetProductos(Recibo recibo)
        {
            var lista = new List<DtoDetalleRecibo>();

            if (recibo.DetalleRecibos != null)
            {
                foreach (var servicio in recibo.DetalleRecibos)
                {

                    lista.Add(new DtoDetalleRecibo
                    {
                        
                        Id = servicio.Id,
                        ReciboId = recibo.Id,
                        ServicioId = servicio.ServicioId,
                        Monto = servicio.Monto,
                        CantidadServicio = servicio.CantidadServicio,
                        Servicio = new Dtos.Servicio.DtoServicioCompleto {
                            Id= servicio.Servicio.Id,
                            NombreServicio = servicio.Servicio.NombreServicio,
                            NombreSubServicio = servicio.Servicio.NombreSubServicio,
                            CategoriaId=servicio.Servicio.CategoriaId,
                            TipoServicioId = servicio.Servicio.TipoServicioId,
                            AreaId = servicio.Servicio.AreaId,
                            TipoCobroId = servicio.Servicio.TipoCobroId,
                            MonedaId = servicio.Servicio.MonedaId,
                            AdicionarMismoServicio = servicio.Servicio.AdicionarMismoServicio,
                            Monto = servicio.Servicio.Monto,
                            Rubro= servicio.Servicio.Rubro,
                            Descripcion = servicio.Servicio.Descripcion,
                            FechaRegistro = servicio.Servicio.FechaRegistro,
                            FechaModificacion = servicio.Servicio.FechaModificacion,
                            Tag= servicio.Servicio.Tag,
                        }

                    });
                }
            }
            return lista;
        }
        private Metadata Getmetadata<T>(IPagina<T> paging)
        {
            var metada = new Metadata
            {
                CurrentPage = paging.CurrentPage,
                HasPrevious = paging.HasPrevious,
                PageSize = paging.PageSize,
                TotalCount = paging.TotalCount,
                TotalPages = paging.TotalPages,
                HasNext = paging.HasNext
            };
            return metada;
        }
         
    }
}
