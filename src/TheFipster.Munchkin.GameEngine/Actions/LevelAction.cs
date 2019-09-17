using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class LevelAction : MessageAction, IGameAction
    {
        public LevelAction(GameMessage message, Scoreboard board)
            : base(message, board) { }

        public new LevelMessage Message => (LevelMessage)base.Message;

        public Scoreboard Do()
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

        public Scoreboard Undo()
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
            if (heroIsMissing())
                throw new InvalidActionException("Couldn't find the hero in the dungeon.");
        }

        private bool heroIsMissing() =>
            !Board.Heroes.Any(hero => hero.Player.Id == Message.PlayerId);

        private Scoreboard increaseLevel()
        {
            Board.Heroes.First(hero => hero.Player.Id == Message.PlayerId).Level += Message.Delta;
            return Board;
        }

        private Scoreboard decreaseLevel()
        {
            Board.Heroes.First(hero => hero.Player.Id == Message.PlayerId).Level -= Message.Delta;
            return Board;
        }
    }
}
