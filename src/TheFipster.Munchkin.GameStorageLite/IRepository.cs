using LiteDB;

namespace TheFipster.Munchkin.GameStorageLite
{
    public interface IRepository<T>
    {
        LiteCollection<T> GetCollection();
    }
}
