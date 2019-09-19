using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class RaceAction : ModifierMessageAction
    {
        public RaceAction(GameMessage message, Game game)
            : base(message, game) { }

        public new RaceMessage Message => (RaceMessage) base.Message;

        public override void Validate()
        {
            if (!IsHeroThere(Message.PlayerId))
                throw new InvalidActionException("Can't add a race to a hero that is not in the dungeon.");

            if (IsAddMessage && heroAlreadyHasRace)
                throw new InvalidActionException("Hero can't be of the same race twice.");

            if (IsRemoveMessage && !heroAlreadyHasRace)
                throw new InvalidActionException("Can't remove a race that a hero doesn't have.");
        }

        public override Game Do()
        {
            base.Do();
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return addRaceToHero();
                case Modifier.Remove:
                    return removeRaceFromHero();

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
                    return removeRaceFromHero();
                case Modifier.Remove:
                    return addRaceToHero();

                default:
                    throw new InvalidModifierException();
            }
        }

        private bool heroAlreadyHasRace =>
            Game.Score.Heroes.First(x => x.Player.Id == Message.PlayerId).Races.Contains(Message.Race);

        private Game addRaceToHero()
        {
            Game.Score.Heroes
                .First(x => x.Player.Id == Message.PlayerId)
                .Races.Add(Message.Race);

            return Game;
        }

        private Game removeRaceFromHero()
        {
            Game.Score.Heroes
                .First(x => x.Player.Id == Message.PlayerId)
                .Races.Remove(Message.Race);

            return Game;
        }
    }
}
