using System;
using System.Collections.Generic;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Decorators
{
    public class CachedReader<TEntity> : IRead<TEntity>
    {
        private IEnumerable<TEntity> allCache;
        private Dictionary<string, TEntity> oneCache;
        private IRead<TEntity> reader;

        public CachedReader(IRead<TEntity> reader)
        {
            this.reader = reader;
            this.oneCache = new Dictionary<string, TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            if (allCache == null)
                allCache = reader.GetAll();
            else
                Console.WriteLine("Reading all " + typeof(TEntity).Name + "s from cache");

            return allCache;
        }

        public TEntity GetOne(string identifier)
        {
            if (oneCache.ContainsKey(identifier))
                return fromCache(identifier);

            return fromReader(identifier);
        }

        private TEntity fromReader(string identifier)
        {
            var entity = reader.GetOne(identifier);
            oneCache.Add(identifier, entity);
            return entity;
        }

        private TEntity fromCache(string identifier)
        {
            Console.WriteLine("Reading the " + typeof(TEntity).Name + " with id '" + identifier + "' from cache");
            return oneCache[identifier];
        }
    }
}
