using LiteDB;
using TheFipster.Munchkin.StashDatabase;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Components
{
    public class LiteWriter<TEntity> : IWrite<TEntity>
    {
        private readonly LiteCollection<TEntity> collection;

        public LiteWriter(IContext context) =>
            collection = context.GetCollection<TEntity>();

        public void Write(TEntity entity) =>
            collection.Upsert(entity);
    }
}
