using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;
using TheFipster.Munchkin.Api.Extensions;

namespace TheFipster.Munchkin.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args) => WebHost
            .CreateDefaultBuilder(args)
            .UseConfig(args)
            .UseSerilog()
            .UseStartup<Startup>()
            .Build()
            .Run();
    }
}
