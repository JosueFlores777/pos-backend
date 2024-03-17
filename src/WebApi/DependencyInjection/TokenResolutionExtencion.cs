using Aplicacion.Services.Validaciones;
using Dominio.Service;
using Infraestructura.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.DependencyInjection
{
    public static class TokenResolutionExtencion
    {
        public static void AddTokenConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IAutenticationHelper, AutenticationHelper>();
            services.AddTransient<ITokenService, TokenService>();
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
        }
    }
}
