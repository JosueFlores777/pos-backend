using Aplicacion.Dtos;
using Dominio.Service;

namespace Aplicacion.Commands.CatalogoCasos
{
    public class EditarCatalogo : IMessage
    {
        public DtoCatalogo Catalogo { get; set; }
    }
}
