using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Service
{
    public interface IUnitOfWork
    {
        void Save();
        DbContext GetContext();

    }
}
