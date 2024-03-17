using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Repositories.Extensiones
{
	public interface IPagina<T>:IList<T>
	{
		int CurrentPage { get;  }
		int TotalPages { get; }
		int PageSize { get; }
		int TotalCount { get; }
		bool HasPrevious { get; }
		bool HasNext {get;}
	}
}
