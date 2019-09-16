using System;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.Persistance
{
    public interface IProtocolStore
    {
        void AddMessage(Guid gameId, GameMessage message);

        IEnumerable<GameMessage> Get(Guid gameId);
    }
}
