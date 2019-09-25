using System;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GamePersistance;
using TheFipster.Munchkin.GameStorageVolatile;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Helper
{
    public class QuestFactory
    {
        public static Quest Create(IGameStore gameStore)
        {
            var actionFactory = new PrimitiveActionFactory();
            return new Quest(gameStore, actionFactory);
        }

        public static Quest CreateStored(out IGameStore gameStore, out Guid gameId)
        {
            gameStore = new MockedGameStore();
            var quest = Create(gameStore);
            gameId = quest.StartJourney();
            return quest;
        }

        public static Quest CreateStarted(out IGameStore gameStore, out Guid gameId)
        {
            var quest = CreateStored(out gameStore, out gameId);
            startQuest(quest, gameId);
            return quest;
        }

        public static Quest CreateStartedWithMaleHero(out IGameStore gameStore, out Guid gameId, out Guid playerId)
        {
            var quest = CreateStarted(out gameStore, out gameId);
            addMaleHeroToQuest(quest, gameId, out playerId);
            return quest;
        }

        private static void startQuest(Quest quest, Guid gameId)
        {
            var startMsg = new StartMessage();
            quest.AddMessage(gameId, startMsg);
        }

        private static void addMaleHeroToQuest(Quest quest, Guid gameId, out Guid playerId)
        {
            var player = PlayerFactory.CreateMale("John Doe");
            var heroAddMsg = new PlayerMessage(player, Modifier.Add);
            quest.AddMessage(gameId, heroAddMsg);
            playerId = player.Id;
        }
    }
}
