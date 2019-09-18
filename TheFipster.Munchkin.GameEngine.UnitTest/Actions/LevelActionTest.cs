using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using TheFipster.Munchkin.GameStorageVolatile;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class LevelActionTest
    {
        [Fact]
        public void IncreaseLevelWithNotStartedGameThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStored(gameStore, out var gameId);
            var increaseLevelMsg = new LevelMessage(gameId, Guid.NewGuid(), 1, Modifier.Add);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(increaseLevelMsg));
        }
        [Fact]
        public void IncreaseLevelOnUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStarted(gameStore, out var gameId);
            var increaseLevelMsg = new LevelMessage(gameId, Guid.NewGuid(), 1, Modifier.Add);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(increaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelOnNotStartedGameThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStored(gameStore, out var gameId);
            var decreaseLevelMsg = new LevelMessage(gameId, Guid.NewGuid(), 1, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(decreaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelOnUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStarted(gameStore, out var gameId);
            var decreaseLevelMsg = new LevelMessage(gameId, Guid.NewGuid(), 1, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(decreaseLevelMsg));
        }

        [Fact]
        public void IncreaseLevelOnHeroTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(gameId, playerId, 1, Modifier.Add);

            // Act
            var score = quest.AddMessage(increaseLevelMsg);

            // Assert
            Assert.Equal(2, score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseLevelOnHeroAndUndoTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(gameId, playerId, 1, Modifier.Add);

            // Act
            quest.AddMessage(increaseLevelMsg);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Equal(1, score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseLevelOnHeroTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(gameId, playerId, 1, Modifier.Add);
            var decreaseLevelMsg = new LevelMessage(gameId, playerId, 1, Modifier.Remove);

            // Act
            quest.AddMessage(increaseLevelMsg);
            var score = quest.AddMessage(decreaseLevelMsg);

            // Assert
            Assert.Equal(1, score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseLevelOnHeroAndUndoTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(gameId, playerId, 1, Modifier.Add);
            var decreaseLevelMsg = new LevelMessage(gameId, playerId, 1, Modifier.Remove);

            // Act
            quest.AddMessage(increaseLevelMsg);
            quest.AddMessage(decreaseLevelMsg);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Equal(2, score.Heroes.First().Level);
        }
    }
}
