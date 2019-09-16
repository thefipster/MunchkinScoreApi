using System;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public interface IQuest
    {
        Guid StartJourney();

        Scoreboard AddMessage(GameMessage message);
    }
}
