using LiteDB;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.GameStorageLite
{
    public class MonsterStore : IMonsterStore
    {
        private readonly LiteCollection<Monster> _collection;

        public MonsterStore(IRepository<Monster> repository)
        {
            BsonMapper.Global.Entity<Monster>().Id(monster => monster.Name);

            var repo = repository;
            _collection = repo.GetCollection();
        }

        public void Add(Monster gameMaster) =>
            _collection.Insert(gameMaster);

        public Monster Get(string name) =>
            _collection.Find(x => x.Name == name).FirstOrDefault()
            ?? throw new UnknownMonsterException($"Monster '{name}' is unknown.");

        public IEnumerable<Monster> Get() => _collection
            .FindAll()
            .OrderBy(x => x.Name);

        public Monster Upsert(Monster monster)
        {
            var storedMonster = _collection.Find(x => x.Name == monster.Name).FirstOrDefault();
            if (storedMonster == null)
            {
                Add(monster);
                return monster;
            }

            syncMonsters(monster, storedMonster);
            _collection.Upsert(storedMonster);
            return storedMonster;
        }

        private void syncMonsters(Monster newMonster, Monster oldMonster)
        {
            if (newMonster.Level.HasValue)
                oldMonster.Level = newMonster.Level;

            if (newMonster.Treasures.HasValue)
                oldMonster.Treasures = newMonster.Treasures;

            if (newMonster.LevelGain.HasValue)
                oldMonster.LevelGain = newMonster.LevelGain;
        }
    }
}
