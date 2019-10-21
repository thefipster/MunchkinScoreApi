using LiteDB;
using TheFipster.Munchkin.CardStash.Database;
using TheFipster.Munchkin.CardStash.Repository.Abstractions;

namespace TheFipster.Munchkin.CardStash.Repository.Components
{
    public class LiteWriter<Card> : IWrite<Card>
    {
        private readonly LiteCollection<Card> collection;

        public LiteWriter(IContext context) =>
            collection = context.GetCollection<Card>();

        public void Write(Card entity) =>
            collection.Upsert(entity);
    }
}
