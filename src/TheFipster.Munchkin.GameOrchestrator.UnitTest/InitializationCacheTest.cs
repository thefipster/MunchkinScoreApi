using Xunit;

namespace TheFipster.Munchkin.GameOrchestrator.UnitTest
{
    public class InitializationCacheTest
    {
        [Fact]
        public void GenerateInitCode_ThenCheckIt_ResultsInTrueResponse_Test()
        {
            // Arrange
            var cache = new InitCodeCache();

            // Act
            var initCode = cache.GenerateInitCode();

            // Assert
            Assert.True(cache.CheckInitCode(initCode));
        }

        [Fact]
        public void CheckInitCodeWithInvalidCode_ResultsInFalseResponse_Test()
        {
            // Arrange
            var invalidKey = "ABCDEF";
            var cache = new InitCodeCache();

            // Act & Assert
            Assert.False(cache.CheckInitCode(invalidKey));
        }
    }
}
