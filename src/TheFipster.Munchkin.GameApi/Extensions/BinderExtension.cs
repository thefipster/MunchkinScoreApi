using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.GameApi.Binders;
using TheFipster.Munchkin.GameEvents;

namespace TheFipster.Munchkin.GameApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class BinderExtension
    {
        public static IServiceCollection AddMvcWithCustomBinders(this IServiceCollection services)
        {
            services
                .AddMvc(addGameMessageBinder())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        private static Action<MvcOptions> addGameMessageBinder() => 
            options => options.ModelBinderProviders
                .Insert(0, new GameMessageModelBinderProvider(new Inventory()));
    }
}
