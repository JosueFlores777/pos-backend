using Dominio.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Dominio.Models
{
    public class Usuario : IEntity
    {

        public static string correoUsuarioAdmin = "administrador@senasa.gob.hn";
        public static string tipoCliente = "importador";
        public static string usuarioInterno = "usuario-interno";
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string IdentificadorAcceso { get; set; }
        public bool Activo { get; set; }
        public string Contrasena { get; set; }
        public int? DepartamentoId { get; set; }
        public Catalogo Departamento { get; set; }
        public bool CambiarContrasena { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string TipoUsuario { get; set; }
        public ICollection<UsuarioRegional> UsuarioRegional { get; set; }
        public ICollection<UsuarioArea> UsuarioArea { get; set; }
        public IList<UsuarioRol> Roles { get; set; }
        public static string getPassword(string constrasena)
        {
            return PasswordHelper.getPassword(constrasena);
        }

    }
}
