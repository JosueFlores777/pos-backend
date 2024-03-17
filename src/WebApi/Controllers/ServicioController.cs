using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Commands;
using Aplicacion.Commands.Servicio;
using Aplicacion.Dtos;
using Aplicacion.Dtos.Servicio;
using Aplicacion.Services.Comandos;
using AutoMapper;
using Dominio.Especificaciones;
using Dominio.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dominio.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly IServicioRepository repository;
        private readonly IMapper mapper;


        public ServicioController(ICommandBus commandBus, IServicioRepository repository, IMapper mapper)
        {
            this.commandBus = commandBus;
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/Prducto
        [HttpGet]
        public IResponse GetServicios([FromQuery] ConsultarServicio ownerParameter)
        {
            var respuesta = commandBus.execute(ownerParameter);
            return respuesta;
        }

        // GET: api/Prducto/5
        [HttpGet("{id}", Name = "GetServicioById")]
        public DtoServicioCompleto GetById(int id)
        {
            var servicio = repository.GetServicio(id);
            return mapper.Map<DtoServicioCompleto>(servicio);
        }

        // POST: api/Prducto
        [HttpPost]
        public void Post([FromBody] DtoServicioCompleto value)
        {
            commandBus.execute(new CrearServicio { ServicioCompleto = value });
        }

        // PUT: api/Prducto/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DtoServicioCompleto value)
        {
            commandBus.execute(new EditarServicio { ServicioCompleto = value });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
