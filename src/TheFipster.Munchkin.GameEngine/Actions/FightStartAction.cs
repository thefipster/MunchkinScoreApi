using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class FightStartAction : GameAction
    {
        public FightStartAction(GameMessage message, Game game) 
            : base(message, game) { }

        public new FightStartMessage Message => (FightStartMessage)base.Message;

        public override Game Do()
        {
            var fight = new Fight(Message.Hero, Message.Monster);
            Game.Score.Fight = fight;

            return base.Do();
        }

        public override void Validate()
        {
            base.Validate();
        }
    }
}
