using LiteDB;

namespace TheFipster.Munchkin.GameStorageLite
{
    public class Repository<T> : IRepository<T>
    {
        private const string Filename = "munchkin_store.litedb";

        private readonly LiteDatabase _db;

        public Repository()
        {
            _db = new LiteDatabase(Filename);
        }

        public LiteCollection<T> GetCollection() =>
            _db.GetCollection<T>();
    }
}
