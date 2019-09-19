using System;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Helper
{
    public class QuestFactory
    {
        public static Quest Create(IGameStore gameStore)
        {
            var actionFactory = new PrimitiveActionFactory();
            return new Quest(gameStore, actionFactory);
        }

        public static Quest CreateStored(IGameStore gameStore, out Guid gameId)
        {
            var quest = Create(gameStore);
            gameId = quest.StartJourney();
            return quest;
        }

        public static Quest CreateStarted(IGameStore gameStore, out Guid gameId)
        {
            var quest = CreateStored(gameStore, out gameId);
            startQuest(quest, gameId);
            return quest;
        }

        public static Quest CreateStartedWithMaleHero(IGameStore gameStore, out Guid gameId, out Guid playerId)
        {
            var quest = CreateStarted(gameStore, out gameId);
            addMaleHeroToQuest(quest, gameId, out playerId);
            return quest;
        }

        private static void startQuest(Quest quest, Guid gameId)
        {
            var startMsg = new StartMessage(gameId);
            quest.AddMessage(startMsg);
        }

        private static void addMaleHeroToQuest(Quest quest, Guid gameId, out Guid playerId)
        {
            var hero = HeroFactory.CreateMaleHero("John Doe");
            var heroAddMsg = new HeroMessage(gameId, hero, Modifier.Add);
            quest.AddMessage(heroAddMsg);
            playerId = hero.Player.Id;
        }
    }
}
