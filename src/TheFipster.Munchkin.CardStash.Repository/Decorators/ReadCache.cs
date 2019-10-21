using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheFipster.Munchkin.CardStash.Repository.Abstractions;

namespace TheFipster.Munchkin.CardStash.Repository.Decorators
{
    /// <summary>
    /// Decorator for a Repository Reader Component.
    /// </summary>
    /// <typeparam name="Card">Repository Data Model</typeparam>
    public class ReadCache<Card> : IRead<Card>
    {
        private readonly IRead<Card> component;

        private static IEnumerable<Card> allCache;
        private static Dictionary<string, Card> oneCache;

        public ReadCache(IRead<Card> reader)
        {
            component = reader;
            oneCache = new Dictionary<string, Card>();
        }

        public IEnumerable<Card> FindAll()
        {
            if (allCache == null)
                allCache = component.FindAll();

            return allCache;
        }

        public Card FindOne(string identifier)
        {
            if (oneCache.ContainsKey(identifier))
                return fromCache(identifier);

            return fromReader(identifier);
        }

        private Card fromReader(string identifier)
        {
            var entity = component.FindOne(identifier);
            oneCache.Add(identifier, entity);
            return entity;
        }

        private Card fromCache(string identifier) =>
            oneCache[identifier];

        public IEnumerable<Card> Find(Expression<Func<Card, bool>> filter) =>
            component.Find(filter);
    }
}
