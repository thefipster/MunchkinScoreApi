using System.Collections.Generic;
using TheFipster.Munchkin.Gaming.Domain;

namespace TheFipster.Munchkin.Gaming.Storage
{
    public interface IMonsterStore
    {
        Monster Get(string monsterName);
        IEnumerable<Monster> Get();
        Monster Upsert(Monster monster);
    }
}
