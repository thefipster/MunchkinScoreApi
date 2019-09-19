using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class HeroAction : ModifierMessageAction
    {
        public HeroAction(HeroMessage message, Game game)
            : base(message, game) { }

        public new HeroMessage Message => (HeroMessage)base.Message;

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
            if (IsAddMessage && IsHeroThere(Message.Hero.Player.Id))
                throw new InvalidActionException("The hero is already part of the game.");

            if (IsRemoveMessage && !IsHeroThere(Message.Hero.Player.Id))
                throw new InvalidActionException("The hero isn't even in the game.");
        }

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
