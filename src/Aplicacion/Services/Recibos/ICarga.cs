

using Dominio.Exceptions;
using Dominio.Models;
using Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aplicacion.Services.Recibos
{
    public interface ICarga
    {
        int GetUsuariMenosCargar(int permiso);
    }

    public class UsuarioService : ICarga
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IUsuarioRolRepository usuarioRolRepository;
        private readonly IRolPermisoRepository rolPermisoRepository;

        public UsuarioService(IReciboRepository reciboRepository, IUsuarioRolRepository usuarioRolRepository, IRolPermisoRepository rolPermisoRepository)
        {
            this.reciboRepository = reciboRepository;
            this.usuarioRolRepository = usuarioRolRepository;
            this.rolPermisoRepository = rolPermisoRepository;
        }
        public int GetUsuariMenosCargar(int permiso)
        {
            var roles = rolPermisoRepository.Filter(new Func<RolPermiso, bool>(c => c.PermisoId == permiso)).ToList();
            var usuarios = usuarioRolRepository.FilterWithDetalle(new Func<UsuarioRol, bool>(cc => roles.All(c => c.RolId == cc.RolId) && cc.Usuario.Activo)).ToList();
            if (usuarios.Count == 0) {
                throw new HttpException(422, "No se han creado usuarios para gestionar tu recibo, Contacta al administrador");
            }
            var sol = reciboRepository.GetCountRecibosPorUsuarioAsignado();
            var lista = new List<KeyValuePair<int?, int>>();
            foreach (var user in usuarios)
            {
                var monto = sol.Where(c => c.Key == user.UsuarioId).FirstOrDefault();
                if (monto.Value == 0) lista.Add(new KeyValuePair<int?, int>(user.UsuarioId, 0));
                else lista.Add(monto);
            }
            var selet = lista.OrderByDescending(c => c.Value).LastOrDefault();
            return selet.Key.GetValueOrDefault();
        }
    }
}
