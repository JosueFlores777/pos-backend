using Aplicacion.Commands;
using Aplicacion.Dtos;
using AutoMapper;
using Dominio.Repositories;
using Dominio.Service;
using System;
using Dominio.Models;
using Dominio.Especificaciones;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.CommandHandlers
{
    public class GetTokenHandler : AbstractHandler<GetToken>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IMapper mapper;
        private readonly IUsuarioRepository user;
        private readonly ITokenService tokenService;

        public GetTokenHandler(IReciboRepository reciboRepository, IMapper mapper, IUsuarioRepository user, ITokenService tokenService)
        {
            this.reciboRepository = reciboRepository;
            this.mapper = mapper;
            this.user = user;
            this.tokenService = tokenService;
        }
        public override IResponse Handle(GetToken message)
        {
            DtoUsuarioToken respuesta = new DtoUsuarioToken();
            Usuario usuario;
            usuario = user.GetUsuarioConRolPermiso(new BuscarUsuarioPorIdentificadorYContrasena(message.usuario, message.password));
            if (!String.IsNullOrEmpty(usuario.Nombre)) {
                respuesta.access_token = tokenService.CrearOtraerToken(usuario);
                respuesta.expires_in = 8;
                respuesta.token_type = "test";
            }
            
            return respuesta;
        }
    }
    

}
