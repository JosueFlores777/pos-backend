using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarCatalogoPorTipoYNombre : ISpecification<Catalogo>
    {
        private readonly string tipo;
        private readonly string nombre;

        public BuscarCatalogoPorTipoYNombre(string tipo, string nombre)
        {
            this.tipo = tipo;
            this.nombre = nombre;
        }
        public Func<Catalogo, bool> Traer()
        {
            if(!string.IsNullOrWhiteSpace(tipo) && !string.IsNullOrWhiteSpace(nombre)) return new Func<Catalogo, bool>(c => c.Tipo == tipo && c.Nombre.ToUpper().Contains(nombre.ToUpper()));
            if ( !string.IsNullOrWhiteSpace(nombre)) return new Func<Catalogo, bool>(c => c.Nombre.ToUpper().Contains(nombre.ToUpper()));
            return new Func<Catalogo, bool>(c => c.Tipo == tipo);
        }
    }
}
