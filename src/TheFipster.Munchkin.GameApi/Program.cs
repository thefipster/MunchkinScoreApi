using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TheFipster.Munchkin.GameApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Run();

        public static IWebHost CreateHostBuilder(string[] args) => WebHost
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json");
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            })
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom
                .Configuration(hostingContext.Configuration))
            .UseStartup<Startup>()
            .Build();
    }
}
