using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Decorators
{
    public class CachedReader<TEntity> : IRead<TEntity>
    {
        private IEnumerable<TEntity> _allCache;
        private Dictionary<string, TEntity> _oneCache;
        private readonly ILogger<CachedReader<TEntity>> _logger;
        private IRead<TEntity> _reader;

        public CachedReader(IRead<TEntity> reader, ILogger<CachedReader<TEntity>> logger)
        {
            _logger = logger;
            _reader = reader;
            _oneCache = new Dictionary<string, TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            if (_allCache == null)
                _allCache = _reader.GetAll();
            else
                _logger.LogInformation("Reading all {EntityName}s from cache", typeof(TEntity).Name);

            return _allCache;
        }

        public TEntity GetOne(string identifier)
        {
            if (_oneCache.ContainsKey(identifier))
                return fromCache(identifier);

            return fromReader(identifier);
        }

        private TEntity fromReader(string identifier)
        {
            var entity = _reader.GetOne(identifier);
            _oneCache.Add(identifier, entity);
            return entity;
        }

        private TEntity fromCache(string identifier)
        {
            _logger.LogInformation("Reading the {EntityName} with id '{EntityName}' from cache", typeof(TEntity).Name, identifier);
            return _oneCache[identifier];
        }
    }
}
