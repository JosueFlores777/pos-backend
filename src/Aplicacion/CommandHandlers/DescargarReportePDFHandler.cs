using Aplicacion.Commands;
using Aplicacion.Dtos;
using Dominio.Models;
using Dominio.Repositories;
using Dominio.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Aplicacion.CommandHandlers
{
    public class DescargarReportePDFHandler : AbstractHandler<DescargarReportePDF>
    {
        private readonly IReciboRepository reciboRepository;
        private readonly IPdfHelper pdfhelper;
        private readonly IServicioRepository servicioRepository;
        public DescargarReportePDFHandler(IReciboRepository reciboRepository, IPdfHelper pdfhelper, IServicioRepository servicioRepository)
        {
            this.reciboRepository = reciboRepository;
            this.pdfhelper = pdfhelper;
            this.servicioRepository = servicioRepository;

        }
        public override IResponse Handle(DescargarReportePDF message)
        {
            String format = "dd MMM yyyy hh:mm tt";
            var cont = 1;
            ReciboReporteConsolidado dtoRecibos = new ReciboReporteConsolidado { };
            var fecha1 = message.FechaInicio;
            var fecha2 = message.FechaFin;
            fecha2 = fecha2.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(59);
            List<string> nombresMeses = ObtenerNombresMeses(fecha1, fecha2);
            var mes = string.Join(" - ", nombresMeses);
            var lista = reciboRepository.Set()
                .Where(c => c.FechaPago >= fecha1 
                        && c.FechaPago <= fecha2 && c.EstadoSefinId == 8 
                        && c.EstadoSenasaId != 11 
                        && c.NombreRazon.ToLower().Contains(message.nombreRazon.ToLower()))
                .Include(c => c.DetalleRecibos)
                .ThenInclude(dr => dr.Servicio)
                .AsNoTracking()
                .OrderBy(c => c.FechaPago) // Ordenar por FechaPago en orden ascendente
                .ToList();

            var listaTasasVarias = new List<ReciboReportePDF> { };
            var listaConstancias = new List<ReciboReportePDF> { };
            var listaMultas = new List<ReciboReportePDF> { };
            var listaVentas = new List<ReciboReportePDF> { };
            var listaDevoluciones = new List<ReciboReportePDF> { };
            var servicios = servicioRepository.GetAll();
            foreach (var recibo in lista)
            {
                var Rubro = recibo.DetalleRecibos.FirstOrDefault().Servicio.Rubro;

                ReciboReportePDF dtoRecibo = new ReciboReportePDF
                {
                    NombreRazon = recibo.NombreRazon,
                    Codigo = Rubro,
                    Descripcion = SeleccionarRubro(Rubro),
                    Recibo = recibo.Id,
                    Banco = recibo.Banco,
                    FechaPago = recibo.FechaPago.ToString(format),
                    FechaCreado = recibo.FechaCreacion.ToString(format),
                    FechaUtilizado = recibo.FechaUtilizado.ToString(format),
                    Monto = recibo.MontoTotal,
                    Comision = CalcularComision(recibo.MontoTotal),
                    Total = recibo.MontoTotal - CalcularComision(recibo.MontoTotal),
                };
                if (Rubro == 12199)
                {
                    listaTasasVarias.Add(dtoRecibo);
                }
                else if (Rubro == 12121)
                {
                    listaConstancias.Add(dtoRecibo);
                }
                else if (Rubro == 12499)
                {
                    listaMultas.Add(dtoRecibo);
                }
                else if (Rubro == 12806)
                {
                    listaDevoluciones.Add(dtoRecibo);
                }
                else if (Rubro == 15104)
                {
                    listaVentas.Add(dtoRecibo);
                }
                cont++;

            }
            dtoRecibos = crearLista(listaTasasVarias, listaConstancias, listaMultas, listaDevoluciones, listaVentas);
            dtoRecibos.Mes = mes;

            var stream = pdfhelper.traerReporte(dtoRecibos);
            return new DescargaArchivoDto { File = stream, FileName = "Permiso.pdf" };
        }
        public string SeleccionarRubro(int rubro)
        {
            if (rubro == 12199)
            {
                return "12199 - Tasas Varias";
            }
            else if (rubro == 12121)
            {
                return "12121 - Emisión, Constancias, Certificaciones y Otros";
            }
            else if (rubro == 12499)
            {
                return "12499 - Multas y Penas Diversas";
            }
            else if (rubro == 12806)
            {
                return "12806 - Devoluciones de Ejercicios Fisc. Anteriores";
            }
            else if (rubro == 15104)
            {
                return "15104 - Venta de Artículos y Mat. Diversos";
            }
            return "";

        }
        public ReciboReporteConsolidado crearLista(List<ReciboReportePDF> tasas, List<ReciboReportePDF> constancias, List<ReciboReportePDF> multas, List<ReciboReportePDF> devoluciones, List<ReciboReportePDF> ventas)
        {
            ReciboReporteConsolidado consolidado = new ReciboReporteConsolidado();
            IList<ReciboReportePDF> dtoRecibos = new List<ReciboReportePDF> { };
            var cont = 0;
            var contTasas = 0;
            var contEmision = 0;
            var contMultas = 0;
            var contDevolucion = 0;
            var contVentas = 0;

            var montoContTasas = 0.0;
            var montoContEmision = 0.0;
            var montoContMultas = 0.0;
            var montoContDevolucion = 0.0;
            var montoContVentas = 0.0;

            foreach (var recibo in tasas)
            {
                montoContTasas += recibo.Monto;
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                contTasas++;
                cont++;
            }
            foreach (var recibo in constancias)
            {
                montoContEmision += recibo.Monto;
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
                contEmision++;
            }
            foreach (var recibo in multas)
            {
                montoContMultas += recibo.Monto;
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
                contMultas++;
            }
            foreach (var recibo in devoluciones)
            {
                montoContDevolucion += recibo.Monto;
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
                contDevolucion++;
            }
            foreach (var recibo in ventas)
            {
                montoContVentas += recibo.Monto;
                recibo.No = cont;
                dtoRecibos.Add(recibo);
                cont++;
                contVentas++;
            }

            consolidado.reciboReportes = dtoRecibos;

            consolidado.MontoTotalRecibosTasas = montoContTasas;
            consolidado.MontoTotalRecibosEmision = montoContEmision;
            consolidado.MontoTotalRecibosMultas = montoContMultas;
            consolidado.MontoTotalRecibosDevolucion = montoContDevolucion;
            consolidado.MontoTotalRecibosVentas = montoContVentas;

            consolidado.TotalRecibosTasas = contTasas;
            consolidado.TotalRecibosEmision = contEmision;
            consolidado.TotalRecibosMultas = contMultas;
            consolidado.TotalRecibosDevolucion = contDevolucion;
            consolidado.TotalRecibosVentas = contVentas;

            return consolidado;
        }
        public double CalcularComision(double monto)
        {
            return (monto * 0.0032);
        }

        static List<string> ObtenerNombresMeses(DateTime fechaInicio, DateTime fechaFin)
        {
            List<string> nombresMeses = new List<string>();
            DateTime fechaActual = fechaInicio;

            while (fechaActual <= fechaFin)
            {
                nombresMeses.Add(fechaActual.ToString("MMMM", new CultureInfo("es-ES")));
                fechaActual = fechaActual.AddMonths(1);
            }

            return nombresMeses;
        }

    }

}
