using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Models
{
    public class DetalleRecibo : IEntity
    {
        public int Id { get; set; }
        public int ReciboId { get; set; }
        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }
        public int? CantidadServicio { get; set; }
        public double Monto { get; set; }
    }
}
