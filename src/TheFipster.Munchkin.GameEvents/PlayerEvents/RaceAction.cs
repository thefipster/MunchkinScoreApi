using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameEvents
{
    public class RaceAction : GameSwitchAction<string>
    {
        public RaceAction(GameMessage message, Game game)
            : base(message, game) { }

        public new RaceMessage Message => (RaceMessage) base.Message;

        public override void Validate()
        {   
            base.Validate();

            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Can't add a race to a hero that is not in the dungeon.");

            if (heroHasRace())
                throw new InvalidActionException("Hero already has the race.");

            if (heroHasNotRace())
                throw new InvalidActionException("Hero doesn't has the race.");
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

        private bool heroHasRace()
        {
            var hero = Game.GetHero(Message.PlayerId);
            return Message.Add.Any(x => hero.Races.Contains(x));
        }

        private bool heroHasNotRace()
        {
            var hero = Game.GetHero(Message.PlayerId);
            return Message.Remove.Any(x => !hero.Races.Contains(x));
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
                if (hero.Races.Contains(race))
                    hero.Races.Remove(race);
        }
    }
}
