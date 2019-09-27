using System;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class DeathActionTest
    {
        private Player someDude = PlayerFactory.CreateMale("Caballo Blanko");

        [Fact]
        public void LetHeroDieBeforeGameStarted_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var playerMsg = PlayerMessage.CreateAdd(1, new[] { someDude });
            var deathMsg = DeathMessage.Create(2, someDude.Id);

            // Act & Assert
            quest.AddMessage(gameId, playerMsg);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, deathMsg));
        }

        [Fact]
        public void LetHeroDieThatIsNotInTheGame_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStarted(out var gameStore, out var gameId, out var sequence);
            var deathMsg = DeathMessage.Create(sequence + 1, Guid.NewGuid());

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, deathMsg));
        }

        [Fact]
        public void LevelUpHeroThenKillHim_ResultsInLevelReset_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(
                out var gameStore,
                out var gameId,
                out var playerId,
                out var sequence);

            var levelMsg = LevelMessage.Create(sequence + 1, playerId, 1);
            var deathMsg = DeathMessage.Create(sequence + 2, playerId);

            // Act
            quest.AddMessage(gameId, levelMsg);
            var game = quest.AddMessage(gameId, deathMsg);

            // Assert
            var hero = game.GetHero(playerId);
            Assert.Equal(1, hero.Level);
        }

        [Fact]
        public void LevelUpHeroThenKillHim_ResultsInBonusReset_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(
                out var gameStore,
                out var gameId,
                out var playerId,
                out var sequence);

            var bonusMsg = BonusMessage.Create(sequence + 1, playerId, 1);
            var deathMsg = DeathMessage.Create(sequence + 2, playerId);

            // Act
            quest.AddMessage(gameId, bonusMsg);
            var game = quest.AddMessage(gameId, deathMsg);

            // Assert
            var hero = game.GetHero(playerId);
            Assert.Equal(0, hero.Bonus);
        }

        [Fact]
        public void LevelUpHeroThenKillHim_ResultsInClassesAndRacesUnchanged_Test()
        {
            // Arrange
            var dwarf = "Dwarf";
            var priest = "Priest";
            var quest = QuestFactory.CreateStartedWithMaleHero(
                out var gameStore,
                out var gameId,
                out var playerId,
                out var sequence);

            var raceMessage = RaceMessage.CreateAdd(sequence + 1, playerId, new[] { dwarf });
            var classMessage = ClassMessage.CreateAdd(sequence + 2, playerId, new[] { priest });
            var deathMsg = DeathMessage.Create(sequence + 3, playerId);

            // Act
            quest.AddMessage(gameId, raceMessage);
            quest.AddMessage(gameId, classMessage);
            var game = quest.AddMessage(gameId, deathMsg);

            // Assert
            var hero = game.GetHero(playerId);
            Assert.Single(hero.Races);
            Assert.Single(hero.Classes);
        }
    }
}
