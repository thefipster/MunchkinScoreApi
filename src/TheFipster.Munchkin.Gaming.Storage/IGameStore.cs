using System;
using TheFipster.Munchkin.Gaming.Domain;

namespace TheFipster.Munchkin.Gaming.Storage
{
    public interface IGameStore
    {
        void Upsert(Game game);

        Game Get(Guid gameId);
    }
}
