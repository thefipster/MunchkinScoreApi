using LiteDB;
using Microsoft.Extensions.Logging;
using TheFipster.Munchkin.CardStash.Repository.Abstractions;

namespace TheFipster.Munchkin.CardStash.Repository
{
    public class Writer<TEntity> : ISave<TEntity>
    {
        private readonly ILogger<Writer<TEntity>> _logger;
        private readonly LiteCollection<TEntity> _collection;

        public Writer(IDatabase database, ILogger<Writer<TEntity>> logger)
        {
            _logger = logger;
            _collection = database.GetCollection<TEntity>();
        }

        public void Save(TEntity entity)
        {
            _collection.Upsert(entity);
            _logger.LogInformation("Save {EntityName} completed", entity.GetType().Name);
        }
    }
}
