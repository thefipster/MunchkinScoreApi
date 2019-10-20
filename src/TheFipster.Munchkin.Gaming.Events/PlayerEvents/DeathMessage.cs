using System;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
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
