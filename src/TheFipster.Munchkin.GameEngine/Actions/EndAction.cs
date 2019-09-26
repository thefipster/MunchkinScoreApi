using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class EndAction : MessageAction
    {
        public EndAction(GameMessage message, Game game)
            : base(message, game) { }

        public new EndMessage Message => (EndMessage)base.Message;

        public override Game Do()
        {
            base.Do();
            Game.Score.End = Message.Timestamp;
            return Game;
        }

        public override void Validate()
        {
            if (!IsGameStarted)
                throw new InvalidActionException("Already quitting before it has even begun, just how I imagined it.");

            if (Game.Score.End.HasValue)
                throw new InvalidActionException("The battle was already fought, my young hero.");
        }
    }
}
