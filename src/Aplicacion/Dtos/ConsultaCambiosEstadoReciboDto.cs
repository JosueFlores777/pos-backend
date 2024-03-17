using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
   public class ConsultaCambiosEstadoReciboDto: IResponse
    {
        public ConsultaCambiosEstadoReciboDto(IList<CambioEstado> estados)
        {
            Estados = estados;
        }

        public IList<CambioEstado> Estados { get; }
    }
}
