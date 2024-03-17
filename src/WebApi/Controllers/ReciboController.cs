using Aplicacion.Commands;
using Aplicacion.Dtos;
using Aplicacion.Services.Comandos;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Commands.Consultas;
using Aplicacion.Commands.Recibo;
using Dominio.Models;
using System;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ReciboController : ControllerBase
    {
        private readonly ICommandBus commandBus;


        public ReciboController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;

        }
        // GET: api/Recibos
        [HttpGet]
        public IResponse Get([FromQuery] ConsultarRecibos ownerParameter)
        {
            var respuesta = commandBus.execute(ownerParameter);
            return respuesta;
        }
        // GET: api/Recibos/5
        [HttpGet("{id}/estados", Name = "GetRecibosEstados")]
        public IResponse GetRecibosEstados(int id)
        {
            return commandBus.execute(new ConsultarCambiosEstadosRecibo(id));
        }
       
        [HttpGet("gestionesActivas", Name = "GetGestionesActivas")]
        public IResponse GetGestionesActivas([FromQuery] ConsultarRecibosGestion ownerParameter)
        {
            var respuesta = commandBus.execute(ownerParameter);
            return respuesta;
        }

        [HttpPost("reporteUsuarioExterno/")]
        public IResponse GetRecibosDasboardUsuarioExterno([FromBody] DtoReciboReporte value)
        {
            var respuesta = commandBus.execute(new ConsultarRecibosDashboardUsuarioExterno {
                FechaInicio = value.FechaInicio, 
                FechaFin = value.FechaFin, 
                AreaId = value.AreaId, 
                RegionalId = value.RegionalId}
            );
            return respuesta;
        }


        [HttpPost("listaRecibosPorMes", Name = "GetRecibosLista")]
        public IResponse GetRecibosLista([FromBody] DtoReciboReporte value)
        {
            return commandBus.execute(new ConsultarListaRecibo(value.FechaInicio, value.FechaFin, value.nombreRazon));
        }

        [HttpGet("pdf/reporte/{fechaInicio}/{fechaFin}/{nombreRazon}", Name = "DescargarReportePDF")]
        public IActionResult DescargarReportePDF(DateTime fechaInicio, DateTime fechaFin, string nombreRazon)
        {
            var permiso = (DescargaArchivoDto)commandBus.execute(new DescargarReportePDF(fechaInicio, fechaFin, true, nombreRazon));
            var contentType = "application/pdf";

            return File(permiso.File, contentType, permiso.FileName);

        }

        [HttpGet("pdf/reporte/{fechaInicio}/{fechaFin}", Name = "DescargarReportePDF2")]
        public IActionResult DescargarReportePDF2(DateTime fechaInicio, DateTime fechaFin)
        {
            var permiso = (DescargaArchivoDto)commandBus.execute(new DescargarReportePDF(fechaInicio, fechaFin, true, ""));
            var contentType = "application/pdf";

            return File(permiso.File, contentType, permiso.FileName);

        }
        [HttpPost]
        public IResponse Post([FromBody] DtoRecibo value)
        {
            return commandBus.execute(new CrearRecibo { Recibo = value });
        }
        // GET: api/Recibos/5
        [HttpGet("{id}", Name = "GetRecibos")]
        public IResponse Get(int id)
        {
            return commandBus.execute(new ConsultarRecibo { Id = id });

        }
        // GET: api/Recibos/pdf/permiso
        [HttpGet("pdf/{id}", Name = "DescargarReciboPDF")]
        public IActionResult DescargarReciboPDF(int id)
        {
            var permiso = (DescargaArchivoDto)commandBus.execute(new DescargarReciboPDF { nroRecibo = id });
            var contentType = "application/pdf";
            
            return File(permiso.File, contentType, permiso.FileName); 

        }
        
        // POST: api/Recibos

        // PUT: api/recibo/
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DtoRecibo value)
        {
            commandBus.execute(new ProcesarRecibo { Recibo = value });
        }


        //Web services 
        [HttpPut("pagar/{id}")]
        public void Pagar(int id)
        {
            commandBus.execute(new PagarRecibo { idRecibo = id });
        }
        [HttpPut("anular/{id}")]
        public void Anular(int id)
        {
            commandBus.execute(new AnularRecibo { idRecibo = id });
        }


        //conexiones

        [HttpPost("generar")]
        public IResponse Generar([FromBody] ReciboWebService value)
        {
            return commandBus.execute(new GenerarReciboWebService { Recibo = value });
        }

        [HttpGet("consultar/{id}")]
        public IResponse GetReciboWS(int id)
        {

            return commandBus.execute(new GetReciboWebService { id = id });
        }
        [HttpPost("procesar")]
        public IResponse PostReciboWS([FromBody] ReciboResponse value)
        {

            return commandBus.execute(new PostReciboWebService { recibo = value });
        }

    }
}
