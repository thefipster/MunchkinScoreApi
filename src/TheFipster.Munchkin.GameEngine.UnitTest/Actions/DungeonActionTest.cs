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
            var dungeonMessage = DungeonMessage.CreateAdd(1, new[] { expectedDungeon });

            // Act
            var game = quest.AddMessage(gameId, dungeonMessage);

            // Assert
            Assert.Single(game.Score.Dungeons);
            Assert.Equal(expectedDungeon, game.Score.Dungeons.First());
        }

        [Fact]
        public void AddDungeonAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var dungeonMessage = DungeonMessage.CreateAdd(1, new[] { expectedDungeon });

            // Act
            quest.AddMessage(gameId, dungeonMessage);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Empty(game.Score.Dungeons);
        }

        [Fact]
        public void AddDungeonAndRemoveDungeonTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = DungeonMessage.CreateAdd(1, new[] { expectedDungeon });
            var removeDungeon = DungeonMessage.CreateRemove(2, new[] { expectedDungeon });

            // Act
            quest.AddMessage(gameId, addDungeon);
            var game = quest.AddMessage(gameId, removeDungeon);

            // Assert
            Assert.Empty(game.Score.Dungeons);
        }

        [Fact]
        public void AddDungeonAndRemoveDungeonAndUndoTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = DungeonMessage.CreateAdd(1, new[] { expectedDungeon });
            var removeDungeon = DungeonMessage.CreateRemove(2, new[] { expectedDungeon });

            // Act
            quest.AddMessage(gameId, addDungeon);
            quest.AddMessage(gameId, removeDungeon);
            var game = quest.Undo(gameId);

            // Assert
            Assert.Single(game.Score.Dungeons);
            Assert.Equal(expectedDungeon, game.Score.Dungeons.First());
        }

        [Fact]
        public void RemoveNotExistantDungeonThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var removeDungeon = DungeonMessage.CreateRemove(1, new[] { expectedDungeon });

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, removeDungeon));
        }

        [Fact]
        public void AddExistantDungeonThrowsExceptionTest()
        {
            // Arrange
            var quest = QuestFactory.CreateStored(out var gameStore, out var gameId);
            var addDungeon = DungeonMessage.CreateAdd(1, new[] { expectedDungeon });
            var addAnotherDungeon = DungeonMessage.CreateAdd(2, new[] { expectedDungeon });

            quest.AddMessage(gameId, addDungeon);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(gameId, addAnotherDungeon));
        }
    }
}
