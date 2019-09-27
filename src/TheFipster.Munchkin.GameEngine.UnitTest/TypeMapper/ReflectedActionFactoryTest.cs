using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest.TypeMapper
{
    public class ReflectedActionFactoryTest
    {
        [Fact]
        public void CreateActionFromStartMessage_ResultsInStartAction_Test()
        {
            // Arrance
            var actionInventory = new ActionInventory();
            var actionFactory = new ReflectedActionFactory(actionInventory);
            var startMessage = StartMessage.Create(1);
            var game = new Game();

            // Act
            var action = actionFactory.CreateActionFrom(startMessage, game);

            // Assert
            Assert.Equal("StartAction", action.GetType().Name);
        }
    }
}
