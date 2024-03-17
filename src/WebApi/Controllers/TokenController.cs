using Aplicacion.Commands;
using Aplicacion.Dtos;
using Aplicacion.Services.Comandos;
using Microsoft.AspNetCore.Mvc;
using Dominio.Repositories;
using Dominio.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Aplicacion.Commands.Consultas;
using Aplicacion.Commands.Recibo;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly IReciboRepository repository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private HttpResponse response;

        public TokenController(ICommandBus commandBus, IReciboRepository repository, IMapper mapper, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.commandBus = commandBus;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IResponse GetToken([FromHeader] GetTokenDto value)
        {
            return commandBus.execute(new GetToken {usuario = value.username, password=value.password });
        }


        
    }
    
}
