using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Dtos.Servicio;
namespace Aplicacion.Dtos
{
    public class DtoReciboExcel
    {
        public int No { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Recibo { get; set; }
        public string NombreRazon { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaPago { get; set; }
        public string Fecha { get; set; }
        public string Banco { get; set; }
        public double Monto { get; set; }
        public double Comision { get; set; }
        public double Total { get; set; }
        public string Regional { get; set; }
        public string Estado { get; set;}
        public string Rubro { get; set;}
        public string UsuarioAsignado { get; set;}

        public string Institucion { get; set; }


    }
}
