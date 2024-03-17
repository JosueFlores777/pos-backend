using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class GetTokenDto : IResponse
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
