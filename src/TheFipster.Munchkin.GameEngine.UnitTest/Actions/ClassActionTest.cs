using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using TheFipster.Munchkin.GameEvents;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class ClassActionTest
    {
        private string warriorClass = "Krieger";
        private string thiefClass = "Dieb";

        [Fact]
        public void AddClassToUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence.Next, Guid.NewGuid(), new[] { warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addClassMessage));
        }

        [Fact]
        public void RemoveClassFromUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeClassMessage = ClassMessage.CreateRemove(sequence.Next, Guid.NewGuid(), new[] { warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeClassMessage));
        }

        [Fact]
        public void RemoveNotExistingClassFromHero_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var removeClassMessage = ClassMessage.CreateRemove(sequence.Next, playerId, new[] { warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeClassMessage));
        }

        [Fact]
        public void AddClassToHero_ResultsInAddedClass_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence.Next, playerId, new[] { warriorClass });

            // Act
            var game = quest.AddMessage(gameId, addClassMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
            Assert.Contains(warriorClass, game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHero_ThenUndoIt_ResultsInNoChange_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence.Next, playerId, new[] { warriorClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHero_ThenRemoveIt_ResultsInNoChange_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence.Next, playerId, new[] { warriorClass });
            var removeClassMessage = ClassMessage.CreateRemove(sequence.Next, playerId, new[] { warriorClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            var game = quest.AddMessage(gameId, removeClassMessage);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHero_ThenRemoveIt_ThenUndoIt_ResultsInAddedClass_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence.Next, playerId, new[] { warriorClass });
            var removeClassMessage = ClassMessage.CreateRemove(sequence.Next, playerId, new[] { warriorClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            quest.AddMessage(gameId, removeClassMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
            Assert.Contains(warriorClass, game.Score.Heroes.First(x => x.Player.Id == playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroTwice_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence.Next, playerId, new[] { warriorClass });
            var addAnotherClassMessage = ClassMessage.CreateAdd(sequence.Next, playerId, new[] { warriorClass });

            // Act & Assert
            quest.AddMessage(gameId, addClassMessage);
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addAnotherClassMessage));
        }

        [Fact]
        public void AddClassToHero_ThenSwitchClasses_ResultsInSwitchedClass_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStartedWithMaleHero(out var gameStore, out var gameId, out var playerId, out var sequence);
            var addClassMessage = ClassMessage.CreateAdd(sequence.Next, playerId, new[] { warriorClass });
            var switchClassMessage = ClassMessage.Create(sequence.Next, playerId, new[] { thiefClass }, new[] { warriorClass });

            // Act
            quest.AddMessage(gameId, addClassMessage);
            var game = quest.AddMessage(gameId, switchClassMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First().Classes);
            Assert.Equal(thiefClass, game.Score.Heroes.First().Classes.First());
        }
    }
}
