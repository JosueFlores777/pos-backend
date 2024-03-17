using Dominio.Especificaciones;
using Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Models.Regla
{
    public interface IReciboEstadoAprobado : IRegla
    {
        bool ReciboEstaAprobado(int IdRecibo);
    }

    public class ReciboEstadoAprobado : IReciboEstadoAprobado
    {
        private readonly IReciboRepository reciboRepository;

        public ReciboEstadoAprobado(IReciboRepository reciboRepository) {
            this.reciboRepository = reciboRepository;
        }

        public bool ReciboEstaAprobado(int IdRecibo)
        {
            var recibos = reciboRepository.Set().Where(c => c.Id == IdRecibo).Count();
            return recibos == 1;
        }
    
    }
}
