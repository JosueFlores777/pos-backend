using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
   public class UsuarioRol: IEntity
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
