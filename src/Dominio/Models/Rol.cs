using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Models
{
    public class Rol : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public IList<RolPermiso> Permisos { get; set; }
        public bool Asignable { get; set; }
    }
}
