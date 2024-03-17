using System.Collections;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;
using Dominio.Repositories.Extensiones;

namespace Aplicacion.Mappers
{
    public static class Extencion
    {
        private static Metadata Getmetadata<T>(IPagina<T> paging)
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

        public static IMappingExpression<IPagina<T>, DtoRespuestaPaginada<T>> paginar<T>(this IMappingExpression<IPagina<T>, DtoRespuestaPaginada<T>> expression)
        {


            expression.ForMember(response => response.Metadata,
                opt => opt.MapFrom((extendable) =>
                    Getmetadata(extendable))).ForMember(c => c.valores, f => f.MapFrom((g, orderDto, i, context) => g));


            return expression;
        }
    }
}