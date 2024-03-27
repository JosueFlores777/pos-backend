using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarClientePorIndentificador : ISpecification<Cliente>
    {
        private readonly string identificador;

        public BuscarClientePorIndentificador(string Identificador)
        {
            identificador = Identificador;
        }


        Func<Cliente, bool> ISpecification<Cliente>.Traer()
        {
            return new Func<Cliente, bool>(c => c.Identificador.Trim() == identificador.Trim() /*&& c.AccesoAprobado==true*/);
        }
    }
}