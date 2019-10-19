using LiteDB;
using TheFipster.Munchkin.StashDatabase;
using TheFipster.Munchkin.StashRepository.Abstractions;

namespace TheFipster.Munchkin.StashRepository.Components
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
