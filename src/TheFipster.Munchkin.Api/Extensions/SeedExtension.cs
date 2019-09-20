using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace TheFipster.Munchkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SeedExtensions
    {
        public static void SynchronizeSeedData(this IHostingEnvironment env, IConfiguration config)
        {
            var dungeons = config
                .GetSection("dungeons")
                .AsEnumerable()
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value)
                .ToArray();
        }
    }
}
