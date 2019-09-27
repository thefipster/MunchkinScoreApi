using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class BonusMessage : GameMessage
    {
        public static BonusMessage Create(int sequence, Guid playerId, int delta, string reason = null)
        {
            return new BonusMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Delta = delta,
                Reason = reason
            };
        }
        
        public Guid PlayerId { get; set; }
        public int Delta { get; set; }
        public string Reason { get; set; }
    }
}
