using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Abstractions;
using TheFipster.Munchkin.GameEngine;
using TheFipster.Munchkin.GameOrchestrator;
using TheFipster.Munchkin.GamePersistance;
using TheFipster.Munchkin.GameStorageLite;

namespace TheFipster.Munchkin.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class InjectionExtension
    {
        public static IServiceCollection AddDependecies(this IServiceCollection services)
        {
            services.AddSingleton<ITypeInventory>(new ActionInventory());
            services.AddSingleton<IActionFactory>(new ReflectedActionFactory(
                getActionInventoryFrom(services)));

            services.AddSingleton<IInitializationCache>(new InitCodeCache());
            services.AddSingleton<IInitCodePollService>(new InitCodePollService());
            services.AddSingleton<IGameStatePollService>(new GameStatePollService());

            services.AddSingleton<IRepository<Game>>(new Repository<Game>());
            services.AddTransient<IGameStore, GameStore>();

            services.AddSingleton<IRepository<GameMaster>>(new Repository<GameMaster>());
            services.AddTransient<IPlayerStore, PlayerStore>();

            services.AddSingleton<IRepository<CardCollection>>(new Repository<CardCollection>());
            services.AddTransient<ICardStore, CardStore>();

            services.AddSingleton<IRepository<Monster>>(new Repository<Monster>());
            services.AddTransient<IMonsterStore, MonsterStore>();

            services.AddTransient<IQuest, Quest>();

            return services;
        }

        private static ITypeInventory getActionInventoryFrom(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<ITypeInventory>();
        }
    }
}
    