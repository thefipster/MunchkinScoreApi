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
        private string warriorClass = "Kireger";
        private string thiefClass = "Dieb";

        [Fact]
        public void AddClassToUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence + 1, Guid.NewGuid(), new[] { warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addClassMessage));
        }

        [Fact]
        public void RemoveClassFromUnknownHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeClassMessage = ClassMessage.CreateRemove(sequence + 1, Guid.NewGuid(), new[] { warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeClassMessage));
        }

        [Fact]
        public void RemoveNotExistingClassFromHeroThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeClassMessage = ClassMessage.CreateRemove(sequence + 1, playerId, new[] { warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeClassMessage));
        }

        [Fact]
        public void AddClassToHeroTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence + 1, playerId, new[] { warriorClass });

            // Act
            var game = quest.AddMessage(gameId, addClassMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
            Assert.Contains(warriorClass, game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence + 1, playerId, new[] { warriorClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroAndRemoveItTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence + 1, playerId, new[] { warriorClass });
            var removeClassMessage = ClassMessage.CreateRemove(sequence + 2, playerId, new[] { warriorClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            var game = quest.AddMessage(gameId, removeClassMessage);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroAndRemoveItAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence + 1, playerId, new[] { warriorClass });
            var removeClassMessage = ClassMessage.CreateRemove(sequence + 2, playerId, new[] { warriorClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            quest.AddMessage(gameId, removeClassMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
            Assert.Contains(warriorClass, game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroTwiceThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence + 1, playerId, new[] { warriorClass });
            var addAnotherClassMessage = ClassMessage.CreateAdd(sequence + 2, playerId, new[] { warriorClass });

            // Act & Assert
            quest.AddMessage(gameId, addClassMessage);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addAnotherClassMessage));
        }

        [Fact]
        public void AddClassToHeroThenSwitchThenRemoveTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence + 1, playerId, new[] { warriorClass });
            var switchClassMessage = ClassMessage.Create(sequence + 2, playerId, new[] { thiefClass }, new[] { warriorClass });
            var removeClassMessage = ClassMessage.CreateRemove(sequence + 3, playerId, new[] { thiefClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            quest.AddMessage(gameId, switchClassMessage);
            var game = quest.AddMessage(gameId, removeClassMessage);

            // Assert
            Assert.Empty(game.Score.Heroes.First().Classes);
        }
    }
}
