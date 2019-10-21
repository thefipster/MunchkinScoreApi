using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class EndAction : GameAction
    {
        public EndAction(GameMessage message, Game game)
            : base(message, game) { }

        public new EndMessage Message => (EndMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (!IsGameStarted)
                throw new InvalidActionException("Already quitting before it has even begun, just how I imagined it.");

            if (Game.Score.End.HasValue)
                throw new InvalidActionException("The battle was already fought, my young hero.");
        }

        public override Game Do()
        {
            base.Do();

            Game.Score.End = Message.Timestamp;
            return Game;
        }
    }
}
