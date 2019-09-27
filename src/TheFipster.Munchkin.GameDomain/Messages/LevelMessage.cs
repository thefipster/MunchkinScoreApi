using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class LevelMessage : GameMessage
    {
        public static LevelMessage Create(int sequence, Guid playerId, int delta, string reason = null)
        {
            return new LevelMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Delta = delta,
                Reason = reason
            };
        }

        public int Delta { get; set; }
        public Guid PlayerId { get; set; }
        public string Reason { get; set; }
    }
}
