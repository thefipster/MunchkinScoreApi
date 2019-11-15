using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.Api.Common;

namespace TheFipster.Munchkin.Identity.Api
{
    using TheFipster.Munchkin.Identity.Api.Extensions;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsights();
            services.AddIdentityContext(Configuration);
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddCors();
            services.AddIdentityServer();
            services.AddExternalProviders(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCorsPolicy();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
            app.UseDeveloperSettingsBasedOnEnvironment(env);
        }
    }
}