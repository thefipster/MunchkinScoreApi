using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Api.Common;

namespace TheFipster.Munchkin.Statistics.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddJwtAuth(Configuration);
            services.AddCorsPolicy(Configuration);
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
