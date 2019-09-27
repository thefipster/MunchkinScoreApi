using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class DungeonActionTest
    {
        private string epicDungeon = "EpicDungeon";
        private string mightyDungeon = "MightyDungeon";

        [Fact]
        public void AddDungeon_ResultsInAddedDungeon_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var dungeonMessage = DungeonMessage.CreateAdd(1, new[] { epicDungeon });

            // Act
            var game = quest.AddMessage(gameId, dungeonMessage);

            // Assert
            Assert.Single(game.Score.Dungeons);
            Assert.Equal(epicDungeon, game.Score.Dungeons.First());
        }

        [Fact]
        public void AddDungeon_ThenUndoIt_ResultsInNoChange_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var dungeonMessage = DungeonMessage.CreateAdd(1, new[] { epicDungeon });

            // Act
            quest.AddMessage(gameId, dungeonMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Empty(game.Score.Dungeons);
        }

        [Fact]
        public void AddDungeon_ThenRemoveIt_ResultsInNoChange_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = DungeonMessage.CreateAdd(1, new[] { epicDungeon });
            var removeDungeon = DungeonMessage.CreateRemove(2, new[] { epicDungeon });

            // Act
            quest.AddMessage(gameId, addDungeon);
            var game = quest.AddMessage(gameId, removeDungeon);

            // Assert
            Assert.Empty(game.Score.Dungeons);
        }

        [Fact]
        public void AddDungeon_ThenRemove_ThenUndoIt_ResultsInAddedDungeon_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = DungeonMessage.CreateAdd(1, new[] { epicDungeon });
            var removeDungeon = DungeonMessage.CreateRemove(2, new[] { epicDungeon });

            // Act
            quest.AddMessage(gameId, addDungeon);
            quest.AddMessage(gameId, removeDungeon);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Single(game.Score.Dungeons);
            Assert.Equal(epicDungeon, game.Score.Dungeons.First());
        }

        [Fact]
        public void AddDungeon_ThenSwitch_ResultsInSwitchedDungeon_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = DungeonMessage.CreateAdd(1, new[] { epicDungeon });
            var switchDungeon = DungeonMessage.Create(2, new[] { mightyDungeon }, new[] { epicDungeon });

            // Act
            quest.AddMessage(gameId, addDungeon);
            var game = quest.AddMessage(gameId, switchDungeon);

            // Assert
            Assert.Single(game.Score.Dungeons);
            Assert.Equal(mightyDungeon, game.Score.Dungeons.First());
        }

        [Fact]
        public void RemoveNotExistantDungeon_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var removeDungeon = DungeonMessage.CreateRemove(1, new[] { epicDungeon });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeDungeon));
        }

        [Fact]
        public void AddExistantDungeon_ThrowsException_Test()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = DungeonMessage.CreateAdd(1, new[] { epicDungeon });
            var addAnotherDungeon = DungeonMessage.CreateAdd(2, new[] { epicDungeon });

            quest.AddMessage(gameId, addDungeon);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addAnotherDungeon));
        }
    }
}
