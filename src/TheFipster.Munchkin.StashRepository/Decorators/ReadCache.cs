using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Decorators
{
    /// <summary>
    /// Decorator for a Repository Reader Component.
    /// </summary>
    /// <typeparam name="TEntity">Repository Data Model</typeparam>
    public class ReadCache<TEntity> : IRead<TEntity>
    {
        private readonly IRead<TEntity> component;

        private static IEnumerable<TEntity> allCache;
        private static Dictionary<string, TEntity> oneCache;

        public ReadCache(IRead<TEntity> reader)
        {
            component = reader;
            oneCache = new Dictionary<string, TEntity>();
        }

        public IEnumerable<TEntity> FindAll()
        {
            if (allCache == null)
                allCache = component.FindAll();

            return allCache;
        }

        public TEntity FindOne(string identifier)
        {
            if (oneCache.ContainsKey(identifier))
                return fromCache(identifier);

            return fromReader(identifier);
        }

        private TEntity fromReader(string identifier)
        {
            var entity = component.FindOne(identifier);
            oneCache.Add(identifier, entity);
            return entity;
        }

        private TEntity fromCache(string identifier) =>
            oneCache[identifier];

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter) =>
            component.Find(filter);
    }
}
