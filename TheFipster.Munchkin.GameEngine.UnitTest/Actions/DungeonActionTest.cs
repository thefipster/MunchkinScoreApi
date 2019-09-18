using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.UnitTest.Helper;
using TheFipster.Munchkin.GameStorageVolatile;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Actions
{
    public class DungeonActionTest
    {
        [Fact]
        public void AddDungeonTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var expectedDungeon = "My Dungeon";
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
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var expectedDungeon = "My Dungeon";
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
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var expectedDungeon = "My Dungeon";
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
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var expectedDungeon = "My Dungeon";
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
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var expectedDungeon = "My Dungeon";
            var removeDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Remove);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(removeDungeon));
        }

        [Fact]
        public void AddExistantDungeonThrowsExceptionTest()
        {
            // Arrange
            var gameStore = new MockedGameStore();
            var quest = QuestFactory.CreateAndStore(gameStore, out var gameId);
            var expectedDungeon = "My Dungeon";
            var addDungeon = new DungeonMessage(gameId, expectedDungeon, Modifier.Add);

            quest.AddMessage(addDungeon);

            // Act & Assert
            Assert.Throws<InvalidActionException>(() => quest.AddMessage(addDungeon));
        }
    }
}
