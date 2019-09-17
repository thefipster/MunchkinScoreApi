using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Api.Binders;

namespace TheFipster.Munchkin.Api.Extensions
{
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
                .Insert(0, new GameMessageModelBinderProvider());
    }
}
