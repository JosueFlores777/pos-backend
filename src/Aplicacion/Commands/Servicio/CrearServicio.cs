using Aplicacion.Dtos;
using Aplicacion.Dtos.Servicio;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Commands.Servicio
{
    public class CrearServicio : IMessage
    {
        public DtoServicioCompleto ServicioCompleto { get; set; }
    }
}
