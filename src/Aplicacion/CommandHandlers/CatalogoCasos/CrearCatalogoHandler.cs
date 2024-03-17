
using Aplicacion.Commands.CatalogoCasos;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CommandHandlers.CatalogoCasos
{
   public  class CrearCatalogoHandler : AbstractHandler<CrearCatalogo>
    {
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public CrearCatalogoHandler(ICatalogoRepository catalogoRepository, IMapper mapper,ITokenService tokenService)
        {
            this.catalogoRepository = catalogoRepository;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }
        public override IResponse Handle(CrearCatalogo message)
        {
            var catalogo = mapper.Map<Dominio.Models.Catalogo>(message.Catalogo);
            catalogo.FechaCreacion = DateTime.Now;
            catalogo.UsuarioCrea = tokenService.GetIdUsuario();
            catalogoRepository.Create(catalogo);
            return new OkResponse();
        }
    }
}