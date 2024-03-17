using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Service
{
    public interface ICorreoHelper
    {
        void EnviarPermisoCorreo(Recibo recibo);

    }
}
