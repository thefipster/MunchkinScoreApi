using LiteDB;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TheFipster.Munchkin.StashRepository.Abstractions;
using TheFipster.Munchkin.StashRepository.Context;

namespace TheFipster.Munchkin.StashRepository
{
    public class Reader<TEntity> : IRead<TEntity>
    {
        private readonly ILogger<Reader<TEntity>> _logger;
        private readonly LiteCollection<TEntity> _collection;

        public Reader(IDatabase database, ILogger<Reader<TEntity>> logger)
        {
            _logger = logger;
            _collection = database.GetCollection<TEntity>();
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
