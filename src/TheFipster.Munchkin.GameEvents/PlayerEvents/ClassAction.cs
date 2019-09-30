using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameEvents
{
    public class ClassAction : GameSwitchAction<string>
    {
        public ClassAction(GameMessage message, Game game)
            : base(message, game) { }

        public new ClassMessage Message => (ClassMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Can't add a class to a hero that is not in the dungeon.");

            if (heroHasClass())
                throw new InvalidActionException("Hero already has the class.");

            if (heroHasNotClass())
                throw new InvalidActionException("Hero doesn't has the class.");
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

        private bool heroHasClass()
        {
            var hero = Game.GetHero(Message.PlayerId);
            return Message.Add.Any(x => hero.Classes.Contains(x));
        }

        private bool heroHasNotClass()
        {
            var hero = Game.GetHero(Message.PlayerId);
            return Message.Remove.Any(x => !hero.Classes.Contains(x));
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
