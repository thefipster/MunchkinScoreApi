using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class RaceMessage : GameModifierMessage
    {
        public RaceMessage() { }

        public RaceMessage(Guid playerId, string race, Modifier modifier)
            : base(modifier)
        {
            PlayerId = playerId;
            Race = race;
        }

        public Guid PlayerId { get; set; }
        public string Race { get; set; }
    }
}
