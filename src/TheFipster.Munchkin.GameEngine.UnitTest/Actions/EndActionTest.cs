using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class EndActionTest
    {
        [Fact]
        public void EndANotStartedGameThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var endMsg = new EndMessage();

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, endMsg));
        }

        [Fact]
        public void EndAStartedGameSetsEndToMessageTimestampTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = new StartMessage();
            var endMsg = new EndMessage();

            // Act
            quest.AddMessage(gameId, startMsg);
            var score = quest.AddMessage(gameId, endMsg);

            // Assert
            Assert.NotNull(score.End);
            Assert.Equal(endMsg.Timestamp, score.End);
        }

        [Fact]
        public void EndGameAndUndoSetsEndToNullTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = new StartMessage();
            var endMsg = new EndMessage();

            // Act
            quest.AddMessage(gameId, startMsg);
            quest.AddMessage(gameId, endMsg);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Null(score.End);
        }

        [Fact]
        public void EndGameTwiceThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = new StartMessage();
            var endMsg = new EndMessage();

            // Act & Assert
            quest.AddMessage(gameId, startMsg);
            quest.AddMessage(gameId, endMsg);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, endMsg));
        }
    }
}
