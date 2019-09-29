using System;

namespace TheFipster.Munchkin.GameDomain.Messages
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
