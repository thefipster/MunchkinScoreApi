using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class LevelAction : MessageAction
    {
        public LevelAction(GameMessage message, Game game)
            : base(message, game) { }

        public new LevelMessage Message => (LevelMessage)base.Message;

        public override Game Do()
        {
            base.Do();
            applyLevelDelta();

            return Game;
        }

        public override void Validate()
        {
            if (!IsGameStarted)
                throw new InvalidActionException("The adventure hasn't even started.");

            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Couldn't find the hero in the dungeon.");
        }

        private void applyLevelDelta()
        {
            Hero hero = Game.GetHero(Message.PlayerId);
            hero.Level += Message.Delta;
        }
    }
}
