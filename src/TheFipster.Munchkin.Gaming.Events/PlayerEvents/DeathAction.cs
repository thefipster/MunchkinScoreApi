using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class DeathAction : GameAction
    {
        public DeathAction(GameMessage message, Game game)
            : base(message, game) { }

        public new DeathMessage Message => (DeathMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (!IsGameStarted)
                throw new InvalidActionException("Look man, dying before the game even started is quite an accomplishment, but it doesn't count.");

            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("I realy can't find that hero in this dungeon.");
        }

        public override Game Do()
        {
            base.Do();

            Game.GetHero(Message.PlayerId).Level = 1;
            Game.GetHero(Message.PlayerId).Bonus = 0;

            return Game;
        }
    }
}
