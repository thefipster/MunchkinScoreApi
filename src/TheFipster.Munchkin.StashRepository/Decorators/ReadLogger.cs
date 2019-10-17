using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Decorators
{
    public class ReadLogger<TEntity> : IRead<TEntity>
    {
        private const string EntryMessage = "{MethodName} method for entity {EntityName} executing...";
        private const string ExitMessage = "{MethodName} method for entity {EntityName} executed";

        private readonly IRead<TEntity> component;
        private readonly ILogger<ReadLogger<TEntity>> log;

        public ReadLogger(IRead<TEntity> reader, ILogger<ReadLogger<TEntity>> logger)
        {
            component = reader;
            log = logger;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            log.LogTrace(EntryMessage, nameof(Find), typeof(TEntity).Name);
            var entities = component.Find(filter);
            log.LogTrace(ExitMessage, nameof(Find), typeof(TEntity).Name);
            return entities;
        }

        public IEnumerable<TEntity> FindAll()
        {
            log.LogTrace(EntryMessage, nameof(FindAll), typeof(TEntity).Name);
            var entities = component.FindAll();
            log.LogTrace(ExitMessage, nameof(FindAll), typeof(TEntity).Name);
            return entities;
        }

        public TEntity FindOne(string identifier)
        {
            log.LogTrace(EntryMessage, nameof(FindOne), typeof(TEntity).Name);
            var entities = component.FindOne(identifier);
            log.LogTrace(ExitMessage, nameof(FindOne), typeof(TEntity).Name);
            return entities;
        }
    }
}
