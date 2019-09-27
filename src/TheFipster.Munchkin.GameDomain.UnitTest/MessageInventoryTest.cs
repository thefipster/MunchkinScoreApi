using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest
{
    public class MessageInventoryTest
    {
        [Fact]
        public void GetAllMessageTypesInAssembly_ResultsInNotEmptyMessageTypeList_Test()
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
