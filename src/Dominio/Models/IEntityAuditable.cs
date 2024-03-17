using System;

namespace Dominio.Models
{
    public interface IEntityAuditable: IEntity
    { 
         DateTime FechaCreacion { get; set; }
        int UsuarioCreo { get; set; }
        DateTime? FechaModificacion { get; set; }
        int? UsuarioModifica { get; set; }
    }
}
