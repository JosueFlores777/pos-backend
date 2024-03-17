using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using System;
using System.Collections.Generic;
using System.Text;


namespace Dominio.Repositories
{
    public interface IServicioRepository : IGenericRepository<Servicio>
    {
        Servicio GetServicio(int id);
      
    }
}
