using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class FightStartMessage : GameMessage
    {
        public static FightStartMessage Create(int sequence, Guid playerId, Monster monster)
        {
            return new FightStartMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Monster = monster,
            };
        }

        public Guid PlayerId { get; set; }
        public Monster Monster { get; set; }
        public string Reason { get; set; }
    }
}
