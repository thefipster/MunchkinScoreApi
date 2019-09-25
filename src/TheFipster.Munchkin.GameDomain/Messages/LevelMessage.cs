using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class LevelMessage : GameMessage
    {
        public LevelMessage() { }

        public LevelMessage(Guid playerId, int levelDelta, Modifier modifier)
            : this(playerId, levelDelta, modifier, null) { }

        public LevelMessage(Guid playerId, int levelDelta, Modifier modifier, string reason)
        {
            PlayerId = playerId;
            Delta = levelDelta;
            Reason = reason;
            Modifier = modifier;
        }

        public int Delta { get; set; }
        public Guid PlayerId { get; set; }
        public string Reason { get; set; }
        public Modifier Modifier { get; set; }
    }
}
