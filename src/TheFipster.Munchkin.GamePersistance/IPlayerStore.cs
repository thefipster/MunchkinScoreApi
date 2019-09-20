using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GamePersistance
{
    public interface IPlayerStore
    {
        void Add(GameMaster gameMaster);
        GameMaster Get(string email);
        GameMaster Get(Guid gameMasterId);
    }
}
