﻿using System;
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
            var increaseLevelMsg = new LevelMessage(Guid.NewGuid(), 1, Modifier.Add);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseLevelMsg));
        }
        [Fact]
        public void IncreaseLevelOnUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId);
            var increaseLevelMsg = new LevelMessage(Guid.NewGuid(), 1, Modifier.Add);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, increaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelOnNotStartedGameThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var decreaseLevelMsg = new LevelMessage(Guid.NewGuid(), 1, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseLevelMsg));
        }

        [Fact]
        public void DecreaseLevelOnUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId);
            var decreaseLevelMsg = new LevelMessage(Guid.NewGuid(), 1, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, decreaseLevelMsg));
        }

        [Fact]
        public void IncreaseLevelOnHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(playerId, 1, Modifier.Add);

            // Act
            var score = quest.AddMessage(gameId, increaseLevelMsg);

            // Assert
            Assert.Equal(2, score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseLevelOnHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(playerId, 1, Modifier.Add);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Equal(1, score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseLevelOnHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(playerId, 1, Modifier.Add);
            var decreaseLevelMsg = new LevelMessage(playerId, 1, Modifier.Remove);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            var score = quest.AddMessage(gameId, decreaseLevelMsg);

            // Assert
            Assert.Equal(1, score.Heroes.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseLevelOnHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var increaseLevelMsg = new LevelMessage(playerId, 1, Modifier.Add);
            var decreaseLevelMsg = new LevelMessage(playerId, 1, Modifier.Remove);

            // Act
            quest.AddMessage(gameId, increaseLevelMsg);
            quest.AddMessage(gameId, decreaseLevelMsg);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Equal(2, score.Heroes.First().Level);
        }
    }
}
