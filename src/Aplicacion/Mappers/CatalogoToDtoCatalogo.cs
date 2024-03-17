using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Mappers
{
    public class CatalogoToDtoCatalogo : Profile
    {
        public CatalogoToDtoCatalogo()
        {
            CreateMap<Catalogo, DtoCatalogo>().ReverseMap();

        }
    }
}
