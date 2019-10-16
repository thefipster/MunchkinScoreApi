using System.Collections.Generic;

namespace TheFipster.Munchkin.StashRepository.Abstractions
{
    public interface IRead<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetOne(string name);
    }
}
