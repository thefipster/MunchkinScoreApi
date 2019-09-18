using System;
using TheFipster.Munchkin.GameStorageVolatile;
using Xunit;

namespace TheFipster.Munchkin.GameEngine.UnitTest
{
    public class QuestTest
    {
        [Fact]
        public void StartJourneyGeneratesGameAndReturnsGameIdTest()
        {
            var gameStore = new MockedGameStore();
            var actionFactory = new PrimitiveActionFactory();

            var quest = new Quest(gameStore, actionFactory);

            var gameId = quest.StartJourney();

            Assert.NotEqual(Guid.Empty, gameId);
            Assert.NotNull(gameStore.Get(gameId));
        }
    }
}
