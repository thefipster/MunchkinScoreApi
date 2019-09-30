using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameStorage
{
    public interface IMonsterStore
    {
        Monster Get(string monsterName);
        IEnumerable<Monster> Get();
        Monster Upsert(Monster monster);
    }
}
