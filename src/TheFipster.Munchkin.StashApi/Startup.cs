using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.StashDatabase;
using TheFipster.Munchkin.StashDomain;
using TheFipster.Munchkin.StashRepository.Abstractions;
using TheFipster.Munchkin.StashRepository.Components;

namespace StashApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IContext>((_) => new Context(Configuration.GetConnectionString("StashContext")));
            services.AddTransient<IRead<Monster>, LiteReader<Monster>>();
            services.AddTransient<IRead<Curse>, LiteReader<Curse>>();
            services.AddTransient<IRead<Dungeon>, LiteReader<Dungeon>>();
            services.AddTransient<ISave<Monster>, LiteWriter<Monster>>();
            services.AddTransient<ISave<Curse>, LiteWriter<Curse>>();
            services.AddTransient<ISave<Dungeon>, LiteWriter<Dungeon>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
