using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TheFipster.Munchkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ConfigExtension
    {
        public static IWebHostBuilder UseConfig(this IWebHostBuilder webHostBuilder, string[] args)
        {
            webHostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json");
                config.AddJsonFile("Seed/classes.json");
                config.AddJsonFile("Seed/curses.json");
                config.AddJsonFile("Seed/dungeons.json");
                config.AddJsonFile("Seed/monsters.json");
                config.AddJsonFile("Seed/races.json");
                config.AddEnvironmentVariables();
                config.AddCommandLine(args);
            });

            return webHostBuilder;
        }

        public static List<string> GetArray(this IConfiguration config, string key)
        {
            return config
                .GetSection(key)
                .AsEnumerable()
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value)
                .ToList();
        }
    }
}
