using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.Configuration;
using TheFipster.Munchkin.Logging.Api;

namespace TheFipster.Munchkin.Gaming.Api
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
                     webBuilder.UseUrls("https://localhost:5003", "http://localhost:5002");
                     webBuilder.UseAppSettings();
                     webBuilder.UseLogging();
                     webBuilder.UseStartup<Startup>();
                 });
    }
}
