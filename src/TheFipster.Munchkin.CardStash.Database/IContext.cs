using LiteDB;

namespace TheFipster.Munchkin.CardStash.Database
{
    public interface IContext
    {
        LiteCollection<TEntity> GetCollection<TEntity>();
    }
}
