using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository
{
    public class Reader<TEntity> : IRead<TEntity>
    {
        public IEnumerable<TEntity> GetAll()
        {
            Console.WriteLine("Reading all " + typeof(TEntity).Name + "s");
            return Enumerable.Empty<TEntity>();
        }

        public TEntity GetOne(string identifier)
        {
            Console.WriteLine("Reading the " + typeof(TEntity).Name + " with id '" + identifier + "'");
            return default;
        }
    }
}
