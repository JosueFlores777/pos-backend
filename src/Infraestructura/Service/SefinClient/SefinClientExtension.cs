using Dominio.Service;
using Infraestructura.Service.SefinClient;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DependencyInjection
{
    public static class SefinClientExtension
    {
        public static void AddSefinClient(this IServiceCollection services)
        {
            services.AddTransient<ISefinClient, RpcClient>();
        }
    }
}
