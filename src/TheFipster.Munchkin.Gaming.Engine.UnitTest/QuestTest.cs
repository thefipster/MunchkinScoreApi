using FluentAssertions;
using System;
using System.Web;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;
using TheFipster.Munchkin.Gaming.Events;
using TheFipster.Munchkin.Gaming.Storage;
using TheFipster.Munchkin.Gaming.Storage.Volatile;
using TheFipster.Munchkin.TestFactory;
using Xunit;

namespace TheFipster.Munchkin.Gaming.Engine.UnitTest
{
    public class QuestTest
    {
        private Quest _quest;
        private IGameStore _gameStore;
        private Guid _gameId;

        public QuestTest()
        {
            _quest = QuestFactory.CreateStored(
                out _gameStore, 
                out _gameId);
        }

        [Fact]
        public void StartJourney_ResultsInGeneratedGame_AndReturnsGameId_Test()
        {
            // Arrange
            var gameStore = new VolatileGameStore();
            var quest = QuestFactory.Create(gameStore);

            // Act
            var gameId = quest.StartJourney();

            // Assert
            Assert.NotEqual(Guid.Empty, gameId);
            Assert.NotNull(gameStore.Get(gameId));
        }

        [Fact]
        public void UndoActionWithEmptyProtocol_ThrowsException_Test()
        {
            // Act & Assert
            Assert.Throws<ProtocolEmptyException>(() => _quest.Undo(_gameId));
        }

        [Fact]
        public void AddMessageToQuest_ResultsInMessageBeingPutIntoProtocol_Test()
        {
            // Arrange
            var startMsg = StartMessage.Create(1);

            // Act
            _quest.AddMessage(_gameId, startMsg);

            // Assert
            var game = _gameStore.Get(_gameId);
            Assert.Single(game.Protocol);
        }

        [Fact]
        public void AddMessageToQuest_ThenUndoIt_ResultsInEmptyProtocol_Test()
        {
            // Arrange
            var startMsg = StartMessage.Create(1);

            // Act
            _quest.AddMessage(_gameId, startMsg);
            _quest.Undo(_gameId);

            // Assert
            var game = _gameStore.Get(_gameId);
            Assert.Empty(game.Protocol);
        }

        [Fact]
        public void AddTwoMessagesWithSameSequence_ThrowsGameOutOfSyncException_Test()
        {
            // Arrange
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(1);

            // Act & Assert
            _quest.AddMessage(_gameId, startMsg);
            Assert.Throws<GameOutOfSyncException>(() => _quest.AddMessage(_gameId, endMsg));
        }

        [Fact]
        public void AddTwoMessagesWithGapInSequence_ThrowsGameOutOfSyncException_Test()
        {
            // Arrange
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(3);

            // Act & Assert
            _quest.AddMessage(_gameId, startMsg);
            Assert.Throws<GameOutOfSyncException>(() => _quest.AddMessage(_gameId, endMsg));
        }

        [Fact]
        public void AddStartAndEndMessagesWithCorrectSequence_ResultsInEndedGame_Test()
        {
            // Arrange
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(2);

            // Act
            _quest.AddMessage(_gameId, startMsg);
            var game = _quest.AddMessage(_gameId, endMsg);

            // Assert
            Assert.NotNull(game.Score.Begin);
            Assert.NotNull(game.Score.End);
        }

        [Fact]
        public void GenerateAnInitCode_ResultsInAnStringWith6Chars_Test()
        {
            // Arrange
            var quest = QuestFactory.Create();

            // Act
            var initCode = quest.GenerateInitCode();

            // Assert
            initCode.Should().HaveLength(6);
        }

        [Fact]
        public void GenerateAnInitCode_ResultsInAnUrlEncodedString_Test()
        {
            // Arrange
            var quest = QuestFactory.Create();

            // Act
            var initCode = quest.GenerateInitCode();
            var urlEncodedInitCode = HttpUtility.UrlEncode(initCode);

            // Assert
            initCode.Should().BeEquivalentTo(urlEncodedInitCode);
        }
    }
}
