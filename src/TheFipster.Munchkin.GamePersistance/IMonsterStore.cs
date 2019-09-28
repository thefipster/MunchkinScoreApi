using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GamePersistance
{
    public interface IMonsterStore
    {
        Monster Get(string monsterName);
        IEnumerable<Monster> Get();
        void Add(Monster monster);
        Monster Upsert(Monster monster);
    }
}
