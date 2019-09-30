using TheFipster.Munchkin.GameEvents;
using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest.Messages
{
    public class EndMessageTest
    {
        [Fact]
        public void GetSpecificTypeFromBaseClass_ResultsInSpecificClassName_Test()
        {
            // Arrange
            var endMessage = new EndMessage();
            var expectedType = endMessage.GetType().Name;

            // Act
            var actualType = endMessage.Type;

            // Assert
            Assert.Equal(expectedType, actualType);
        }
    }
}
