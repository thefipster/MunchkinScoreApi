using LiteDB;

namespace TheFipster.Munchkin.GameStorage.LiteDb
{
    public class Repository<T> : IRepository<T>
    {
        private const string Filename = "games.litedb";

        private readonly LiteDatabase _db;

        public Repository() =>
            _db = new LiteDatabase(Filename);

        public LiteCollection<T> GetCollection() =>
            _db.GetCollection<T>();
    }
}
