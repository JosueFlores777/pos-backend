using System.Collections.Generic;

namespace Aplicacion.Dtos
{
    public class DtoListaCatalogo : DtoListResponse<DtoCatalogo>
    {

    }

    public class DtoListResponse<T> : IResponse
    {
        public IList<T> Lista { get; set; }
    }
}
