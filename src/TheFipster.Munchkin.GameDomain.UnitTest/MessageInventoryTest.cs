using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest
{
    public class MessageInventoryTest
    {
        [Fact]
        public void LoadAllMessagesTest()
        {
            // Arrange
            var inventory = new MessageInventory();

            // Act
            var types = inventory.Get();

            // Assert
            Assert.NotEmpty(types);
        }
    }
}
