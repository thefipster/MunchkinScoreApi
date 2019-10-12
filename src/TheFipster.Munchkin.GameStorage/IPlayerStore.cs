using System;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameStorage
{
    public interface IPlayerStore
    {
        void Add(GameMaster gameMaster);
        GameMaster GetByExternalId(string externalId);
        GameMaster Get(Guid gameMasterId);
        GameMaster Register(string name, string externalId, string email = null);
    }
}
