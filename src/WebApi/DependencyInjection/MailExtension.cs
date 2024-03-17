using Aplicacion.CommandHandlers;
using Aplicacion.Mappers;
using AutoMapper;
using Dominio.Repositories;
using Dominio.Service;
using Infraestructura.Service.Correo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

namespace WebApi.DependencyInjection
{
    public static class MailExtension
    {
        public static void AddMail(this IServiceCollection services)
        {
            services.AddTransient<ICorreoHelper, CorreoHelper>();
            services.AddTransient<IMailBuilder, MailBuilder>();
            services.AddScoped((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<string>("Email:Smtp:Host"),
                    Port = config.GetValue<int>("Email:Smtp:Port"),
                    Credentials = new NetworkCredential(
                            config.GetValue<string>("Email:Smtp:Username"),
                            config.GetValue<string>("Email:Smtp:Password")
                        )
                };
            });
        }
    }

}
