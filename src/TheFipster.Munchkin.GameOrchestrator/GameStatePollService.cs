using Microsoft.Extensions.Caching.Memory;
using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameOrchestrator
{
    public class GameStatePollService : IGameStatePollService
    {
        private MemoryCache _cache;

        public GameStatePollService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromSeconds(30),
                SizeLimit = 100
            });
        }

        public GameStatePollRequest GetScoreRequest(Guid gameId)
        {
            if (_cache.TryGetValue<GameStatePollRequest>(gameId, out var request))
                return request;

            request = new GameStatePollRequest();
            _cache.Set(gameId, request, EntryOptions);
            return request;
        }

        public void FinishRequest(Guid gameId, Scoreboard score)
        {
            if (!_cache.TryGetValue<GameStatePollRequest>(gameId, out var request))
                throw new UnknownGameException();

            request.Notify(score);
            _cache.Remove(gameId);
        }

        private MemoryCacheEntryOptions EntryOptions =>
            new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(CacheItemPriority.High)
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));
    }
}
