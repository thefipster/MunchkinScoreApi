using LiteDB;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.GameStorageLite
{
    public class CardStore : ICardStore
    {
        private const string DungeonId = "dungeons";

        private readonly LiteCollection<CardCollection> _collection;

        public CardStore(IRepository<CardCollection> repository)
        {
            var repo = repository;
            _collection = repo.GetCollection();
        }

        public List<string> GetDungeons() =>
            _collection.Find(x => x.Id == DungeonId).FirstOrDefault()?.Cards;

        public void SyncDungeons(List<string> dungeons)
        {
            var storedDungeons = GetDungeons() ?? new List<string>();
            dungeons.AddRange(storedDungeons);
            var collection = new CardCollection(DungeonId, dungeons.Distinct().ToList());
            _collection.Upsert(collection);
        }
    }
}
