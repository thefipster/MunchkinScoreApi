using LiteDB;

namespace TheFipster.Munchkin.LiteStorage
{
    public interface IRepository<T>
    {
        LiteCollection<T> GetCollection();
    }
}
