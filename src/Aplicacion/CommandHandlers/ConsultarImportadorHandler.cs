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
  public  class ConsultarImportadorHandler : AbstractHandler<ConsultarImportador>
    {
        private readonly IImportadorRepository importadorRepository;
        private readonly IMapper mapper;

        public ConsultarImportadorHandler(IImportadorRepository importadorRepository, IMapper  mapper) {
            this.importadorRepository = importadorRepository;
            this.mapper = mapper;
        }
        public override IResponse Handle(ConsultarImportador message)
        {
            var importador = importadorRepository.GetImportadorConCatalogos(new BuscarImportadorPorIndentificador(message.Identificador));
            
            return mapper.Map<ImportadorDto>(importador);
        }
    }
}
