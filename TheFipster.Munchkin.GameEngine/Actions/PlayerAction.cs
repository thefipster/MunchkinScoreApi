using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class PlayerAction : PlayerMessage, IGameAction
    {
        public void ApplyTo(GameState state)
        {
            switch (Modifier)
            {
                case Modifier.Add:
                    AddPlayer(state);
                    break;
                case Modifier.Remove:
                    RemovePlayer(state);
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
                    RemovePlayer(state);
                    break;
                case Modifier.Remove:
                    AddPlayer(state);
                    break;
                default:
                    throw new InvalidModifierException();
            }
        }

        private void AddPlayer(GameState state) => state.Players.Add(Player);
        private void RemovePlayer(GameState state) => state.Players.Remove(Player);
    }
}
