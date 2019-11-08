using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using TheFipster.Munchkin.Gaming.Api.Binders;
using TheFipster.Munchkin.Gaming.Api.Extensions;
using TheFipster.Munchkin.Gaming.Api.Middlewares;
using TheFipster.Munchkin.Gaming.Events;

namespace TheFipster.Munchkin.Gaming.Api
{
    using Microsoft.AspNetCore.HttpOverrides;

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
            IHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseCorsPolicy();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
