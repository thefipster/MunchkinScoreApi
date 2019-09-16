using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public abstract class MessageAction
    {
        public MessageAction(GameMessage message, Scoreboard board)
        {
            Board = board;
            Message = message;
        }

        public GameMessage Message { get; }
        public T GetMessage<T>() where T : GameMessage
        {
            return (T)Message;
        }

        public Scoreboard Board { get; }
    }
}
