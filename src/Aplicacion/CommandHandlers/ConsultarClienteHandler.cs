using Aplicacion.Commands;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Especificaciones;
using Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CommandHandlers
{
  public  class ConsultarClienteHandler : AbstractHandler<ConsultarCliente>
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IMapper mapper;

        public ConsultarClienteHandler(IClienteRepository importadorRepository, IMapper  mapper) {
            this.clienteRepository = importadorRepository;
            this.mapper = mapper;
        }
        public override IResponse Handle(ConsultarCliente message)
        {
            var importador = clienteRepository.GetClienteConCatalogo(new BuscarClientePorIndentificador(message.Identificador));
            
            return mapper.Map<ClienteDto>(importador);
        }
    }
}
