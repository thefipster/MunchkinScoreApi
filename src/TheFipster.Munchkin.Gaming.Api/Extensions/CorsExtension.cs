using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Configuration;

namespace TheFipster.Munchkin.Gaming.Api.Extensions
{
    public static class CorsExtensions
    {
        private const string CorsPolicyName = "default";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration.GetArray("AllowedOrigins");
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy => policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(origins.ToArray())
                    .AllowCredentials());
            });

            return services;
        }

        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicyName);
            return app;
        }
    }
}
