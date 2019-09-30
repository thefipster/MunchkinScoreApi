using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameStorage;

namespace TheFipster.Munchkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SeedExtensions
    {
        public static void SynchronizeSeedData(
            this IHostingEnvironment env, 
            IConfiguration config, 
            ICardStore cardStore, 
            IMonsterStore monsterStore)
        {
            sync(config, cardStore, CardCollection.Classes);
            sync(config, cardStore, CardCollection.Curses);
            sync(config, cardStore, CardCollection.Dungeons);
            sync(config, cardStore, CardCollection.Races);
            sync(config, cardStore, CardCollection.Genders);
            sync(config, cardStore, CardCollection.FightResults);
            sync(config, cardStore, CardCollection.LevelIncreaseReasons);
            sync(config, cardStore, CardCollection.LevelDecreaseReasons);
            sync(config, cardStore, CardCollection.FightStarters);

            syncMonsters(config, monsterStore);
        }

        private static void syncMonsters(
            IConfiguration config, 
            IMonsterStore monsterStore)
        {
            var MonsterNames = config.GetArray(Monster.Collection);
            foreach (var name in MonsterNames)
            {
                var monster = new Monster(name);
                monsterStore.Upsert(monster);
            }
        }

        private static void sync(
            IConfiguration config, 
            ICardStore cardStore, 
            string collectionName)
        {
            var cards = config.GetArray(collectionName);
            cardStore.Sync(collectionName, cards);
        }
    }
}
