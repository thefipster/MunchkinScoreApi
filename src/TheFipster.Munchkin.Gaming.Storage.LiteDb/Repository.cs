using LiteDB;

namespace TheFipster.Munchkin.Gaming.Storage.LiteDb
{
    public class Repository<T> : IRepository<T>
    {
        public const string Filename = "games.litedb";

        private readonly LiteDatabase _db;

        public Repository() =>
            _db = new LiteDatabase(Filename);

        public void Dispose() => _db.Dispose();

        public LiteCollection<T> GetCollection() =>
            _db.GetCollection<T>();
    }
}
