using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameStorage
{
    public interface IGameStore
    {
        void Upsert(Game game);

        Game Get(Guid gameId);
    }
}
