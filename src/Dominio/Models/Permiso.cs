using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Models
{
    public class Permiso : IEntity
    {
        public static int idPermisoAdministracion = 1;
        public static int idPermisoGestionRecibo = 27;
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public int? PermisoPadre { get; set; }
        public bool EsMenu { get; set; }
        public string Icono { get; set; }
        public int Orden { get; set; }
        public bool Asignable { get; set; }
        public bool TieneHijos { get; set; }
    }
}
