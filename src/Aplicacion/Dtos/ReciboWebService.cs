using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class ReciboWebService : IResponse
    {
        public int Id { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string NombreRazon { get; set; }
        public string Rubro { get; set; }
        public int Area { get; set; }
        public int Departamento { get; set; }
        public string Cantidad { get; set; }
        public List<DtoServiciosRecibo> Servicios { get; set; }
        public double MontoTotal { get; set; }
        public string Institucion { get; set; }
        public string usuarioCreacion { get; set; }

    }
}
