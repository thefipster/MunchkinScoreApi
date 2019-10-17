using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.DependencyInjection;
using TheFipster.Munchkin.StashDatabase;
using TheFipster.Munchkin.StashDomain;
using TheFipster.Munchkin.StashRepository.Abstractions;
using TheFipster.Munchkin.StashRepository.Components;
using TheFipster.Munchkin.StashRepository.Decorators;

namespace TheFipster.Munchkin.StashApi
{
    public static class Dependencies
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            addDatastore(services, configuration);
            addMonsterRepository(services);
            addCurseRepository(services);
            addDungeonRepository(services);
        }

        private static void addDatastore(IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton<IContext>((_) =>
                new Context(configuration.GetConnectionString("StashContext")));

        private static void addMonsterRepository(IServiceCollection services)
        {
            services.AddTransient<IRead<Monster>, LiteReader<Monster>>();
            services.AddDecoration<IRead<Monster>, ReadCache<Monster>>();
            services.AddDecoration<IRead<Monster>, ReadLogger<Monster>>();

            services.AddTransient<IWrite<Monster>, LiteWriter<Monster>>();
        }

        private static void addCurseRepository(IServiceCollection services)
        {
            services.AddTransient<IRead<Curse>, LiteReader<Curse>>();
            services.AddDecoration<IRead<Curse>, ReadLogger<Curse>>();

            services.AddTransient<IWrite<Curse>, LiteWriter<Curse>>();
        }

        private static void addDungeonRepository(IServiceCollection services)
        {
            services.AddTransient<IRead<Dungeon>, LiteReader<Dungeon>>();
            services.AddDecoration<IRead<Dungeon>, ReadLogger<Dungeon>>();

            services.AddTransient<IWrite<Dungeon>, LiteWriter<Dungeon>>();
        }
    }
}
