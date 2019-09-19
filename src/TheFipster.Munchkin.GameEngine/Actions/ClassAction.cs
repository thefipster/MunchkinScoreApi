using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class ClassAction : ModifierMessageAction
    {
        public ClassAction(GameMessage message, Game game)
            : base(message, game) { }

        public new ClassMessage Message => (ClassMessage)base.Message;

        public override void Validate()
        {
            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Can't add a class to a hero that is not in the dungeon.");

            if (IsAddMessage && heroAlreadyHasClass)
                throw new InvalidActionException("Hero can't be of the same class twice.");

            if (IsRemoveMessage && !heroAlreadyHasClass)
                throw new InvalidActionException("Can't remove a class that a hero doesn't have.");
        }

        public override Game Do()
        {
            base.Do();
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return addClassToHero();
                case Modifier.Remove:
                    return removeClassFromHero();

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
                    return removeClassFromHero();
                case Modifier.Remove:
                    return addClassToHero();

                default:
                    throw new InvalidModifierException();
            }
        }

        private bool heroAlreadyHasClass =>
            Game.Score.Heroes.First(x => x.Player.Id == Message.PlayerId).Classes.Contains(Message.Class);

        private Game addClassToHero()
        {
            Game.Score.Heroes
                .First(x => x.Player.Id == Message.PlayerId)
                .Classes.Add(Message.Class);

            return Game;
        }

        private Game removeClassFromHero()
        {
            Game.Score.Heroes
                .First(x => x.Player.Id == Message.PlayerId)
                .Classes.Remove(Message.Class);

            return Game;
        }
    }
}
