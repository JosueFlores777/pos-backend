using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Dtos.Servicio;

namespace Aplicacion.Dtos
{
    public class DtoServiciosRecibo : IResponse
    {
        public int Id { get; set; }
        public int Servicio { get; set; }
        public string Descuento { get; set; }
        public double Monto { get; set; }
    }
}
