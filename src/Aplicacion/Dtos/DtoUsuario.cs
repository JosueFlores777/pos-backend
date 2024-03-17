using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class DtoUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string IdentificadorAcceso { get; set; }
        public bool Activo { get; set; }
        public string Contrasena { get; set; }
        public bool CambiarContrasena { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string TipoUsuario { get; set; }
    }
}
