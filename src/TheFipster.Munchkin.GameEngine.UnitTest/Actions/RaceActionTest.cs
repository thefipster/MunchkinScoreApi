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
        private string dwarfRace = "Dwarf";

        [Fact]
        public void AddRaceToUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, Guid.NewGuid(), new[] { humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addRaceMessage));
        }

        [Fact]
        public void RemoveRaceFromUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeRaceMessage = RaceMessage.CreateRemove(sequence + 1, Guid.NewGuid(), new[] { humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeRaceMessage));
        }

        [Fact]
        public void RemoveNotExistingRaceFromHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeRaceMessage = RaceMessage.CreateRemove(sequence + 1, playerId, new[] { humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeRaceMessage));
        }

        [Fact]
        public void AddRaceToHero_ResultsInAddedRace_Test()
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
        public void AddRaceToHero_ThenUndoIt_ResultsInNoChange_Test()
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
        public void AddRaceToHero_ThenRemoveIt_ResultsInNoChange_Test()
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
        public void AddRaceToHero_ThenSwitchIt_ResultsInSwitchedRace_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addRaceMessage = RaceMessage.CreateAdd(sequence + 1, playerId, new[] { humanRace });
            var switchRaceMessage = RaceMessage.Create(sequence + 2, playerId, new[] { dwarfRace }, new[] { humanRace });

            // Act
            quest.AddMessage(gameId, addRaceMessage);
            var game = quest.AddMessage(gameId, switchRaceMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == playerId).Races);
            Assert.Equal(dwarfRace, game.Score.Heroes.First(x => x.Player.Id == playerId).Races.First());
        }

        [Fact]
        public void AddRaceToHero_ThenRemoveIt_ThenUndoIt_ResultsInAddedRace_Test()
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
        public void AddRaceToHeroTwice_ThrowsException_Test()
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
