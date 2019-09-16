using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.Persistance;

namespace TheFipster.Munchkin.LiteStorage
{
    public class PlayerStore : IPlayerStore
    {
        public void Add(GameMaster gameMaster)
        {
            throw new NotImplementedException();
        }

        public void Add(Player player, Guid gameMasterId)
        {
            throw new NotImplementedException();
        }

        public void Get(string email)
        {
            throw new NotImplementedException();
        }

        public void Get(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public void GetPool(Guid gameMasterId)
        {
            throw new NotImplementedException();
        }
    }
}
