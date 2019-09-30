using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameEvents;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class StartActionTest
    {
        [Fact]
        public void StartGame_ResultsInBeginTimestampBeginSet_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var start = StartMessage.Create(1);

            // Act
            var game = quest.AddMessage(gameId, start);

            // Assert
            Assert.NotNull(game.Score.Begin);
            Assert.Equal(start.Timestamp, game.Score.Begin);
        }

        [Fact]
        public void StartGame_ThenUndoIt_ResultsInTimestampBeingSetToNull_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var start = StartMessage.Create(1);

            // Act
            quest.AddMessage(gameId, start);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Null(game.Score.Begin);
        }

        [Fact]
        public void StartGameTwice_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var start = StartMessage.Create(1);
            var secondStart = StartMessage.Create(2);

            // Act & Assert
            quest.AddMessage(gameId, start);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, secondStart));
        }
    }
}
