using System;
using System.Linq;
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

        protected bool IsGameStarted => Game.Score.Begin.HasValue;

        protected bool IsHeroThere(Guid playerId) =>
            Game.Score.Heroes.Any(hero => hero.Player.Id == playerId);

        public virtual Game Do()
        {
            Game.Protocol.Add(Message);
            return Game;
        }

        public virtual void Validate() { }
    }
}
