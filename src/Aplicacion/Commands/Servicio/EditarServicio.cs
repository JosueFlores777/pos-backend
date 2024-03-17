using Aplicacion.Dtos.Servicio;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands.Servicio
{
    public class EditarServicio : IMessage
    {
        public DtoServicioCompleto ServicioCompleto { get; set; }
    }
}
