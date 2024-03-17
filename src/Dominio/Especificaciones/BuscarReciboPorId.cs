using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarReciboPorId : ISpecification<Recibo>
    {
        private readonly int nroRecibo;

        public BuscarReciboPorId(int nroRecibo) {
            this.nroRecibo = nroRecibo;
        }
        public Func<Recibo, bool> Traer()
        {
            var x = new Func<Recibo, bool>(c => c.Id == nroRecibo);
            return x;
        }
    }
}
