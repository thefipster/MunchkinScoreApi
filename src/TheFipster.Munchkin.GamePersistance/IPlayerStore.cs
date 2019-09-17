using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GamePersistance
{
    public interface IPlayerStore
    {
        void Add(GameMaster gameMaster);
        void Add(Player player, Guid gameMasterId);
        void Get(string email);
        void Get(Guid gameMasterId);
        void GetPool(Guid gameMasterId);
    }
}
