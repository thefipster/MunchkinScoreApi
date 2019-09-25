using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class ClassActionTest
    {
        private string  expectedClass = "Warrior";

        [Fact]
        public void AddClassToUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addClassMessage = new ClassMessage(Guid.NewGuid(), expectedClass, Modifier.Add);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addClassMessage));
        }

        [Fact]
        public void RemoveClassFromUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var removeClassMessage = new ClassMessage(Guid.NewGuid(), expectedClass, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeClassMessage));
        }

        [Fact]
        public void RemoveNotExistingClassFromHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var removeClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeClassMessage));
        }

        [Fact]
        public void AddClassToHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Add);

            // Act
            var score = quest.AddMessage(gameId, addClassMessage);

            // Assert
            Assert.Single(score.Heroes.First(x => x.Player.Id == playerId).Classes);
            Assert.Contains(expectedClass, score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Add);

            // Act
            quest.AddMessage(gameId, addClassMessage);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Empty(score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroAndRemoveItTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Add);
            var removeClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Remove);

            // Act
            quest.AddMessage(gameId, addClassMessage);
            var score = quest.AddMessage(gameId, removeClassMessage);

            // Assert
            Assert.Empty(score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroAndRemoveItAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Add);
            var removeClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Remove);

            // Act
            quest.AddMessage(gameId, addClassMessage);
            quest.AddMessage(gameId, removeClassMessage);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Single(score.Heroes.First(x => x.Player.Id == playerId).Classes);
            Assert.Contains(expectedClass, score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroTwiceThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId);
            var addClassMessage = new ClassMessage(playerId, expectedClass, Modifier.Add);

            // Act & Assert
            quest.AddMessage(gameId, addClassMessage);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addClassMessage));
        }
    }
}
