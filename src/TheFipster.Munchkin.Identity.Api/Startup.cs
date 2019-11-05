using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheFipster.Munchkin.AppInsights.Core;
using TheFipster.Munchkin.Identity.Api.Config;
using TheFipster.Munchkin.Identity.Api.Data;
using TheFipster.Munchkin.Identity.Api.Models;

namespace TheFipster.Munchkin.Identity.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private string[] corsOrigins => Clients
            .Get(Configuration)
            .Where(x => x.AllowedCorsOrigins != null)
            .SelectMany(x => x.AllowedCorsOrigins)
            .ToArray();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMunchkinInsights();
            addIdentityStorage(services);
            services.AddMvc(options => options.EnableEndpointRouting = false);
            addCorsPolicy(services);
            var identityServerBuilder = addIdentityServer(services);
            addAuthenticationProviders(services);
            addSigningCredentials(identityServerBuilder);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseCors(ConfigKeys.CorsOriginPolicyName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }

        private void addSigningCredentials(IIdentityServerBuilder identityServerBuilder)
        {
            if (Environment.IsDevelopment())
                identityServerBuilder.AddDeveloperSigningCredential();
            else
                throw new Exception("need to configure key material");
        }

        private void addAuthenticationProviders(IServiceCollection services) => services
            .AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = Configuration[ConfigKeys.LoginProvider.Google.ClientId];
                options.ClientSecret = Configuration[ConfigKeys.LoginProvider.Google.ClientSecret];
            })
            .AddFacebook(options =>
            {
                options.ClientId = Configuration[ConfigKeys.LoginProvider.Facebook.ClientId];
                options.ClientSecret = Configuration[ConfigKeys.LoginProvider.Facebook.ClientSecret];
            })
            .AddMicrosoftAccount(options =>
            {
                options.ClientId = Configuration[ConfigKeys.LoginProvider.Microsoft.ClientId];
                options.ClientSecret = Configuration[ConfigKeys.LoginProvider.Microsoft.ClientSecret];
            })
            .AddTwitter(options =>
            {
                options.ConsumerKey = Configuration[ConfigKeys.LoginProvider.Twitter.ConsumerKey];
                options.ConsumerSecret = Configuration[ConfigKeys.LoginProvider.Twitter.ConsumerSecret];
            });

        private IIdentityServerBuilder addIdentityServer(IServiceCollection services) => services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(Resources.Get())
            .AddInMemoryApiResources(Apis.Get())
            .AddInMemoryClients(Clients.Get(Configuration))
            .AddAspNetIdentity<ApplicationUser>();

        private void addCorsPolicy(IServiceCollection services) => services
            .AddCors(options => options
                .AddPolicy(ConfigKeys.CorsOriginPolicyName, policy => policy
                    .WithOrigins(corsOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));

        private void addIdentityStorage(IServiceCollection services) => services
            .AddDbContext<ApplicationDbContext>(options => options
                .UseSqlite(Configuration
                    .GetConnectionString(ConfigKeys.IdentityContextConfigName)))
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}