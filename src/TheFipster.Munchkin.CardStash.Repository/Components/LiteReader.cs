using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheFipster.Munchkin.CardStash.Database;
using TheFipster.Munchkin.CardStash.Repository.Abstractions;

namespace TheFipster.Munchkin.CardStash.Repository.Components
{
    public class LiteReader<Card> : IRead<Card>
    {
        private readonly LiteCollection<Card> collection;

        public LiteReader(IContext context) =>
            collection = context.GetCollection<Card>();

        public IEnumerable<Card> Find(Expression<Func<Card, bool>> filter) =>
            collection.Find(filter);

        public IEnumerable<Card> FindAll() =>
            collection.FindAll();

        public Card FindOne(string identifier) =>
            collection.FindById(new BsonValue(identifier));
    }
}
