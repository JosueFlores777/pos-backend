
using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dominio.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetById(int id);
        TEntity Create(TEntity entity);
        void SaveAll(IList<TEntity> entity);
        TEntity Update(int id, TEntity entity);
        TEntity Delete(int id);
        TEntity Delete(TEntity entity);
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);
        TEntity GetByIdEspecificacion(ISpecification<TEntity> especificaciones);
        TEntity GetFirs(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Filter(ISpecification<TEntity> especificaciones);
        IEnumerable<TEntity> Filter(ISpecification<TEntity> especificaciones, params Expression<Func<TEntity, object>>[] includes);
        IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters, ISpecification<TEntity> busqueda);

        IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters, ISpecification<TEntity> busqueda, params Expression<Func<TEntity, object>>[] includes);
        IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters, Func<TEntity, bool> predicate);
        IPagina<TEntity> ConsultarPaginado(IConsulta ownerParameters);
        IQueryable<TEntity> Set();

        IGenericRepository<TEntity> Specify(Ardalis.Specification.ISpecification<TEntity> specification);
        IPagina<TEntity> Paginar(IConsulta ownerParameters);
        IQueryable<TEntity> WithSpecs();

    }
}
