using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TheFipster.Munchkin.Configuration
{
    using System.Runtime.CompilerServices;

    public static class AspCoreAppSettingsExtension
    {
        private const string AppSettingsTemplate = "appsettings{0}.json";

        public static void UseAppSettings(this IWebHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile(DefaultAppSettings);
                config.AddJsonFile(GetAppSettingsByEnvironment(hostingContext.HostingEnvironment), true);
            });
        }

        private static string DefaultAppSettings => string.Format(AppSettingsTemplate, string.Empty);

        private static string GetAppSettingsByEnvironment(IWebHostEnvironment env)
            => string.Format(AppSettingsTemplate, $".{env.EnvironmentName}");
    }
}
