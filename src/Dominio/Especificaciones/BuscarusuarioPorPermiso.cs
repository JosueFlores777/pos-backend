using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Especificaciones
{
   public class BuscarusuarioPorPermiso : ISpecification<UsuarioRol>
    {
        private readonly int idPermiso;

        public BuscarusuarioPorPermiso(int idPermiso) {
            this.idPermiso = idPermiso;
        }
        Func<UsuarioRol, bool> ISpecification<UsuarioRol>.Traer()
        {

            return new Func<UsuarioRol, bool>(c => c.Rol.Permisos.All(p=>p.PermisoId== idPermiso));
        }
    }
}
