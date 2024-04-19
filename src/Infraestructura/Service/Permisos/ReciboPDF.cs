using Dominio.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Text;
using System;
using System.Collections;
using Infraestructura.Service.utilidades;
using Dominio.Repositories;

namespace Infraestructura.Service.Permisos
{

    public class ReciboPDF : IPermisoRecibo
    {
        public ReciboPDF(IQrHelper qrHelper, IConfiguration configuration, ICatalogoRepository catalogo)
        {
            this.catalogo = catalogo;
            this.qrHelper = qrHelper;
            this.configuration = configuration;
        }
        public int SePuedeUytilizar { get { return 1; } }
        public bool SePuedeUtilizar(int depenedencia)
        {
            return true;
        }
        private const string logo = "senasa";
        private const string firmaDigital = "firmaDigital";
        private const string tipoLetra = "Arial";
        private const int tamanioLetraTitulo = 11;
        private const int tamanioLetraTitulo2 = 8;
        private const int tamanioLetraParrafo = 9;
        private const int tamanioLetraTabla2 = 7;
        private readonly IQrHelper qrHelper;
        private readonly IConfiguration configuration;
        private readonly ICatalogoRepository catalogo;
        public Stream Getpermiso(Dominio.Models.Recibo recibo)
        {
            var respuesta = new MemoryStream();
            var document = new Document();

            var inst = PdfWriter.GetInstance(document, respuesta);
            document.AddAuthor("GQ racing Sport");
            document.Open();

            Encabezado(document, recibo);
            Cuerpo(document, recibo);
            DataCuerpo(document, recibo);
            Detalles(document, recibo);
            Precio(document, recibo);
            Firma(document, recibo);
            PiePagina(document);
            document.Close();
            respuesta.Position = 0;
            return respuesta;
        }
        public Stream GetReportePDF(ReciboReporteConsolidado recibo)
        {
            
            ArrayList arlColumnas = new ArrayList();
            arlColumnas.Add(new ReporteColumna("Nro. Recibo", 10, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Insititución", 10, true, Element.ALIGN_CENTER, Element.ALIGN_LEFT, "", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Nombre o Razón", 25, true, Element.ALIGN_CENTER, Element.ALIGN_LEFT, "", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Rubro", 15, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "d", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Estado", 10, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Monto", 10, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Banco", 15, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Creado en", 15, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "d", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Pagado en", 15, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "d", FontFactory.TIMES_ROMAN, 8));
            arlColumnas.Add(new ReporteColumna("Procesado en", 15, true, Element.ALIGN_CENTER, Element.ALIGN_CENTER, "d", FontFactory.TIMES_ROMAN, 8));
            var Encabezado = "Recibos";
            var SubEncabezado = "GQ racing Sport";
            var PiePagina = "Reporte GQ racing Sport";
            PdfPTable tablaTmp = new PdfPTable(arlColumnas.Count);
            PdfWriter m_writerTmp;
            IPdfPageEvent m_peTmp;
            ReporteColumna udtCIDTmp;
            Font fuenteTmp = new Font();
            int[] iTamanio = new int[arlColumnas.Count];
            PdfPCell celdaTmp;
            Rectangle PapelTamanio = iTextSharp.text.PageSize.A4.Rotate();
            var respuesta = new MemoryStream();
            var document = new Document(PapelTamanio, 36, 36, 144, 27);

            m_writerTmp = PdfWriter.GetInstance(document, respuesta);
            m_peTmp = new ReportePagina(Encabezado, SubEncabezado, PiePagina, arlColumnas, "", true, recibo);
            m_writerTmp.PageEvent = m_peTmp;


            document.AddAuthor("GQ racing Sport");
            document.Open();

            tablaTmp.WidthPercentage = 100;

            // obtener ancho de columnas
            for (int i = 0; i <= arlColumnas.Count - 1; i++)
            {
                // obtener columna
                udtCIDTmp = (ReporteColumna)arlColumnas[i];

                // obtener tamaño de la columna
                iTamanio[i] = udtCIDTmp.Tamanio;

                // destruir
                udtCIDTmp = null;
            }

            // fijar ancho de columnas
            tablaTmp.SetWidths(iTamanio);
            // por cada registro
            foreach (var drwTmp in recibo.reciboReportes)
            {
                // por cada columna
                for (int i = 0; i <= arlColumnas.Count - 1; i++)
                {
                    // obtener columna
                    udtCIDTmp = (ReporteColumna)arlColumnas[i];

                    // crear fuente
                    fuenteTmp = new Font();
                    fuenteTmp = FontFactory.GetFont(udtCIDTmp.Fuente, udtCIDTmp.FuenteTamanio, Font.NORMAL);

                    // asignar información
                    if (udtCIDTmp.Formato.Length > 0)
                    {
                        // con formato
                        if (drwTmp != null)
                        {
                            if (i == 0)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", drwTmp.Recibo), fuenteTmp));
                            }
                            else if (i == 1)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", "145- GQ racing Sport"), fuenteTmp));
                            }
                            else if (i == 2)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", drwTmp.NombreRazon), fuenteTmp));
                            }
                            else if (i == 3)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", drwTmp.Descripcion), fuenteTmp));
                            }
                            else if (i == 4)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", "SOLICITADO"), fuenteTmp));
                            }
                            else if (i == 7)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", drwTmp.FechaCreado), fuenteTmp));
                            }
                            else if (i == 8)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", drwTmp.FechaPago), fuenteTmp));
                            }
                            else if (i == 9)
                            {
                                celdaTmp = new PdfPCell(new Phrase(String.Format("{0:" + udtCIDTmp.Formato + "}", drwTmp.FechaUtilizado), fuenteTmp));
                            }
                            
                            else
                            {
                                celdaTmp = new PdfPCell(new Phrase(" 2", fuenteTmp));
                            }


                        }
                        else
                        {
                            celdaTmp = new PdfPCell(new Phrase(" 2", fuenteTmp));
                        }
                    }
                    else
                    {
                        //sin formato
                        if (drwTmp != null)
                        {
                            if (i == 0)
                            {
                                celdaTmp = new PdfPCell(new Phrase(drwTmp.Recibo.ToString(), fuenteTmp));
                            }
                            else if (i == 1)
                            {
                                celdaTmp = new PdfPCell(new Phrase("145 - GQ racing Sport", fuenteTmp));
                            }
                            else if (i == 2)
                            {
                                celdaTmp = new PdfPCell(new Phrase(drwTmp.NombreRazon, fuenteTmp));
                            }
                            else if (i == 3)
                            {
                                celdaTmp = new PdfPCell(new Phrase(drwTmp.Descripcion, fuenteTmp));
                            }
                            else if (i == 4)
                            {
                                celdaTmp = new PdfPCell(new Phrase("SOLICITADO", fuenteTmp));
                            }
                            else if (i == 5)
                            {
                                celdaTmp = new PdfPCell(new Phrase("$. " + drwTmp.Monto.ToString(), fuenteTmp));
                            }
                            else if (i == 6)
                            {
                                celdaTmp = new PdfPCell(new Phrase(drwTmp.Banco, fuenteTmp));
                            }
                            else
                            {
                                celdaTmp = new PdfPCell(new Phrase("a", fuenteTmp));
                            }

                        }
                        else
                        {
                            celdaTmp = new PdfPCell(new Phrase("b ", fuenteTmp));
                        }
                    }

                    // adicionar
                    celdaTmp.Border = PdfPCell.NO_BORDER;
                    celdaTmp.HorizontalAlignment = udtCIDTmp.AlineacionValor;
                    celdaTmp.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaTmp.AddCell(celdaTmp);

                    // resetear
                    udtCIDTmp = null;
                }
            }
            // adicionar línea en blanco
            celdaTmp = new PdfPCell(new Phrase(" ", fuenteTmp))
            {
                Border = PdfPCell.NO_BORDER,
                Colspan = arlColumnas.Count,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_TOP
            };
            tablaTmp.AddCell(celdaTmp);

            // adicionar tabla con la información
            document.Add(tablaTmp);
            DetallesConsolidadoReporte(document, recibo);
            PrecioReportePDF(document, recibo);
            CantidadReportePDF(document, recibo);
            document.Close();
            respuesta.Position = 0;
            return respuesta;
        }
        //======================Recibo==========================================//
        private void Encabezado(Document document, Dominio.Models.Recibo recibo)
        {
            PdfPTable tblEncabezado = new PdfPTable(2);
            tblEncabezado.WidthPercentage = 100;
            tblEncabezado.TotalWidth = 500f;

            float[] widths = new float[] { 250f, 250f };
            tblEncabezado.SetWidths(widths);

            var celda = new PdfPCell();


            Font AvenirNegroBoldMedium = FontFactory.GetFont("Avenir", 12f, Font.NORMAL, new BaseColor(60, 60, 59));
            Font AvenirNegroBold = FontFactory.GetFont("Avenir", 12f, Font.BOLD, new BaseColor(60, 60, 59));




            var imagen = Image.GetInstance(GetStream(logo));
            imagen.ScalePercent(10);
            imagen.Alignment = Element.ALIGN_LEFT;
            celda.Border = Rectangle.NO_BORDER;
            celda.AddElement(imagen);
            tblEncabezado.AddCell(celda);

            celda = new PdfPCell(new Paragraph("Emision: " + DateTime.Now.Date.ToShortDateString(), AvenirNegroBoldMedium));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            tblEncabezado.AddCell(celda);

            document.Add(tblEncabezado);

        }


        private void Cuerpo(Document document, Dominio.Models.Recibo recibo)
        {
            PdfPTable tblEncabezado = new PdfPTable(1);
            tblEncabezado.WidthPercentage = 100;
            tblEncabezado.TotalWidth = 500f;

            float[] widths = new float[] { 450f };
            tblEncabezado.SetWidths(widths);

            var celda = new PdfPCell();
            var departamento = 0;
            foreach (var item in recibo.DetalleRecibos)
            {
                departamento = item.Servicio.DepartamentoId;
            }

            Font AvenirNegroBoldMedium = FontFactory.GetFont("Avenir", 12f, Font.NORMAL, new BaseColor(60, 60, 59));
            Font AvenirNegroBold = FontFactory.GetFont("Avenir", 12f, Font.BOLD, new BaseColor(60, 60, 59));


            celda = new PdfPCell(new Paragraph(" ", AvenirNegroBold));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tblEncabezado.AddCell(celda);

            celda = new PdfPCell(new Paragraph("Taller Mecánico Automotriz, Enderezado y Pintura, Carwash Accesorios. Venta y Distribucion de Llantas.", AvenirNegroBold));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tblEncabezado.AddCell(celda);

            celda = new PdfPCell(new Paragraph(getNombreCatalogo(recibo.AreaId), AvenirNegroBold)); // area
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tblEncabezado.AddCell(celda);

            document.Add(tblEncabezado);



        }
        private string getNombreCatalogo(int id) {

            var catalogoBusquedad = catalogo.GetById(id);
            return catalogoBusquedad.Nombre;
        }



        private void DataCuerpo(Document document, Dominio.Models.Recibo recibo)
        {

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 95;
            tabla.SetWidths(new int[] { 45, 45 });

            Font AvenirNegroMedium = FontFactory.GetFont("Avenir", 12f, Font.NORMAL, new BaseColor(60, 60, 59));
            Font AvenirNegroBold = FontFactory.GetFont("Avenir", 12f, Font.BOLD, new BaseColor(60, 60, 59));
            Font blanco = new Font(Font.TIMES_ROMAN, 9f, Font.NORMAL, new BaseColor(255, 255, 255));

            PdfPCell celda = new PdfPCell();

            Phrase parteIzq = new Phrase();
            Chunk txt0 = new Chunk("\n", AvenirNegroMedium);
            Chunk txt1 = new Chunk("Identificacion: ", AvenirNegroMedium);
            Chunk txt2 = new Chunk(recibo.Identificacion, AvenirNegroBold);
            Chunk txt3 = new Chunk("\n", AvenirNegroMedium);
            Chunk txt4 = new Chunk("Nombre o razon: \n", AvenirNegroMedium);
            Chunk txt5 = new Chunk(recibo.NombreRazon, AvenirNegroBold);
            Chunk txt6 = new Chunk("\nNombre Marca: ", AvenirNegroMedium);
            Chunk txt7 = new Chunk(getNombreCatalogo(recibo.MarcaId), AvenirNegroBold);
            Chunk txt8 = new Chunk("\nNombre Modelo: ", AvenirNegroMedium);
            Chunk txt9 = new Chunk(getNombreCatalogo(recibo.ModeloId), AvenirNegroBold);



  

            parteIzq.Add(txt0);
            parteIzq.Add(txt1);
            parteIzq.Add(txt2);
            parteIzq.Add(txt3);
            parteIzq.Add(txt4);
            parteIzq.Add(txt5);
            parteIzq.Add(txt6);
            parteIzq.Add(txt7);
            parteIzq.Add(txt8);
            parteIzq.Add(txt9);

            Paragraph parrafo = new Paragraph();
            parrafo.Add(parteIzq);

            celda = new PdfPCell(parrafo);
            celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);
            //
            Phrase parteDere = new Phrase();
            Chunk txtD0 = new Chunk("\n", AvenirNegroMedium);
            Chunk txtD1 = new Chunk("Numero de Recibo: ", AvenirNegroMedium);
            Chunk txtD2 = new Chunk(recibo.Id.ToString(), AvenirNegroBold);
            Chunk txtD3 = new Chunk("\n", AvenirNegroMedium);
            Chunk txtD4 = new Chunk("Generado desde la plataforma de de pagos GQ racing Sport. ", AvenirNegroBold);

            parteDere.Add(txtD0);
            parteDere.Add(txtD1);
            parteDere.Add(txtD2);
            parteDere.Add(txtD3);
            parteDere.Add(txtD4);
            Paragraph parrafoD = new Paragraph();
            parrafoD.Add(parteDere);

            celda = new PdfPCell(parrafoD);
            celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);

            celda = new PdfPCell(new Paragraph("\n", AvenirNegroBold));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tabla.AddCell(celda);




            document.Add(tabla);
        }

        private void Detalles(Document document, Dominio.Models.Recibo recibo)
        {
            string tipomoneda = "";
            if (recibo.MonedaId == 64)
            {
                tipomoneda = "$.";
            }
            else {
                tipomoneda = "$ ";
            }

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 95;
            tabla.SetWidths(new int[] { 65, 25 });

            Font AvenirNegroMedium = FontFactory.GetFont("Avenir", 12f, Font.NORMAL, new BaseColor(60, 60, 59));
            Font AvenirNegroBold = FontFactory.GetFont("Avenir", 12f, Font.BOLD, new BaseColor(60, 60, 59));
            Font blanco = new Font(Font.TIMES_ROMAN, 9f, Font.NORMAL, new BaseColor(255, 255, 255));

            PdfPCell celda = new PdfPCell();

            celda = new PdfPCell(new Paragraph("Servicio", AvenirNegroBold));
            celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            celda.BackgroundColor = new BaseColor(255, 255, 255);
            celda.Border = PdfPCell.BOTTOM_BORDER;
            celda.BorderColor = new BaseColor(60, 60, 59);
            celda.BorderWidthBottom = 2f;
            celda.FixedHeight = 30;
            tabla.AddCell(celda);


            celda = new PdfPCell(new Paragraph("Monto ("+tipomoneda+")", AvenirNegroBold));
            celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            celda.BackgroundColor = new BaseColor(255, 255, 255);
            celda.Border = PdfPCell.BOTTOM_BORDER;
            celda.BorderColor = new BaseColor(60, 60, 59);
            celda.BorderWidthBottom = 2f;
            celda.FixedHeight = 30;
            tabla.AddCell(celda);

            GetServicio(tabla, recibo, true);

            document.Add(tabla);
        }

        private void GetServicio(PdfPTable table, Dominio.Models.Recibo recibo, bool mostrarMonto)
        {
            int tamañoComposicion = 0;
            int tamañosCampos = 65;
            float tamañoNombreGenerico = 6.9f;

            foreach (var item in recibo.DetalleRecibos)
            {
                tamañoComposicion = item.Servicio.NombreServicio.Length + item.Servicio.NombreSubServicio.Length;
            }
            if (tamañoComposicion > 190)
            {
                tamañoNombreGenerico = 8.8f; tamañosCampos = 140;
            }
            else if (tamañoComposicion > 130 && tamañoComposicion <= 190)
            {
                tamañoNombreGenerico = 8.8f; tamañosCampos = 75;
            }
            else if (tamañoComposicion > 70 && tamañoComposicion <= 130)
            {
                tamañosCampos = 65; tamañoNombreGenerico = 7.3f;
            }
            else
            {
                tamañosCampos = 50; tamañoNombreGenerico = 7.3f;
            }
            Font contenidoNormal = new Font(Font.TIMES_ROMAN, 6.9f, Font.NORMAL, new BaseColor(60, 60, 59));
            Font AvenirNegroMediumItalic = FontFactory.GetFont("Avenir", 6.9f, Font.ITALIC, new BaseColor(60, 60, 59));
            Font AvenirNegroMedium = FontFactory.GetFont("Avenir", 13, Font.NORMAL, new BaseColor(60, 60, 59));

            foreach (var item in recibo.DetalleRecibos)
            {

                string nombreCompleto = item.Servicio.NombreServicio;

                if (!String.IsNullOrEmpty(item.Servicio.NombreSubServicio))
                {
                    nombreCompleto = item.Servicio.NombreServicio + " / " + item.Servicio.NombreSubServicio;
                }
                else if (!String.IsNullOrEmpty(item.Servicio.Descripcion))
                {
                    nombreCompleto = item.Servicio.NombreServicio + " / " + item.Servicio.NombreSubServicio + " / " + item.Servicio.Descripcion;
                }

                PdfPCell celda = new PdfPCell();


                celda = new PdfPCell(new Paragraph(nombreCompleto, AvenirNegroMedium));
                celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celda.FixedHeight = tamañosCampos;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

                celda = new PdfPCell(new Paragraph(item.Monto.ToString("0,0.00"), AvenirNegroMedium));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                celda.FixedHeight = tamañosCampos;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);



                string numeroFila = "";
                for (int j = 1; j < 1; j++)
                {
                    numeroFila = (j + 1).ToString();

                    celda = new PdfPCell(new Paragraph(numeroFila + ") ", contenidoNormal));
                    celda.FixedHeight = 75;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = PdfPCell.NO_BORDER;
                    table.AddCell(celda);

                    celda = new PdfPCell(new Paragraph(" ", contenidoNormal));
                    celda.FixedHeight = 75;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = PdfPCell.NO_BORDER;
                    table.AddCell(celda);

                    celda = new PdfPCell(new Paragraph(" ", contenidoNormal));
                    celda.FixedHeight = 75;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = PdfPCell.NO_BORDER;
                    table.AddCell(celda);

                    celda = new PdfPCell(new Paragraph(" ", contenidoNormal));
                    celda.FixedHeight = 75;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = PdfPCell.NO_BORDER;
                    table.AddCell(celda);

                    celda = new PdfPCell(new Paragraph(" ", contenidoNormal));
                    celda.FixedHeight = 75;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = PdfPCell.NO_BORDER;
                    table.AddCell(celda);

                    celda = new PdfPCell(new Paragraph(" ", contenidoNormal));
                    celda.FixedHeight = 75;
                    celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                    celda.Border = PdfPCell.NO_BORDER;
                    table.AddCell(celda);
                }
            }




        }
        private void Precio(Document document, Dominio.Models.Recibo recibo)
        {
            string tipomoneda = "";
            if (recibo.MonedaId == 64)
            {
                tipomoneda = "$.";
            }
            else
            {
                tipomoneda = "$ ";
            }

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 95;
            tabla.SetWidths(new int[] { 45, 45 });

            Font AvenirNegroMedium = FontFactory.GetFont("Avenir", 12f, Font.NORMAL, new BaseColor(60, 60, 59));
            Font AvenirNegroBold = FontFactory.GetFont("Avenir", 12f, Font.BOLD, new BaseColor(60, 60, 59));
            Font blanco = new Font(Font.TIMES_ROMAN, 9f, Font.NORMAL, new BaseColor(255, 255, 255));

            PdfPCell celda = new PdfPCell();

            Phrase parteIzq = new Phrase();
            Chunk txt0 = new Chunk("\n", AvenirNegroMedium);
            Chunk txt1 = new Chunk("Total a Pagar: ", AvenirNegroMedium);

            parteIzq.Add(txt0);
            parteIzq.Add(txt1);

            Paragraph parrafo = new Paragraph();
            parrafo.Add(parteIzq);

            celda = new PdfPCell(parrafo);
            celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);
            //
            Phrase parteDere = new Phrase();
            Chunk txtD0 = new Chunk("\n", AvenirNegroMedium);
            Chunk txtD1 = new Chunk(tipomoneda, AvenirNegroMedium);
            Chunk txtD2 = new Chunk(recibo.MontoTotal.ToString("0,0.00"), AvenirNegroBold);

            parteDere.Add(txtD0);
            parteDere.Add(txtD1);
            parteDere.Add(txtD2);

            Paragraph parrafoD = new Paragraph();
            parrafoD.Add(parteDere);

            celda = new PdfPCell(parrafoD);
            celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);

            celda = new PdfPCell(new Paragraph("\n", AvenirNegroBold));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tabla.AddCell(celda);




            document.Add(tabla);
        }
        private void Firma(Document document, Dominio.Models.Recibo recibo)
        {
            PdfPTable tblEncabezado = new PdfPTable(1);
            tblEncabezado.WidthPercentage = 30;
            tblEncabezado.TotalWidth = 100f;

            float[] widths = new float[] { 100f };
            tblEncabezado.SetWidths(widths);

            var celda = new PdfPCell();


            Font AvenirNegroBoldMedium = FontFactory.GetFont("Avenir", 12f, Font.NORMAL, new BaseColor(60, 60, 59));
            Font AvenirNegroBold = FontFactory.GetFont("Avenir", 12f, Font.BOLD, new BaseColor(60, 60, 59));




            var imagen = Image.GetInstance(GetStream(firmaDigital));
            imagen.ScalePercent(35);
            imagen.Alignment = Element.ALIGN_CENTER;
            celda.Border = Rectangle.NO_BORDER;
            celda.AddElement(imagen);
            tblEncabezado.AddCell(celda);



            celda = new PdfPCell(new Paragraph("Generado Online", AvenirNegroBoldMedium));
            celda.Border = Rectangle.BOTTOM_BORDER;
            celda.BorderWidthBottom = 1f;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tblEncabezado.AddCell(celda);

            celda = new PdfPCell(new Paragraph("Firma*", AvenirNegroBold));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tblEncabezado.AddCell(celda);

            document.Add(tblEncabezado);

        }
        private void PiePagina(Document document)
        {
            PdfPTable tblEncabezado = new PdfPTable(1);
            tblEncabezado.WidthPercentage = 100;
            tblEncabezado.TotalWidth = 500f;
            float[] widths = new float[] { 500f };
            tblEncabezado.SetWidths(widths);
            var celda = new PdfPCell();
            Font AvenirNegroSmall = FontFactory.GetFont("Avenir", 7f, Font.NORMAL, new BaseColor(60, 60, 59));

            string text = @"Recibo generado el " + DateTime.Now.Date.ToShortDateString() + ". Sera valido durante 15 días calendario para pago.";
            celda = new PdfPCell(new Paragraph("\n\n\n" + text, AvenirNegroSmall));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            celda.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
            celda.Colspan = 2;
            tblEncabezado.AddCell(celda);
            document.Add(tblEncabezado);


        }
        //=========================================================================//


        //======================Reporte=========================================//

        private void DetallesConsolidadoReporte(Document document, ReciboReporteConsolidado recibos)
        {
            Font fEncabezado;
            fEncabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD);

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 95;
            tabla.SetWidths(new int[] { 65, 25 });


            PdfPCell celda = new PdfPCell();

            celda = new PdfPCell(new Paragraph("Rubro", fEncabezado));
            celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            celda.BackgroundColor = new BaseColor(255, 255, 255);
            celda.Border = PdfPCell.BOTTOM_BORDER;
            celda.BorderColor = new BaseColor(60, 60, 59);
            celda.BorderWidthBottom = 2f;
            celda.FixedHeight = 30;
            tabla.AddCell(celda);


            celda = new PdfPCell(new Paragraph("Monto ($.)", fEncabezado));
            celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            celda.BackgroundColor = new BaseColor(255, 255, 255);
            celda.Border = PdfPCell.BOTTOM_BORDER;
            celda.BorderColor = new BaseColor(60, 60, 59);
            celda.BorderWidthBottom = 2f;
            celda.FixedHeight = 15;
            tabla.AddCell(celda);

            GetReporteRubro(tabla, recibos, true);

            document.Add(tabla);
        }

        private void GetReporteRubro(PdfPTable table, ReciboReporteConsolidado recibo, bool mostrarMonto)
        {

            Font fSubEncabezado;
            fSubEncabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL);

            PdfPCell celda = new PdfPCell();

            if (recibo.TotalRecibosTasas > 0)
            {
               
                celda = new PdfPCell(new Paragraph("12199 - Tasas Varias", fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

                celda = new PdfPCell(new Paragraph("$. " + recibo.MontoTotalRecibosTasas.ToString("0,0.00"), fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

            }
            if (recibo.TotalRecibosEmision > 0) {
                celda = new PdfPCell(new Paragraph("12121 - Emisión, Constancias, Certificaciones y Otros", fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

                celda = new PdfPCell(new Paragraph("$. " + recibo.MontoTotalRecibosEmision.ToString("0,0.00"), fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

            }
            if (recibo.TotalRecibosMultas > 0)
            {
                celda = new PdfPCell(new Paragraph("12499 - Multas", fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

                celda = new PdfPCell(new Paragraph("$. " + recibo.MontoTotalRecibosMultas.ToString("0,0.00"), fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

            }
            if (recibo.TotalRecibosDevolucion > 0)
            {
                celda = new PdfPCell(new Paragraph("12806 - Devoluciones de Ejercicios Fisc. Anteriores", fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

                celda = new PdfPCell(new Paragraph("$. " + recibo.MontoTotalRecibosDevolucion.ToString("0,0.00"), fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

            }
            if (recibo.TotalRecibosVentas > 0)
            {
                celda = new PdfPCell(new Paragraph("15104 - Venta de Artículos y Mat. Diversos", fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

                celda = new PdfPCell(new Paragraph("$. " + recibo.MontoTotalRecibosVentas.ToString("0,0.00"), fSubEncabezado));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                celda.FixedHeight = 20;
                celda.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                celda.Border = PdfPCell.NO_BORDER;
                table.AddCell(celda);

            }


        }
        private void CantidadReportePDF(Document document, ReciboReporteConsolidado recibo)
        {

            var cantTotal = recibo.TotalRecibosDevolucion + recibo.TotalRecibosEmision + recibo.TotalRecibosMultas + recibo.TotalRecibosTasas + recibo.TotalRecibosVentas; 
            Font Encabezado;
            Encabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD);
            Font fSubEncabezado;
            fSubEncabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL);

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 95;
            tabla.SetWidths(new int[] { 45, 45 });

            PdfPCell celda = new PdfPCell();

            Phrase parteIzq = new Phrase();
            Chunk txt0 = new Chunk("\n", Encabezado);
           

            parteIzq.Add(txt0);

            Paragraph parrafo = new Paragraph();
            parrafo.Add(parteIzq);

            celda = new PdfPCell(parrafo);
            celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);
            //
            Phrase parteDere = new Phrase();
            Chunk txtD00 = new Chunk("\n", Encabezado);
            Chunk txtD0 = new Chunk("Cantidad de Recibos: ", Encabezado);
            Chunk txtD2 = new Chunk(cantTotal.ToString(), fSubEncabezado);
            parteDere.Add(txtD00);
            parteDere.Add(txtD0);
            parteDere.Add(txtD2);

            Paragraph parrafoD = new Paragraph();
            parrafoD.Add(parteDere);

            celda = new PdfPCell(parrafoD);
            celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);

            celda = new PdfPCell(new Paragraph("\n", Encabezado));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tabla.AddCell(celda);




            document.Add(tabla);
        }
        private void PrecioReportePDF(Document document, ReciboReporteConsolidado recibo)
        {

            var montoTotal = recibo.MontoTotalRecibosDevolucion + recibo.MontoTotalRecibosEmision + recibo.MontoTotalRecibosMultas + recibo.MontoTotalRecibosTasas + recibo.MontoTotalRecibosVentas;
            Font Encabezado;
            Encabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD);
            Font fSubEncabezado;
            fSubEncabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL);

            PdfPTable tabla = new PdfPTable(2);
            tabla.WidthPercentage = 95;
            tabla.SetWidths(new int[] { 45, 45 });

            



            PdfPCell celda = new PdfPCell();

            Phrase parteIzq = new Phrase();
            Chunk txt0 = new Chunk("\n", Encabezado);
            

            parteIzq.Add(txt0);


            Paragraph parrafo = new Paragraph();
            parrafo.Add(parteIzq);

            celda = new PdfPCell(parrafo);
            celda.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);
            //
            Phrase parteDere = new Phrase();
            Chunk txtD0 = new Chunk("Monto Total: ", Encabezado);
            Chunk txtD1 = new Chunk("$. ", fSubEncabezado);
            Chunk txtD2 = new Chunk(montoTotal.ToString("0,0.00"), fSubEncabezado);
            parteDere.Add(txtD0);
            parteDere.Add(txtD1);
            parteDere.Add(txtD2);

            Paragraph parrafoD = new Paragraph();
            parrafoD.Add(parteDere);

            celda = new PdfPCell(parrafoD);
            celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            celda.Border = PdfPCell.NO_BORDER;
            tabla.AddCell(celda);

            celda = new PdfPCell(new Paragraph("\n", Encabezado));
            celda.Border = Rectangle.NO_BORDER;
            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tabla.AddCell(celda);




            document.Add(tabla);
        }
        //===============================================================//
        public Stream GetStream(string name)
        {

            var assembly = Assembly.GetExecutingAssembly();
            string[] imagen = assembly.GetManifestResourceNames();
            string resource = imagen.FirstOrDefault(x => x.Contains(name));

            return assembly.GetManifestResourceStream(resource);
        }
    }


}

