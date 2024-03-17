using Aplicacion.Dtos;
using Dominio.Service;

namespace Aplicacion.Commands.CatalogoCasos
{
    public class CrearCatalogo: IMessage
    {
       public DtoCatalogo Catalogo { get; set; }
    }
}
