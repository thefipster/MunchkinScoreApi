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
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.ModelBinderProviders.Insert(0, new GameMessageModelBinderProvider(new Inventory()));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }
    }
}
