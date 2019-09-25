using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class DungeonActionTest
    {
        private string expectedDungeon = "My Dungeon";

        [Fact]
        public void AddDungeonTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var dungeonMessage = new DungeonMessage(expectedDungeon, Modifier.Add);

            // Act
            var score = quest.AddMessage(gameId, dungeonMessage);

            // Assert
            Assert.Single(score.Dungeons);
            Assert.Equal(expectedDungeon, score.Dungeons.First());
        }

        [Fact]
        public void AddDungeonAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var dungeonMessage = new DungeonMessage(expectedDungeon, Modifier.Add);

            // Act
            quest.AddMessage(gameId, dungeonMessage);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Empty(score.Dungeons);
        }

        [Fact]
        public void AddDungeonAndRemoveDungeonTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = new DungeonMessage(expectedDungeon, Modifier.Add);
            var removeDungeon = new DungeonMessage(expectedDungeon, Modifier.Remove);

            // Act
            quest.AddMessage(gameId, addDungeon);
            var score = quest.AddMessage(gameId, removeDungeon);

            // Assert
            Assert.Empty(score.Dungeons);
        }

        [Fact]
        public void AddDungeonAndRemoveDungeonAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = new DungeonMessage(expectedDungeon, Modifier.Add);
            var removeDungeon = new DungeonMessage(expectedDungeon, Modifier.Remove);

            // Act
            quest.AddMessage(gameId, addDungeon);
            quest.AddMessage(gameId, removeDungeon);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Single(score.Dungeons);
            Assert.Equal(expectedDungeon, score.Dungeons.First());
        }

        [Fact]
        public void RemoveNotExistantDungeonThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var removeDungeon = new DungeonMessage(expectedDungeon, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeDungeon));
        }

        [Fact]
        public void AddExistantDungeonThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = new DungeonMessage(expectedDungeon, Modifier.Add);

            quest.AddMessage(gameId, addDungeon);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addDungeon));
        }
    }
}
