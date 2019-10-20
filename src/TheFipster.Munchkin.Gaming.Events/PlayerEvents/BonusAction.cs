using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Events
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
            var hero = Game.GetHero(Message.PlayerId);
            hero.Bonus += Message.Delta;
        }
    }
}
