using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarFabricantePorNombre : ISpecification<Fabricante>
    {
        private readonly string Nombre;

        public BuscarFabricantePorNombre(string nombre)
        {
            this.Nombre = nombre;
        }


        Func<Fabricante, bool> ISpecification<Fabricante>.Traer()
        {
            return new Func<Fabricante, bool>(c => c.Nombre.ToLower().Trim().Contains(Nombre.ToLower().Trim()));
        }
    }
}
