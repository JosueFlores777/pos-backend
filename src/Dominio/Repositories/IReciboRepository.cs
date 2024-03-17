using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dominio.Repositories
{
    public interface IReciboRepository : IGenericRepository<Recibo>
    {
        IPagina<Recibo> Filter(IConsulta ownerParameters,  string especificaciones);
        Recibo ReciboConDetalleParaPdf(int id);
        Recibo ReciboConDetalle(int id);
        List<KeyValuePair<int?, int>> GetCountRecibosPorUsuarioAsignado();
        Recibo TraerDelSistemaPagos(int id);
        Recibo ProcesarRecibos(int id, string comentario, int idSolicitud);

    }
}
