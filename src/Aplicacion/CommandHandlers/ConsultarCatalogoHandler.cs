using Aplicacion.Commands;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacion.CommandHandlers
{
    public class ConsultarCatalogoHandler : AbstractHandler<ConsultarCatalogo>
    {
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IMapper mapper;

        public ConsultarCatalogoHandler(ICatalogoRepository catalogoRepository, IMapper mapper)
        {
            this.catalogoRepository = catalogoRepository;
            this.mapper = mapper;
        }
        public override IResponse Handle(ConsultarCatalogo message)
        {
            var listaCatalogo = catalogoRepository.Specify(new BuscarCatalogoPorTipo(message.Tipo))
                     .Specify(new BuscarCatalogoPorPadre(message.IdPadre)).WithSpecs().ToList();

            var listaDto = new List<DtoCatalogo>();
            foreach (var item in listaCatalogo) listaDto.Add(mapper.Map<DtoCatalogo>(item));
            return new DtoListaCatalogo { Lista = listaDto };
        }

    }
}
