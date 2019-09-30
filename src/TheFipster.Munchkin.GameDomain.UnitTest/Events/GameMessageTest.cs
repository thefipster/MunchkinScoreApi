using TheFipster.Munchkin.GameDomain.Events;
using TheFipster.Munchkin.GameEvents;
using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest.Events
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
