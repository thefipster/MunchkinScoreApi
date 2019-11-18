using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Gaming.Api.Binders;
using TheFipster.Munchkin.Gaming.Events;

namespace TheFipster.Munchkin.Gaming.Api.Extensions
{
    public static class GameMessageBinder
    {
        public static void AddGameMessageModelBinder(this IServiceCollection services) => services
           .AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new GameMessageModelBinderProvider(new Inventory()));
            });
    }
}
