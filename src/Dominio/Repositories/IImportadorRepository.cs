using Dominio.Especificaciones;
using Dominio.Models;


namespace Dominio.Repositories
{
    public interface IImportadorRepository : IGenericRepository<Importador>
    {
        Importador GetImportadorConCatalogos(ISpecification<Importador> busqueda);
    }
}
