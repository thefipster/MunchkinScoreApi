using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GamePersistance
{
    public interface IGameStore
    {
        void Upsert(Game state);

        Game Get(Guid gameId);
    }
}
