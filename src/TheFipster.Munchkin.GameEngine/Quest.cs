using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.GameEngine
{
    public class Quest : IQuest
    {
        private readonly IGameStore _gameStore;
        private readonly IActionFactory _actionFactory;

        public Quest(IGameStore gameStore, IActionFactory actionFactory)
        {
            _gameStore = gameStore;
            _actionFactory = actionFactory;
        }

        public Guid StartJourney()
        {
            var game = new Game();
            _gameStore.Upsert(game);
            return game.Id;
        }

        public Scoreboard AddMessage(GameMessage message)
        {
            var game = _gameStore.Get(message.GameId);
            game = performActionIfPossible(game, message);
            _gameStore.Upsert(game);
            return game.Score;
        }

        public Scoreboard Undo(Guid gameId)
        {
            var game = _gameStore.Get(gameId);
            game = performUndo(game);
            _gameStore.Upsert(game);
            return game.Score;
        }

        private Game performActionIfPossible(Game game, GameMessage message)
        {
            game.Protocol.Add(message);
            var action = _actionFactory.CreateActionFrom(message, game);
            action.Validate();
            return action.Do();
        }

        private Game performUndo(Game game)
        {
            var lastMessage = game.Protocol.Last();
            game.Protocol.Remove(lastMessage);
            var action = _actionFactory.CreateActionFrom(lastMessage, game);
            return action.Undo();
        }
    }
}
