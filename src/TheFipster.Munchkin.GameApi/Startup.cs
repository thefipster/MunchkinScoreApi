using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.GameApi.Binders;
using TheFipster.Munchkin.GameApi.Extensions;
using TheFipster.Munchkin.GameApi.Middlewares;
using TheFipster.Munchkin.GameEvents;
using TheFipster.Munchkin.GameStorage;

namespace TheFipster.Munchkin.GameApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsPolicy(Configuration);
            services.AddOAuth();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.ModelBinderProviders.Insert(0, new GameMessageModelBinderProvider(new Inventory()));
            });
            services.AddDependecies();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostEnvironment env, 
            ICardStore cardStore, 
            IMonsterStore monsterStore)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCorsPolicy();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            env.SynchronizeSeedData(Configuration, cardStore, monsterStore);
        }
    }
}
