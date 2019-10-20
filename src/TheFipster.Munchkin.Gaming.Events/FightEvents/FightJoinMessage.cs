using System;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class FightJoinMessage : GameMessage
    {
        public static FightJoinMessage Create(int sequence, Guid playerId)
        {
            return new FightJoinMessage
            {
                Sequence = sequence,
                PlayerId = playerId
            };
        }

        public Guid PlayerId { get; set; }
    }
}
