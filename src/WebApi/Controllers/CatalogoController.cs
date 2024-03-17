using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Commands;
using Aplicacion.Commands.CatalogoCasos;
using Aplicacion.Dtos;
using Aplicacion.Services.Comandos;
using AutoMapper;
using Dominio.Especificaciones;
using Dominio.Models;

using Dominio.Repositories;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using Dominio.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IMapper mapper;

        public ICommandBus CommandBus { get; }

        public CatalogoController(ICommandBus commandBus, ICatalogoRepository catalogoRepository, IMapper mapper)
        {
            CommandBus = commandBus;
            this.catalogoRepository = catalogoRepository;
            this.mapper = mapper;
        }

        [HttpGet(Name = "catalogos")]
        public IResponse Get([FromQuery] ConsultarCatalogoPaginado ownerParameter)
        {
            ISpecification<Catalogo> especificacion = null;
            if (!string.IsNullOrWhiteSpace(ownerParameter.Nombre) || !string.IsNullOrWhiteSpace(ownerParameter.Tipo)) especificacion = new BuscarCatalogoPorTipoYNombre(ownerParameter.Tipo, ownerParameter.Nombre);
            var lista = catalogoRepository.ConsultarPaginadoConPadre(ownerParameter, especificacion);
            return new CatalogosPaginados { valores = lista, Metadata = Getmetadata(lista) };
        }


        [HttpGet("tipos", Name = "catalogos-tipos")]
        public IEnumerable<string> GetTiposCatalogo()
        {
            var lista = catalogoRepository.ConsultarTipos();
            return lista;
        }
        [HttpGet("padres", Name = "catalogos-padres")]
        public List<Catalogo> GetPadresCatalogo()
        {
            var lista = catalogoRepository.ConsultarPadres();
            return lista;
        }
        // GET: api/Catalogo/5
        [HttpGet("{tipo}", Name = "ConsultaCatalogoPorTipo")]
        public IResponse ConsultaCatalogoPorTipo(string tipo)
        {
            return CommandBus.execute(new ConsultarCatalogo { Tipo = tipo, IdPadre = 0 });
        }

        // GET: api/Catalogo/5
        [HttpGet("identificador/{Id}", Name = "ConsultaCatalogoPorId")]
        public Catalogo ConsultaCatalogoPorId(int id)
        {
            return catalogoRepository.GetById(id);
        }

        [HttpPost]
        public IResponse PostCatalogo([FromBody] DtoCatalogo catalogo)
        {

            return CommandBus.execute(new CrearCatalogo { Catalogo = catalogo });
        }

        [HttpPut]
        public IResponse PutCatalogo([FromBody] DtoCatalogo catalogo)
        {

            return CommandBus.execute(new EditarCatalogo { Catalogo = catalogo });
        }

        // GET: api/Catalogo/5
        [HttpGet("{tipo}/id-padre/{idpadre}", Name = "ConsultaCatalogoPorPadre")]
        public IResponse Get(string tipo, int idpadre)
        {
            return CommandBus.execute(new ConsultarCatalogo { Tipo = tipo, IdPadre = idpadre });
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

    public class ConsultarCatalogoPaginado : QueryStringParameters, IMessage
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
    }

    public class CatalogosPaginados : DtoRespuestaPaginada<Catalogo>
    {
    }
}
