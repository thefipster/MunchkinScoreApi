﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SeedExtensions
    {
        public static void SynchronizeSeedData(this IHostingEnvironment env, IConfiguration config, ICardStore cardStore)
        {
            var dungeons = config
                .GetSection("dungeons")
                .AsEnumerable()
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value)
                .ToList();

            cardStore.SyncDungeons(dungeons);
        }
    }
}
