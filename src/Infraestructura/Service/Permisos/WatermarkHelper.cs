using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Service.Permisos
{
   public class WatermarkHelper
    {
        public void AddWatermark(PdfWriter pdfWriter, string mark) {
           
                PdfWriterEvents writerEvent = new PdfWriterEvents(mark);
                pdfWriter.PageEvent = writerEvent;

        }
    }
}
