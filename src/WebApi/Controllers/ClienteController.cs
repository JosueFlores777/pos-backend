using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Commands;
using Aplicacion.Dtos;
using Aplicacion.Services.Comandos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public ICommandBus CommandBus { get; }

        public ClienteController(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }

      
        // GET: api/Importador/5
        [HttpGet("{id}", Name = "Get")]
        public IResponse Get(string id)
        {
            
            return CommandBus.execute(new ConsultarCliente { Identificador = id });
        }


    }
}
