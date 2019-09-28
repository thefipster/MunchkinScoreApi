using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class FightEndAction : GameAction
    {
        public FightEndAction(GameMessage message, Game game)
            : base(message, game) { }

        public new FightEndMessage Message => (FightEndMessage)base.Message;

        public override Game Do()
        {
            Game.Score.Fight = null;
            return base.Do();
        }

        public override void Validate()
        {
            base.Validate();
        }
    }
}
