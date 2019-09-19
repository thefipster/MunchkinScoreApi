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
    public class RaceActionTest
    {
        [Fact]
        public void AddRaceToUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, Guid.NewGuid(), "Human", Modifier.Add);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addRaceMessage));
        }

        [Fact]
        public void RemoveRaceFromUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var removeRaceMessage = new RaceMessage(gameId, Guid.NewGuid(), "Human", Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeRaceMessage));
        }

        [Fact]
        public void RemoveNotExistingRaceFromHeroThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var removeRaceMessage = new RaceMessage(gameId, playerId, "Human", Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeRaceMessage));
        }

        [Fact]
        public void AddRaceToHeroTest()
        {
            // Arrange
            var expectedRace = "Human";
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, expectedRace, Modifier.Add);

            // Act
            var score = quest.AddMessage(addRaceMessage);

            // Assert
            Assert.Single(score.Heroes.First(x => x.Player.Id == playerId).Races);
            Assert.Contains(expectedRace, score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroAndUndoTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, "Human", Modifier.Add);

            // Act
            quest.AddMessage(addRaceMessage);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Empty(score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroAndRemoveItTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, "Human", Modifier.Add);
            var removeRaceMessage = new RaceMessage(gameId, playerId, "Human", Modifier.Remove);

            // Act
            quest.AddMessage(addRaceMessage);
            var score = quest.AddMessage(removeRaceMessage);

            // Assert
            Assert.Empty(score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroAndRemoveItAndUndoTest()
        {
            // Arrange
            var expectedRace = "Human";
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, expectedRace, Modifier.Add);
            var removeRaceMessage = new RaceMessage(gameId, playerId, expectedRace, Modifier.Remove);

            // Act
            quest.AddMessage(addRaceMessage);
            quest.AddMessage(removeRaceMessage);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Single(score.Heroes.First(x => x.Player.Id == playerId).Races);
            Assert.Contains(expectedRace, score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroTwiceThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateStartedWithMaleHero(gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, "Human", Modifier.Add);

            // Act & Assert
            quest.AddMessage(addRaceMessage);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addRaceMessage));
        }
    }
}
