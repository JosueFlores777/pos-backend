using System;
using System.Collections.Generic;
using System.Text;


using Dominio.Service;

using Dominio.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;


using Dominio.Models;
namespace Dominio.Service.Recibos
{
    
    public interface IReciboSefin 
    {
        string TraerCodigo();
        void PostCodigo();
    }

}