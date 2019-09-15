using System;
using TheFipster.Munchkin.GameEngine.Model;

namespace TheFipster.Munchkin.GameEngine.Messages
{
    public abstract class GameMessage
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }

        public abstract void ApplyTo(GameState state);

        public abstract void Undo(GameState state);
    }
}
