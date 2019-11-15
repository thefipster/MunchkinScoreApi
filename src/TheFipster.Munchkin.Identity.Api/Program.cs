using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.Api.Common;

namespace TheFipster.Munchkin.Identity.Api
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseAppSettings();
                    webBuilder.UseLogging();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
