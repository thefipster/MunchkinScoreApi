using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheFipster.Munchkin.StashDatabase;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Components
{
    public class LiteReader<TEntity> : IRead<TEntity>
    {
        private readonly LiteCollection<TEntity> collection;

        public LiteReader(IContext context) =>
            collection = context.GetCollection<TEntity>();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter) =>
            collection.Find(filter);

        public IEnumerable<TEntity> FindAll() =>
            collection.FindAll();

        public TEntity FindOne(string identifier) =>
            collection.FindById(new BsonValue(identifier));
    }
}
