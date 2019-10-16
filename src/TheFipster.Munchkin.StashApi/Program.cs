using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.Logging.Api;

namespace StashApi
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseLogging();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
