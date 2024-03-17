using Dominio.Models;
using Dominio.Utilities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Dominio.Especificaciones
{
    public class BuscarUsuarioPorIdentificadorYContrasena : ISpecification<Usuario>
    {
        private readonly string identificador;
        private readonly string contrasena;

        public BuscarUsuarioPorIdentificadorYContrasena(string identificador, string contrasena)
        {
            this.identificador = identificador;
            this.contrasena = contrasena;
        }
        public Func<Usuario, bool> Traer()
        {

            var pass = Usuario.getPassword(contrasena);
            if (RegexUtilities.IsValidEmail(identificador))
            {
                return new Func<Usuario, bool>(c => c.IdentificadorAcceso.ToLower().Trim() == this.identificador.ToLower().Trim() && c.Contrasena == pass);
            }
            else
            {
                return new Func<Usuario, bool>(c => c.IdentificadorAcceso.Replace("-", "").Trim() == this.identificador.Replace("-", "").Trim() && c.Contrasena == pass);
            }

        }
    }
}
