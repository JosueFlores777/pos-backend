using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarImportadorAprobado : ISpecification<Importador>
    {
        private readonly string identificador;

        public BuscarImportadorAprobado(string Identificador)
        {
            identificador = Identificador;
        }


        Func<Importador, bool> ISpecification<Importador>.Traer()
        {
            return new Func<Importador, bool>(c => c.Identificador.Trim() == identificador.Trim() && c.AccesoAprobado==true);
        }
    }
}
