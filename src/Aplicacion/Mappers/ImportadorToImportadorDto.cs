using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    public class ImportadorToImportadorDto : Profile
    {
        public ImportadorToImportadorDto()
        {
            CreateMap<Importador, ImportadorDto>().ReverseMap();
            CreateMap<Catalogo, DepartamentoDto>();
            CreateMap<Catalogo, MunicipioDto>();
            CreateMap<Catalogo, PaisDto>();
            CreateMap<Catalogo, TipoIdentificadorDto>();
        }
    }
}
