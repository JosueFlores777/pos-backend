using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Dominio.Repositories
{
    public interface ICatalogoRepository : IGenericRepository<Catalogo>
    {
        IPagina<Catalogo> ConsultarPaginadoConPadre(IConsulta ownerParameters, ISpecification<Catalogo> busqueda);
        IEnumerable<string> ConsultarTipos();
        List<Catalogo> ConsultarPadres();
        void setModified(Catalogo catalogo);
        void DetachLocal(Catalogo catalogo, int entryId);

    }
}
