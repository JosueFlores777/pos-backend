using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;

using Dominio.Repositories.Extensiones;
using System.Collections.Generic;
using Aplicacion.Dtos.Servicio;


namespace Aplicacion.Mappers
{

    public class ServicioToDtoServicio : Profile
    {
        public ServicioToDtoServicio()
        {
            CreateMap<DtoServicioCompleto, Servicio>().ReverseMap();
            CreateMap<Servicio, DtoServicioCompleto>().ReverseMap();
            CreateMap<DtoCategoria, Catalogo>().ReverseMap();
            CreateMap<RangoCobros, DtoRangoCobros>().ReverseMap();
            CreateMap<DtoTipoCobro, Catalogo>().ReverseMap();

            CreateMap<Catalogo, DtoTipoCobroUnidades>().ReverseMap();

            CreateMap<DtoTipoCobroUnidades, Catalogo>().ReverseMap();


            CreateMap<DtoTipoServicio, Catalogo>().ReverseMap();
            CreateMap<DtoArea, Catalogo>().ReverseMap();
            CreateMap<Servicio, DtoServicio>();
            CreateMap<DtoServicio, Servicio>();


            CreateMap<IPagina<Servicio>, DtoServicioPaginado>().ForMember(c => c.Metadata, f => f.MapFrom(c => Getmetadata(c)))
              .ForMember(c => c.valores, f => f.MapFrom((g, orderDto, i, context) => GetList(g, context)));
        }

        public IList<DtoServicioCompleto> GetList(IList<Servicio> servicios, ResolutionContext context)
        {
            var respuesta = new List<DtoServicioCompleto>();
            foreach (var servicio in servicios) respuesta.Add(context.Mapper.Map<DtoServicioCompleto>(servicio));
            return respuesta;
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
