using System;
using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameEvents
{
    public class DeathMessage : GameMessage
    {
        public static DeathMessage Create(int sequence, Guid playerId)
        {
            return new DeathMessage
            {
                Sequence = sequence,
                PlayerId = playerId
            };
        }

        public Guid PlayerId { get; set; }
    }
}
