using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public abstract class MessageSwitchAction : MessageAction
    {
        public MessageSwitchAction(GameMessage message, Game game)
            : base(message, game) { }

        public new GameSwitchMessage Message => (GameSwitchMessage)base.Message;

        protected bool IsAddMessage => Message.Add.Any();
        protected bool IsRemoveMessage => Message.Remove.Any();
    }
}
