using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class ClienteDto: IResponse
    {
        public string Nombre { get; set; }
        public int NacionalidadId { get; set; }
        public PaisDto Nacionalidad { get; set; }
        public string Identificador { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public int DepartamentoId { get; set; }
        public DepartamentoDto Departamento { get; set; }

        public int MunicipioId { get; set; }
        public MunicipioDto Municipio { get; set; }



        public string Celular { get; set; }
        public string Correo { get; set; }
        public long ProveedorId { get; set; }
        public string TipoIngreso { get; set; }
        public int Id { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public bool CorreoEnviado { get; set; }

        public DateTime? FechaEnvioCorreo { get; set; }


    }
}
