using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class DtoCatalogo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPadre { get; set; }
        public string Tipo { get; set; }
        public string Abreviatura { get; set; }
    }
}
