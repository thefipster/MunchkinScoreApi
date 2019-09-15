using TheFipster.Munchkin.GameEngine.Messages;
using TheFipster.Munchkin.GameEngine.Model;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest
{
    public class PlayerMessageTest
    {
        [Fact]
        public void AddPlayerMessageTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedPlayerName = "Dr. Jekyll";
            var expectedPlayerGender = "male";

            var addPlayer = new PlayerAddMessage(expectedPlayerName, expectedPlayerGender);

            // Act
            game.AddMessage(addPlayer);

            // Assert
            Assert.Single(game.State.Players);
            Assert.Contains(game.State.Players, player => player.Name == expectedPlayerName);
            Assert.Contains(game.State.Players, player => player.Gender == expectedPlayerGender);
        }

        [Fact]
        public void AddPlayerMessageAndUndoTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedPlayerName = "Dr. Jekyll";
            var expectedPlayerGender = "male";

            var addPlayer = new PlayerAddMessage(expectedPlayerName, expectedPlayerGender);

            // Act
            game.AddMessage(addPlayer);
            game.Undo();

            // Assert
            Assert.Empty(game.State.Players);
        }

        [Fact]
        public void AddTwoPlayersTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedFirstPlayerName = "Dr. Jekyll";
            var expectedSecondPlayerName = "Mr. Hyde";
            var playerGender = "male";

            var firstPlayer = new PlayerAddMessage(expectedFirstPlayerName, playerGender);
            var secondPlayer = new PlayerAddMessage(expectedSecondPlayerName, playerGender);

            // Act
            game.AddMessage(firstPlayer);
            game.AddMessage(secondPlayer);

            // Assert
            Assert.Equal(2, game.State.Players.Count);
            Assert.Contains(game.State.Players, player => player.Name == expectedFirstPlayerName);
            Assert.Contains(game.State.Players, player => player.Name == expectedSecondPlayerName);
        }

        [Fact]
        public void AddTwoPlayersAndUndoOnceTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedFirstPlayerName = "Dr. Jekyll";
            var expectedSecondPlayerName = "Mr. Hyde";
            var playerGender = "male";

            var firstPlayer = new PlayerAddMessage(expectedFirstPlayerName, playerGender);
            var secondPlayer = new PlayerAddMessage(expectedSecondPlayerName, playerGender);

            // Act
            game.AddMessage(firstPlayer);
            game.AddMessage(secondPlayer);
            game.Undo();

            // Assert
            Assert.Single(game.State.Players);
            Assert.Contains(game.State.Players, player => player.Name == expectedFirstPlayerName);
        }

        [Fact]
        public void AddTwoPlayersAndUndoTwiceTest()
        {
            // Arrange
            var game = new MunchkinGame();
            var expectedFirstPlayerName = "Dr. Jekyll";
            var expectedSecondPlayerName = "Mr. Hyde";
            var playerGender = "male";

            var firstPlayer = new PlayerAddMessage(expectedFirstPlayerName, playerGender);
            var secondPlayer = new PlayerAddMessage(expectedSecondPlayerName, playerGender);

            // Act
            game.AddMessage(firstPlayer);
            game.AddMessage(secondPlayer);
            game.Undo();
            game.Undo();

            // Assert
            Assert.Empty(game.State.Players);
        }

        [Fact]
        public void AddAndRemovePlayer()
        {
            // Arrange
            var game = new MunchkinGame();
            var playerName = "Dr. Jekyll";
            var playerGender = "male";
            var player = new Player(playerName, playerGender);

            var addPlayer = new PlayerAddMessage(player);
            var removePlayer = new PlayerRemoveMessage(player);

            // Act
            game.AddMessage(addPlayer);
            game.AddMessage(removePlayer);

            // Assert
            Assert.Empty(game.State.Players);
        }

        [Fact]
        public void AddAndRemoveAndUndoPlayer()
        {
            // Arrange
            var game = new MunchkinGame();
            var playerName = "Dr. Jekyll";
            var playerGender = "male";
            var player = new Player(playerName, playerGender);

            var addPlayer = new PlayerAddMessage(player);
            var removePlayer = new PlayerRemoveMessage(player);

            // Act
            game.AddMessage(addPlayer);
            game.AddMessage(removePlayer);
            game.Undo();

            // Assert
            Assert.Single(game.State.Players);
            Assert.Contains(game.State.Players, p => p.Name == playerName);
        }
    }
}
