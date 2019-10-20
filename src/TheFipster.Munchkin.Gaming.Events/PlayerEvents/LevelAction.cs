using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class LevelAction : GameAction
    {
        public LevelAction(GameMessage message, Game game)
            : base(message, game) { }

        public new LevelMessage Message => (LevelMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (!IsGameStarted)
                throw new InvalidActionException("The adventure hasn't even started.");

            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Couldn't find the hero in the dungeon.");
        }

        public override Game Do()
        {
            base.Do();

            applyLevelDelta();

            return Game;
        }

        private void applyLevelDelta()
        {
            var hero = Game.GetHero(Message.PlayerId);
            hero.Level += Message.Delta;
        }
    }
}
