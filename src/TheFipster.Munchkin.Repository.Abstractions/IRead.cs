using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TheFipster.Munchkin.StashRepository.Abstractions
{
    public interface IRead<TEntity>
    {
        IEnumerable<TEntity> FindAll();

        TEntity FindOne(string identifier);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);
    }
}
