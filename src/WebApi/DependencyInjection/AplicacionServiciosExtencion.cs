
using Aplicacion.Services.Recibos;
using Microsoft.Extensions.DependencyInjection;


namespace WebApi.DependencyInjection
{
    public static class AplicacionServiciosExtencion
    {
        public static void AddAplicacionServices(this IServiceCollection services)
        {
            services.AddTransient<ICarga, UsuarioService>();

        }
    }
}
