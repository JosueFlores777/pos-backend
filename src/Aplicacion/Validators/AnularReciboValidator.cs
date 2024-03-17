﻿using Aplicacion.Commands.Recibo;
using Aplicacion.Services.Validaciones;
using Dominio.Repositories;
using Dominio.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.Commands;
using FluentValidation;
using System.Linq;
using Dominio.Especificaciones;

namespace Aplicacion.Validators
{
    public class AnularReciboValidator : Validador<AnularRecibo>
    {
        private readonly IReciboRepository reciboRepository;
        public AnularReciboValidator(IAutenticationHelper autenticationHelper, ITokenService tokenService, IUsuarioRepository user, IReciboRepository reciboRepository) : base(autenticationHelper)
        {
            
        }

        public override IList<string> Permisos => new List<string> { };
    }
}
