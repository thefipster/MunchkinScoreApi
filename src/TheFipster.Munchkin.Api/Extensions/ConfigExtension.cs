using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TheFipster.Munchkin.Api.Extensions
{
    public static class ConfigExtension
    {
        public static IWebHostBuilder UseConfig(this IWebHostBuilder webHostBuilder, string[] args)
        {
            webHostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json");
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            });

            return webHostBuilder;
        }
    }
}
