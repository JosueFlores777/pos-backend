using Aplicacion.Commands;
using Aplicacion.Services.Validaciones;
using Dominio.Especificaciones;
using Dominio.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacion.Validators
{
    public class ConsultarReciboValidator : Validador<ConsultarRecibo>
    {
        private readonly IReciboRepository reciboRepository;

        public ConsultarReciboValidator(IAutenticationHelper autenticationHelper, IReciboRepository reciboRepository) : base(autenticationHelper)
        {
            RuleFor(x => x.Id).NotEmpty().Must(c => ExisteRecibo(c)).WithMessage("El Recibo TGR-1 No Existe");
            this.reciboRepository = reciboRepository;

        }
        private bool ExisteRecibo(int reciboId)
        {

            var rec = reciboRepository.Filter(new BuscarReciboPorId(reciboId)).FirstOrDefault();
            if (rec!=null)
            {
                return true;
            }
            return false;

        }
        public override IList<string> Permisos =>new List<string> { };
    }
}
