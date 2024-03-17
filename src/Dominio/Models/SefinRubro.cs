using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{

    public class SefinRubro
    {
        private string rubro;
        private string descripcion;
        private double monto;
        private List<SefinArticulo> articulos;

        public string Rubro { get => rubro; set => rubro = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public double Monto { get => monto; set => monto = value; }
        public List<SefinArticulo> Articulos { get => articulos; set => articulos = value; }
    }
}
