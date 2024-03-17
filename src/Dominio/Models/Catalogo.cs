using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class Catalogo : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Abreviatura { get; set; }
        public int? IdPadre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCrea { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? UsuarioActualiza { get; set; }

    }
}
