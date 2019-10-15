using System;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Decorators
{
    public class WriteEventEmitter<TEntity> : ISave<TEntity>
    {
        private readonly ISave<TEntity> writer;

        public WriteEventEmitter(ISave<TEntity> writer)
        {
            this.writer = writer;
        }

        public void Save(TEntity entity)
        {
            writer.Save(entity);
            Console.WriteLine("Emitting write event for " + entity.GetType().Name);
        }
    }
}
