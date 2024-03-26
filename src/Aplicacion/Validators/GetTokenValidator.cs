using System.Collections.Generic;
using System.Linq;
using Aplicacion.Commands;

using Aplicacion.Services.Validaciones;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using FluentValidation;
using Dominio.Especificaciones;

namespace Aplicacion.Validators
{
    public class GetTokenValidator : Validador<GetToken>
    {
        private readonly ITokenService tokenService;
        private readonly IClienteRepository importRepo;
        private readonly IUsuarioRepository user;
        public GetTokenValidator( IUsuarioRepository user, IAutenticationHelper autenticationHelper, ITokenService tokenService, IClienteRepository importRepo) : base(autenticationHelper)
        {

            this.tokenService = tokenService;
            this.importRepo = importRepo;
            this.user = user;

            RuleFor(x => x).NotEmpty()
               .Must(c => ValidarCredencialesUsuario(c.usuario, c.password))
               .WithMessage("Usuario o contraseña es incorrecto");

        }
        private bool ValidarCredencialesUsuario(string usuario, string contrasena)
        {
            var resultado = user.Filter(new BuscarUsuarioPorIdentificadorYContrasena(usuario, contrasena));
            return resultado.Count() > 0;
        }

        public override IList<string> Permisos => new List<string> { };
    }
}
