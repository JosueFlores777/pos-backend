using Dominio.Exceptions;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service.Recibos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using System.Net.Http.Headers;
using Infraestructura.Data;
using System.Globalization;
using System.Linq.Dynamic.Core;
namespace Infraestructura.Service.Recibos
{
    public class ReciboSefin : IReciboSefin
    {
        private readonly IConfiguration configuration;

        private readonly RecibosContext dbContext;
        public ReciboSefin(IConfiguration configuration, RecibosContext dbContext)
        {
            this.configuration = configuration;
            this.dbContext = dbContext;
        }
        public string TraerCodigo()
        {
            var vconsulta = "AppSettings:Aduanas:ServicioConsulta";
            return Consultar( vconsulta);
        }

        public void PostCodigo()
        {
            var vconsulta = "AppSettings:Aduanas:ServicioPost";
            Consultar( vconsulta);

        }
        private string Consultar( string vconsulta)
        {
            var TARGETURL = configuration.GetValue<string>("AppSettings:Aduanas:UrlConsultaDuca");
            HttpClient client = new HttpClient();
            var usuario = configuration.GetValue<string>("AppSettings:Aduanas:Usuario");
            var password = configuration.GetValue<string>("AppSettings:Aduanas:Password");
            var byteArray = Encoding.ASCII.GetBytes(usuario + ":" + password);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            //var xml = ToXML(consulta);
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("pinSistema", configuration.GetValue<string>("AppSettings:Aduanas:PinSistema")),
                new KeyValuePair<string, string>("service", configuration.GetValue<string>(vconsulta)),
                //new KeyValuePair<string, string>("message", xml)
            };

            var encodedItems = formData.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
            var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");

            HttpResponseMessage response = client.PostAsync(TARGETURL, encodedContent).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var rs = response.Content.ReadAsStringAsync().Result;

                return rs;
            }
            return "";
        }

    }
}
