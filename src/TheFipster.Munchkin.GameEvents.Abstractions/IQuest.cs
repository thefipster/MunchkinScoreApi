using System;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameAbstractions
{
    public interface IQuest
    {
        Guid StartJourney();

        Game AddMessage(Guid gameId, GameMessage message);
        Game AddMessages(Guid gameId, IEnumerable<GameMessage> messages);

        Game GetState(Guid gameId);

        string GenerateInitCode();
    }
}
