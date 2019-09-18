using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public abstract class MessageAction
    {
        public MessageAction(GameMessage message, Game game)
        {
            Game = game;
            Message = message;
        }

        public virtual GameMessage Message { get; }

        public Game Game { get; }

        protected bool GameHasStarted => Game.Score.Begin.HasValue;
    }
}
