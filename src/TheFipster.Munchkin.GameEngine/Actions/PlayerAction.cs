using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class PlayerAction : ModifierMessageAction
    {
        public PlayerAction(PlayerMessage message, Game game)
            : base(message, game) { }

        public new PlayerMessage Message => (PlayerMessage)base.Message;

        public override Game Do()
        {
            base.Do();
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

        public override Game Undo()
        {
            base.Undo();
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

        public override void Validate()
        {
            if (IsAddMessage && IsHeroThere(Message.Player.Id))
                throw new InvalidActionException("The hero is already part of the game.");

            if (IsRemoveMessage && !IsHeroThere(Message.Player.Id))
                throw new InvalidActionException("The hero isn't even in the game.");
        }

        private Game addPlayer()
        {
            var hero = new Hero(Message.Player);
            Game.Score.Heroes.Add(hero);
            return Game;
        }

        private Game removePlayer()
        {
            var hero = Game.Score.Heroes.First(h => h.Player.Id == Message.Player.Id);
            Game.Score.Heroes.Remove(hero);
            return Game;
        }
    }
}
