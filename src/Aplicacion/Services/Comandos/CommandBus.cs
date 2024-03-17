using Aplicacion.CommandHandlers;
using Aplicacion.Commands;
using Aplicacion.Dtos;
using Aplicacion.Services.Validaciones;
using Aplicacion.Validators;
using Dominio.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aplicacion.Services.Comandos
{
    public class CommandBus : ICommandBus
    {
        private readonly IEnumerable<ICommandHandler> commandHandlers;
        private readonly IValidatorService validatorService;
        private readonly ILogger<CommandBus> logger;
        private readonly ITokenService tokenService;

        public CommandBus(IEnumerable<ICommandHandler> commandHandlers, IValidatorService validatorService, 
            ILogger<CommandBus> logger, ITokenService tokenService)
        {
            this.commandHandlers = commandHandlers;
            this.validatorService = validatorService;
            this.logger = logger;
            this.tokenService = tokenService;
        }
        public IResponse execute(IMessage comando)
        {
            //TryLog(comando);
            validatorService.AplicarValidador(comando);
            var instance = commandHandlers.FirstOrDefault(c => c.GetType().Name == comando.GetType().Name + "Handler");
            
            if (instance == null)
            {
                throw new NotImplementedException("Handler no implementado para el mensaje: " + comando.GetType().Name);
            }
           
 
            return instance.ejecutar(comando);
        }

        private void TryLog(IMessage comando) {
            var usuario = 0;
            try
            {
                usuario = tokenService.GetIdUsuario();
            }
            catch (Exception)
            {

            }
            var texto=usuario>0? "El Usuario " + usuario: "";
            logger.LogInformation( texto+", Ejecuto Comando: " + comando.GetType().Name + "Handler"+" Fecha:"+ DateTime.Now);
        }

    }
}
