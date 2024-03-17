using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos.Servicio
{
    public class DtoServicio : IResponse
    {
        public int NombreServicio { get; set; }
        public int NombreSubServicio { get; set; }
        public int AreaId { get; set; }
        public DtoArea Area { get; set; }
        public int Rubro { get; set; }
        public bool Activo { get; set; }
        public bool Descuento { get; set; }
    }
}
