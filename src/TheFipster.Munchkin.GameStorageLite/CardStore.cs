using LiteDB;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.GameStorageLite
{
    public class CardStore : ICardStore
    {
        private readonly LiteCollection<CardCollection> _collection;

        public CardStore(IRepository<CardCollection> repository)
        {
            var repo = repository;
            _collection = repo.GetCollection();
        }

        public List<string> Get(string cardCollectionName) =>
            _collection.Find(x => x.Id == cardCollectionName).FirstOrDefault()?.Cards;

        public void Sync(string cardCollectionName, List<string> cards)
        {
            var storedCards = Get(cardCollectionName) ?? new List<string>();
            storedCards.AddRange(cards);
            storedCards = storedCards.Distinct().ToList();
            var collection = new CardCollection(cardCollectionName, storedCards);
            _collection.Upsert(collection);
        }
    }
}
