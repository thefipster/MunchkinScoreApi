using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace TheFipster.Munchkin.Logging.Api
{
    public static class LoggingExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder hostBuilder)
        {
            hostBuilder
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom
                .Configuration(hostingContext.Configuration));

            return hostBuilder;
        }
    }
}
