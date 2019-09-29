using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class EndActionTest
    {
        [Fact]
        public void EndingANotStartedGame_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var endMsg = EndMessage.Create(1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, endMsg));
        }

        [Fact]
        public void EndingAStartedGame_EndsItByAddingTimestamp_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(2);

            // Act
            quest.AddMessage(gameId, startMsg);
            var game = quest.AddMessage(gameId, endMsg);

            // Assert
            Assert.NotNull(game.Score.End);
            Assert.Equal(endMsg.Timestamp, game.Score.End);
        }

        [Fact]
        public void EndingGame_ThenUndoIt_RemovesTheEndTimestamp_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(2);

            // Act
            quest.AddMessage(gameId, startMsg);
            quest.AddMessage(gameId, endMsg);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Null(game.Score.End);
        }

        [Fact]
        public void EndingGameTwice_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var startMsg = StartMessage.Create(1);
            var endMsg = EndMessage.Create(2);
            var secondEndMsg = EndMessage.Create(3);

            // Act & Assert
            quest.AddMessage(gameId, startMsg);
            quest.AddMessage(gameId, endMsg);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, secondEndMsg));
        }
    }
}
