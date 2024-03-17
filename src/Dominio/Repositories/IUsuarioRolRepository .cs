using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositories
{
   public interface IUsuarioRolRepository : IGenericRepository<UsuarioRol>
    {
        IEnumerable<UsuarioRol> FilterWithDetalle(Func<UsuarioRol, bool> predicate);
    
    }
}
