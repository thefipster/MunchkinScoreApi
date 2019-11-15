using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Identity.Api.Config;

namespace TheFipster.Munchkin.Identity.Api.Extensions
{
    using TheFipster.Munchkin.Api.Common;

    public static class Cors
    {
        public static void AddCors(this IServiceCollection services, IConfiguration config) => services
            .AddCors(options => options
            .AddPolicy(CorsExtension.CorsPolicyName, policy => policy
            .WithOrigins(Clients.GetCorsOrigin(config))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));
    }
}
