using System;
using TheFipster.Munchkin.GamePersistance;
using TheFipster.Munchkin.GameStorageVolatile;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Helper
{
    public class QuestFactory
    {
        public static Quest Create() =>
            Create(new MockedGameStore());

        public static Quest Create(IGameStore gameStore)
        {
            var actionFactory = new PrimitiveActionFactory();
            return new Quest(gameStore, actionFactory);
        }

        public static Quest CreateAndStore(IGameStore gameStore, out Guid gameId)
        {
            var quest = Create(gameStore);
            gameId = quest.StartJourney();
            return quest;
        }
    }
}
