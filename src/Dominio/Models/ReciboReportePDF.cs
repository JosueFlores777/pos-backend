using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dominio.Models
{
    public class ReciboReportePDF : IEntity
    {
        public int Id { get; set; }
        public string NombreRazon { get; set; }
        public string Banco { get; set; }
        public int No { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Recibo { get; set; }
        public string FechaCreado{ get; set; }
        public string FechaPago { get; set; }
        public string FechaUtilizado { get; set; }
        public double Monto { get; set; }
        public double Comision { get; set; }
        public double Total { get; set; }
        

    }
}
