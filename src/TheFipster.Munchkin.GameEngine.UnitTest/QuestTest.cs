using System;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using TheFipster.Munchkin.GameStorageVolatile;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest
{
    public class QuestTest
    {
        [Fact]
        public void StartJourneyGeneratesGameAndReturnsGameIdTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.Create(gameStore);

            // Act
            var gameId = quest.StartJourney();

            // Assert
            Assert.NotEqual(Guid.Empty, gameId);
            Assert.NotNull(gameStore.Get(gameId));
        }

        [Fact]
        public void UndoActionWithEmptyProtocolThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);

            // Act & Assert
            Assert.Throws<ProtocolEmptyException>(() => quest.Undo(gameId));
        }

        [Fact]
        public void AddActionToQuestMovesTheMessageIntoTheProtocolTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);

            // Act
            quest.AddMessage(gameId, startMsg);

            // Assert
            var game = gameStore.Get(gameId);
            Assert.Single(game.Protocol);
        }

        [Fact]
        public void AddActionToQuestAndUndoItResultsInEmptyProtocolTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);

            // Act
            quest.AddMessage(gameId, startMsg);
            quest.Undo(gameId);

            // Assert
            var game = gameStore.Get(gameId);
            Assert.Empty(game.Protocol);
        }

        [Fact]
        public void AddTwoMessagesWithSameSequenceThrowsGameOutOfSyncExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(1);

            // Act & Assert
            quest.AddMessage(gameId, startMsg);
            Assert.Throws<GameOutOfSyncException>(() => quest.AddMessage(gameId, endMsg));
        }

        [Fact]
        public void AddTwoMessagesWithGapInSequenceThrowsGameOutOfSyncExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(3);

            // Act & Assert
            quest.AddMessage(gameId, startMsg);
            Assert.Throws<GameOutOfSyncException>(() => quest.AddMessage(gameId, endMsg));
        }

        [Fact]
        public void AddTwoMessagesWithCorrectSequenceTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(2);

            // Act
            quest.AddMessage(gameId, startMsg);
            var game = quest.AddMessage(gameId, endMsg);

            // Assert
            Assert.NotNull(game.Score.Begin);
            Assert.NotNull(game.Score.End);
        }
    }
}
