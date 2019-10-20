using System;
using System.Collections.Generic;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Abstractions
{
    public interface IQuest
    {
        Guid StartJourney();

        Game AddMessage(Guid gameId, GameMessage message);
        Game AddMessages(Guid gameId, IEnumerable<GameMessage> messages);

        Game Undo(Guid gameId);

        Game GetState(Guid gameId);

        string GenerateInitCode();
    }
}
