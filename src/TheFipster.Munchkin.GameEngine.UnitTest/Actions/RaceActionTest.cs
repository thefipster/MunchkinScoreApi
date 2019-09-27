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
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, Guid.NewGuid(), new[] { humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addRaceMessage));
        }

        [Fact]
        public void RemoveRaceFromUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeRaceMessage = RaceMessage.CreateRemove(sequence + 1, Guid.NewGuid(), new[] { humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeRaceMessage));
        }

        [Fact]
        public void RemoveNotExistingRaceFromHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeRaceMessage = RaceMessage.CreateRemove(sequence + 1, playerId, new[] { humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeRaceMessage));
        }

        [Fact]
        public void AddRaceToHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, playerId, new[] { humanRace });

            // Act
            var game = quest.AddMessage(gameId, addRaceMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == playerId).Races);
            Assert.Contains(humanRace, game.Score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, playerId, new[] { humanRace });

            // Act
            quest.AddMessage(gameId, addRaceMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroAndRemoveItTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, playerId, new[] { humanRace });
            var removeRaceMessage = RaceMessage.CreateRemove(sequence + 2, playerId, new[] { humanRace });

            // Act
            quest.AddMessage(gameId, addRaceMessage);
            var game = quest.AddMessage(gameId, removeRaceMessage);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroAndRemoveItAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, playerId, new[] { humanRace });
            var removeRaceMessage = RaceMessage.CreateRemove(sequence + 2, playerId, new[] { humanRace });

            // Act
            quest.AddMessage(gameId, addRaceMessage);
            quest.AddMessage(gameId, removeRaceMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == playerId).Races);
            Assert.Contains(humanRace, game.Score.Heroes.First(x => x.Player.Id == playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroTwiceThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, playerId, new[] { humanRace });
            var addAnotherRaceMessage = RaceMessage.CreateAdd(sequence + 2, playerId, new[] { humanRace });

            // Act & Assert
            quest.AddMessage(gameId, addRaceMessage);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addAnotherRaceMessage));
        }
    }
}
