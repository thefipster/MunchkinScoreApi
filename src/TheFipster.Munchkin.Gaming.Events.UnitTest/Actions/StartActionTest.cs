using System;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;
using TheFipster.Munchkin.Gaming.Engine;
using TheFipster.Munchkin.TestFactory;
using Xunit;

namespace TheFipster.Munchkin.Gaming.Events.UnitTest.Actions
{
    public class StartActionTest
    {
        private Quest _quest;
        private Guid _gameId;

        public StartActionTest()
        {
            _quest = QuestFactory.CreateStored(
                out _, 
                out _gameId);
        }

        [Fact]
        public void StartGame_ResultsInBeginTimestampBeginSet_Test()
        {
            // Arrange
            var start = StartMessage.Create(1);

            // Act
            var game = _quest.AddMessage(_gameId, start);

            // Assert
            Assert.NotNull(game.Score.Begin);
            Assert.Equal(start.Timestamp, game.Score.Begin);
        }

        [Fact]
        public void StartGame_ThenUndoIt_ResultsInTimestampBeingSetToNull_Test()
        {
            // Arrange
            var start = StartMessage.Create(1);

            // Act
            _quest.AddMessage(_gameId, start);
            var game = _quest.Undo(_gameId);

            // Assert
            Assert.Null(game.Score.Begin);
        }

        [Fact]
        public void StartGameTwice_ThrowsException_Test()
        {
            // Arrange
            var start = StartMessage.Create(1);
            var secondStart = StartMessage.Create(2);

            // Act & Assert
            _quest.AddMessage(_gameId, start);
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, secondStart));
        }
    }
}
