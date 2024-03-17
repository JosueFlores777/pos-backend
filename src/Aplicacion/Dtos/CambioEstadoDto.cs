using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
   public class CambioEstadoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EstadoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
    }
}
