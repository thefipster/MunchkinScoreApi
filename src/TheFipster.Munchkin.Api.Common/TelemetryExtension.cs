using Microsoft.Extensions.DependencyInjection;

namespace TheFipster.Munchkin.Api.Common
{
    public static class TelemetryExtension
    {
        public static void AddApplicationInsights(this IServiceCollection services) => services
           .AddApplicationInsightsTelemetry();
    }
}
