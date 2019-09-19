using Xunit;

namespace TheFipster.Munchkin.GameOrchestrator.UnitTest
{
    public class InitializationCacheTest
    {
        [Fact]
        public void GenerateAndCheckTest()
        {
            // Arrange
            var cache = new InitCodeCache();

            // Act
            var initCode = cache.GenerateInitCode();

            // Assert
            Assert.True(cache.CheckInitCode(initCode));
        }

        [Fact]
        public void CheckInvalidCodeTest()
        {
            // Arrange
            var invalidKey = "ABCDEF";
            var cache = new InitCodeCache();

            // Act & Assert
            Assert.False(cache.CheckInitCode(invalidKey));
        }
    }
}
