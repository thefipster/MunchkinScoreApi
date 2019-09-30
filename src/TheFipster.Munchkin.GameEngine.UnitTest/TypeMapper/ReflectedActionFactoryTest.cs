using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameEvents;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.TypeMapper
{
    public class ReflectedActionFactoryTest
    {
        [Fact]
        public void CreateActionFromStartMessage_ResultsInStartAction_Test()
        {
            // Arrance
            var actionFactory = new Inventory();
            var startMessage = StartMessage.Create(1);
            var game = new Game();

            // Act
            var action = actionFactory.GetActionFromMessage(startMessage, game);

            // Assert
            Assert.Equal("StartAction", action.GetType().Name);
        }
    }
}
