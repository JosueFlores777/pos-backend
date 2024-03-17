
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
   public  class EditarCatalogoHandler : AbstractHandler<EditarCatalogo>
    {
        private readonly ICatalogoRepository catalogoRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public EditarCatalogoHandler(ICatalogoRepository catalogoRepository, IMapper mapper,ITokenService tokenService)
        {
            this.catalogoRepository = catalogoRepository;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }
        public override IResponse Handle(EditarCatalogo message)
        {
            var catalogo = catalogoRepository.GetById(message.Catalogo.Id);
            catalogo.FechaActualizacion = DateTime.Now;
            catalogo.UsuarioActualiza = tokenService.GetIdUsuario();
            catalogo.Nombre = message.Catalogo.Nombre;
            catalogo.IdPadre = message.Catalogo.IdPadre;
            catalogo.Tipo = message.Catalogo.Tipo;
            catalogo.Abreviatura = message.Catalogo.Abreviatura;
            catalogoRepository.DetachLocal(catalogo,catalogo.Id);
            catalogoRepository.Update(catalogo.Id, catalogo);
            return new OkResponse();
        }

        public void LimpiarTipoCatalogo() {

            

        }
    }
}