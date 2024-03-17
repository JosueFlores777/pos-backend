using Aplicacion.Dtos;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aplicacion.Commands.Recibo
{
    public class AnularRecibo : IMessage
    {
        public int idRecibo { get; set; }

        //public DtoRecibo Recibo { get; set; }
    }
}
