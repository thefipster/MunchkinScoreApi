using System;
using System.Linq;
using TheFipster.Munchkin.GameAbstractions;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.TestFactory;
using Xunit;

namespace TheFipster.Munchkin.GameEvents.UnitTest.Actions
{
    public class ClassActionTest
    {
        private string _warriorClass = "Krieger";
        private string _thiefClass = "Dieb";

        private IQuest _quest;
        private Guid _gameId;
        private Guid _playerId;
        private Sequence _sequence;

        public ClassActionTest()
        {
            _quest = QuestFactory.CreateStartedWithMaleHero(
                out _, 
                out _gameId, 
                out _playerId, 
                out _sequence);
        }

        [Fact]
        public void AddClassToUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var addClassMessage = ClassMessage.CreateAdd(_sequence.Next, Guid.NewGuid(), new[] { _warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, addClassMessage));
        }

        [Fact]
        public void RemoveClassFromUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var removeClassMessage = ClassMessage.CreateRemove(_sequence.Next, Guid.NewGuid(), new[] { _warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, removeClassMessage));
        }

        [Fact]
        public void RemoveNotExistingClassFromHero_ThrowsException_Test()
        {
            // Arrange
            var removeClassMessage = ClassMessage.CreateRemove(_sequence.Next, _playerId, new[] { _warriorClass });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, removeClassMessage));
        }

        [Fact]
        public void AddClassToHero_ResultsInAddedClass_Test()
        {
            // Arrange
            var addClassMessage = ClassMessage.CreateAdd(_sequence.Next, _playerId, new[] { _warriorClass });

            // Act
            var game = _quest.AddMessage(_gameId, addClassMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == _playerId).Classes);
            Assert.Contains(_warriorClass, game.Score.Heroes.First(x => x.Player.Id == _playerId).Classes);
        }

        [Fact]
        public void AddClassToHero_ThenUndoIt_ResultsInNoChange_Test()
        {
            // Arrange
            var addClassMessage = ClassMessage.CreateAdd(_sequence.Next, _playerId, new[] { _warriorClass });

            // Act
            _quest.AddMessage(_gameId, addClassMessage);
            var game = _quest.Undo(_gameId);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == _playerId).Classes);
        }

        [Fact]
        public void AddClassToHero_ThenRemoveIt_ResultsInNoChange_Test()
        {
            // Arrange
            var addClassMessage = ClassMessage.CreateAdd(_sequence.Next, _playerId, new[] { _warriorClass });
            var removeClassMessage = ClassMessage.CreateRemove(_sequence.Next, _playerId, new[] { _warriorClass });

            // Act
            _quest.AddMessage(_gameId, addClassMessage);
            var game = _quest.AddMessage(_gameId, removeClassMessage);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == _playerId).Classes);
        }

        [Fact]
        public void AddClassToHero_ThenRemoveIt_ThenUndoIt_ResultsInAddedClass_Test()
        {
            // Arrange
            var addClassMessage = ClassMessage.CreateAdd(_sequence.Next, _playerId, new[] { _warriorClass });
            var removeClassMessage = ClassMessage.CreateRemove(_sequence.Next, _playerId, new[] { _warriorClass });

            // Act
            _quest.AddMessage(_gameId, addClassMessage);
            _quest.AddMessage(_gameId, removeClassMessage);
            var game = _quest.Undo(_gameId);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == _playerId).Classes);
            Assert.Contains(_warriorClass, game.Score.Heroes.First(x => x.Player.Id == _playerId).Classes);
        }

        [Fact]
        public void AddClassToHeroTwice_ThrowsException_Test()
        {
            // Arrange
            var addClassMessage = ClassMessage.CreateAdd(_sequence.Next, _playerId, new[] { _warriorClass });
            var addAnotherClassMessage = ClassMessage.CreateAdd(_sequence.Next, _playerId, new[] { _warriorClass });

            // Act & Assert
            _quest.AddMessage(_gameId, addClassMessage);
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, addAnotherClassMessage));
        }

        [Fact]
        public void AddClassToHero_ThenSwitchClasses_ResultsInSwitchedClass_Test()
        {
            // Arrange
            var addClassMessage = ClassMessage.CreateAdd(_sequence.Next, _playerId, new[] { _warriorClass });
            var switchClassMessage = ClassMessage.Create(_sequence.Next, _playerId, new[] { _thiefClass }, new[] { _warriorClass });

            // Act
            _quest.AddMessage(_gameId, addClassMessage);
            var game = _quest.AddMessage(_gameId, switchClassMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First().Classes);
            Assert.Equal(_thiefClass, game.Score.Heroes.First().Classes.First());
        }
    }
}
