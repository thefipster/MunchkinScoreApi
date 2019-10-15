using System;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Decorators
{
    public class WriteAuditing<TEntity> : ISave<TEntity>
    {
        private readonly ISave<TEntity> decorated;

        public WriteAuditing(ISave<TEntity> decorated)
        {
            this.decorated = decorated;
        }

        public void Save(TEntity entity)
        {
            Console.WriteLine("Entity " + entity.GetType().Name + " was audited");
            decorated.Save(entity);
        }
    }
}
