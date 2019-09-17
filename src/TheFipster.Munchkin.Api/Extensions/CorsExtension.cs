using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TheFipster.Munchkin.Api.Extensions
{
    public static class CorsExtensions
    {
        private const string CorsPolicyName = "default";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration
                .GetSection("AllowedOrigins")
                .AsEnumerable()
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value)
                .ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy => policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(origins)
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
