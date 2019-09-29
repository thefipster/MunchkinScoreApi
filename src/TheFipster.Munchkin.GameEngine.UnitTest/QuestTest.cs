using System;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using TheFipster.Munchkin.GameStorage.Volatile;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest
{
    public class QuestTest
    {
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
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);

            // Act & Assert
            Assert.Throws<ProtocolEmptyException>(() => quest.Undo(gameId));
        }

        [Fact]
        public void AddMessageToQuest_ResultsInMessageBeingPutIntoProtocol_Test()
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
        public void AddMessageToQuest_ThenUndoIt_ResultsInEmptyProtocol_Test()
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
        public void AddTwoMessagesWithSameSequence_ThrowsGameOutOfSyncException_Test()
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
        public void AddTwoMessagesWithGapInSequence_ThrowsGameOutOfSyncException_Test()
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
        public void AddStartAndEndMessagesWithCorrectSequence_ResultsInEndedGame_Test()
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
