using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameEvents
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
