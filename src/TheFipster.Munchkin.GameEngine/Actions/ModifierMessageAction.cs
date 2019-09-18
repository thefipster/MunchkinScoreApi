using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public abstract class ModifierMessageAction : MessageAction
    {
        public ModifierMessageAction(GameMessage message, Game game)
            : base(message, game) { }

        public new GameModifierMessage Message => (GameModifierMessage)base.Message;

        protected bool IsAddMessage => Message.Modifier == Modifier.Add;
        protected bool IsRemoveMessage => Message.Modifier == Modifier.Remove;
    }
}
