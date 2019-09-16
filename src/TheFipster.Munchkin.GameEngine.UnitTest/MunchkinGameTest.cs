using System;
using TheFipster.Munchkin.GameDomain.Exceptions;
using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest
{
    public class MunchkinGameTest
    {
        [Fact]
        public void NewGameIsInitialized()
        {
            var game = new MunchkinGame();

            Assert.NotNull(game.State);
            Assert.Empty(game.Protocol);
            Assert.NotEqual(Guid.Empty, game.Id);
        }

        [Fact]
        public void UndoWithEmptyProtocolTest()
        {
            // Arrange
            var game = new MunchkinGame();

            // Act & Assert
            Assert.Throws<ProtocolEmptyException>(() => game.Undo());
        }
    }
}
