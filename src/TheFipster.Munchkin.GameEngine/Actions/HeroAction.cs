using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class HeroAction : MessageAction, IGameAction
    {
        public HeroAction(HeroMessage message, Game game)
            : base(message, game) { }

        public new HeroMessage Message => (HeroMessage)base.Message;

        public Game Do()
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

        public Game Undo()
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
            if (Message.Modifier == Modifier.Add && playerExists())
                throw new InvalidActionException("The hero is already part of the game.");

            if (Message.Modifier == Modifier.Remove && !playerExists())
                throw new InvalidActionException("The hero isn't even in the game.");
        }

        private bool playerExists() =>
            Game.Score.Heroes.Any(x => x.Player.Id == Message.Hero.Player.Id);

        private Game addPlayer()
        {
            Game.Score.Heroes.Add(Message.Hero);
            return Game;
        }

        private Game removePlayer()
        {
            Game.Score.Heroes.Remove(Message.Hero);
            return Game;
        }
    }
}
