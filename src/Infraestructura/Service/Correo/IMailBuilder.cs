
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Infraestructura.Service.Correo
{
    public interface IMailBuilder
    {
        string GetHtml(string name);
        string ConfigurarTextos(string html, List<KeyValuePair<string, string>> textos);
        AlternateView GetAlternateView(string html);
        void ConfigurarImagenes(AlternateView htmlView, List<KeyValuePair<string, string>> imagenes);
        void Enviar(string asunto, string destino, AlternateView htmlView, IList<Attachment> adjuntos);
    }

    public class MailBuilder : IMailBuilder
    {
        private readonly SmtpClient smtpClient;
        private readonly IConfiguration configuration;
        public MailBuilder(SmtpClient smtpClient, IConfiguration configuration)
        {
            this.smtpClient = smtpClient;
            this.configuration = configuration;
        }

        public void Enviar(string asunto, string destino, AlternateView htmlView, IList<Attachment> adjuntos)
        {
            try
            {
                MailMessage message = new MailMessage
                {

                    From = new MailAddress(configuration.GetValue<string>("Email:Smtp:Username"), "SENASA")
                };
                message.To.Add(destino);
                message.Subject = asunto;
                message.AlternateViews.Add(htmlView);
                foreach (var adjunto in adjuntos)
                {
                    message.Attachments.Add(adjunto);
                }
                message.IsBodyHtml = true;
                smtpClient.Send(message);
            }
            catch 
            {
                //throw new HttpException(422,"No se ha podido enviar el correo, Contacta al departamento de IT");
            }
          

        }

        public AlternateView GetAlternateView(string html)
        {
            return AlternateView.CreateAlternateViewFromString(html, null, "text/html");
        }
        public string ConfigurarTextos(string html, List<KeyValuePair<string, string>> textos)
        {
            foreach (var texto in textos)
            {
                html = html.Replace(texto.Key, texto.Value);
            }
            var ruta = configuration.GetValue<string>("AppSettings:DireccionPortal");
            html = html.Replace("DireccionPortalId", ruta);
            return html;
        }

        public string GetHtml(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(name)); ;

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public void ConfigurarImagenes(AlternateView htmlView, List<KeyValuePair<string, string>> imagenes)
        {

            foreach (var imagen in imagenes)
            {
                htmlView.LinkedResources.Add(new LinkedResource(GetStream(imagen.Value))
                {
                    ContentId = imagen.Key
                });

            }
        }


        public Stream GetStream(string name)
        {

            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(name));

            return assembly.GetManifestResourceStream(resourcePath);
        }
    }
}
