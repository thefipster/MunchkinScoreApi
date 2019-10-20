using System;
using System.Linq;
using TheFipster.Munchkin.Gaming.Abstractions;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;
using TheFipster.Munchkin.TestFactory;
using Xunit;

namespace TheFipster.Munchkin.Gaming.Events.UnitTest.Actions
{
    public class RaceActionTest
    {
        private string _humanRace = "Human";
        private string _dwarfRace = "Dwarf";

        private IQuest _quest;
        private Guid _gameId;
        private Guid _playerId;
        private Sequence _sequence;

        public RaceActionTest()
        {
            _quest = QuestFactory.CreateStartedWithMaleHero(
                out _, 
                out _gameId, 
                out _playerId, 
                out _sequence);
        }

        [Fact]
        public void AddRaceToUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var addRaceMessage = RaceMessage.CreateAdd(_sequence.Next, Guid.NewGuid(), new[] { _humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, addRaceMessage));
        }

        [Fact]
        public void RemoveRaceFromUnknownHero_ThrowsException_Test()
        {
            // Arrange
            var removeRaceMessage = RaceMessage.CreateRemove(_sequence.Next, Guid.NewGuid(), new[] { _humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, removeRaceMessage));
        }

        [Fact]
        public void RemoveNotExistingRaceFromHero_ThrowsException_Test()
        {
            // Arrange
            var removeRaceMessage = RaceMessage.CreateRemove(_sequence.Next, _playerId, new[] { _humanRace });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, removeRaceMessage));
        }

        [Fact]
        public void AddRaceToHero_ResultsInAddedRace_Test()
        {
            // Arrange
            var addRaceMessage = RaceMessage.CreateAdd(_sequence.Next, _playerId, new[] { _humanRace });

            // Act
            var game = _quest.AddMessage(_gameId, addRaceMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == _playerId).Races);
            Assert.Contains(_humanRace, game.Score.Heroes.First(x => x.Player.Id == _playerId).Races);
        }

        [Fact]
        public void AddRaceToHero_ThenUndoIt_ResultsInNoChange_Test()
        {
            // Arrange
            var addRaceMessage = RaceMessage.CreateAdd(_sequence.Next, _playerId, new[] { _humanRace });

            // Act
            _quest.AddMessage(_gameId, addRaceMessage);
            var game = _quest.Undo(_gameId);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == _playerId).Races);
        }

        [Fact]
        public void AddRaceToHero_ThenRemoveIt_ResultsInNoChange_Test()
        {
            // Arrange
            var addRaceMessage = RaceMessage.CreateAdd(_sequence.Next, _playerId, new[] { _humanRace });
            var removeRaceMessage = RaceMessage.CreateRemove(_sequence.Next, _playerId, new[] { _humanRace });

            // Act
            _quest.AddMessage(_gameId, addRaceMessage);
            var game = _quest.AddMessage(_gameId, removeRaceMessage);

            // Assert
            Assert.Empty(game.Score.Heroes.First(x => x.Player.Id == _playerId).Races);
        }

        [Fact]
        public void AddRaceToHero_ThenSwitchIt_ResultsInSwitchedRace_Test()
        {
            // Arrange
            var addRaceMessage = RaceMessage.CreateAdd(_sequence.Next, _playerId, new[] { _humanRace });
            var switchRaceMessage = RaceMessage.Create(_sequence.Next, _playerId, new[] { _dwarfRace }, new[] { _humanRace });

            // Act
            _quest.AddMessage(_gameId, addRaceMessage);
            var game = _quest.AddMessage(_gameId, switchRaceMessage);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == _playerId).Races);
            Assert.Equal(_dwarfRace, game.Score.Heroes.First(x => x.Player.Id == _playerId).Races.First());
        }

        [Fact]
        public void AddRaceToHero_ThenRemoveIt_ThenUndoIt_ResultsInAddedRace_Test()
        {
            // Arrange
            var addRaceMessage = RaceMessage.CreateAdd(_sequence.Next, _playerId, new[] { _humanRace });
            var removeRaceMessage = RaceMessage.CreateRemove(_sequence.Next, _playerId, new[] { _humanRace });

            // Act
            _quest.AddMessage(_gameId, addRaceMessage);
            _quest.AddMessage(_gameId, removeRaceMessage);
            var game = _quest.Undo(_gameId);

            // Assert
            Assert.Single(game.Score.Heroes.First(x => x.Player.Id == _playerId).Races);
            Assert.Contains(_humanRace, game.Score.Heroes.First(x => x.Player.Id == _playerId).Races);
        }

        [Fact]
        public void AddRaceToHeroTwice_ThrowsException_Test()
        {
            // Arrange
            var addRaceMessage = RaceMessage.CreateAdd(_sequence.Next, _playerId, new[] { _humanRace });
            var addAnotherRaceMessage = RaceMessage.CreateAdd(_sequence.Next, _playerId, new[] { _humanRace });

            // Act & Assert
            _quest.AddMessage(_gameId, addRaceMessage);
            Assert.Throws<InvalidActionException>(() => _quest.AddMessage(_gameId, addAnotherRaceMessage));
        }
    }
}
