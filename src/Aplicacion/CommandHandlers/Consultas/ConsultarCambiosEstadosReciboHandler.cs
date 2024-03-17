using Aplicacion.Commands.Consultas;
using Aplicacion.Dtos;
using Dominio.Models;
using Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacion.CommandHandlers.Consultas
{
    public class ConsultarCambiosEstadosReciboHandler : AbstractHandler<ConsultarCambiosEstadosRecibo>
    {
        private readonly IGenericRepository<CambioEstado> repository;

        public ConsultarCambiosEstadosReciboHandler(IGenericRepository<CambioEstado> repository)
        {
            this.repository = repository;
        }
        public override IResponse Handle(ConsultarCambiosEstadosRecibo message)
        {
            var lista = repository.Set().Where(c => c.ReciboId == message.reciboId).Include(c => c.Estado).Include(s=>s.Usuario).OrderByDescending(c=>c.Fecha).AsNoTracking().ToList();
            lista.ForEach(s=> {
                s.Usuario.Contrasena = null;
                s.Usuario.Departamento = null;
                s.Usuario.TipoUsuario = null;
                s.Usuario.IdentificadorAcceso = null;
            });
            return new ConsultaCambiosEstadoReciboDto(lista);
        }
    }
}
