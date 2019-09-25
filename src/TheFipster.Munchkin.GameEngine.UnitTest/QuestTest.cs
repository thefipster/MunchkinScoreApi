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
            var startMsg = new StartMessage();

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
            var startMsg = new StartMessage();

            // Act
            quest.AddMessage(gameId, startMsg);
            quest.Undo(gameId);

            // Assert
            var game = gameStore.Get(gameId);
            Assert.Empty(game.Protocol);
        }
    }
}
