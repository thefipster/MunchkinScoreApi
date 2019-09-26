using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class ClassAction : MessageSwitchAction
    {
        public ClassAction(GameMessage message, Game game)
            : base(message, game) { }

        public new ClassMessage Message => (ClassMessage)base.Message;

        public override void Validate()
        {
            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Can't add a class to a hero that is not in the dungeon.");
        }

        public override Game Do()
        {
            base.Do();
            if (IsAddMessage)
                addClassToHero();

            if (IsRemoveMessage)
                removeClassFromHero();

            return Game;
        }

        private void addClassToHero()
        {
            var hero = Game.GetHero(Message.PlayerId);
            foreach (var @class in Message.Add)
                if (!hero.Classes.Contains(@class))
                    hero.Classes.Add(@class);
        }

        private void removeClassFromHero()
        {
            var hero = Game.GetHero(Message.PlayerId);
            foreach (var @class in Message.Remove)
                if (hero.Classes.Contains(@class))
                    hero.Classes.Remove(@class);
        }
    }
}
