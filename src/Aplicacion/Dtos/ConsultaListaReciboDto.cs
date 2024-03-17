using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
   public class ConsultaListaReciboDto : IResponse
    {
        public ConsultaListaReciboDto(IList<DtoReciboExcel> recibos)
        {
            Recibos = recibos;
        }

        public IList<DtoReciboExcel> Recibos { get; }
    }
}
