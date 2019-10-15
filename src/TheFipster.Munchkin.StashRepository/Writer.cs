using System;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository
{
    public class Writer<TEntity> : ISave<TEntity>
    {
        public void Save(TEntity entity)
        {
            Console.WriteLine("Save " + entity.GetType().Name + " completed");
        }
    }
}
