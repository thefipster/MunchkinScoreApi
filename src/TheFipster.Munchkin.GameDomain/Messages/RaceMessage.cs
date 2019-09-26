using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class RaceMessage : GameSwitchMessage<string>
    {
        public Guid PlayerId { get; set; }
    }
}
