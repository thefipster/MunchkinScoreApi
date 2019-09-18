using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class EndAction : MessageAction, IGameAction
    {
        public EndAction(GameMessage message, Game game)
            : base(message, game) { }

        public new EndMessage Message => (EndMessage)base.Message;

        public Game Do()
        {
            Game.Score.End = Message.Timestamp;
            return Game;
        }

        public Game Undo()
        {
            Game.Score.End = null;
            return Game;
        }

        public void Validate()
        {
            if (!GameHasStarted)
                throw new InvalidActionException("Already quitting before it has even begun, just how I imagined it.");

            if (Game.Score.End.HasValue)
                throw new InvalidActionException("The battle was already fought, my young hero.");
        }
    }
}
