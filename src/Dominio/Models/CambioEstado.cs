using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class CambioEstado: IEntity
    {
        public int Id { get; set; }
        public Recibo Recibo { get; set; }
        public int ReciboId { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public Catalogo Estado { get; set; }
        public int EstadoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }

    }
}
