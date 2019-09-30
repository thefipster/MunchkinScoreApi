using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameEvents;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class PlayerActionTest
    {
        private Player bonnie = PlayerFactory.CreateFemale("Bonnie");
        private Player clyde = PlayerFactory.CreateMale("Clyde");

        [Fact]
        public void AddHeroToGame_ResultsInAddedHero_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addHero = PlayerMessage.CreateAdd(1, new[] { bonnie });

            // Act
            var game = quest.AddMessage(gameId, addHero);

            // Assert
            Assert.Single(game.Score.Heroes);
            Assert.Equal(bonnie.Name, game.Score.Heroes.First().Player.Name);
            Assert.Equal(bonnie.Gender, game.Score.Heroes.First().Player.Gender);
        }

        [Fact]
        public void AddHero_ThenUndoId_ResultsInEmptyGame_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addHero = PlayerMessage.CreateAdd(1, new[] { bonnie });

            // Act
            quest.AddMessage(gameId, addHero);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Empty(game.Score.Heroes);
        }

        [Fact]
        public void AddTwoHeroes_ResultsInTwoAddedHeroes_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = PlayerMessage.CreateAdd(1, new[] { bonnie });
            var addClyde = PlayerMessage.CreateAdd(2, new[] { clyde });

            // Act
            quest.AddMessage(gameId, addBonnie);
            var game = quest.AddMessage(gameId, addClyde);

            // Assert
            Assert.Equal(2, game.Score.Heroes.Count);
        }

        [Fact]
        public void AddTwoHeroes_ThenUndoIt_ResultsInNoChange_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = PlayerMessage.CreateAdd(1, new[] { bonnie });
            var addClyde = PlayerMessage.CreateAdd(2, new[] { clyde });

            // Act
            quest.AddMessage(gameId, addBonnie);
            quest.AddMessage(gameId, addClyde);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Single(game.Score.Heroes);
        }

        [Fact]
        public void AddSameHeroTwice_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = PlayerMessage.CreateAdd(1, new[] { bonnie });
            var addAnotherBonnie = PlayerMessage.CreateAdd(2, new[] { bonnie });

            // Act & Assert
            quest.AddMessage(gameId, addBonnie);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addAnotherBonnie));
        }

        [Fact]
        public void RemoveUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var removeBonnie = PlayerMessage.CreateRemove(1, new[] { bonnie });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeBonnie));
        }

        [Fact]
        public void AddHero_ThenRemoveHim_ThenUndoIt_ResultsInAddedHero_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addBonnie = PlayerMessage.CreateAdd(1, new[] { bonnie });
            var removeBonnie = PlayerMessage.CreateRemove(2, new[] { bonnie });

            // Act
            quest.AddMessage(gameId, addBonnie);
            quest.AddMessage(gameId, removeBonnie);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Single(game.Score.Heroes);
            Assert.Equal(bonnie.Name, game.Score.Heroes.First().Player.Name);
            Assert.Equal(bonnie.Gender, game.Score.Heroes.First().Player.Gender);
        }
    }
}
