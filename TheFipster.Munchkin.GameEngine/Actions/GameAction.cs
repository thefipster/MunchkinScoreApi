using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public abstract class GameAction : GameMessage, IProtocolActions
    {
        public void ApplyTo(GameState state)
        {
            Do().Invoke(state);
        }

        public void RevertFrom(GameState state)
        {
            Undo().Invoke(state);
        }

        public abstract Action<GameState> Do();
        public abstract Action<GameState> Undo();
    }
}
