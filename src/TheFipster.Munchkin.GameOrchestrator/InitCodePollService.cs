using Microsoft.Extensions.Caching.Memory;
using System;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public class InitCodePollService : IInitCodePollService
    {
        private MemoryCache _cache;

        public InitCodePollService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromSeconds(30),
                SizeLimit = 100
            });
        }

        public InitCodePollRequest GetWaitHandle(string initCode)
        {
            if (_cache.TryGetValue<InitCodePollRequest>(initCode, out var request))
                return request;

            throw new InvalidInitCodeException();
        }

        public void FinishCodePollRequest(string initCode, Guid gameId)
        {
            if (!_cache.TryGetValue<InitCodePollRequest>(initCode, out var request))
                throw new InvalidInitCodeException();

            request.Notify(gameId);
            _cache.Remove(initCode);
        }

        public void CreateWaitHandle(string initCode)
        {
            _cache.Set(initCode, new InitCodePollRequest(), EntryOptions);
        }

        private MemoryCacheEntryOptions EntryOptions =>
            new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(CacheItemPriority.High)
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));


    }
}
