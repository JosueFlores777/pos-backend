using Dominio.Especificaciones;
using Dominio.Models;


namespace Dominio.Repositories
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Cliente GetImportadorConCatalogos(ISpecification<Cliente> busqueda);
    }
}
