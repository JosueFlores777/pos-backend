using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infraestructura.Service;

namespace WebApi.DependencyInjection
{
    public static class RedisExtencion
    {
        public static void AddRedis(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings").Get<AppSettings>();
      
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = appSettingsSection.ConnectionStringsRedis;
                options.InstanceName = "";
            });
        }
}
}
