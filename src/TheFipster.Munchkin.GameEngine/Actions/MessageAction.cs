using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public abstract class MessageAction : IGameAction
    {
        public MessageAction(GameMessage message, Game game)
        {
            Game = game;
            Message = message;
        }

        public virtual GameMessage Message { get; }

        public Game Game { get; }

        protected bool GameHasStarted => Game.Score.Begin.HasValue;

        public virtual Game Do()
        {
            Game.Protocol.Add(Message);
            return Game;
        }

        public virtual Game Undo()
        {
            Game.Protocol.Remove(Message);
            return Game;
        }

        public virtual void Validate() { }
    }
}
