using Aplicacion.CommandHandlers;
using Aplicacion.Services.Comandos;
using Aplicacion.Services.Validaciones;
using Aplicacion.Validators;
using Dominio.Service;
using Dominio.Service.Recibos;
using Infraestructura.Service;
using Infraestructura.Service.Permisos;

using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DependencyInjection
{
    public static class PermisosExtension
    {
        public static void AddPermisos(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssemblyOf<ReciboPDF>().AddClasses(classes => classes.AssignableTo<IPermisoRecibo>()).AsImplementedInterfaces().WithTransientLifetime());
            services.AddTransient<IQrHelper, QrHelper>();
            services.AddTransient<IPdfHelper, PdfHelper>();


        }
    }

}
