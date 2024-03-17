using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Service
{
   public interface ITokenService
    {
        string TraerTokenDeRequest();
        bool VerificarToken();
        string GetIdentificacionUsuario();
        string GetIdUsuarioWebService();
        int GetIdUsuario();
        string CrearOtraerToken(Usuario usuario);

        List<Permiso> TraerPermisos();
    }
}
