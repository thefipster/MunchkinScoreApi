using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.Persistance
{
    public interface IGameStore
    {
        void Upsert(Game state);

        Game Get(Guid gameId);
    }
}
