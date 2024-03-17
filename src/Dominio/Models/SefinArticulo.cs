using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class SefinArticulo
    {
        private string articulo;
        private string descripcion;
        private double monto;

        public string Articulo { get => articulo; set => articulo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public double Monto { get => monto; set => monto = value; }
    }
}
