using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public abstract class GameModifierAction : GameModifierMessage
    {
        public void ApplyTo(GameState state)
        {
            switch (Modifier)
            {
                case Modifier.Add:
                    Do().Invoke(state);
                    break;
                case Modifier.Remove:
                    Undo().Invoke(state);
                    break;
                default:
                    throw new InvalidModifierException();
            }
        }

        public void RevertFrom(GameState state)
        {
            switch (Modifier)
            {
                case Modifier.Add:
                    Undo().Invoke(state);
                    break;
                case Modifier.Remove:
                    Do().Invoke(state);
                    break;
                default:
                    throw new InvalidModifierException();
            }
        }

        public abstract Action<GameState> Do();

        public abstract Action<GameState> Undo();
    }
}
