using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class BonusAction : GameAction
    {
        public BonusAction(GameMessage message, Game game)
            : base(message, game) { }

        public new BonusMessage Message => (BonusMessage)base.Message;

        public override Game Do()
        {
            base.Do();
            applyBonusDelta();

            return Game;
        }

        public override void Validate()
        {
            if (!IsGameStarted)
                throw new InvalidActionException("The adventure hasn't even started.");

            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Couldn't find the hero in the dungeon.");
        }

        private void applyBonusDelta()
        {
            Hero hero = Game.GetHero(Message.PlayerId);
            hero.Bonus += Message.Delta;
        }
    }
}
