using LiteDB;
using TheFipster.Munchkin.StashDomain;

namespace TheFipster.Munchkin.StashDatabase
{
    public class Context : IContext
    {
        private readonly LiteDatabase _database;

        public Context(string filepath)
        {
            _database = new LiteDatabase(filepath);

            BsonMapper.Global.Entity<Monster>().Id(monster => monster.Name);
            BsonMapper.Global.Entity<Curse>().Id(monster => monster.Name);
            BsonMapper.Global.Entity<Dungeon>().Id(monster => monster.Name);
        }

        public void Dispose() =>
            _database.Dispose();

        public LiteCollection<TEntity> GetCollection<TEntity>() =>
            _database.GetCollection<TEntity>();
    }
}
