using System;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public interface IQuest
    {
        Guid StartJourney();

        Game AddMessage(Guid gameId, GameMessage message);
        Game AddMessages(Guid gameId, IEnumerable<GameMessage> messages);

        Game GetState(Guid gameId);
    }
}
