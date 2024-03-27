using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    public class ClienteToClienteDto : Profile
    {
        public ClienteToClienteDto()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Catalogo, DepartamentoDto>();
            CreateMap<Catalogo, MunicipioDto>();
            CreateMap<Catalogo, PaisDto>();
            CreateMap<Catalogo, TipoIdentificadorDto>();
        }
    }
}
