using System;

namespace TheFipster.Munchkin.GameDomain.Messages
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
