using LiteDB;
using Microsoft.Extensions.Logging;
using TheFipster.Munchkin.StashDatabase;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Components
{
    public class LiteWriter<TEntity> : ISave<TEntity>
    {
        private readonly ILogger<LiteWriter<TEntity>> _logger;
        private readonly LiteCollection<TEntity> _collection;

        public LiteWriter(IContext context, ILogger<LiteWriter<TEntity>> logger)
        {
            _logger = logger;
            _collection = context.GetCollection<TEntity>();
        }

        public void Save(TEntity entity)
        {
            _collection.Upsert(entity);
            _logger.LogInformation("Save {EntityName} completed", entity.GetType().Name);
        }
    }
}
