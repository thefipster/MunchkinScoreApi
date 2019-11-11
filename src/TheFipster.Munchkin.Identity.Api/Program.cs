﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.Configuration;
using TheFipster.Munchkin.Logging.Api;

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
                    webBuilder.UseUrls("http://localhost:5000");
                    webBuilder.UseAppSettings();
                    webBuilder.UseLogging();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
