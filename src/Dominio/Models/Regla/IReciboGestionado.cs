using Dominio.Especificaciones;
using Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Models.Regla
{
    public interface IReciboGestionado : IRegla
    {
        bool ReciboFueGestionado(int IdRecibo);
    }

    public class ReciboGestionado : IReciboGestionado
    {
        private readonly IReciboRepository reciboRepository;

        public ReciboGestionado(IReciboRepository reciboRepository) {
            this.reciboRepository = reciboRepository;
        }
        public bool ReciboFueGestionado(int IdRecibo)
        {
            var recibos = reciboRepository.Filter(new BuscarReciboPorId(IdRecibo)).Count();
            return recibos == 0;
        }
    }
}
