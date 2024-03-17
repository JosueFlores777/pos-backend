using Ardalis.Specification;
using Dominio.Especificaciones;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Repositories.Extenciones;
using Dominio.Repositories.Extensiones;
using Infraestructura.Data;
using Infraestructura.Repositories.Extenciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using Dominio.Exceptions;
using System.Net.Http.Headers;

namespace Infraestructura.Repositories
{
    public class ReciboRepository : GenericRepository<Recibo>, IReciboRepository
    {

        private readonly RecibosContext dbContext;
        private readonly IConfiguration configuration;

        public ReciboRepository(RecibosContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public IPagina<Recibo> Filter(IConsulta ownerParameters, string especificaciones)
        {
            var queriable = dbContext.Set<Recibo>().OrderByDescending(c => c.FechaCreacion).Include(c => c.Cambios).OrderByDescending(c => c.Id)
                  .Include(c => c.Importador).Include(c => c.EstadoSenasa).Include(c=> c.UsuarioAsignado).Include(c=>c.DetalleRecibos)
                  .Where(especificaciones);

            return PagedList<Recibo>.ToPagedList(queriable,
                      ownerParameters.PageNumber,
                      ownerParameters.PageSize);
        }

        public List<KeyValuePair<int?, int>> GetCountRecibosPorUsuarioAsignado()
        {
            return dbContext.Set<Recibo>()
                      .GroupBy(p => p.UsuarioAsignadoId)
                      .Select(g => new KeyValuePair<int?, int>(g.Key, g.Count())).ToList();
        }

       
        public Recibo ReciboConDetalle(int id)
        {
            var resultado =dbContext.Set<Recibo>()
                .Include(c => c.Importador)
                .Include(c => c.Cambios).Include(c => c.UsuarioAsignado)
                .Where(c => c.Id == id).FirstOrDefault();
            var detalleRecibos = dbContext.Set<DetalleRecibo>().Where(c => c.ReciboId == id).Include(c=>c.Servicio).ToList();
            resultado.DetalleRecibos = detalleRecibos;
            return resultado;
          
        }

        public Recibo ReciboConDetalleParaPdf(int id)
        {
            return dbContext.Set<Recibo>()
                  .Include(c => c.Importador).Include(c => c.EstadoSenasa)
                  .Include(c => c.Cambios)
                  .Include(c => c.UsuarioAsignado)
                  .Where(c => c.Id == id).FirstOrDefault();
        }
        public Recibo TraerDelSistemaPagos(int id)
        {

            Recibo recibo = null;
            var token = this.GetToken();
            if (token != null)
            {
                var client = new HttpClient();
                var direccion = configuration.GetValue<string>("AppSettings:ReciboConfiguracion:UrlConsulta") + id;
                var req = new HttpRequestMessage(HttpMethod.Get, direccion);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var res = client.SendAsync(req);
                res.Wait();
                var result = res.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var respuesta = JsonConvert.DeserializeObject<ResponseRecibo>(readTask.Result);
                    if (respuesta.Entity.Count() > 0)
                    {
                        recibo = new Recibo();
                        recibo.EstadoSefinId = SeleccionarEstado (respuesta.Entity[0].ApiEstadoSefin);
                        recibo.EstadoSenasaId = SeleccionarEstado( respuesta.Entity[0].ApiEstadoSenasa);
                        recibo.Comentario = respuesta.Entity[0].Comentario;
                        recibo.Id = respuesta.Entity[0].Id;
                        recibo.Identificacion = respuesta.Entity[0].Identificacion;
                        recibo.MontoTotal = respuesta.Entity[0].MontoTotal;
                        recibo.NombreRazon = respuesta.Entity[0].NombreRazon;
                        recibo.Id = (int)respuesta.Entity[0].NroRecibo;
                    }

                }
                else
                {
                    throw new HttpException(422, "No se ha podido consultar el recibo, contacte al administrados");
                }
            }

            return recibo;
        }
        public Recibo ProcesarRecibos(int id, string comentario, int idSolicitud)
        {

            Recibo recibo = TraerDelSistemaPagos(id);
            var token = this.GetToken();
            if (token != null)
            {
                var client = new HttpClient();
                var direccion = configuration.GetValue<string>("AppSettings:ReciboConfiguracion:UrlProcesar");
                var req = new HttpRequestMessage(HttpMethod.Post, direccion);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject("{\"nroRecibo\": \"" + id + "\", \"puesto\": \"21\",   \"comentario\": \"" + comentario + "\"}");
                req.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var res = client.SendAsync(req);
                res.Wait();
                var result = res.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var jsonString = readTask.Result;
                    var respuesta = JsonConvert.DeserializeObject<ResponseReciboPost>(jsonString);
                    if (respuesta.Success == true)
                    {
                        return recibo;
                    }
                    else
                    {
                        throw new HttpException(422, "El Recibo no se pudo procesar: " + respuesta.Message);
                    }
                }
                else
                {
                    var resultado = TraerDelSistemaPagos(id);
                    if (resultado.Comentario.Contains("- " + idSolicitud))
                    {
                        return recibo;
                    }
                    else
                    {
                        throw new HttpException(422, "No coincide el numero de recibo con el id de la Solicitud");
                    }
                }
            }
            else
            {
                throw new HttpException(422, "El Token no fue encontrado");
            }
        }
        private string GetToken()
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", configuration.GetValue<string>("AppSettings:ReciboConfiguracion:Token:Usuario")),
                new KeyValuePair<string, string>("password", configuration.GetValue<string>("AppSettings:ReciboConfiguracion:Token:Password")),
                new KeyValuePair<string, string>("grant_type", configuration.GetValue<string>("AppSettings:ReciboConfiguracion:Token:Grant_type"))
            };
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, configuration.GetValue<string>("AppSettings:ReciboConfiguracion:Token:Url")) { Content = new FormUrlEncodedContent(nvc) };
            var res = client.SendAsync(req);
            res.Wait();
            var result = res.Result;
            Token respuesta;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                respuesta = JsonConvert.DeserializeObject<Token>(readTask.Result);
            }
            else
            {
                throw new HttpException(422, "No se ha podido consultar el recibo, contacte al administrados");
            }
            return respuesta?.access_token;
        }
        private int SeleccionarEstado(string estado)
        {
            if (estado == "CREADO")
            {
                return Dominio.Models.Recibo.EstadoReciboCreado;
            }
            else if (estado == "ELIMINADO")
            {
                return Dominio.Models.Recibo.EstadoReciboEliminado ;
            }
            else if (estado == "PAGADO")
            {
                return  Dominio.Models.Recibo.EstadoReciboPagado;
            }
            else if (estado == "PROCESADO")
            {

                return Dominio.Models.Recibo.EstadoReciboProcesado;
            }
            else if (estado == "UTILIZADO")
            {
                
                return Dominio.Models.Recibo.EstadoReciboUtilizado;
            }
            else if (estado == "SOLICITADO")
            {
                return Dominio.Models.Recibo.EstadoReciboSolicitado;
            }
            return 0;
        }
    }

}

public class Token
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
}
public class ResponseRecibo
{
    public List<RespondeEntity> Entity { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
public class ResponseReciboPost
{
    public bool Entity { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}

public class RespondeEntity {
    public double NroRecibo { get; set; }
    public string ApiEstadoSefin { get; set; }
    public string ApiEstadoSenasa { get; set; }
    public string Identificacion { get; set; }
    public string NombreRazon { get; set; }

    public string Puesto { get; set; }
    public string Comentario { get; set; }
    public string Departamento { get; set; }
    public string Area { get; set; }
    public double MontoTotal { get; set; }
    public string FecCre { get; set; }
    public string UsuMod { get; set; }
    public string UsuCre { get; set; }
    public string FecMod { get; set; }
    public int Id { get; set; }
    public string NumeroPermiso { get; set; }
    public int? IdSolicitud { get; set; }

}

