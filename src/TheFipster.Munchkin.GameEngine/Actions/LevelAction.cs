using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class LevelAction : MessageAction, IGameAction
    {
        public LevelAction(GameMessage message, Game game)
            : base(message, game) { }

        public new LevelMessage Message => (LevelMessage)base.Message;

        public Game Do()
        {
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return increaseLevel();
                case Modifier.Remove:
                    return decreaseLevel();
                default:
                    throw new InvalidModifierException();
            }
        }

        public Game Undo()
        {
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return decreaseLevel();
                case Modifier.Remove:
                    return increaseLevel();
                default:
                    throw new InvalidModifierException();
            }
        }

        public void Validate()
        {
            if (!GameHasStarted)
                throw new InvalidActionException("The adventure hasn't even started.");

            if (heroIsMissing)
                throw new InvalidActionException("Couldn't find the hero in the dungeon.");
        }

        private bool heroIsMissing =>
            Game.Score.Heroes.All(hero => hero.Player.Id != Message.PlayerId);

        private Game increaseLevel()
        {
            Game.Score.Heroes.First(hero => hero.Player.Id == Message.PlayerId).Level += Message.Delta;
            return Game;
        }

        private Game decreaseLevel()
        {
            Game.Score.Heroes.First(hero => hero.Player.Id == Message.PlayerId).Level -= Message.Delta;
            return Game;
        }
    }
}
