using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;
using TheFipster.Munchkin.GameApi.Extensions;

namespace TheFipster.Munchkin.GameApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args) =>
            BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) => WebHost
            .CreateDefaultBuilder(args)
            .UseConfig(args)
            .UseSerilog()
            .UseStartup<Startup>()
            .Build();
    }
}
