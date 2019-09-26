using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class RaceAction : MessageSwitchAction
    {
        public RaceAction(GameMessage message, Game game)
            : base(message, game) { }

        public new RaceMessage Message => (RaceMessage) base.Message;

        public override void Validate()
        {   
            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Can't add a race to a hero that is not in the dungeon.");
        }

        public override Game Do()
        {
            base.Do();
            if (IsAddMessage)
                addRaceToHero();

            if (IsRemoveMessage)
                removeRaceFromHero();

            return Game;
        }

        private void addRaceToHero()
        {
            var hero = Game.GetHero(Message.PlayerId);
            foreach (var race in Message.Add)
                if (!hero.Races.Contains(race))
                    hero.Races.Add(race);
        }

        private void removeRaceFromHero()
        {
            var hero = Game.GetHero(Message.PlayerId);
            foreach (var race in Message.Remove)
                if (!hero.Races.Contains(race))
                    hero.Races.Remove(race);
        }
    }
}
