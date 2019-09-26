using System.Collections.Generic;

namespace TheFipster.Munchkin.GamePersistance
{
    public interface ICardStore
    {
        List<string> Get(string cardCollectionName);
        void Sync(string cardCollectionName, List<string> cards);
    }
}
