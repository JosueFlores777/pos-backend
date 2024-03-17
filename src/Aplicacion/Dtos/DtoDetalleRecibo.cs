using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Dtos.Servicio;

namespace Aplicacion.Dtos
{
    public class DtoDetalleRecibo : IResponse
    {
        public int Id { get; set; }
        public int ReciboId { get; set; }
        public DtoServicioCompleto Servicio { get; set; }
        public int ServicioId { get; set; }
        public int? CantidadServicio { get; set; }
        public double Monto { get; set; }
    }
}
