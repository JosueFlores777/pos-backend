using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Dominio.Models;
using Microsoft.AspNetCore.Http;

namespace Infraestructura.Service.ApiKey
{
    public static class Extensions
    {

        public static ClaimsIdentity GetClaimsIdentity(this IHttpContextAccessor httpContextAccessor)
        {
            return  (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity;
            
        }
        public static bool isApiKeyAuthentication(this IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.GetClaimsIdentity();
            return identity.AuthenticationType == "API_KEY";

        }
        public static string GetEmailClientFromClaims(this IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.GetClaimsIdentity();
            var claim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            return claim.Value;

        }
        public static string GetTokenApiKey(this IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.GetClaimsIdentity();
            var claim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            return claim.Value;

        }
        
        public static string GetNameIdentifierClaim(this IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.GetClaimsIdentity();
            var claim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return claim.Value;

        }

    
        public static List<Permiso> GetPermissionFromClaims(this IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.GetClaimsIdentity();
            return identity.Claims.Where(x => x.Type == "permisos").Select(x => new Permiso() { Codigo = x.Value })
                     .ToList();
        }
    }
}