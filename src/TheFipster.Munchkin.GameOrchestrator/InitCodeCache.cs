using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public class InitCodeCache : IInitializationCache
    {
        private MemoryCache _cache;

        public InitCodeCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromSeconds(30),
                SizeLimit = 100
            });
        }

        public bool CheckInitCode(string gameInitId)
        {
            return _cache.TryGetValue(gameInitId, out string value);
        }

        public string GenerateInitCode()
        {
            var initCode = generteInitCode();
            _cache.Set(initCode, string.Empty, EntryOptions);
            return initCode;
        }

        private MemoryCacheEntryOptions EntryOptions =>
            new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(CacheItemPriority.High)
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

        private string generteInitCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
