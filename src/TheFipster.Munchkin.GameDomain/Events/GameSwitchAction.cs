using System.Linq;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameDomain.Events
{
    public abstract class GameSwitchAction<T> : GameAction
    {
        public GameSwitchAction(GameMessage message, Game game)
            : base(message, game) { }

        public new GameSwitchMessage<T> Message => (GameSwitchMessage<T>)base.Message;

        protected bool IsAddMessage => Message.Add != null && Message.Add.Any();
        protected bool IsRemoveMessage => Message.Remove != null && Message.Remove.Any();
    }
}
