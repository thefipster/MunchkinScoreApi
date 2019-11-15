using System.Collections.Generic;

namespace TheFipster.Munchkin.Api.Common
{
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public static class ConfigExtensions
    {
        private const string AppSettingsTemplate = "appsettings{0}.json";

        public static void UseAppSettings(this IWebHostBuilder hostBuilder) =>
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile(DefaultAppSettings);
                config.AddJsonFile(GetAppSettingsByEnvironment(hostingContext.HostingEnvironment), true);
            });

        private static string DefaultAppSettings 
            => string.Format(AppSettingsTemplate, string.Empty);

        private static string GetAppSettingsByEnvironment(IWebHostEnvironment env)
            => string.Format(AppSettingsTemplate, $".{env.EnvironmentName}");

        public static IEnumerable<string> GetArray(this IConfiguration config, string key)
            => config.GetSection(key)
                     .AsEnumerable()
                     .Where(section => !string.IsNullOrWhiteSpace(section.Value))
                     .Select(section => section.Value);

        public static string GetAudience(this IConfiguration config) => config["ApiAudience"];
        public static string GetAuthority(this IConfiguration config) => config["AuthorityUrl"];
    }
}
