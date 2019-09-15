using System;

namespace TheFipster.Munchkin.GameEngine.Messages
{
    public abstract class LevelMessage : GameMessage
    {
        public int Delta { get; set; }
        public Guid PlayerId { get; set; }
        public string Reason { get; set; }
    }
}
