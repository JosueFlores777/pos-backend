using Dominio.Service;

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Mail;

namespace Infraestructura.Service.Correo
{
    public class CorreoHelper : ICorreoHelper
    {

        private readonly string rutaHtmlaprobacionRecibo = "ReciboAprobada.html";


        private readonly IMailBuilder mailBuilder;
        private readonly IPdfHelper pdfHelper;
        private readonly IConfiguration configuration;

        public CorreoHelper(IMailBuilder mailBuilder, IPdfHelper pdfHelper, IConfiguration configuration)
        {
            this.mailBuilder = mailBuilder;
            this.pdfHelper = pdfHelper;
            this.configuration = configuration;
        }


        public void EnviarPermisoCorreo(Dominio.Models.Recibo recibo)
        {
            var html = mailBuilder.GetHtml(rutaHtmlaprobacionRecibo);
            enviarCorreoConPdf(html, recibo);
        }
        private void enviarCorreoConPdf(string html, Dominio.Models.Recibo recibo) {
            html = mailBuilder.ConfigurarTextos(html, new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("ReciboID", recibo.Id.ToString()),


                new KeyValuePair<string, string>("LogoDireccion",configuration.GetValue<string>("AppSettings:ServerPortal")+"/assets/img/senasa.png"),
           });
            var alternative = mailBuilder.GetAlternateView(html);

            var myfile = pdfHelper.Traerpermiso(recibo);
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            System.Net.Mail.Attachment attach = new Attachment(myfile, ct);
            attach.ContentDisposition.FileName = "Recibo.pdf";

            mailBuilder.Enviar("Notificación", recibo.Importador.Correo, alternative, new List<Attachment> { attach });
            
            myfile.Close();
        }


    }
}
