using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class StartAction : GameAction
    {
        public StartAction(GameMessage message, Game game)
            : base(message, game) { }

        public new StartMessage Message => (StartMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (IsGameStarted)
                throw new InvalidActionException("The adventure has already begun my young hero.");
        }

        public override Game Do()
        {
            base.Do();

            Game.Score.Begin = Message.Timestamp;
            return Game;
        }
    }
}
