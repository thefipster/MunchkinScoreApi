using System;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public interface IQuest
    {
        Guid StartJourney();

        Scoreboard AddMessage(Guid gameId, GameMessage message);
        Scoreboard AddMessages(Guid gameId, IEnumerable<GameMessage> messages);

        Scoreboard GetState(Guid gameId);
    }
}
