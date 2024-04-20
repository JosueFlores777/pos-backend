using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Dominio.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Infraestructura.Service.utilidades
{
    class ReportePagina : PdfPageEventHelper
    {

        #region "enumerador"

        /// <summary>
        /// Enumerador que indica el borde que se debe de applicar a una celda de la tabla <see cref="PdfPCell"/>
        /// </summary>
        protected enum CeldaBorde
        {
            ninguno,
            arriba,
            abajo,
            arribayabajo,
            izquierda,
            izquierdayarriba,
            izquierdayabajo,
            izquierdaarribayabajo,
            derecha,
            derechayarriba,
            derechayabajo,
            derechaarribayabajo,
            izquierdayderecha,
            izquierdaderechayarriba,
            izquierdaderechayabajo,
            todos
        }

        #endregion

        #region "varibles"

        private string m_Encabezado;
        private string m_Subencabezado;
        private string m_PiePagina;
        private ArrayList m_EncabezadoColumnas;
        private readonly string m_Logo;
        private bool m_Detalle;
        private bool m_Activo;
        //
        private PdfContentByte m_cbTmp;
        private PdfTemplate m_template;
        private BaseFont m_bfTmp;
        private ReciboReporteConsolidado m_recibo;
        #endregion

        #region "constructor"

        /// <summary>
        /// Inicializa una nueva instancia de clase <see cref="ReportePagina"/>, esta clase se encarga del encabezado y pie de página
        /// </summary>
        /// <param name="encabezado">Encabezado a mostrar</param>
        /// <param name="subencabezado">Subencabezado a mostrar</param>
        /// <param name="piePagina">Pie de página a mostrar</param>
        /// <param name="encabezadocolumnas">Arreglo de columnas que contiene el reporte</param>
        /// <param name="logoNombre">Ruta y nombre del Logotipo</param>
        /// <param name="blnDetalle">true = Para mostrar lo títulos de las columnas, false = Para no mostrar los títulos de la columnas</param>
        public ReportePagina(string encabezado, string subencabezado, string piePagina, ArrayList encabezadocolumnas, string logotipo, bool blnDetalle, ReciboReporteConsolidado recibo)
        {
            try
            {
                // asignar valores
                m_Encabezado = encabezado;
                m_recibo = recibo;
                m_Subencabezado = subencabezado;
                m_PiePagina = piePagina;
                m_EncabezadoColumnas = encabezadocolumnas;
                m_Logo = logotipo;
                m_Detalle = blnDetalle;
                m_Activo = true;
            }
            catch (Exception Ex)
            {
                // heredar
                throw Ex;
            }
        }

        #endregion

        #region "funciones"

        /// <summary>
        /// Insertar saltos de línea
        /// </summary>
        /// <param name="numeroSaltos">Saltos de línea a insertar</param>
        /// <returns></returns>
        private string LineaSalto(int numeroSaltos)
        {
            StringBuilder sbTmp = new StringBuilder();
            try
            {
                // incrementar saltos
                for (int i = 1; i <= numeroSaltos; i++)
                {
                    // adicionar saltos
                    sbTmp.Append("\r\n");
                }

                // final
                return sbTmp.ToString();
            }
            catch (Exception Ex)
            {
                // heredar
                throw Ex;
            }
        }

        #endregion

        #region "propiedades"

        public string Encabezado
        {
            get { return m_Encabezado; }
            set { m_Encabezado = value; }
        }

        public string Subencabezado
        {
            get { return m_Subencabezado; }
            set { m_Subencabezado = value; }
        }

        public string PiePagina
        {
            get { return m_PiePagina; }
            set { m_PiePagina = value; }
        }

        public ArrayList EncabezadoColumnas
        {
            get { return m_EncabezadoColumnas; }
            set { m_EncabezadoColumnas = value; }
        }

        public bool Detalle
        {
            get { return m_Detalle; }
            set { m_Detalle = value; }
        }

        public bool Activo
        {
            get { return m_Activo; }
            set { m_Activo = value; }
        }

        #endregion

        #region "sobreescritura"

        /// <summary>
        /// Sobreescritura del método OnOpenDocument, para inicializar las variables necesarias
        /// </summary>
        /// <param name="writer">Objeto PdfWriter del pdf</param>
        /// <param name="document">Objeto Document del pdf</param>
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                // variables
                m_bfTmp = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                // crear plantilla
                m_cbTmp = writer.DirectContent;
                m_template = m_cbTmp.CreateTemplate(50, 50);
            }
            catch (Exception ex)
            {
                // bitacora heredar
                throw ex;
            }
        }

        /// <summary>
        /// Sobreescritura del método OnEndPage, para escribir el pie de página
        /// </summary>
        /// <param name="writer">Objeto PdfWriter del pdf</param>
        /// <param name="document">Objeto Document del pdf</param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            String format = "dd MMM yyyy hh:mm tt";
            PdfPTable tblEncabezado = new PdfPTable(2);
            PdfPTable tblTitulo = new PdfPTable(3);
            PdfPTable tblSubencabezado = new PdfPTable(1);
            PdfPTable tblEncabezadoColumnas = new PdfPTable((m_EncabezadoColumnas.Count == 0) ? 1 : m_EncabezadoColumnas.Count);
            PdfPTable tblLogotipo = new PdfPTable(1);
            PdfPTable tblPiePagina = new PdfPTable(3);
            int[] iTamanio = new int[m_EncabezadoColumnas.Count];
            ReporteColumna udtCIDCuerpo;
            //
            PdfPCell cellTmp;
            Image imgLogo;
            Phrase fraseTmp;
            //
            Font fEncabezado;
            Font fEncabezado2;
            Font fSubEncabezado;
            Font fSubEncabezado2;
            Font fPiePagina;
            Font fEncabezadoColumnas;
            Font fTamanio2;
            Rectangle recTmp;
            //
            string tPagina = " Página " + writer.PageNumber.ToString() + " de ";
            string fecha = "Impreso el " + DateTime.Now.ToString(format);
            float sLongitud = m_bfTmp.GetWidthPoint(tPagina, 6);
            try
            {
                // verificar
                if (m_Activo)
                {
                    // definir encabezado, subencabezado, logotipo y pie de página
                    tblEncabezado.WidthPercentage = 100;
                    tblTitulo.WidthPercentage = 100;
                    tblSubencabezado.WidthPercentage = 100;
                    tblEncabezadoColumnas.WidthPercentage = 100;
                    tblLogotipo.WidthPercentage = 100;
                    tblPiePagina.WidthPercentage = 100;

                    // columnas encabezado, subencabezado, logotipo y pie de página
                    tblEncabezado.SetWidths(new int[] { 94, 6 });
                    tblTitulo.SetWidths(new int[] { 20, 60, 20 });
                    tblSubencabezado.SetWidths(new int[] { 100 });
                    tblLogotipo.SetWidths(new int[] { 100 });
                    tblPiePagina.SetWidths(new int[] { 7, 15, 10 });

                    // definir fuentes
                    fEncabezado = new Font();
                    fEncabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD);
                    fEncabezado2 = new Font();
                    fEncabezado2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 15, Font.BOLD);

                    fSubEncabezado = new Font();
                    fSubEncabezado = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD);
                    fSubEncabezado2 = new Font();
                    fSubEncabezado2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.NORMAL);
                    fTamanio2 = new Font();
                    fTamanio2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 2, Font.NORMAL);
                    // cargar logotipo
                    imgLogo = Image.GetInstance(GetStream("logoSenasa"));
                    imgLogo.ScalePercent(31);


                    // adicionar logotipo
                    cellTmp = new PdfPCell(imgLogo)
                    {
                        Border = (int)CeldaBorde.ninguno,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    tblLogotipo.AddCell(cellTmp);

                    cellTmp = new PdfPCell(new Phrase("Reporteria de Sistema de GQ Racing", fEncabezado2))
                    {
                        Border = (int)CeldaBorde.ninguno,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    tblLogotipo.AddCell(cellTmp);

                    cellTmp = new PdfPCell(new Phrase(fecha, fSubEncabezado2))
                    {
                        Border = (int)CeldaBorde.ninguno,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_TOP
                    };
                    tblLogotipo.AddCell(cellTmp);

                    cellTmp = new PdfPCell(new Phrase("Reporte de: "+ m_recibo.Mes, fEncabezado))
                    {
                        Border = (int)CeldaBorde.ninguno,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_TOP
                    };
                    tblLogotipo.AddCell(cellTmp);

                    // adicionar encabezado
                    cellTmp = new PdfPCell(new Phrase(m_Encabezado, fEncabezado))
                    {
                        Border = (int)CeldaBorde.ninguno,
                        HorizontalAlignment = Element.ALIGN_RIGHT,

                        VerticalAlignment = Element.ALIGN_TOP,
                    };
                    tblSubencabezado.AddCell(cellTmp);

                    // verificar
                    if (m_Subencabezado.Length > 0)
                    {
                        // adicionar subencabezado
                        cellTmp = new PdfPCell(new Phrase(m_Subencabezado, fSubEncabezado))
                        {
                            Border = (int)CeldaBorde.ninguno,
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_TOP
                        };
                        tblSubencabezado.AddCell(cellTmp);
                    }

                    // adicionar logotipo
                    cellTmp = new PdfPCell(tblLogotipo)
                    {
                        Border = (int)CeldaBorde.ninguno,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    tblEncabezado.AddCell(cellTmp);

                    // adicionar encabezado
                    cellTmp = new PdfPCell(tblSubencabezado)
                    {
                        Border = (int)CeldaBorde.ninguno,
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_TOP
                    };
                    tblEncabezado.AddCell(cellTmp);




                    // verificar
                    if (m_Detalle)
                    {

                        // por cada columna
                        for (int i = 0; i <= m_EncabezadoColumnas.Count - 1; i++)
                        {
                            // recuperar columna
                            udtCIDCuerpo = (ReporteColumna)m_EncabezadoColumnas[i];

                            // obtener ancho de la columna
                            iTamanio[i] = udtCIDCuerpo.Tamanio;

                            // resetear
                            udtCIDCuerpo = null;
                        }

                        // fijar ancho de columnas
                        tblEncabezadoColumnas.SetWidths(iTamanio);


                        // por cada columna
                        for (int i = 0; i <= m_EncabezadoColumnas.Count - 1; i++)
                        {

                            // recuperar columna
                            udtCIDCuerpo = (ReporteColumna)m_EncabezadoColumnas[i];

                            // crear fuente
                            fEncabezadoColumnas = new Font();
                            fEncabezadoColumnas = FontFactory.GetFont(udtCIDCuerpo.Fuente, udtCIDCuerpo.FuenteTamanio, (udtCIDCuerpo.Negrita == true) ? Font.BOLD : Font.NORMAL);

                            // adicionar etiqueta
                            fraseTmp = new Phrase(udtCIDCuerpo.Etiqueta, fEncabezadoColumnas);
                            cellTmp = new PdfPCell(fraseTmp)
                            {
                                BackgroundColor = new BaseColor(247, 247, 247),
                                Border = PdfPCell.NO_BORDER,
                                HorizontalAlignment = udtCIDCuerpo.AlineacionEtiqueta,
                                VerticalAlignment = Element.ALIGN_MIDDLE
                            };
                            tblEncabezadoColumnas.AddCell(cellTmp);

                            // resetear
                            udtCIDCuerpo = null;
                        }

                        // adicionar titulos a encabezado
                        cellTmp = new PdfPCell(tblEncabezadoColumnas)
                        {
                            Border = (int)CeldaBorde.arribayabajo,
                            BorderColor = new BaseColor(24, 26, 27),
                            Colspan = 2,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };
                        tblEncabezado.AddCell(cellTmp);
                    }
                    else
                    {
                        // linea en blanco
                        fraseTmp = new Phrase(LineaSalto(3), fTamanio2);
                        cellTmp = new PdfPCell(fraseTmp)
                        {
                            Border = (int)CeldaBorde.arriba,
                            BorderColor = new BaseColor(24, 26, 27),
                            Colspan = 2,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };
                        tblEncabezado.AddCell(cellTmp);
                    }

                    // pie de página
                    fPiePagina = new Font();
                    fPiePagina = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 6, Font.BOLD);

                    // adicionar plantilla para número de paginas
                    recTmp = document.PageSize;
                    m_cbTmp.BeginText();
                    m_cbTmp.SetFontAndSize(m_bfTmp, 6);
                    m_cbTmp.SetTextMatrix(recTmp.Width - (document.RightMargin + sLongitud + 5), 11);
                    m_cbTmp.ShowText(tPagina);
                    m_cbTmp.EndText();
                    m_cbTmp.AddTemplate(m_template, (recTmp.Width - document.RightMargin - 5), 11);

                    // adicionar texto pie de página
                    fraseTmp = new Phrase(m_PiePagina, fPiePagina);
                    cellTmp = new PdfPCell(fraseTmp)
                    {
                        Border = (int)CeldaBorde.arriba,
                        BorderColor = new BaseColor(24, 26, 27),
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                    tblPiePagina.AddCell(cellTmp);

                    // adicionar espacios
                    fraseTmp = new Phrase(" ", fPiePagina);
                    cellTmp = new PdfPCell(fraseTmp)
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Border = (int)CeldaBorde.arriba,
                        BorderColor = new BaseColor(24, 26, 27),
                        Colspan = 2
                    };
                    tblPiePagina.AddCell(cellTmp);

                    // establecer Encabezado
                    recTmp = document.PageSize;
                    tblEncabezado.TotalWidth = recTmp.Width - document.LeftMargin - document.RightMargin;
                    tblEncabezado.WriteSelectedRows(0, -1, document.LeftMargin, recTmp.Height - document.TopMargin + tblEncabezado.TotalHeight, writer.DirectContent);

                    // establecer pie de página
                    recTmp = document.PageSize;
                    tblPiePagina.TotalWidth = recTmp.Width - document.LeftMargin - document.RightMargin;
                    tblPiePagina.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
                }
            }
            catch (Exception Ex)
            {
                // heredar
                throw Ex;
            }

        }

        public Stream GetStream(string name)
        {

            var assembly = Assembly.GetExecutingAssembly();
            string[] imagen = assembly.GetManifestResourceNames();
            string resource = imagen.FirstOrDefault(x => x.Contains(name));

            return assembly.GetManifestResourceStream(resource);
        }


        /// <summary>
        /// Sobreescritura del método OnCloseDocument, para actualizar el número de página
        /// </summary>
        /// <param name="writer">Objeto PdfWriter del pdf</param>
        /// <param name="document">Objeto Document del pdf</param>
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            try
            {
                // escribir número de página final
                m_template.BeginText();
                m_template.SetFontAndSize(m_bfTmp, 6);
                m_template.ShowText((writer.PageNumber - 1).ToString());
                m_template.EndText();
            }
            catch (Exception Ex)
            {
                // heredar
                throw Ex;
            }
        }

        #endregion

    }

}
