using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class ReciboResponse 
    {
        public int id { get; set; }
        public double NroRecibo { get; set; }
        public string Identificacion { get; set; }
        public string NombreRazon { get; set; }
        public double MontoTotal { get; set; }
        public string ApiEstadoSefin { get; set; }
        public int EstadoSefinId { get; set; }
        public string ApiEstadoSenasa { get; set; }
        public int EstadoSenasaId { get; set; }
        public int? UsuarioAsignadoId { get; set; }
        public string Comentario { get; set; }
        public int RegionalId { get; set; }
        public int puesto { get; set; }
        public string Departamento { get; set; }
        public string Servicio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaUtilizado { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? InicioVigencia { get; set; }
    }
}
