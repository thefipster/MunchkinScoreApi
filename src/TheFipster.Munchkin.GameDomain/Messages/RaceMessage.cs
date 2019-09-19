using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class RaceMessage : GameModifierMessage
    {
        public RaceMessage() { }

        public RaceMessage(Guid gameId, Guid playerId, string race, Modifier modifier)
            : base(gameId, modifier)
        {
            PlayerId = playerId;
            Race = race;
        }

        public Guid PlayerId { get; set; }
        public string Race { get; set; }
    }
}
