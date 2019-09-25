using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class StartActionTest
    {
        [Fact]
        public void StartGameSetsBeginToMessageTimestampTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var start = new StartMessage();

            // Act
            var score = quest.AddMessage(gameId, start);

            // Assert
            Assert.NotNull(score.Begin);
            Assert.Equal(start.Timestamp, score.Begin);
        }

        [Fact]
        public void StartGameAndUndoSetsBeginToNullTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var start = new StartMessage();

            // Act
            quest.AddMessage(gameId, start);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Null(score.Begin);
        }

        [Fact]
        public void StartGameTwiceThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var start = new StartMessage();

            // Act & Assert
            quest.AddMessage(gameId, start);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, start));
        }
    }
}
