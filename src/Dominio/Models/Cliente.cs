
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class Cliente : IEntity
    {
        public static string TipoIngresoManual = "manual";
        public string Nombre { get; set; }
        public int NacionalidadId { get; set; }
        public Catalogo Nacionalidad { get; set; }
        public string Identificador { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool AccesoAprobado { get; set; }
        public int DepartamentoId { get; set; }
        public Catalogo Departamento { get; set; }
        public int MunicipioId { get; set; }
        public Catalogo Municipio { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string TipoIngreso { get; set; }
        public int Id { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool CorreoEnviado { get; set; }
        public DateTime? FechaEnvioCorreo { get; set; }

    }
}
