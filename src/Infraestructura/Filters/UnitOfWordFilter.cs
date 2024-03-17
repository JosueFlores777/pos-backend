using Dominio.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Infraestructura.Filters
{
    public class UnitOfWordFilter : IActionFilter
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWordFilter(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
            using (var contexto = unitOfWork.GetContext())
            {
                
                    using (var transaction = contexto.Database.BeginTransaction())
                    {
                        Exception exception = context.Exception;

                        if (exception == null)
                        {
                            contexto.SaveChanges();
                            transaction.Commit();

                        }
                        else { 
                            transaction.Rollback();
                        }
                    }
                
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
