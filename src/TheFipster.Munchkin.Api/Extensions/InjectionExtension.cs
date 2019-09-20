using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.GameDomain;
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
            services.AddSingleton<IActionFactory>(new PrimitiveActionFactory());
            services.AddSingleton<IInitializationCache>(new InitCodeCache());
            services.AddSingleton<IInitCodePollService>(new InitCodePollService());

            services.AddSingleton<IRepository<Game>>(new Repository<Game>());
            services.AddTransient<IGameStore, GameStore>();

            services.AddSingleton<IRepository<GameMaster>>(new Repository<GameMaster>());
            services.AddTransient<IPlayerStore, PlayerStore>();

            services.AddTransient<IQuest, Quest>();

            return services;
        }
    }
}
    