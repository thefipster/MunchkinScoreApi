using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class GameStartMessage : GameMessage
    {
        public override void ApplyTo(GameState state)
            => state.Begin = DateTime.UtcNow;

        public override void Undo(GameState state)
            => state.Begin = null;
    }
}
