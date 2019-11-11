using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace TheFipster.Munchkin.Configuration
{
    public static class IConfigurationExtension
    {
        public static List<string> GetArray(this IConfiguration config, string key) => config
            .GetSection(key)
            .AsEnumerable()
            .Where(section => !string.IsNullOrWhiteSpace(section.Value))
            .Select(section => section.Value)
            .ToList();

        public static string GetAudience(this IConfiguration config) => config["ApiAudience"];
        public static string GetAuthority(this IConfiguration config) => config["AuthorityUrl"];
    }
}
