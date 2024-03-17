using Ardalis.Specification.EntityFrameworkCore;
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
using System.Linq.Expressions;
using System.Text;

namespace Infraestructura.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly RecibosContext _dbContext;
        private List<Ardalis.Specification.ISpecification<TEntity>> Specs { get; }
        public GenericRepository(RecibosContext dbContext)
        {
            _dbContext = dbContext;
            Specs = new List<Ardalis.Specification.ISpecification<TEntity>>();
        }

        public IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters, ISpecification<TEntity> busqueda)
        {
            return ConsultarPaginado(ownerParameters, busqueda.Traer());
        }

        public IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters, ISpecification<TEntity> busqueda,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();
            var rs=includes
                .Aggregate(
                    query.AsQueryable(),
                    (current, include) => current.Include(include)
                ).AsEnumerable()
                .Where(busqueda.Traer()).AsQueryable();

            return PagedList<TEntity>.ToPagedList(rs, ownerParameters.PageNumber, ownerParameters.PageSize);
        }

        public IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters, Func<TEntity, bool> predicate)
        {
            return PagedList<TEntity>.ToPagedList(_dbContext.Set<TEntity>().OrderBy(on => on.Id).Where(predicate).AsQueryable(),
                ownerParameters.PageNumber,
                ownerParameters.PageSize);
        }

        public IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters)
        {
            return PagedList<TEntity>.ToPagedList(_dbContext.Set<TEntity>().OrderBy(on => on.Id),
           ownerParameters.PageNumber,
           ownerParameters.PageSize);
        }

        public IQueryable<TEntity> Set()
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
             _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Delete(int id)
        {
            var entity =  GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return entity;
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            var respultado= _dbContext.Set<TEntity>().AsNoTracking().Where(predicate);

            return respultado;
        }

        public TEntity GetFirs(Func<TEntity, bool> predicate)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Filter(ISpecification<TEntity> especificaciones)
        {
            var respultado = _dbContext.Set<TEntity>().AsNoTracking().Where(especificaciones.Traer());

            return respultado;
        }

       

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();
            return includes
                .Aggregate(
                    query.AsQueryable(),
                    (current, include) => current.Include(include)
                )
                .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TEntity> Filter(ISpecification<TEntity> especificaciones, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();
            
            return includes
                .Aggregate(
                    query.AsQueryable(),
                    (current, include) => current.Include(include)
                )
                  .Where(especificaciones.Traer());

        }
        public TEntity GetByIdEspecificacion(ISpecification<TEntity> especificaciones)
        {
            return GetFirs(especificaciones.Traer());
        }

        public void SaveAll(IList<TEntity> entity)
        {
            foreach (var item in entity) Create(item);
        }

        public TEntity Update(int id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return entity;
        }

        public IGenericRepository<TEntity> Specify(Ardalis.Specification.ISpecification<TEntity> specification)
        {
            this.Specs.Add(specification);
            return this;
        }

        public IQueryable<TEntity> WithSpecs()
        {
            var query = _dbContext.Set<TEntity>().OrderBy(on => on.Id).AsQueryable();
            var queryable = Specs.Aggregate(query,
                (current, spec) => new SpecificationEvaluator<TEntity>().GetQuery(current, spec));
            Specs.Clear();
            return queryable;
        }

        public IPagina<TEntity> Paginar(IConsulta ownerParameters)
        {
            var ff= PagedList<TEntity>.ToPagedList(WithSpecs().AsQueryable(),
                ownerParameters.PageNumber,
                ownerParameters.PageSize);
            return ff;
        }
    }
}
