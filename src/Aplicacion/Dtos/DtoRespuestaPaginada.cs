using System.Collections.Generic;

namespace Aplicacion.Dtos
{
    public class DtoRespuestaPaginada<T> : IResponse
    {
        public IEnumerable<T> valores { get; set; }
        public Metadata Metadata { get; set; }
    }
}
