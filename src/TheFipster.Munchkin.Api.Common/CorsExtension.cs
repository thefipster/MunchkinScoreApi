using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TheFipster.Munchkin.Api.Common
{
    public static class CorsExtension
    {
        public const string CorsPolicyName = "default";
        private const string CorsAllowedOriginSetting = "AllowedOrigins";

        public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration.GetArray(CorsAllowedOriginSetting);
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy => policy
                                                           .WithOrigins(origins.ToArray())
                                                           .AllowAnyMethod()
                                                           .AllowAnyHeader()
                                                           .AllowCredentials());
            });
        }

        public static void UseCorsPolicy(this IApplicationBuilder app) =>
            app.UseCors(CorsPolicyName);
    }
}
