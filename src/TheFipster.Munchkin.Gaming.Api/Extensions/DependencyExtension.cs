using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Gaming.Abstractions;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Engine;
using TheFipster.Munchkin.Gaming.Engine.Polling;
using TheFipster.Munchkin.Gaming.Events;
using TheFipster.Munchkin.Gaming.Polling;
using TheFipster.Munchkin.Gaming.Storage;
using TheFipster.Munchkin.Gaming.Storage.LiteDb;

namespace TheFipster.Munchkin.Gaming.Api.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddDependecies(this IServiceCollection services)
        {
            services.AddSingleton<IEventInventory>(new Inventory());

            services.AddSingleton<IInitCodePollService>(new InitCodePollService());
            services.AddSingleton<IGameStatePollService>(new GameStatePollService());

            services.AddSingleton<IRepository<Game>>(new Repository<Game>());
            services.AddTransient<IGameStore, GameStore>();

            services.AddSingleton<IRepository<GameMaster>>(new Repository<GameMaster>());
            services.AddTransient<IPlayerStore, PlayerStore>();

            services.AddTransient<IQuest, Quest>();

            return services;
        }
    }
}
