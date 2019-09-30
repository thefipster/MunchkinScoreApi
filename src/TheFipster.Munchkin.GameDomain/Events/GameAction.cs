using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameDomain.Events
{
    public abstract class GameAction
    {
        public GameAction(GameMessage message, Game game)
        {
            Game = game;
            Message = message;
        }

        public virtual GameMessage Message { get; }

        public Game Game { get; }

        protected bool IsGameStarted => Game.Score.Begin.HasValue;

        protected bool IsHeroThere(Guid playerId) =>
            Game.Score.Heroes.Any(hero => hero.Player.Id == playerId);

        public virtual void Validate() { }

        public virtual Game Do()
        {
            Game.Protocol.Add(Message);
            return Game;
        }
    }
}
