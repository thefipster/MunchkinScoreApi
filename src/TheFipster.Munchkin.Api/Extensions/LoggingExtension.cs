using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace TheFipster.Munchkin.Api.Extensions
{
    public static class LoggingExtension
    {
        public static IWebHostBuilder UseSerilog(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom
                    .Configuration(hostingContext.Configuration));

            return webHostBuilder;
        }
    }
}
