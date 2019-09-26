using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class LevelMessage : GameMessage
    {
        public LevelMessage() { }

        public LevelMessage(Guid playerId, int levelDelta)
            : this(playerId, levelDelta, null) { }

        public LevelMessage(Guid playerId, int levelDelta, string reason)
        {
            PlayerId = playerId;
            Delta = levelDelta;
            Reason = reason;
        }

        public int Delta { get; set; }
        public Guid PlayerId { get; set; }
        public string Reason { get; set; }
    }
}
