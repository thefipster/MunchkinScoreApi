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
            var dungeonMessage = new DungeonMessage(gameId, expectedDungeon, Modifier.Add);

            // Act
            var score = quest.AddMessage(dungeonMessage);

            // Assert
            Assert.Single(score.Dungeons);
            Assert.Equal(expectedDungeon, score.Dungeons.First());
        }

        [Fact]
        public void AddDungeonAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var dungeonMessage = new DungeonMessage(gameId, expectedDungeon, Modifier.Add);

            // Act
            quest.AddMessage(dungeonMessage);
            var score = quest.Undo(gameId);

            // Assert
            Assert.Empty(score.Dungeons);
        }

        [Fact]
        public void AddDungeonAndRemoveDungeonTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Add);
            var removeDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Remove);

            // Act
            quest.AddMessage(addDungeon);
            var score = quest.AddMessage(removeDungeon);

            // Assert
            Assert.Empty(score.Dungeons);
        }

        [Fact]
        public void AddDungeonAndRemoveDungeonAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Add);
            var removeDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Remove);

            // Act
            quest.AddMessage(addDungeon);
            quest.AddMessage(removeDungeon);
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
            var removeDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeDungeon));
        }

        [Fact]
        public void AddExistantDungeonThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Add);

            quest.AddMessage(addDungeon);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addDungeon));
        }
    }
}
