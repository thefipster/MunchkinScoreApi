using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameEngine;
using TheFipster.Munchkin.LiteStorage;
using TheFipster.Munchkin.Persistance;

namespace TheFipster.Munchkin.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IRepository<Game>>(new Repository<Game>());
            services.AddSingleton<IActionFactory>(new PrimitiveActionFactory());
            services.AddTransient<IGameStore, GameStore>();
            services.AddTransient<IQuest, Quest>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
