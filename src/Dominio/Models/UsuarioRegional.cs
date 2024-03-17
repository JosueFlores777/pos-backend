using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class UsuarioRegional : IEntity
    {
        public int Id { get; set; }
        public Catalogo Regional { get; set; }
        public int RegionalId { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; } 
    }
}
