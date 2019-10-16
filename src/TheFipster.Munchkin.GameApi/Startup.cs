using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
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
            services
                .AddAuthorization()
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.ModelBinderProviders.Insert(0, new GameMessageModelBinderProvider(new Inventory()));
                });


            services
                .AddControllers(options =>
                {
                    options.ModelBinderProviders.Insert(0, new GameMessageModelBinderProvider(new Inventory()));
                });

            services
                .AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = true;
                    options.Audience = "game-api";
                });

            services
                .AddCorsPolicy(Configuration)
                .AddDependecies();
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

            app
                .UseHttpsRedirection()
                .UseCorsPolicy()
                .UseMiddleware<ExceptionMiddleware>()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
