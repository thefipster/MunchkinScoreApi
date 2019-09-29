using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameStorage;

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

        public Game AddMessage(Guid gameId, GameMessage message) =>
            AddMessages(gameId, new GameMessage[] { message });

        public Game AddMessages(Guid gameId, IEnumerable<GameMessage> messages)
        {
            var game = _gameStore.Get(gameId);
            game = executeMessages(game, messages);
            _gameStore.Upsert(game);
            return game;
        }

        private Game executeMessages(Game game, IEnumerable<GameMessage> messages)
        {
            foreach (var msg in messages)
            {
                checkSequence(game, msg);
                game = applyAction(game, msg);
            }

            return game;
        }

        private void checkSequence(Game game, GameMessage msg)
        {
            var lastSequence = getLatestSequence(game.Protocol);
            var nextSequence = lastSequence + 1;

            if (msg.Sequence != nextSequence)
                throw new GameOutOfSyncException(lastSequence);
        }

        private int getLatestSequence(IList<GameMessage> protocol) =>
            protocol.Any()
                ? protocol.Max(item => item.Sequence)
                : 0;

        public Game Undo(Guid gameId)
        {
            var game = _gameStore.Get(gameId);
            game = performUndo(game);
            _gameStore.Upsert(game);
            return game;
        }

        public Game GetState(Guid gameId) => _gameStore.Get(gameId);

        private Game applyAction(Game game, GameMessage message)
        {
            var action = _actionFactory.CreateActionFrom(message, game);
            action.Validate();
            return action.Do();
        }

        private Game performUndo(Game game)
        {
            if (protocolIsEmpty(game))
                throw new ProtocolEmptyException();

            game.Protocol.Remove(game.Protocol.Last());
            return createNewGameFromProtocol(game.Protocol);
        }

        private bool protocolIsEmpty(Game game) =>
            !game.Protocol.Any();

        private Game createNewGameFromProtocol(IList<GameMessage> protocol)
        {
            var game = new Game();
            foreach (var msg in protocol)
                game = applyAction(game, msg);

            return game;
        }
    }
}
