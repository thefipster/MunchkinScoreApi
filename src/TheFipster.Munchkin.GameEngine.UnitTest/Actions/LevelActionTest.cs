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
        public void IncreaseLevelWithNotStartedGameThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var increaseLevelMsg = LevelMessage.Create(1, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseLevelMsg));
        }
        [Fact]
        public void IncreaseLevelOnUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence + 1, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelOnNotStartedGameThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var decreaseLevelMsg = LevelMessage.Create(1, Guid.NewGuid(), -1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelOnUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId, out var sequence);
            var decreaseLevelMsg = LevelMessage.Create(sequence + 1, Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseLevelMsg));
        }

        [Fact]
        public void IncreaseLevelOnHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence + 1, playerId, 1);

            // Act
            var game = quest.AddMessage(gameId, increaseLevelMsg);

            // Assert
            Assert.Equal(2, game.Score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseLevelOnHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence + 1, playerId, 1);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Equal(1, game.Score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseLevelOnHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence + 1, playerId, 1);
            var decreaseLevelMsg = LevelMessage.Create(sequence + 2, playerId, -1);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            var game = quest.AddMessage(gameId, decreaseLevelMsg);

            // Assert
            Assert.Equal(1, game.Score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseLevelOnHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var increaseLevelMsg = LevelMessage.Create(sequence + 1, playerId, 1);
            var decreaseLevelMsg = LevelMessage.Create(sequence + 2, playerId, -1);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            quest.AddMessage(gameId, decreaseLevelMsg);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Equal(2, game.Score.Heroes.First().Level);
        }
    }
}
