using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class LevelMessage : GameMessage
    {
        public int Delta { get; set; }
        public Guid PlayerId { get; set; }
        public string Reason { get; set; }
        public Modifier Modifier { get; set; }
    }
}
