using TheFipster.Munchkin.GameEngine.Messages;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest
{
    public class DungeonMessageTest
    {
        [Fact]
        public void AddDungeonTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedDungeon = "Fligenfallen Dungeon";

            var addDungeon = new DungeonAddMessage(expectedDungeon);

            // Act
            game.AddMessage(addDungeon);

            // Assert
            Assert.Single(game.State.Dungeons);
            Assert.Contains(game.State.Dungeons, dungeon => dungeon == expectedDungeon);
        }

        [Fact]
        public void AddAndUndoDungeonTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedDungeon = "Fligenfallen Dungeon";

            var addDungeon = new DungeonAddMessage(expectedDungeon);

            // Act
            game.AddMessage(addDungeon);
            game.Undo();

            // Assert
            Assert.Empty(game.State.Dungeons);
        }

        [Fact]
        public void AddAndRemoveDungeonTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedDungeon = "Fligenfallen Dungeon";

            var addDungeon = new DungeonAddMessage(expectedDungeon);
            var removeDungeon = new DungeonRemoveMessage(expectedDungeon);

            // Act
            game.AddMessage(addDungeon);
            game.AddMessage(removeDungeon);

            // Assert
            Assert.Empty(game.State.Dungeons);
        }

        [Fact]
        public void AddAndRemoveAndUndoDungeonTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedDungeon = "Fligenfallen Dungeon";

            var addDungeon = new DungeonAddMessage(expectedDungeon);
            var removeDungeon = new DungeonRemoveMessage(expectedDungeon);

            // Act
            game.AddMessage(addDungeon);
            game.AddMessage(removeDungeon);
            game.Undo();

            // Assert
            Assert.Single(game.State.Dungeons);
            Assert.Contains(game.State.Dungeons, dungeon => dungeon == expectedDungeon);
        }
    }
}
