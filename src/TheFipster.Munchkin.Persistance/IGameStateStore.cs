using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.Persistance
{
    public interface IGameStateStore
    {
        void Add(Guid gameId, GameState state);

        GameState Get(Guid gameId);
    }
}
