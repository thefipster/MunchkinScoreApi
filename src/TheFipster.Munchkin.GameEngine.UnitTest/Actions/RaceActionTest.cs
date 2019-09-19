using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class RaceActionTest
    {
        private string humanRace = "Human";

        [Fact]
        public void AddRaceToUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, Guid.NewGuid(), humanRace, Modifier.Add);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addRaceMessage));
        }

        [Fact]
        public void RemoveRaceFromUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var removeRaceMessage = new RaceMessage(gameId, Guid.NewGuid(), humanRace, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeRaceMessage));
        }

        [Fact]
        public void RemoveNotExistingRaceFromHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var removeRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeRaceMessage));
        }

        [Fact]
        public void AddRaceToHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Add);

            // Act
            var score = quest.AddMessage(addRaceMessage);

            // Assert
            Assert.Single(score.Heroes.First(x => x.Player.Id == playerId).Races);
            Assert.Contains(humanRace, score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Add);

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
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Add);
            var removeRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Remove);

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
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Add);
            var removeRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Remove);

            // Act
            quest.AddMessage(addRaceMessage);
            quest.AddMessage(removeRaceMessage);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Single(score.Heroes.First(x => x.Player.Id == playerId).Races);
            Assert.Contains(humanRace, score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroTwiceThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addRaceMessage = new RaceMessage(gameId, playerId, humanRace, Modifier.Add);

            // Act & Assert
            quest.AddMessage(addRaceMessage);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addRaceMessage));
        }
    }
}
