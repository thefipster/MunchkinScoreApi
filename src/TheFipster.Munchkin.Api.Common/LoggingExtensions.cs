using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace TheFipster.Munchkin.Api.Common
{
    public static class LoggingExtensions
    {
        public static void UseLogging(this IWebHostBuilder hostBuilder) => hostBuilder
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration));
    }
}
