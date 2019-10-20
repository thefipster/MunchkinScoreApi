using System.Collections.Generic;

namespace TheFipster.Munchkin.Gaming.Storage
{
    public interface ICardStore
    {
        List<string> Get(string cardCollectionName);
        void Sync(string cardCollectionName, List<string> cards);
    }
}
