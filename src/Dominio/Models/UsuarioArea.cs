using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class UsuarioArea : IEntity
    {
        public int Id { get; set; }
        public Catalogo Area { get; set; }
        public int AreaId { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; } 
    }
}
