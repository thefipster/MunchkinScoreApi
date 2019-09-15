using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheFipster.Munchkin.GameEngine.Exceptions;
using TheFipster.Munchkin.GameEngine.Messages;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest
{
    public class LevelMessageTest
    {
        [Fact]
        public void IncreaseLevelFromUnknownPlayerRaisesExceptionTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var increaseLevel = new LevelIncreaseMessage(Guid.NewGuid(), 1);

            // Act & Assert
            Assert.Throws<UnknownPlayerException>(() => game.AddMessage(increaseLevel));
        }

        [Fact]
        public void IncreaseLevelTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var addPlayer = new PlayerAddMessage("Dr. Jekyll", "male");
            var increaseLevel = new LevelIncreaseMessage(addPlayer.Player.Id, 1);

            // Act 
            game.AddMessage(addPlayer);
            game.AddMessage(increaseLevel);

            // Assert
            Assert.Equal(2, game.State.Players.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseLevelTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var addPlayer = new PlayerAddMessage("Dr. Jekyll", "male");
            var increaseLevel = new LevelIncreaseMessage(addPlayer.Player.Id, 2);
            var decreaseLevel = new LevelDecreaseMessage(addPlayer.Player.Id, 1);

            // Act 
            game.AddMessage(addPlayer);
            game.AddMessage(increaseLevel);
            game.AddMessage(decreaseLevel);

            // Assert
            Assert.Equal(2, game.State.Players.First().Level);
        }

        [Fact]
        public void IncreaseAndDecreaseAndUndoLevelTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var addPlayer = new PlayerAddMessage("Dr. Jekyll", "male");
            var increaseLevel = new LevelIncreaseMessage(addPlayer.Player.Id, 2);
            var decreaseLevel = new LevelDecreaseMessage(addPlayer.Player.Id, 1);

            // Act 
            game.AddMessage(addPlayer);
            game.AddMessage(increaseLevel);
            game.AddMessage(decreaseLevel);
            game.Undo();

            // Assert
            Assert.Equal(3, game.State.Players.First().Level);
        }

        [Fact]
        public void IncreaseLevelAndUndoTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var addPlayer = new PlayerAddMessage("Dr. Jekyll", "male");
            var increaseLevel = new LevelIncreaseMessage(addPlayer.Player.Id, 1);

            // Act 
            game.AddMessage(addPlayer);
            game.AddMessage(increaseLevel);
            game.Undo();

            // Assert
            Assert.Equal(1, game.State.Players.First().Level);
        }
    }
}
