using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TheFipster.Munchkin.Api.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddOAuth(this IServiceCollection services)
        {
            addAuthStore(services);
            addIdentity(services);

            var authBuilder = addAuthentication(services);
            addGoogleProvider(authBuilder);

            return services;
        }

        private static void addGoogleProvider(AuthenticationBuilder authBuilder)
        {
            authBuilder.AddGoogle("Google", options =>
            {
                options.CallbackPath = new PathString("/google-callback");
                options.ClientId = "PUT_YOUR_CLIENT_ID_HERE";
                options.ClientSecret = "PUT_YOUR_CLIENT_SECRET_HERE";
                options.Events = new OAuthEvents
                {
                    OnRemoteFailure = (RemoteFailureContext context) =>
                    {
                        context.Response.Redirect("/home/denied");
                        context.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static AuthenticationBuilder addAuthentication(IServiceCollection services) => services.AddAuthentication(options =>
        {
            options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
        });

        private static void addIdentity(IServiceCollection services)
        {
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void addAuthStore(IServiceCollection services)
        { 
            services.AddDbContext<IdentityDbContext>(
                options => options
                    .UseSqlite(
                        "Data Source=users.sqlite",
                        sqliteOptions => sqliteOptions
                            .MigrationsAssembly("TheFipster.Munchkin.Api")));
        }
    }
}
    