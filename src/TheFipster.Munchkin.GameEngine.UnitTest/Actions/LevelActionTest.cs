using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class LevelActionTest
    {
        [Fact]
        public void IncreaseLevelWhenGameIsNotStarted_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var increaseLevelMsg = LevelMessage.Create(1, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseLevelMsg));
        }

        [Fact]
        public void IncreaseLevelOnUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence.Next, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelWhenGameIsNotStarted_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var decreaseLevelMsg = LevelMessage.Create(1, Guid.NewGuid(), -1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelOnUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId, out var sequence);
            var decreaseLevelMsg = LevelMessage.Create(sequence.Next, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseLevelMsg));
        }

        [Fact]
        public void IncreaseLevelOnHero_ResultsInIncreasedLevel_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence.Next, playerId, 1);

            // Act
            var game = quest.AddMessage(gameId, increaseLevelMsg);

            // Assert
            Assert.Equal(2, game.Score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseLevelOnHero_ThenUndoId_ResultsInOriginalLevel_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence.Next, playerId, 1);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Equal(1, game.Score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseLevel_ThenDecreaseLevelOnHero_ResultsInNoChange_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence.Next, playerId, 1);
            var decreaseLevelMsg = LevelMessage.Create(sequence.Next, playerId, -1);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            var game = quest.AddMessage(gameId, decreaseLevelMsg);

            // Assert
            Assert.Equal(1, game.Score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseLevel_ThenDecreaseLevel_ThenUndoIt_ResultsInIncreasedLevel_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence.Next, playerId, 1);
            var decreaseLevelMsg = LevelMessage.Create(sequence.Next, playerId, -1);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            quest.AddMessage(gameId, decreaseLevelMsg);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Equal(2, game.Score.Heroes.First().Level);
        }
    }
}
