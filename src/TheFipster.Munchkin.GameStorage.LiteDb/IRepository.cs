using LiteDB;

namespace TheFipster.Munchkin.GameStorage.LiteDb
{
    public interface IRepository<T>
    {
        LiteCollection<T> GetCollection();
    }
}
