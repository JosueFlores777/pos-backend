using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using Infraestructura.Data;
using Infraestructura.Repositories.Extenciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ardalis.Specification.EntityFrameworkCore;


namespace Infraestructura.Repositories
{
    public class CatalogoRepository : GenericRepository<Catalogo>, ICatalogoRepository
    {
        private readonly RecibosContext dbContext;

        public CatalogoRepository(RecibosContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IPagina<Catalogo> ConsultarPaginadoConPadre(IConsulta ownerParameters, ISpecification<Catalogo> busqueda)
        {
            if (busqueda != null)
            {
                return PagedList<Catalogo>.ToPagedList(dbContext.Set<Catalogo>().OrderBy(on => on.Id).Where(busqueda.Traer()).AsQueryable(),
             ownerParameters.PageNumber,
             ownerParameters.PageSize);
            }
            return PagedList<Catalogo>.ToPagedList(dbContext.Set<Catalogo>().OrderBy(on => on.Id),
          ownerParameters.PageNumber,
          ownerParameters.PageSize);
        }

        public IEnumerable<string> ConsultarTipos()
        {
            var respultado = dbContext.Set<Catalogo>().Select(c => c.Tipo).Distinct().ToList();

            return respultado;
        }
        public List<Catalogo> ConsultarPadres()
        {
            var respuesta = dbContext.Set<Catalogo>().ToList();

            return respuesta;
        }
        public void setModified(Catalogo catalogo)
        {

            dbContext.Entry(catalogo).State = EntityState.Modified;

        }
        public void DetachLocal( Catalogo catalogo, int entryId)
        {
            var local = dbContext.Set<Catalogo>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                dbContext.Entry(local).State = EntityState.Detached;
            }
            dbContext.Entry(catalogo).State = EntityState.Modified;
        }

    }
}
