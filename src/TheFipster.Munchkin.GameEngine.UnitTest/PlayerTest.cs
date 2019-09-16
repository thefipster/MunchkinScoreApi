using System;
using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest
{
    public class PlayerTest
    {
        [Fact]
        public void NewPlayerHasLevelOneTest()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            Assert.Equal(1, player.Level);
        }

        [Fact]
        public void NewPlayerWithNameAndGenderTest()
        {
            // Arrange
            var expectedName = "Dr. Jekyll";
            var expectedGender = "male";

            // Act
            var player = new Player(expectedName, expectedGender);

            // Assert
            Assert.Equal(expectedName, player.Name);
            Assert.Equal(expectedGender, player.Gender);
        }

        [Fact]
        public void NewPlayerHasId()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            Assert.NotEqual(Guid.Empty, player.Id);
        }

        [Fact]
        public void NewPlayerIsInitialized()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            Assert.NotNull(player.Races);
            Assert.NotNull(player.Classes);
        }
    }
}
