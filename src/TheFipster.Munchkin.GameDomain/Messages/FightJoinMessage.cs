using System;

namespace TheFipster.Munchkin.GameDomain.Messages
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
