using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace TheFipster.Munchkin.Api.Common
{
    public static class DeveloperExtensions
    {
        public static void UseDeveloperSettingsBasedOnEnvironment(
            this IApplicationBuilder app, 
            IHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
        }
    }
}
    