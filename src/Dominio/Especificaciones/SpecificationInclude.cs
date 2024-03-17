using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dominio.Especificaciones
{
    public sealed class Includes<TEntity> : Specification<TEntity>
    {
        public Includes(IEnumerable<string> includes)
        {
            foreach (var include in includes)
            {
                Query.Include(include);
            }
        }

        public Includes(Expression<Func<TEntity, object>> expression)
        {
            Query.Include(expression);
        }
    }
}
