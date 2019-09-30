using Microsoft.Extensions.Caching.Memory;
using System;

namespace TheFipster.Munchkin.Polling
{
    public class PollService<TKey, TValue> : IPollService<TKey, TValue>
    {
        private MemoryCacheEntryOptions _entryOptions;
        private MemoryCache _cache;

        public PollService(int size, TimeSpan expiration, TimeSpan scanFrequency)
        {
            _entryOptions = new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetAbsoluteExpiration(expiration);

            _cache = new MemoryCache(new MemoryCacheOptions
            {
                ExpirationScanFrequency = scanFrequency,
                SizeLimit = size
            });
        }

        public PollRequest<TValue> StartRequest(TKey requestId)
        {
            if (_cache.TryGetValue<PollRequest<TValue>>(requestId, out var request))
                return request;

            request = new PollRequest<TValue>();
            _cache.Set(requestId, request, _entryOptions);
            return request;
        }

        public bool CheckRequest(TKey requestId)
        {
            if (_cache.TryGetValue<PollRequest<TValue>>(requestId, out var _))
                return true;

            return false;
        }

        public void FinishRequest(TKey requestId, TValue requestPayload)
        {
            if (_cache.TryGetValue<PollRequest<TValue>>(requestId, out var request))
            {
                request.Notify(requestPayload);
                _cache.Remove(requestId);
            }
        }
    }
}
