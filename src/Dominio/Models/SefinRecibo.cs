using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class SefinRecibo
    {
        private uint numeroRecibo;
        private string tipoIdentificacion;
        private string descripcionIdentificacion;
        private string nombreRazon;
        private string institucion;
        private string descripcionInstitucion;
        private double monto;
        private string apiEstado;
        private string usuarioModificacion;
        private string apiTransacion;
        private DateTime fechaMod;
        private List<SefinRubro> rubros;

        public uint NumeroRecibo { get => numeroRecibo; set => numeroRecibo = value; }
        public string TipoIdentificacion { get => tipoIdentificacion; set => tipoIdentificacion = value; }
        public string DescripcionIdentificacion { get => descripcionIdentificacion; set => descripcionIdentificacion = value; }
        public string NombreRazon { get => nombreRazon; set => nombreRazon = value; }
        public string Institucion { get => institucion; set => institucion = value; }
        public string DescripcionInstitucion { get => descripcionInstitucion; set => descripcionInstitucion = value; }
        public double Monto { get => monto; set => monto = value; }
        public string ApiEstado { get => apiEstado; set => apiEstado = value; }
        public string UsuarioModificacion { get => usuarioModificacion; set => usuarioModificacion = value; }
        public string ApiTransacion { get => apiTransacion; set => apiTransacion = value; }
        public DateTime FechaMod { get => fechaMod; set => fechaMod = value; }
        public List<SefinRubro> Rubros { get => rubros; set => rubros = value; }
    }

    
}
