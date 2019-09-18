using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class StartAction : MessageAction, IGameAction
    {
        public StartAction(GameMessage message, Game game)
            : base(message, game) { }

        public new StartMessage Message => (StartMessage)base.Message;

        public Game Do()
        {
            Game.Score.Begin = Message.Timestamp;
            return Game;
        }

        public Game Undo()
        {
            Game.Score.Begin = null;
            return Game;
        }

        public void Validate()
        {
            if (GameHasStarted)
                throw new InvalidActionException("The adventure has already begun my young hero.");
        }
    }
}
