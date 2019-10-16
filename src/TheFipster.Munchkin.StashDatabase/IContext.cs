using LiteDB;

namespace TheFipster.Munchkin.StashDatabase
{
    public interface IContext
    {
        LiteCollection<TEntity> GetCollection<TEntity>();
    }
}
