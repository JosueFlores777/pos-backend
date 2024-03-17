using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos.Servicio
{
    public class DtoRangoCobros : IResponse
    {

        public int Id { get; set; }
        public DtoServicioCompleto Servicio { get; set; }
        public int ServicioId { get; set; }
        public int ValorMinimo { get; set; }
        public int ValorMaximo { get; set; }
        public int PorCada { get; set; }
        public double? BaseTarifa { get; set; }
        public int? BasePeso { get; set; }
        public double? Monto { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public bool Excedente { get; set; }
        public bool Descuento { get; set; }
    }

    public class DtoTipoCobroUnidades : CodigoValorDto
    {
    }
}
