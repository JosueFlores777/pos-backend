using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Especificaciones
{
    public class BuscarCatalogoPorIDPadre : ISpecification<Catalogo>
    {
        private readonly int idPadre;

        public BuscarCatalogoPorIDPadre(int idPadre)
        {
            this.idPadre = idPadre;
        }
        public Func<Catalogo, bool> Traer()
        {

            return new Func<Catalogo, bool>(c => c.IdPadre == idPadre);
        }
    }
}
