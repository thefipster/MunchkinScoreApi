using System;
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
            game.Score = performActionIfPossible(game.Score, message);
            _gameStore.Upsert(game);
            return game.Score;
        }

        private Scoreboard performActionIfPossible(Scoreboard score, GameMessage message)
        {
            var action = _actionFactory.CreateActionFrom(message, score);
            action.Validate();
            return action.Do();
        }
    }
}
