using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace TheFipster.Munchkin.StashApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static List<string> GetArray(this IConfiguration config, string key) => config
            .GetSection(key)
            .AsEnumerable()
            .Where(x => !string.IsNullOrWhiteSpace(x.Value))
            .Select(x => x.Value)
            .ToList();
    }
}
