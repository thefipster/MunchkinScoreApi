using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class BonusMessage : GameMessage
    {
        public BonusMessage() { }

        public BonusMessage(Guid playerId, int bonusDelta)
            : this(playerId, bonusDelta, null) { }

        public BonusMessage(Guid playerId, int bonusDelta, string reason)
        {
            PlayerId = playerId;
            Delta = bonusDelta;
            Reason = reason;
        }

        public int Delta { get; set; }
        public Guid PlayerId { get; set; }
        public string Reason { get; set; }
    }
}
