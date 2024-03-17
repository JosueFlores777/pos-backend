using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Service
{
   public class AppSettings
    {
        public string Secret { get; set; }
        public int TokenTiempoHoras { get; set; }
        public string ConnectionStringsRedis { get; set; }
    }
}
