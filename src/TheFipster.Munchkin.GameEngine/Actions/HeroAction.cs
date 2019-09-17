using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class HeroAction : MessageAction, IGameAction
    {
        public HeroAction(HeroMessage message, Scoreboard board)
            : base(message, board) { }

        public new HeroMessage Message => (HeroMessage)base.Message;

        public Scoreboard Do()
        {
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return addPlayer();
                case Modifier.Remove:
                    return removePlayer();
                default:
                    throw new InvalidModifierException();
            }
        }

        public Scoreboard Undo()
        {
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return removePlayer();
                case Modifier.Remove:
                    return addPlayer();
                default:
                    throw new InvalidModifierException();
            }
        }

        public void Validate()
        {
            if (Message.Modifier == Modifier.Add && playerAlreadyExists())
                throw new InvalidActionException("The hero is already part of the game.");

            if (Message.Modifier == Modifier.Remove && !playerAlreadyExists())
                throw new InvalidActionException("The hero isn't even in the game.");
        }

        private bool playerAlreadyExists() =>
            Board.Heroes.Any(x => x.Player.Id == Message.Hero.Player.Id);

        private Scoreboard addPlayer()
        {
            Board.Heroes.Add(Message.Hero);
            return Board;
        }

        private Scoreboard removePlayer()
        {
            Board.Heroes.Remove(Message.Hero);
            return Board;
        }
    }
}
