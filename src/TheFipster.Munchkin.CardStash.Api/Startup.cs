using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Api.Common;
using TheFipster.Munchkin.CardStash.Api.Extensions;

namespace TheFipster.Munchkin.CardStash.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddJwtAuth(Configuration);
            services.AddCorsPolicy(Configuration);
            services.AddDependencies(Configuration);
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorsPolicy();
            app.UseProxy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseControllers();
            app.UseDeveloperSettingsBasedOnEnvironment(env);
        }
    }
}
