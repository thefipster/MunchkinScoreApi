using LiteDB;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TheFipster.Munchkin.StashDatabase;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Components
{
    public class LiteReader<TEntity> : IRead<TEntity>
    {
        private readonly ILogger<LiteReader<TEntity>> _logger;
        private readonly LiteCollection<TEntity> _collection;

        public LiteReader(IContext context, ILogger<LiteReader<TEntity>> logger)
        {
            _logger = logger;
            _collection = context.GetCollection<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            _logger.LogInformation("Reading all {EntityName}s", typeof(TEntity).Name);
            return _collection.FindAll();
        }

        public TEntity GetOne(string identifier)
        {
            _logger.LogInformation("Reading the {EntityName} with id '{EntityId}'", typeof(TEntity).Name, identifier);
            return _collection.FindById(new BsonValue(identifier));
        }
    }
}
