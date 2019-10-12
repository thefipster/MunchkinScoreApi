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
using TheFipster.Munchkin.IdentityApi.Config;
using TheFipster.Munchkin.IdentityApi.Data;
using TheFipster.Munchkin.IdentityApi.Models;

namespace TheFipster.Munchkin.IdentityApi
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

        public void ConfigureServices(IServiceCollection services)
        {
            addIdentityStorage(services);
            addMvc(services);
            addCorsPolicy(services);
            var identityServerBuilder = addIdentityServer(services);
            addAuthenticationProviders(services);
            addSigningCredentials(identityServerBuilder);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseCors("default");

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

        private void addAuthenticationProviders(IServiceCollection services)
        {
            services
                .AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["ExternalProviders:Google:ClientId"];
                    options.ClientSecret = Configuration["ExternalProviders:Google:ClientSecret"];
                })
                .AddFacebook(options =>
                {
                    options.ClientId = Configuration["ExternalProviders:Facebook:ClientId"];
                    options.ClientSecret = Configuration["ExternalProviders:Facebook:ClientSecret"];
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = Configuration["ExternalProviders:Microsoft:ClientId"];
                    options.ClientSecret = Configuration["ExternalProviders:Microsoft:ClientSecret"];
                })
                .AddTwitter(options =>
                {
                    options.ConsumerKey = Configuration["ExternalProviders:Twitter:ConsumerKey"];
                    options.ConsumerSecret = Configuration["ExternalProviders:Twitter:ConsumerSecret"];
                });
        }

        private static IIdentityServerBuilder addIdentityServer(IServiceCollection services)
        {
            return services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(Resources.Get())
                .AddInMemoryApiResources(Apis.Get())
                .AddInMemoryClients(Clients.Get())
                .AddAspNetIdentity<ApplicationUser>();
        }

        private static void addCorsPolicy(IServiceCollection services)
        {
            var corsOrigins = Clients
                .Get()
                .Where(x => x.AllowedCorsOrigins != null)
                .SelectMany(x => x.AllowedCorsOrigins)
                .ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(corsOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod().AllowCredentials();
                });
            });
        }

        private static void addMvc(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        private void addIdentityStorage(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}