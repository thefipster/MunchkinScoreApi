using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Events;
using Xunit;

namespace TheFipster.Munchkin.Gaming.Domain.UnitTest.Events
{
    public class GameMessageTest
    {
        [Fact]
        public void GetSpecificTypeFromBaseClass_ResultsInSpecificClassName_Test()
        {
            // Arrange
            var gameMsg = new EndMessage() as GameMessage;
            var expectedType = typeof(EndMessage).Name;

            // Act
            var actualType = gameMsg.Type;

            // Assert
            Assert.Equal(expectedType, actualType);
        }
    }
}
