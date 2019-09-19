using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using TheFipster.Munchkin.GameStorageVolatile;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class EndActionTest
    {
        [Fact]
        public void EndANotStartedGameThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStored(gameStore, out var gameId);
            var endMsg = new EndMessage(gameId);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(endMsg));
        }

        [Fact]
        public void EndAStartedGameSetsEndToMessageTimestampTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStored(gameStore, out var gameId);
            var startMsg = new StartMessage(gameId);
            var endMsg = new EndMessage(gameId);

            // Act
            quest.AddMessage(startMsg);
            var score = quest.AddMessage(endMsg);

            // Assert
            Assert.NotNull(score.End);
            Assert.Equal(endMsg.Timestamp, score.End);
        }

        [Fact]
        public void EndGameAndUndoSetsEndToNullTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStored(gameStore, out var gameId);
            var startMsg = new StartMessage(gameId);
            var endMsg = new EndMessage(gameId);

            // Act
            quest.AddMessage(startMsg);
            quest.AddMessage(endMsg);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Null(score.End);
        }

        [Fact]
        public void EndGameTwiceThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStored(gameStore, out var gameId);
            var startMsg = new StartMessage(gameId);
            var endMsg = new EndMessage(gameId);

            // Act & Assert
            quest.AddMessage(startMsg);
            quest.AddMessage(endMsg);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(endMsg));
        }
    }
}
