using System;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class DoorMessage : GameMessage
    {
        public static DoorMessage Create(int sequence, Guid playerId)
        {
            return new DoorMessage
            {
                Sequence = sequence,
                PlayerId = playerId
            };
        }

        public Guid PlayerId { get; set; }
    }
}
